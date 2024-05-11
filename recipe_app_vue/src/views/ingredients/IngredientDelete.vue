<script setup lang="ts">
import type { Ingredient, Optional } from '@/types';
import { inject, onMounted, ref } from 'vue';
import IngredientsService from '@/services/ingredientsService';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';

const ingredientsService = inject('ingredientsService') as IngredientsService;
const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();
const ingredient = ref<Optional<Ingredient>>(null);
const errors = ref<string[]>([]);

onMounted(async () => {
    await handleApiResult<Ingredient>(
        ingredientsService.findById(id),
        ingredient,
        errors,
        router,
        'Ingredients'
    );
});

const deleteIngredient = async () => {
    await handleApiResult<Ingredient>(
        ingredientsService.delete(ingredient.value!.id!),
        ingredient,
        errors,
        router,
        'Ingredients',
        'Ingredients'
    );
};
</script>

<template>
    <h1>Delete</h1>

    <ConditionalContent :errors="errors" :expected-content="ingredient">
        <h3>Are you sure you want to delete this?</h3>
        <div>
            <h4>Ingredient</h4>
            <hr>

            <dl class="row">
                <dt class="col-sm-2">
                    Name
                </dt>
                <dd class="col-sm-10">
                    {{ ingredient!.name }}
                </dd>
            </dl>

            <form method="post">
                <button @click.prevent="deleteIngredient" type="submit" class="btn btn-danger">Delete</button> |
                <RouterLink :to="{name: 'Ingredients'}">Back to List</RouterLink>
            </form>
        </div>
    </ConditionalContent>
</template>

<style scoped></style>