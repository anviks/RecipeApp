<script setup lang="ts">
import IngredientTypesService from '@/services/ingredientTypesService';
import type { IngredientType, ResultObject } from '@/types';
import { onMounted, ref } from 'vue';

let ingredientTypes = ref<ResultObject<IngredientType[]>>({});
onMounted(async () => {
    ingredientTypes.value = await IngredientTypesService.findAll();
});
</script>

<template>
    <h1>Ingredient types</h1>
    <p>
        <RouterLink :to="{name: 'IngredientTypeCreate'}">Create New</RouterLink>
    </p>
    <table v-if="ingredientTypes.data" class="table">
        <thead>
        <tr>
            <th>
                Name
            </th>
            <th>
                Description
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="element in ingredientTypes.data" :key="element.id">
            <td>
                {{ element.name }}
            </td>
            <td>
                {{ element.description }}
            </td>
            <td>
                <RouterLink :to="{name: 'IngredientTypeEdit', params: {id: element.id}}">Edit</RouterLink> |
                <RouterLink :to="{name: 'IngredientTypeDetails', params: {id: element.id}}">Details</RouterLink> |
                <RouterLink :to="{name: 'IngredientTypeDelete', params: {id: element.id}}">Delete</RouterLink>
            </td>
        </tr>
        </tbody>
    </table>
    <span v-else>Loading...</span>
</template>

<style scoped></style>