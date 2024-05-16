<script setup lang="ts">
import type { Optional, Recipe } from '@/types';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import Details from '@/views/recipes/partials/Details.vue';

const { categoriesService, ingredientsService, recipesService, unitsService } = useServices();

const recipe = ref<Optional<Recipe>>(null);
const errors = ref<string[]>([]);

const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();

onMounted(async () => {
    await handleApiResult<Recipe>({
        result: recipesService.findById(id),
        dataRef: recipe,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Recipes'
    });

    for (const recipeIngredient of recipe.value!.recipeIngredients!) {
        recipeIngredient.unit = (await unitsService.findById(recipeIngredient.unitId)).data!;
    }

    for (const recipeIngredient of recipe.value!.recipeIngredients!) {
        recipeIngredient.ingredient = (await ingredientsService.findById(recipeIngredient.ingredientId)).data!;
    }

    for (const recipeCategory of recipe.value!.recipeCategories!) {
        recipeCategory.category = (await categoriesService.findById(recipeCategory.categoryId)).data!;
    }
});

const deleteRecipe = async () => {
    await handleApiResult<Recipe>({
        result: recipesService.delete(recipe.value!.id!),
        errorsRef: errors,
        router,
        fallbackRedirect: 'Recipes',
        successRedirect: 'Recipes'
    });
};
</script>

<template>
    <h1>Delete</h1>

    <ConditionalContent :errors="errors" :expected-content="recipe">
        <div>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Recipe</h4>
                <hr>
                <Details :recipe="recipe!" />
                <form method="post">
                    <input @click.prevent="deleteRecipe" type="submit" value="Delete" class="btn btn-danger"> |
                    <RouterLink :to="{name: 'Recipes'}">Back to List</RouterLink>
                </form>
            </div>
        </div>
    </ConditionalContent>
</template>

<style scoped></style>