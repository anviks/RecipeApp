<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { Sample, Optional } from '@/types';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import CreateEdit from '@/views/samples/partials/CreateEdit.vue';

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

const submitEdit = async () => {
    await handleApiResult<Sample>({
        result: samplesService.update(id, sample.value!),
        dataRef: sample,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Samples',
        successRedirect: 'Samples'
    });
};
</script>

<template>
    <h1>Edit</h1>

    <h4>Sample</h4>
    <hr>
    <div class="row">
        <ConditionalContent :errors="errors" :expected-content="sample">
            <div class="col-md-4">
                <form method="post">
                    <CreateEdit v-model="sample!" />
                    <div class="form-group">
                        <button @click.prevent="submitEdit" type="submit" class="btn btn-primary">Save</button>
                    </div>
                </form>
            </div>
        </ConditionalContent>
    </div>

    <div>
        <RouterLink :to="{name: 'Samples'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>