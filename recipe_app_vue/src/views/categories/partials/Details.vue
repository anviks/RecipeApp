<script setup lang="ts">
import type { Category } from '@/types';
import type { PropType } from 'vue';

defineProps({
    category: {
        type: Object as PropType<Category>,
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
            {{ category.name }}
        </dd>
        <dt class="col-sm-2">
            Description
        </dt>
        <dd class="col-sm-10">
            {{ category.description }}
        </dd>
    </dl>
</template>

<style scoped></style>