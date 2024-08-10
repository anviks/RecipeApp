<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { Category } from '@/types';
import useServices from '@/helpers/useServices';

const { categoriesService } = useServices();
const categories = ref<Category[]>([]);

onMounted(async () => {
    const result = await categoriesService.findAll();
    categories.value = result.data!;
});
</script>

<template>
    <h1>Categories</h1>
    <p>
        <RouterLink :to="{name: 'CategoryCreate'}">Create New</RouterLink>
    </p>
    <table v-if="categories.length" class="table">
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
        <tr v-for="category in categories" :key="category.id">
            <td>
                {{ category.name }}
            </td>
            <td>
                {{ category.description }}
            </td>
            <td>
                <RouterLink :to="{name: 'CategoryEdit', params: {id: category.id}}">Edit</RouterLink>
                |
                <RouterLink :to="{name: 'CategoryDetails', params: {id: category.id}}">Details</RouterLink>
                |
                <RouterLink :to="{name: 'CategoryDelete', params: {id: category.id}}">Delete</RouterLink>
            </td>
        </tr>
        </tbody>
    </table>
    <span v-else>Loading...</span>
</template>

<style scoped></style>