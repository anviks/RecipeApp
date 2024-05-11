<script setup lang="ts">
import type { Ingredient, Optional, ResultObject } from '@/types';
import { inject, onMounted, ref } from 'vue';
import IngredientsService from '@/services/ingredientsService';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';

const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();
const ingredientsService = inject('ingredientsService') as IngredientsService;
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
        </ConditionalContent>
    </div>
    <div>
        <RouterLink :to="{name: 'IngredientEdit', params: {id}}">Edit</RouterLink>
        |
        <RouterLink :to="{name: 'Ingredients'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>