<script setup lang="ts">
import FormInput from '@/components/FormInput.vue';
import type { PropType } from 'vue';
import type { Category } from '@/types';

const category = defineModel({
    type: Object as PropType<Category>,
    required: true
});
</script>

<template>
    <FormInput id="Name" label="Name" v-model="category.name" />
    <FormInput id="Description" label="Description" v-model="category.description" type="textarea" />
</template>

<style scoped></style>