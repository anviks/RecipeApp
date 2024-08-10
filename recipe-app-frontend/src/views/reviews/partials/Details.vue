<script setup lang="ts">
import type { Review } from '@/types';
import type { PropType } from 'vue';

defineProps({
    review: {
        type: Object as PropType<Review>,
        required: true
    }
});
</script>

<template>
    <dl class="row">
        <dt class="col-sm-2">
            User
        </dt>
        <dd class="col-sm-10">
            {{ review.user?.username }}
        </dd>
        <dt class="col-sm-2">
            Recipe
        </dt>
        <dd class="col-sm-10">
            {{ review.recipe?.title }}
        </dd>
        <dt class="col-sm-2">
            Rating
        </dt>
        <dd class="col-sm-10">
            <template v-for="i in Math.floor(review.rating / 2)" :key="i">
                <i class="rating__icon rating__icon--star fa fa-star"></i>
            </template>
            <i v-if="review.rating % 2" class="rating__icon rating__icon--star fa fa-star-half"></i>
        </dd>
        <dt class="col-sm-2">
            Comment
        </dt>
        <dd class="col-sm-10">
            {{ review.comment }}
        </dd>
        <dt class="col-sm-2">
            Created at
        </dt>
        <dd class="col-sm-10">
            {{ review.createdAt }}
        </dd>
    </dl>
</template>

<style scoped></style>