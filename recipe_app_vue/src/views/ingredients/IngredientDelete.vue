<script setup lang="ts">

import type { Ingredient, ResultObject } from '@/types';
import { onMounted, ref } from 'vue';
import IngredientsService from '@/services/ingredientsService';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();
const ingredient = ref<ResultObject<Ingredient>>({});
const deleteIngredient = async () => {
    await IngredientsService.delete(ingredient.value.data?.id!);
    await router.push({ name: 'Ingredients' });
};

onMounted(async () => {
    ingredient.value = await IngredientsService.findById(route.params.id.toString());
});
</script>

<template>
    <h1>Delete</h1>

    <div v-if="ingredient.data">
        <h3>Are you sure you want to delete this?</h3>
        <div>
            <h4>Ingredient</h4>
            <hr>

            <dl class="row">
                <dt class="col-sm-2">
                    Name
                </dt>
                <dd class="col-sm-10">
                    {{ ingredient.data.name }}
                </dd>
            </dl>

            <form method="post">
                <input @click.prevent="deleteIngredient" type="submit" value="Delete" class="btn btn-danger"> |
                <RouterLink :to="{name: 'Ingredients'}">Back to List</RouterLink>
            </form>
        </div>
    </div>
    <span v-else>Loading...</span>
</template>

<style scoped></style>