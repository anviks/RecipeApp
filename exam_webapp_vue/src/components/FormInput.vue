<script setup lang="ts">
const props = defineProps({
    id: {
        type: String,
        required: true
    },
    label: {
        type: String,
        default: ''
    },
    type: {
        type: String,
        default: 'text'
    },
    error: {
        type: String,
        default: ''
    }
});

const model = defineModel<any>();
</script>

<template>
    <div v-if="type === 'checkbox'" class="form-group form-check">
        <label class="form-check-label">
            <input :id="id" v-model="model" class="form-check-input" :type="type">
            {{ label }}
        </label>
    </div>
    <div v-else class="form-group">
        <label v-if="label" :for="id" class="control-label">{{ label }}</label>
        
        <textarea v-if="type === 'textarea'" :id="id" v-model="model" class="form-control"></textarea>
        <input v-else :id="id" v-model="model" class="form-control" :type="type">
        
        <span class="text-danger">{{ error }}</span>
    </div>
</template>

<style scoped></style>