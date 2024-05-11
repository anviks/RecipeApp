<script setup lang="ts">
import type { IngredientType, Optional } from '@/types';
import { inject, onMounted, ref } from 'vue';
import IngredientTypesService from '@/services/ingredientTypesService';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';

const ingredientTypesService = inject('ingredientTypesService') as IngredientTypesService;
const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();
const ingredientType = ref<Optional<IngredientType>>(null);
const errors = ref<string[]>([]);

onMounted(async () => {
    await handleApiResult<IngredientType>(
        ingredientTypesService.findById(id),
        ingredientType,
        errors,
        router,
        'IngredientTypes'
    );
});

const deleteIngredientType = async () => {
    await handleApiResult<IngredientType>(
        ingredientTypesService.delete(ingredientType.value!.id!),
        ingredientType,
        errors,
        router,
        'IngredientTypes',
        'IngredientTypes'
    );
};
</script>

<template>
    <h1>Delete</h1>

    <ConditionalContent :errors="errors" :expected-content="ingredientType">
        <div>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Ingredient type</h4>
                <hr>

                <dl class="row">
                    <dt class="col-sm-2">
                        Name
                    </dt>
                    <dd class="col-sm-10">
                        {{ ingredientType!.name }}
                    </dd>
                    <dt class="col-sm-2">
                        Description
                    </dt>
                    <dd class="col-sm-10">
                        {{ ingredientType!.description }}
                    </dd>
                </dl>

                <form method="post">
                    <input @click.prevent="deleteIngredientType" type="submit" value="Delete" class="btn btn-danger"> |
                    <RouterLink :to="{name: 'IngredientTypes'}">Back to List</RouterLink>
                </form>
            </div>
        </div>
    </ConditionalContent>
</template>

<style scoped></style>