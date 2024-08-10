<script setup lang="ts">
import type { Ingredient, IngredientType } from '@/types';
import type { PropType } from 'vue';

defineProps({
    ingredient: {
        type: Object as PropType<Ingredient>,
        required: true
    },
    ingredientTypes: {
        type: Array as PropType<IngredientType[]>,
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
            {{ ingredient!.name }}
        </dd>
    </dl>
    <dl v-for="type in ingredientTypes" :key="type.id" class="row">
        <dt class="col-sm-2">
            Type
        </dt>
        <dd class="col-sm-10">
            {{ type.name }}
        </dd>
    </dl>
</template>

<style scoped></style>