<script setup lang="ts">
defineProps({
    id: String,
    label: String,
    type: {
        type: String,
        default: 'text',
    },
    error: {
        type: String,
        default: '',
    },
});

const model = defineModel();
</script>

<template>
    <div class="form-group">
        <label :for="id" class="control-label">{{ label }}</label>
        <input :id="id" v-model="model" class="form-control" :type="type">
        <span class="text-danger">{{ error }}</span>
    </div>
</template>

<style scoped></style>