<script setup lang="ts">
import type { IngredientType, Optional } from '@/types';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';

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

const deleteIngredientType = async () => {
    await handleApiResult<IngredientType>({
        result: ingredientTypesService.delete(ingredientType.value!.id!),
        errorsRef: errors,
        router,
        fallbackRedirect: 'IngredientTypes',
        successRedirect: 'IngredientTypes'
    });
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