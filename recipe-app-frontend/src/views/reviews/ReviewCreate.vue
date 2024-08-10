<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { Recipe, Review } from '@/types';
import { useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import CreateEdit from '@/views/reviews/partials/CreateEdit.vue';

const { reviewsService, recipesService } = useServices();

const review = ref<Review>({
    comment: '',
    rating: 0,
    recipeId: '',
});
const recipes = ref<Recipe[]>([]);
const errors = ref<string[]>([]);

const router = useRouter();

onMounted(async () => {
    const types = await recipesService.findAll();
    recipes.value = types.data!;
});

const submitCreate = async () => {
    errors.value = [];
    await handleApiResult<Review>({
        result: reviewsService.create(review.value!),
        dataRef: review,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Reviews',
        successRedirect: 'Reviews'
    });
};
</script>

<template>
    <h1>Create</h1>

    <h4>Review</h4>
    <hr>
    <div class="row">
        <ConditionalContent :errors="errors" :expected-content="review">
            <div class="col-md-4">
                <form method="post">
                    <CreateEdit :recipes="recipes" v-model="review" />
                    <div class="form-group">
                        <button @click.prevent="submitCreate" type="submit" class="btn btn-primary">Create</button>
                    </div>
                </form>
            </div>
        </ConditionalContent>
    </div>

    <div>
        <RouterLink :to="{name: 'Reviews'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>