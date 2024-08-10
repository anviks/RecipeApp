<script setup lang="ts">
import type { IngredientType, Optional } from '@/types';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';

const { ingredientTypesService } = useServices();

const ingredientType = ref<Optional<IngredientType>>(null);
const errors = ref<string[]>([]);

const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();

onMounted(async () => {
    await handleApiResult<IngredientType>({
        result: ingredientTypesService.findById(id),
        dataRef: ingredientType,
        errorsRef: errors,
        router,
        fallbackRedirect: 'IngredientTypes'
    });
});
</script>

<template>
    <h1>Details</h1>
    <div>
        <h4>Ingredient type</h4>
        <hr>
        <ConditionalContent :errors="errors" :expected-content="ingredientType">
            <Details :ingredient-type="ingredientType" />
        </ConditionalContent>
    </div>
    <div>
        <RouterLink :to="{name: 'IngredientTypeEdit', params: {id}}">Edit</RouterLink>
        |
        <RouterLink :to="{name: 'IngredientTypes'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>