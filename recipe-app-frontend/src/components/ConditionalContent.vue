<script setup lang="ts">
defineProps<{
    expectedContent: any;
    errors: string[];
}>();
</script>

<template>
    <slot v-if="expectedContent"/>
    <span v-else-if="errors.length === 0">Loading...</span>
    <div v-if="errors.length > 0">
        <div v-for="error in errors" :key="error" class="text-danger">{{ error }}</div>
    </div>
</template>

<style scoped></style>