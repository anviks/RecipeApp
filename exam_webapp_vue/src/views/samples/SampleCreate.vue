<script setup lang="ts">
import { ref } from 'vue';
import type { Sample } from '@/types';
import { useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import CreateEdit from '@/views/samples/partials/CreateEdit.vue';

const { samplesService } = useServices();

const sample = ref<Sample>({ name: '', description: '' });
const errors = ref<string[]>([]);

const router = useRouter();

const submitCreate = async () => {
    errors.value = [];
    await handleApiResult<Sample>({
        result: samplesService.create(sample.value!),
        dataRef: sample,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Samples',
        successRedirect: 'Samples'
    });
};
</script>

<template>
    <h1>Create</h1>

    <h4>Sample</h4>
    <hr />
    <div class="row">
        <ConditionalContent :errors="errors" :expected-content="sample">
            <div class="col-md-4">
                <form method="post">
                    <CreateEdit v-model="sample" />
                    <div class="form-group">
                        <button @click.prevent="submitCreate" type="submit" class="btn btn-primary">Create</button>
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