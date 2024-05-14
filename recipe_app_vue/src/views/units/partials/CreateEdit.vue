<script setup lang="ts">
import FormInput from '@/components/FormInput.vue';
import type { PropType } from 'vue';
import type { IngredientType, Unit } from '@/types';

defineProps({
    ingredientTypes: {
        type: Array as PropType<IngredientType[]>,
        required: true
    }
});

const unit = defineModel({
    type: Object as PropType<Unit>,
    required: true
});
</script>

<template>
    <FormInput id="Name" label="Name" v-model="unit.name" />
    <FormInput id="Abbreviation" label="Abbreviation" v-model="unit.abbreviation" />
    <FormInput id="UnitMultiplier" label="Unit multiplier" v-model="unit.unitMultiplier" />
    <div class="form-group">
        <label class="control-label" for="Ingredient type">Ingredient type</label>
        <select class="form-control" id="Ingredient type" v-model="unit.ingredientTypeId">
            <option v-for="type in ingredientTypes" :key="type.id" :value="type.id">{{ type.name }}
            </option>
        </select>
        <span class="text-danger field-validation-valid"></span>
    </div>
</template>

<style scoped></style>