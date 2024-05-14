<script setup lang="ts">
import { ref } from 'vue';
import type { IngredientType } from '@/types';
import { useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import FormInput from '@/components/FormInput.vue';
import useServices from '@/helpers/useServices';

const { ingredientTypesService } = useServices();

const ingredientType = ref<IngredientType>({ name: '', description: '' });
const errors = ref<string[]>([]);

const router = useRouter();

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

    <h4>Ingredient type</h4>
    <hr>
    <div class="row">
        <div class="col-md-4">
            <form method="post">
                <FormInput id="Name" label="Name" v-model="ingredientType.name"/>
                <FormInput id="Description" label="Description" v-model="ingredientType.description"/>
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