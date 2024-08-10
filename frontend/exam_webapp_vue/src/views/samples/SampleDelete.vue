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

const deleteSample = async () => {
    await handleApiResult<Sample>({
        result: samplesService.delete(sample.value!.id!),
        errorsRef: errors,
        router,
        fallbackRedirect: 'Samples',
        successRedirect: 'Samples'
    });
};
</script>

<template>
    <h1>Delete</h1>

    <ConditionalContent :errors="errors" :expected-content="sample">
        <div>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Sample</h4>
                <hr>
                <Details :sample="sample!" />
                <form method="post">
                    <input @click.prevent="deleteSample" type="submit" value="Delete" class="btn btn-danger"> |
                    <RouterLink :to="{name: 'Samples'}">Back to List</RouterLink>
                </form>
            </div>
        </div>
    </ConditionalContent>
</template>

<style scoped></style>