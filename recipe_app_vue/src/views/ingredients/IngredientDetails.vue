<script setup lang="ts">
import type { Ingredient, Optional, ResultObject } from '@/types';
import { onMounted, ref } from 'vue';
import IngredientsService from '@/services/ingredientsService';
import { useRoute } from 'vue-router';

let ingredient = ref<ResultObject<Ingredient>>({});
const route = useRoute();
const id = route.params.id.toString();

onMounted(async () => {
    ingredient.value = await IngredientsService.findById(id);
});
</script>

<template>
    <h1>Details</h1>
    <div>
        <h4>Ingredient</h4>
        <hr>

        <dl class="row">
            <dt class="col-sm-2">
                Name
            </dt>
            <dd class="col-sm-10">
                {{ ingredient.data?.name }}
            </dd>
        </dl>
    </div>
    <div>
        <RouterLink :to="{name: 'IngredientEdit', params: {id}}">Edit</RouterLink>
        |
        <RouterLink :to="{name: 'Ingredients'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>