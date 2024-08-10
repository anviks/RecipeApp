<script setup lang="ts">
import type { IngredientType } from '@/types';
import type { PropType } from 'vue';

defineProps({
    ingredientType: {
        type: Object as PropType<IngredientType>,
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
            {{ ingredientType.name }}
        </dd>
        <dt class="col-sm-2">
            Description
        </dt>
        <dd class="col-sm-10">
            {{ ingredientType.description }}
        </dd>
    </dl>
</template>

<style scoped></style>