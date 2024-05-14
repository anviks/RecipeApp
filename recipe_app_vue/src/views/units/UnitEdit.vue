<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { IngredientType, Optional, Unit } from '@/types';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import CreateEdit from '@/views/units/partials/CreateEdit.vue';

const { unitsService, ingredientTypesService } = useServices();

const unit = ref<Optional<Unit>>(null);
const ingredientTypes = ref<IngredientType[]>([]);
const errors = ref<string[]>([]);

const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();

onMounted(async () => {
    await handleApiResult<Unit>({
        result: unitsService.findById(id),
        dataRef: unit,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Units'
    });

    const types = await ingredientTypesService.findAll();
    ingredientTypes.value = types.data!;
});

const submitEdit = async () => {
    await handleApiResult<Unit>({
        result: unitsService.update(id, unit.value!),
        dataRef: unit,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Units',
        successRedirect: 'Units'
    });
};
</script>

<template>
    <h1>Edit</h1>

    <h4>Unit</h4>
    <hr>
    <div class="row">
        <ConditionalContent :errors="errors" :expected-content="unit">
            <div class="col-md-4">
                <form method="post">
                    <CreateEdit :ingredient-types="ingredientTypes" v-model="unit!" />
                    <div class="form-group">
                        <button @click.prevent="submitEdit" type="submit" class="btn btn-primary">Save</button>
                    </div>
                </form>
            </div>
        </ConditionalContent>
    </div>

    <div>
        <RouterLink :to="{name: 'Units'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>