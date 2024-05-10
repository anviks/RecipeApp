<script setup lang="ts">
import IngredientsService from '@/services/ingredientsService';
import type { Ingredient, ResultObject } from '@/types';
import { onMounted, ref } from 'vue';

let ingredients = ref<ResultObject<Ingredient[]>>({});
onMounted(async () => {
    ingredients.value = await IngredientsService.findAll();
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
            <th></th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="element in ingredients.data" :key="element.id">
            <td>
                {{ element.name }}
            </td>
            <td>
                <RouterLink :to="{name: 'IngredientEdit', params: {id: element.id}}">Edit</RouterLink> |
                <RouterLink :to="{name: 'IngredientDetails', params: {id: element.id}}">Details</RouterLink> |
                <RouterLink :to="{name: 'IngredientDelete', params: {id: element.id}}">Delete</RouterLink>
            </td>
        </tr>
        </tbody>
    </table>
    <span v-else>Loading...</span>
</template>

<style scoped></style>