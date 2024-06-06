<script setup lang="ts">
import type { Sample } from '@/types';
import type { PropType } from 'vue';

defineProps({
    sample: {
        type: Object as PropType<Sample>,
        required: true
    }
});
</script>

<template>
    <dl class="row">
        <dt class="col-sm-2">
            Field
        </dt>
        <dd class="col-sm-10">
            {{ sample.field }}
        </dd>
    </dl>
</template>

<style scoped></style>