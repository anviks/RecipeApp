<script setup lang="ts">
import { inject, onMounted, ref } from 'vue';
import UnitsService from '@/services/unitsService';
import type { Unit } from '@/types';
import type IngredientTypesService from '@/services/ingredientTypesService';

const unitsService = inject('unitsService') as UnitsService;
const ingredientTypesService = inject('ingredientTypesService') as IngredientTypesService;
const units = ref<Unit[]>([]);

onMounted(async () => {
    const result = await unitsService.findAll();
    for (const unit of result.data!) {
        unit.ingredientType = (await ingredientTypesService.findById(unit.ingredientTypeId)).data!;
    }
    units.value = result.data!;
});
</script>

<template>
    <h1>Units</h1>
    <p>
        <RouterLink :to="{name: 'UnitCreate'}">Create New</RouterLink>
    </p>
    <table v-if="units" class="table">
        <thead>
        <tr>
            <th>
                Name
            </th>
            <th>
                Abbreviation
            </th>
            <th>
                Unit multiplier
            </th>
            <th>
                Type
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="unit in units" :key="unit.id">
            <td>
                {{ unit.name }}
            </td>
            <td>
                {{ unit.abbreviation }}
            </td>
            <td>
                {{ unit.unitMultiplier }}
            </td>
            <td>
                {{ unit.ingredientType?.name }}
            </td>
            <td>
                <RouterLink :to="{name: 'UnitEdit', params: {id: unit.id}}">Edit</RouterLink>
                |
                <RouterLink :to="{name: 'UnitDetails', params: {id: unit.id}}">Details</RouterLink>
                |
                <RouterLink :to="{name: 'UnitDelete', params: {id: unit.id}}">Delete</RouterLink>
            </td>
        </tr>
        </tbody>
    </table>
    <span v-else>Loading...</span>
</template>

<style scoped></style>