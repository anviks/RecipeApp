<script setup lang="ts">
import type { Recipe } from '@/types';
import type { PropType } from 'vue';
import { backendDomain } from '@/config';

const props = defineProps({
    recipe: {
        type: Object as PropType<Recipe>,
        required: true
    }
});

function getDietaryRestrictions(): string {
    let labels = [];
    if (props.recipe.isVegetarian) {
        labels.push('Vegetarian');
    }
    if (props.recipe.isVegan) {
        labels.push('Vegan');
    }
    if (props.recipe.isGlutenFree) {
        labels.push('Gluten free');
    }
    return labels.join('<br/>') || 'None';
}
</script>

<template>
    <dl class="row">
        <dt class="col-sm-2">
            Image
        </dt>
        <dd class="col-sm-10">
            <img :src="recipe.imageFileUrl.replace('~', backendDomain)" alt="Recipe image" style="width: 60px; height: 60px;">
        </dd>
        <dt class="col-sm-2">
            Title
        </dt>
        <dd class="col-sm-10">
            {{ recipe.title }}
        </dd>
        <dt class="col-sm-2">
            Description
        </dt>
        <dd class="col-sm-10">
            {{ recipe.description }}
        </dd>
        <dt class="col-sm-2">
            Preparation time
        </dt>
        <dd class="col-sm-10">
            {{ recipe.preparationTime }}
        </dd>
        <dt class="col-sm-2">
            Cooking time
        </dt>
        <dd class="col-sm-10">
            {{ recipe.cookingTime }}
        </dd>
        <dt class="col-sm-2">
            Servings
        </dt>
        <dd class="col-sm-10">
            {{ recipe.servings }}
        </dd>
        <dt class="col-sm-2">
            Dietary restrictions
        </dt>
        <dd class="col-sm-10" v-html="getDietaryRestrictions()"/>
        <dt class="col-sm-2">
            Created at
        </dt>
        <dd class="col-sm-10">
            {{ recipe.createdAt }}
        </dd>
        <dt class="col-sm-2">
            Created by
        </dt>
        <dd class="col-sm-10">
            {{ recipe.authorUser.username }}
        </dd>
        <dt class="col-sm-2">
            Updated at
        </dt>
        <dd class="col-sm-10">
            {{ recipe.updatedAt }}
        </dd>
        <dt class="col-sm-2">
            Updated by
        </dt>
        <dd class="col-sm-10">
            {{ recipe.updatingUser?.username }}
        </dd>
        <dt class="col-sm-2">
            Ingredients
        </dt>
        <dd class="col-sm-10">
            <ul>
                <li v-for="ingredient in recipe.recipeIngredients" :key="ingredient.id">
                    {{ ingredient.quantity }} {{ ingredient.unit?.abbreviation ?? ingredient.customUnit }} of {{ ingredient.ingredient?.name }}
                </li>
            </ul>
        </dd>
        <dt class="col-sm-2">
            Steps
        </dt>
        <dd class="col-sm-10">
            <ol>
                <li v-for="step in recipe.instructions" :key="step">
                    {{ step }}
                </li>
            </ol>
        </dd>
    </dl>
</template>

<style scoped></style>