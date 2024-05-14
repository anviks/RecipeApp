<script setup lang="ts">
import { inject, onMounted, ref } from 'vue';
import type { Ingredient, IngredientType } from '@/types';
import IngredientsService from '@/services/ingredientsService';
import { useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import type IngredientTypesService from '@/services/ingredientTypesService';
import type IngredientTypeAssociationsService from '@/services/ingredientTypeAssociationsService';

const ingredientsService = inject('ingredientsService') as IngredientsService;
const ingredientTypesService = inject('ingredientTypesService') as IngredientTypesService;
const ingredientTypeAssociationsService = inject('ingredientTypeAssociationsService') as IngredientTypeAssociationsService;
const router = useRouter();
const ingredient = ref<Ingredient>({ name: '', ingredientTypeAssociations: [] });
const ingredientTypes = ref<IngredientType[]>([]);
const errors = ref<string[]>([]);

onMounted(async () => {
    const types = await ingredientTypesService.findAll();
    ingredientTypes.value = types.data!;
});

const submitCreate = async () => {
    const associations = ingredient.value.ingredientTypeAssociations!;

    await handleApiResult({
        result: ingredientsService.create(ingredient.value),
        dataRef: ingredient,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Ingredients'
    });

    for (const association of associations) {
        association.ingredientId = ingredient.value.id!;
        await handleApiResult({
            result: ingredientTypeAssociationsService.create(association),
            errorsRef: errors,
            router,
            fallbackRedirect: 'Ingredients'
        });
    }

    if (errors.value.length === 0) {
        await router.push({ name: 'Ingredients' });
    }
};

const addType = () => {
    ingredient.value.ingredientTypeAssociations!.push({ ingredientTypeId: '', ingredientId: '' });
};

const removeType = () => {
    const length = ingredient.value.ingredientTypeAssociations!.length;
    ingredient.value.ingredientTypeAssociations!.splice(length - 1, 1);
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
                    <input class="form-control valid" type="text" v-model="ingredient.name" />
                    <span class="text-danger field-validation-valid"></span>
                </div>
                <div v-for="(association, index) in ingredient.ingredientTypeAssociations" :key="index"
                     class="form-group">
                    <label class="control-label" :for="'IngredientType' + index">Type</label>
                    <select class="form-control" :id="'IngredientType' + index"
                            v-model="ingredient.ingredientTypeAssociations![index].ingredientTypeId">
                        <option v-for="type in ingredientTypes" :key="type.id" :value="type.id">{{ type.name }}</option>
                    </select>
                </div>
                <div class="form-group">
                    <button @click="addType" type="button" class="btn btn-primary">Add Type</button>
                    <button @click="removeType" type="button" class="btn btn-danger"
                            :disabled="ingredient.ingredientTypeAssociations?.length === 0">Remove Type
                    </button>
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