<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { Recipe } from '@/types';
import useServices from '@/helpers/useServices';
import { backendDomain } from '@/config';

const { recipesService } = useServices();
const recipes = ref<Recipe[]>([]);
const isLoading = ref(true);

onMounted(async () => {
    const result = await recipesService.findAll();
    recipes.value = result.data!;
    isLoading.value = false;
});

function getDietaryLabels(recipe: Recipe): string {
    let labels = [];
    if (recipe.isVegetarian) {
        labels.push('V');
    }
    if (recipe.isVegan) {
        labels.push('VG');
    }
    if (recipe.isGlutenFree) {
        labels.push('G');
    }
    return labels.join('/');
}
</script>

<template>
    <h1>Recipes</h1>
    <p>
        <RouterLink :to="{name: 'RecipeCreate'}">Create New</RouterLink>
    </p>
    <table v-if="!isLoading" class="table">
        <thead>
        <tr>
            <th>
                Image
            </th>
            <th>
                Title
            </th>
            <th>
                Description
            </th>
            <th>
                Preparation time
            </th>
            <th>
                Cooking time
            </th>
            <th>
                Servings
            </th>
            <th>
                Dietary restrictions
            </th>
            <th>
                Created at
            </th>
            <th>
                Created by
            </th>
            <th>
                Updated at
            </th>
            <th>
                Updated by
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="recipe in recipes" :key="recipe.id">
            <td>
                <img :src="recipe.imageFileUrl.replace('~', backendDomain)" alt="Recipe image" style="width: 60px; height: 60px;">
            </td>
            <td>
                {{ recipe.title }}
            </td>
            <td>
                {{ recipe.description }}
            </td>
            <td>
                {{ recipe.preparationTime }}
            </td>
            <td>
                {{ recipe.cookingTime }}
            </td>
            <td>
                {{ recipe.servings }}
            </td>
            <td>
                {{ getDietaryLabels(recipe) }}
            </td>
            <td>
                {{ recipe.createdAt }}
            </td>
            <td>
                {{ recipe.authorUser.username }}
            </td>
            <td>
                {{ recipe.updatedAt }}
            </td>
            <td>
                {{ recipe.updatingUser?.username }}
            </td>
            <td>
                <RouterLink :to="{name: 'RecipeEdit', params: {id: recipe.id}}">Edit</RouterLink>
                |
                <RouterLink :to="{name: 'RecipeDetails', params: {id: recipe.id}}">Details</RouterLink>
                |
                <RouterLink :to="{name: 'RecipeDelete', params: {id: recipe.id}}">Delete</RouterLink>
            </td>
        </tr>
        </tbody>
    </table>
    <span v-else>Loading...</span>
</template>

<style scoped></style>