<script setup lang="ts">
import { ref } from 'vue';
import type { Optional, Recipe, RecipeRequest } from '@/types';
import { useRouter } from 'vue-router';
import { handleApiResult, objectToFormData } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import CreateEdit from '@/views/recipes/partials/CreateEdit.vue';

const {
    recipesService,
    recipeCategoriesService,
    recipeIngredientsService,
} = useServices();

const recipeRequest = ref<RecipeRequest>({
    cookingTime: 0,
    description: '',
    instructions: [''],
    isGlutenFree: false,
    isVegan: false,
    isVegetarian: false,
    preparationTime: 0,
    servings: 0,
    title: '',
    recipeIngredients: [],
    categoryIds: []
});
const savedRecipe = ref<Optional<Recipe>>(null);
const errors = ref<string[]>([]);

const router = useRouter();

const submitCreate = async () => {
    errors.value = [];

    const categoryIds = recipeRequest.value.categoryIds;
    const recipeIngredients = recipeRequest.value.recipeIngredients;
    
    recipeRequest.value.categoryIds = undefined;
    recipeRequest.value.recipeIngredients = undefined;
    
    await handleApiResult<RecipeRequest>({
        result: recipesService.create(objectToFormData(recipeRequest.value)),
        dataRef: savedRecipe,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Recipes'
    });
    
    for (const categoryId of categoryIds ?? []) {
        await handleApiResult<void>({
            result: recipeCategoriesService.create({ recipeId: savedRecipe.value!.id!, categoryId: categoryId }),
            errorsRef: errors,
            router,
            fallbackRedirect: 'Recipes'
        });
    }
    
    for (const recipeIngredient of recipeIngredients ?? []) {
        recipeIngredient.recipeId = savedRecipe.value!.id!;
        await handleApiResult<void>({
            result: recipeIngredientsService.create(recipeIngredient),
            errorsRef: errors,
            router,
            fallbackRedirect: 'Recipes'
        });
    }
    
    if (errors.value.length === 0) {
        await router.push({ name: 'Recipes' });
    }
};
</script>

<template>
    <h1>Create</h1>

    <h4>Recipe</h4>
    <hr />
    <div class="row">
        <ConditionalContent :errors="errors" :expected-content="recipeRequest">
            <div class="col-md-6">
                <form method="post" enctype="multipart/form-data">
                    <CreateEdit v-model="recipeRequest" />
                    <div class="form-group">
                        <button @click.prevent="submitCreate" type="submit" class="btn btn-primary">Create</button>
                    </div>
                </form>
            </div>
            <div class="col-md-4">
                <pre>{{ JSON.stringify(recipeRequest, null, 2) }}</pre>
            </div>
        </ConditionalContent>
    </div>
    <div>
        <RouterLink :to="{name: 'Recipes'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>