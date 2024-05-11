<script setup lang="ts">
import { inject, ref } from 'vue';
import type { Ingredient } from '@/types';
import IngredientsService from '@/services/ingredientsService';
import { useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';

const ingredientsService = inject('ingredientsService') as IngredientsService;
const router = useRouter();
const ingredient = ref<Ingredient>({ name: '' });
const errors = ref<string[]>([]);

const submitCreate = async () => {
    await handleApiResult<Ingredient>(
        ingredientsService.create(ingredient.value!),
        ingredient,
        errors,
        router,
        'Ingredients',
        'Ingredients'
    );
};
</script>

<template>
    <h1>Create</h1>

    <h4>Ingredient</h4>
    <hr>
    <div class="row">
        <div class="col-md-4">
            <form method="post">
                <div class="form-group">
                    <label class="control-label" for="Name">Name</label>
                    <input class="form-control valid" type="text" v-model="ingredient.name">
                    <span class="text-danger field-validation-valid"></span>
                </div>
                <div class="form-group">
                    <button @click.prevent="submitCreate" type="submit" class="btn btn-primary">Create</button>
                </div>
            </form>
        </div>
    </div>

    <div>
        <RouterLink :to="{name: 'Ingredients'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>