<script setup lang="ts">
import { inject, ref } from 'vue';
import type { IngredientType } from '@/types';
import IngredientTypesService from '@/services/ingredientTypesService';
import { useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';

const ingredientTypesService = inject('ingredientTypesService') as IngredientTypesService;
const router = useRouter();
const ingredientType = ref<IngredientType>({ name: '', description: '' });
const errors = ref<string[]>([]);

const submitCreate = async () => {
    await handleApiResult<IngredientType>({
        result: ingredientTypesService.create(ingredientType.value!),
        dataRef: ingredientType,
        errorsRef: errors,
        router,
        fallbackRedirect: 'IngredientTypes',
        successRedirect: 'IngredientTypes'
    });
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
                    <input class="form-control valid" type="text" v-model="ingredientType.name">
                    <span class="text-danger field-validation-valid"></span>
                </div>
                <div class="form-group">
                    <label class="control-label" for="Description">Description</label>
                    <input class="form-control" type="text" id="Description" v-model="ingredientType.description">
                    <span class="text-danger field-validation-valid"></span>
                </div>
                <div class="form-group">
                    <button @click.prevent="submitCreate" type="submit" class="btn btn-primary">Create</button>
                </div>
            </form>
        </div>
    </div>

    <div>
        <RouterLink :to="{name: 'IngredientTypes'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>