<script setup lang="ts">
import IngredientsService from '@/services/ingredientsService';
import type { Ingredient, ResultObject } from '@/types';
import { inject, onMounted, ref } from 'vue';

const ingredientsService = inject('ingredientsService') as IngredientsService;
const ingredients = ref<ResultObject<Ingredient[]>>({});

onMounted(async () => {
    ingredients.value = await ingredientsService.findAll();
});
</script>

<template>
    <h1>Ingredients</h1>
    <p>
        <RouterLink :to="{name: 'IngredientCreate'}">Create New</RouterLink>
    </p>
    <table v-if="ingredients.data" class="table">
        <thead>
        <tr>
            <th>
                Name
            </th>
            <th>
                Types
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="ingredient in ingredients.data" :key="ingredient.id">
            <td>
                {{ ingredient.name }}
            </td>
            <td>
                {{ ingredient.ingredientTypeAssociations?.length }}
            </td>
            <td>
                <RouterLink :to="{name: 'IngredientEdit', params: {id: ingredient.id}}">Edit</RouterLink> |
                <RouterLink :to="{name: 'IngredientDetails', params: {id: ingredient.id}}">Details</RouterLink> |
                <RouterLink :to="{name: 'IngredientDelete', params: {id: ingredient.id}}">Delete</RouterLink>
            </td>
        </tr>
        </tbody>
    </table>
    <span v-else>Loading...</span>
</template>

<style scoped></style>