<script setup lang="ts">
import FormInput from '@/components/FormInput.vue';
import { type PropType } from 'vue';
import type { Recipe, Review } from '@/types';

defineProps({
    recipes: {
        type: Array as PropType<Recipe[]>,
        required: true
    }
});

const review = defineModel({
    type: Object as PropType<Review>,
    required: true
});

const numberOfStars = 5;

const label = (value: number) => `${value} star${Math.abs(value - 1) < 0.001 ? '' : 's'}`;
const inputId = (i: number) => `rating2-${i * 5}`;
const isHalfStar = (i: number) => i % 2 !== 0;
</script>

<template>
    <div class="form-group">
        <label class="control-label" for="Recipe">Recipe</label>
        <select class="form-control" id="Recipe" v-model="review.recipeId">
            <option v-for="recipe in recipes" :key="recipe.id" :value="recipe.id">{{ recipe.title }}
            </option>
        </select>
        <span class="text-danger field-validation-valid"></span>
    </div>
    <div class="form-group">
        <label class="control-label" for="Rating">Rating</label>
        <br/>
        <div class="rating-group">
            <input class="rating__input rating__input--none" v-model="review.rating" title="hidden-value"
                   id="rating2-0" value="0" type="radio" :checked="review.rating === 0"/>
            <template v-for="i in numberOfStars * 2" :key="i">
                <label :aria-label="label(i / 2)" class="rating__label"
                       :class="{ 'rating__label--half': isHalfStar(i) }" :for="inputId(i)">
                    <i class="rating__icon rating__icon--star fa"
                       :class="isHalfStar(i) ? 'fa-star-half' : 'fa-star'"></i>
                </label>
                <input class="rating__input" :id="inputId(i)" :value="i" type="radio" :checked="review.rating === i" v-model="review.rating">
            </template>
        </div>
        <br/>
        <span class="text-danger"></span>
    </div>
    <FormInput id="Comment" label="Comment" v-model="review.comment" />
</template>

<style scoped></style>