<script setup lang="ts">
import type { Optional, Sample } from '@/types';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import Details from '@/views/samples/partials/Details.vue';

const { samplesService } = useServices();

const sample = ref<Optional<Sample>>(null);
const errors = ref<string[]>([]);

const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();

onMounted(async () => {
    await handleApiResult<Sample>({
        result: samplesService.findById(id),
        dataRef: sample,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Samples'
    });
});
</script>

<template>
    <h1>Details</h1>
    <div>
        <h4>Sample</h4>
        <hr>
        <ConditionalContent :errors="errors" :expected-content="sample">
            <Details :sample="sample!" />
        </ConditionalContent>
    </div>
    <div>
        <RouterLink :to="{name: 'SampleEdit', params: {id}}">Edit</RouterLink>
        |
        <RouterLink :to="{name: 'Samples'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>