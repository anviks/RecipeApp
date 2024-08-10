<script setup lang="ts">
import FormInput from '@/components/FormInput.vue';
import type { PropType } from 'vue';
import type { IngredientType } from '@/types';

const ingredientType = defineModel({
    type: Object as PropType<IngredientType>,
    required: true
});
</script>

<template>
    <FormInput id="Name" label="Name" v-model="ingredientType!.name" />
    <FormInput id="Description" label="Description" v-model="ingredientType!.description" type="textarea" />
</template>

<style scoped></style>