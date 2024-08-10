<script setup lang="ts">
import type { Optional, Category } from '@/types';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import Details from '@/views/categories/partials/Details.vue';

const { categoriesService } = useServices();

const category = ref<Optional<Category>>(null);
const errors = ref<string[]>([]);

const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();

onMounted(async () => {
    await handleApiResult<Category>({
        result: categoriesService.findById(id),
        dataRef: category,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Categories'
    });
});
</script>

<template>
    <h1>Details</h1>
    <div>
        <h4>Category</h4>
        <hr>
        <ConditionalContent :errors="errors" :expected-content="category">
            <Details :category="category!" />
        </ConditionalContent>
    </div>
    <div>
        <RouterLink :to="{name: 'CategoryEdit', params: {id}}">Edit</RouterLink>
        |
        <RouterLink :to="{name: 'Categories'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>