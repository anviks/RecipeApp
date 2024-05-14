<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { IngredientType, Optional } from '@/types';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import FormInput from '@/components/FormInput.vue';
import useServices from '@/helpers/useServices';
import CreateEdit from '@/views/ingredient-types/partials/CreateEdit.vue';

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

const submitEdit = async () => {
    await handleApiResult<IngredientType>({
        result: ingredientTypesService.update(id, ingredientType.value!),
        dataRef: ingredientType,
        errorsRef: errors,
        router,
        fallbackRedirect: 'IngredientTypes',
        successRedirect: 'IngredientTypes'
    });
};
</script>

<template>
    <h1>Edit</h1>

    <h4>Ingredient type</h4>
    <hr>
    <div class="row">
        <ConditionalContent :errors="errors" :expected-content="ingredientType">
            <div class="col-md-4">
                <form method="post">
                    <CreateEdit v-model="ingredientType" />
                    <div class="form-group">
                        <button @click.prevent="submitEdit" type="submit" class="btn btn-primary">Save</button>
                    </div>
                </form>
            </div>
        </ConditionalContent>
    </div>

    <div>
        <RouterLink :to="{name: 'IngredientTypes'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>