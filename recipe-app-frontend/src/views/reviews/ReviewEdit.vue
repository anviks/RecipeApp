<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { Recipe, Optional, Review } from '@/types';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import CreateEdit from '@/views/reviews/partials/CreateEdit.vue';

const { reviewsService, recipesService } = useServices();

const review = ref<Optional<Review>>(null);
const recipes = ref<Recipe[]>([]);
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

    const types = await recipesService.findAll();
    recipes.value = types.data!;
});

const submitEdit = async () => {
    await handleApiResult<Review>({
        result: reviewsService.update(id, review.value!),
        dataRef: review,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Reviews',
        successRedirect: 'Reviews'
    });
};
</script>

<template>
    <h1>Edit</h1>

    <h4>Review</h4>
    <hr>
    <div class="row">
        <ConditionalContent :errors="errors" :expected-content="review">
            <div class="col-md-4">
                <form method="post">
                    <CreateEdit :recipes="recipes" v-model="review!" />
                    <div class="form-group">
                        <button @click.prevent="submitEdit" type="submit" class="btn btn-primary">Save</button>
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