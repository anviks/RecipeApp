<script setup lang="ts">
import type { Optional, Review } from '@/types';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import Details from '@/views/reviews/partials/Details.vue';

const { reviewsService, recipesService } = useServices();

const review = ref<Optional<Review>>(null);
const errors = ref<string[]>([]);

const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();

onMounted(async () => {
    await handleApiResult<Review>({
        result: reviewsService.findById(id),
        dataRef: review,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Reviews'
    });

    if (review.value?.recipeId) {
        review.value.recipe = (await recipesService.findById(review.value.recipeId)).data!;
    }
});

const deleteReview = async () => {
    await handleApiResult<Review>({
        result: reviewsService.delete(review.value!.id!),
        errorsRef: errors,
        router,
        fallbackRedirect: 'Reviews',
        successRedirect: 'Reviews'
    });
};
</script>

<template>
    <h1>Delete</h1>

    <ConditionalContent :errors="errors" :expected-content="review">
        <div>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Review</h4>
                <hr>
                <Details :review="review!" />
                <form method="post">
                    <input @click.prevent="deleteReview" type="submit" value="Delete" class="btn btn-danger"> |
                    <RouterLink :to="{name: 'Reviews'}">Back to List</RouterLink>
                </form>
            </div>
        </div>
    </ConditionalContent>
</template>

<style scoped></style>