<script setup lang="ts">
import { inject, onMounted, ref } from 'vue';
import type { Unit, Optional, IngredientType } from '@/types';
import { useRoute, useRouter } from 'vue-router';
import UnitsService from '@/services/unitsService';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import type IngredientTypesService from '@/services/ingredientTypesService';

const unitsService = inject('unitsService') as UnitsService;
const ingredientTypesService = inject('ingredientTypesService') as IngredientTypesService;

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
                            <option v-for="type in ingredientTypes" :key="type.id" :value="type.id">{{ type.name }}</option>
                        </select>
                        <span class="text-danger field-validation-valid"></span>
                    </div>
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