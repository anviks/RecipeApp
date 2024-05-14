<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { Ingredient, IngredientType } from '@/types';
import { useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import useServices from '@/helpers/useServices';
import CreateEdit from '@/views/ingredients/partials/CreateEdit.vue';

const { ingredientsService, ingredientTypesService, ingredientTypeAssociationsService } = useServices();

const ingredient = ref<Ingredient>({ name: '', ingredientTypeAssociations: [] });
const ingredientTypes = ref<IngredientType[]>([]);
const errors = ref<string[]>([]);

const router = useRouter();

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
</script>

<template>
    <h1>Create</h1>

    <h4>Ingredient</h4>
    <hr>
    <div class="row">
        <div class="col-md-4">
            <form method="post">
                <CreateEdit :ingredient-types="ingredientTypes" v-model="ingredient" />
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