<script setup lang="ts">
import type { Unit } from '@/types';
import type { PropType } from 'vue';

defineProps({
    unit: {
        type: Object as PropType<Unit>,
        required: true
    }
});
</script>

<template>
    <dl class="row">
        <dt class="col-sm-2">
            Name
        </dt>
        <dd class="col-sm-10">
            {{ unit.name }}
        </dd>
        <dt class="col-sm-2">
            Abbreviation
        </dt>
        <dd class="col-sm-10">
            {{ unit.abbreviation }}
        </dd>
        <dt class="col-sm-2">
            Unit multiplier
        </dt>
        <dd class="col-sm-10">
            {{ unit.unitMultiplier }}
        </dd>
        <dt class="col-sm-2">
            Ingredient type
        </dt>
        <dd class="col-sm-10">
            {{ unit.ingredientType?.name }}
        </dd>
    </dl>
</template>

<style scoped></style>