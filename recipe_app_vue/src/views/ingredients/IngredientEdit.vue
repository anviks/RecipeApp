<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { Ingredient, IngredientType, Optional } from '@/types';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import CreateEdit from '@/views/ingredients/partials/CreateEdit.vue';

const { ingredientsService, ingredientTypesService, ingredientTypeAssociationsService } = useServices();

const ingredient = ref<Optional<Ingredient>>(null);
const ingredientTypes = ref<IngredientType[]>([]);
const existingAssociationIds = ref<string[]>([]);
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

    existingAssociationIds.value = ingredient.value!.ingredientTypeAssociations!.map(a => a.id!);

    const types = await ingredientTypesService.findAll();
    ingredientTypes.value = types.data!;
});

const submitEdit = async () => {
    const associations = ingredient.value!.ingredientTypeAssociations!;

    await handleApiResult<Ingredient>({
        result: ingredientsService.update(id, ingredient.value!),
        dataRef: ingredient,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Ingredients'
    });

    for (const associationId of existingAssociationIds.value) {
        if (!associations.some(a => a.id === associationId)) {
            await handleApiResult({
                result: ingredientTypeAssociationsService.delete(associationId),
                errorsRef: errors,
                router,
                fallbackRedirect: 'Ingredients'
            });
        }
    }

    for (const association of associations) {
        association.ingredientId = ingredient.value!.id!;
        if (existingAssociationIds.value.includes(association.id!)) {
            await handleApiResult({
                result: ingredientTypeAssociationsService.update(association.id!, association),
                errorsRef: errors,
                router,
                fallbackRedirect: 'Ingredients'
            });
        } else {
            await handleApiResult({
                result: ingredientTypeAssociationsService.create(association),
                errorsRef: errors,
                router,
                fallbackRedirect: 'Ingredients'
            });
        }
    }

    if (errors.value.length === 0) {
        await router.push({ name: 'Ingredients' });
    }
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
                    <CreateEdit :ingredient-types="ingredientTypes" v-model="ingredient!" />
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