<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { Optional, Recipe, RecipeRequest } from '@/types';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult, objectToFormData } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import CreateEdit from '@/views/recipes/partials/CreateEdit.vue';
import { recipeResponseToRequest } from '@/helpers/mappers';

const {
    recipesService,
    recipeCategoriesService,
    recipeIngredientsService
} = useServices();

const recipeRequest = ref<Optional<RecipeRequest>>(null);
const recipeResponse = ref<Optional<Recipe>>(null);
const existingCategoryIds = ref<string[]>([]);
const existingRecipeIngredientIds = ref<string[]>([]);
const errors = ref<string[]>([]);

const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();

onMounted(async () => {
    await handleApiResult<Recipe>({
        result: recipesService.findById(id),
        dataRef: recipeResponse,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Recipes'
    });
    
    existingCategoryIds.value = recipeResponse.value!.recipeCategories!.map(c => c.categoryId!);
    existingRecipeIngredientIds.value = recipeResponse.value!.recipeIngredients!.map(i => i.id!);

    recipeRequest.value = recipeResponseToRequest(recipeResponse.value!);
});

const submitEdit = async () => {
    errors.value = [];

    const categoryIds = recipeRequest.value!.categoryIds;
    const recipeIngredients = recipeRequest.value!.recipeIngredients;

    recipeRequest.value!.categoryIds = undefined;
    recipeRequest.value!.recipeIngredients = undefined;
    
    for (const existingCategoryId of existingCategoryIds.value) {
        if (!categoryIds?.includes(existingCategoryId)) {
            await handleApiResult({
                result: recipeCategoriesService.delete(existingCategoryId),
                errorsRef: errors,
                router,
                fallbackRedirect: 'Recipes'
            });
        }
    }
    
    for (const existingRecipeIngredientId of existingRecipeIngredientIds.value) {
        if (!recipeIngredients?.some(i => i.id === existingRecipeIngredientId)) {
            await handleApiResult({
                result: recipeIngredientsService.delete(existingRecipeIngredientId),
                errorsRef: errors,
                router,
                fallbackRedirect: 'Recipes'
            });
        }
    }
    
    for (const categoryId of categoryIds ?? []) {
        if (!existingCategoryIds.value.includes(categoryId)) {
            await handleApiResult<void>({
                result: recipeCategoriesService.create({ recipeId: id, categoryId: categoryId }),
                errorsRef: errors,
                router,
                fallbackRedirect: 'Recipes'
            });
        }
    }
    
    for (const recipeIngredient of recipeIngredients ?? []) {
        if (recipeIngredient.id) {
            await handleApiResult<void>({
                result: recipeIngredientsService.update(recipeIngredient.id, recipeIngredient),
                errorsRef: errors,
                router,
                fallbackRedirect: 'Recipes'
            });
        } else {
            recipeIngredient.recipeId = id;
            await handleApiResult<void>({
                result: recipeIngredientsService.create(recipeIngredient),
                errorsRef: errors,
                router,
                fallbackRedirect: 'Recipes'
            });
        }
    }

    await handleApiResult<RecipeRequest>({
        result: recipesService.update(id, objectToFormData(recipeRequest.value!)),
        errorsRef: errors,
        router,
        fallbackRedirect: 'Recipes'
    });

    if (errors.value.length === 0) {
        await router.push({ name: 'Recipes' });
    }
};
</script>

<template>
    <h1>Edit</h1>

    <h4>RecipeRequest</h4>
    <hr>
    <div class="row">
        <ConditionalContent :errors="errors" :expected-content="recipeRequest">
            <div class="col-md-4">
                <form method="post" enctype="multipart/form-data">
                    <CreateEdit v-model="recipeRequest" />
                    <div class="form-group">
                        <button @click.prevent="submitEdit" type="submit" class="btn btn-primary">Save</button>
                    </div>
                </form>
            </div>
        </ConditionalContent>
    </div>

    <div>
        <RouterLink :to="{name: 'Recipes'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>