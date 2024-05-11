<script setup lang="ts">
import { inject, onMounted, ref } from 'vue';
import type { Ingredient, Optional } from '@/types';
import { useRoute, useRouter } from 'vue-router';
import IngredientsService from '@/services/ingredientsService';
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

const submitEdit = async () => {
    await handleApiResult<Ingredient>(
        ingredientsService.update(id, ingredient.value!),
        ingredient,
        errors,
        router,
        'Ingredients',
        'Ingredients'
    );
};
</script>

<template>
    <h1>Edit</h1>

    <h4>Ingredient</h4>
    <hr>
    <div class="row">
        <ConditionalContent :errors="errors" :expected-content="ingredient">
            <div class="col-md-4">
                <form method="post">
                    <div class="form-group">
                        <label class="control-label" for="Name">Name</label>
                        <input class="form-control valid" type="text" v-model="ingredient!.name">
                        <span class="text-danger field-validation-valid"></span>
                    </div>
                    <div class="form-group">
                        <button @click.prevent="submitEdit" type="submit" class="btn btn-primary">Save</button>
                    </div>
                </form>
            </div>
        </ConditionalContent>
    </div>

    <div>
        <RouterLink :to="{name: 'Ingredients'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>