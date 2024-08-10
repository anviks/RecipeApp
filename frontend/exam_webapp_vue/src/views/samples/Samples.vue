<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { Sample } from '@/types';
import useServices from '@/helpers/useServices';

const { samplesService } = useServices();
const samples = ref<Sample[]>([]);

onMounted(async () => {
    const result = await samplesService.findAll();
    samples.value = result.data!;
});
</script>

<template>
    <h1>Samples</h1>
    <p>
        <RouterLink :to="{name: 'SampleCreate'}">Create New</RouterLink>
    </p>
    <table v-if="samples.length" class="table">
        <thead>
        <tr>
            <th>
                Field
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="sample in samples" :key="sample.id">
            <td>
                {{ sample.field }}
            </td>
            <td>
                <RouterLink :to="{name: 'SampleEdit', params: {id: sample.id}}">Edit</RouterLink>
                |
                <RouterLink :to="{name: 'SampleDetails', params: {id: sample.id}}">Details</RouterLink>
                |
                <RouterLink :to="{name: 'SampleDelete', params: {id: sample.id}}">Delete</RouterLink>
            </td>
        </tr>
        </tbody>
    </table>
    <span v-else>Loading...</span>
</template>

<style scoped></style>