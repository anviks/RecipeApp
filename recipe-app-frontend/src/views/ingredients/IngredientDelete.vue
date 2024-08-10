<script setup lang="ts">
import type { Ingredient, IngredientType, Optional } from '@/types';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import Details from '@/views/ingredients/partials/Details.vue';

const { ingredientsService, ingredientTypesService } = useServices();

const ingredient = ref<Optional<Ingredient>>(null);
const ingredientTypes = ref<IngredientType[]>([]);
const errors = ref<string[]>([]);

const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();

onMounted(async () => {
    await handleApiResult<Ingredient>({
        result: ingredientsService.findById(id),
        dataRef: ingredient,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Ingredients'
    });

    for (const association of ingredient.value!.ingredientTypeAssociations!) {
        const type = await ingredientTypesService.findById(association.ingredientTypeId);
        ingredientTypes.value.push(type.data!);
    }
});

const deleteIngredient = async () => {
    await handleApiResult({
        result: ingredientsService.delete(ingredient.value!.id!),
        dataRef: ingredient,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Ingredients',
        successRedirect: 'Ingredients'
    });
};
</script>

<template>
    <h1>Delete</h1>

    <ConditionalContent :errors="errors" :expected-content="ingredient">
        <h3>Are you sure you want to delete this?</h3>
        <div>
            <h4>Ingredient</h4>
            <hr>
            <Details :ingredient-types="ingredientTypes" :ingredient="ingredient!" />
            <form method="post">
                <button @click.prevent="deleteIngredient" type="submit" class="btn btn-danger">Delete</button>
                |
                <RouterLink :to="{name: 'Ingredients'}">Back to List</RouterLink>
            </form>
        </div>
    </ConditionalContent>
</template>

<style scoped></style>