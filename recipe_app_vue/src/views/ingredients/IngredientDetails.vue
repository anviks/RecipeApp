<script setup lang="ts">
import type { Ingredient, IngredientType, Optional } from '@/types';
import { inject, onMounted, ref } from 'vue';
import IngredientsService from '@/services/ingredientsService';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import type IngredientTypesService from '@/services/ingredientTypesService';

const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();
const ingredientsService = inject('ingredientsService') as IngredientsService;
const ingredientTypesService = inject('ingredientTypesService') as IngredientTypesService;
const ingredient = ref<Optional<Ingredient>>(null);
const ingredientTypes = ref<IngredientType[]>([]);
const errors = ref<string[]>([]);

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
</script>

<template>
    <h1>Details</h1>
    <div>
        <h4>Ingredient</h4>
        <hr>

        <ConditionalContent :errors="errors" :expected-content="ingredient">
            <dl class="row">
                <dt class="col-sm-2">
                    Name
                </dt>
                <dd class="col-sm-10">
                    {{ ingredient!.name }}
                </dd>
            </dl>
            <dl v-for="type in ingredientTypes" :key="type.id" class="row">
                <dt class="col-sm-2">
                    Type
                </dt>
                <dd class="col-sm-10">
                    {{ type.name }}
                </dd>
            </dl>
        </ConditionalContent>
    </div>
    <div>
        <RouterLink :to="{name: 'IngredientEdit', params: {id}}">Edit</RouterLink>
        |
        <RouterLink :to="{name: 'Ingredients'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>