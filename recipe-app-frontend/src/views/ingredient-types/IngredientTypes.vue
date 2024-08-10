<script setup lang="ts">
import type { IngredientType } from '@/types';
import { onMounted, ref } from 'vue';
import useServices from '@/helpers/useServices';

const { ingredientTypesService } = useServices();

const ingredientTypes = ref<IngredientType[]>([]);

onMounted(async () => {
    const result = await ingredientTypesService.findAll();
    ingredientTypes.value = result.data!;
});
</script>

<template>
    <h1>Ingredient types</h1>
    <p>
        <RouterLink :to="{name: 'IngredientTypeCreate'}">Create New</RouterLink>
    </p>
    <table v-if="ingredientTypes.length" class="table">
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
        <tr v-for="type in ingredientTypes" :key="type.id">
            <td>
                {{ type.name }}
            </td>
            <td>
                {{ type.description }}
            </td>
            <td>
                <RouterLink :to="{name: 'IngredientTypeEdit', params: {id: type.id}}">Edit</RouterLink> |
                <RouterLink :to="{name: 'IngredientTypeDetails', params: {id: type.id}}">Details</RouterLink> |
                <RouterLink :to="{name: 'IngredientTypeDelete', params: {id: type.id}}">Delete</RouterLink>
            </td>
        </tr>
        </tbody>
    </table>
    <span v-else>Loading...</span>
</template>

<style scoped></style>