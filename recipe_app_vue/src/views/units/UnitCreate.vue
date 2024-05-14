<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { IngredientType, Unit } from '@/types';
import { useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import CreateEdit from '@/views/units/partials/CreateEdit.vue';

const { unitsService, ingredientTypesService } = useServices();

const unit = ref<Unit>({ name: '', abbreviation: '', ingredientTypeId: '', unitMultiplier: 0 });
const ingredientTypes = ref<IngredientType[]>([]);
const errors = ref<string[]>([]);

const router = useRouter();

onMounted(async () => {
    const types = await ingredientTypesService.findAll();
    ingredientTypes.value = types.data!;
});

const submitCreate = async () => {
    errors.value = [];
    await handleApiResult<Unit>({
        result: unitsService.create(unit.value!),
        dataRef: unit,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Units',
        successRedirect: 'Units'
    });
};
</script>

<template>
    <h1>Create</h1>

    <h4>Unit</h4>
    <hr>
    <div class="row">
        <ConditionalContent :errors="errors" :expected-content="unit">
            <div class="col-md-4">
                <form method="post">
                    <CreateEdit :ingredient-types="ingredientTypes" v-model="unit" />
                    <div class="form-group">
                        <button @click.prevent="submitCreate" type="submit" class="btn btn-primary">Create</button>
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