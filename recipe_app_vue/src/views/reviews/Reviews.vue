<script setup lang="ts">
import type { Review } from '@/types';
import { onMounted, ref } from 'vue';
import useServices from '@/helpers/useServices';

const { recipesService, reviewsService } = useServices();

const reviews = ref<Review[]>([]);
const isLoading = ref(true);

onMounted(async () => {
    const result = await reviewsService.findAll();
    reviews.value = result.data!;
    
    for (const review of reviews.value) {
        review.recipe = (await recipesService.findById(review.recipeId)).data!;
    }
    
    isLoading.value = false;
});
</script>

<template>
    <h1>Reviews</h1>
    <p>
        <RouterLink :to="{name: 'ReviewCreate'}">Create New</RouterLink>
    </p>
    <table v-if="!isLoading" class="table">
        <thead>
        <tr>
            <th>
                User
            </th>
            <th>
                Recipe
            </th>
            <th>
                Rating
            </th>
            <th>
                Comment
            </th>
            <th>
                Created at
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="review in reviews" :key="review.id">
            <td>
                {{ review.user!.username }}
            </td>
            <td>
                {{ review.recipe!.title }}
            </td>
            <td>
                <template v-for="i in Math.floor(review.rating / 2)" :key="i">
                    <i class="rating__icon rating__icon--star fa fa-star"></i>
                </template>
                <i v-if="review.rating % 2" class="rating__icon rating__icon--star fa fa-star-half"></i>
            </td>
            <td>
                {{ review.comment }}
            </td>
            <td>
                {{ review.createdAt }}
            </td>
            <td>
                <RouterLink :to="{name: 'ReviewEdit', params: {id: review.id}}">Edit</RouterLink>
                |
                <RouterLink :to="{name: 'ReviewDetails', params: {id: review.id}}">Details</RouterLink>
                |
                <RouterLink :to="{name: 'ReviewDelete', params: {id: review.id}}">Delete</RouterLink>
            </td>
        </tr>
        </tbody>
    </table>
    <span v-else>Loading...</span>
</template>

<style scoped></style>