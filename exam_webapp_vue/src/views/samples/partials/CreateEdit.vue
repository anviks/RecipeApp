<script setup lang="ts">
import FormInput from '@/components/FormInput.vue';
import type { PropType } from 'vue';
import type { Sample } from '@/types';

const sample = defineModel({
    type: Object as PropType<Sample>,
    required: true
});
</script>

<template>
    <FormInput id="field" label="Field" v-model="sample.field" />
</template>

<style scoped></style>