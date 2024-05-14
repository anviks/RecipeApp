<script setup lang="ts">
import { inject, onMounted, ref } from 'vue';
import type { IngredientType, Unit } from '@/types';
import UnitsService from '@/services/unitsService';
import { useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import type IngredientTypesService from '@/services/ingredientTypesService';
import ConditionalContent from '@/components/ConditionalContent.vue';

const unitsService = inject('unitsService') as UnitsService;
const ingredientTypesService = inject('ingredientTypesService') as IngredientTypesService;

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
                    <div class="form-group">
                        <label class="control-label" for="Name">Name</label>
                        <input class="form-control" type="text" id="Name" v-model="unit!.name">
                        <span class="text-danger field-validation-valid"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Abbreviation">Abbreviation</label>
                        <input class="form-control" type="text" id="Abbreviation" v-model="unit!.abbreviation">
                        <span class="text-danger field-validation-valid"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Unit multiplier">Unit multiplier</label>
                        <input class="form-control" type="text" id="Unit multiplier" v-model="unit!.unitMultiplier">
                        <span class="text-danger field-validation-valid"></span>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Ingredient type">Ingredient type</label>
                        <select class="form-control" id="Ingredient type" v-model="unit!.ingredientTypeId">
                            <option v-for="type in ingredientTypes" :key="type.id" :value="type.id">{{ type.name }}
                            </option>
                        </select>
                        <span class="text-danger field-validation-valid"></span>
                    </div>
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