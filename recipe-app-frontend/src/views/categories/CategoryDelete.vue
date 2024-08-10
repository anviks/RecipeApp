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

const deleteCategory = async () => {
    await handleApiResult<Category>({
        result: categoriesService.delete(category.value!.id!),
        errorsRef: errors,
        router,
        fallbackRedirect: 'Categories',
        successRedirect: 'Categories'
    });
};
</script>

<template>
    <h1>Delete</h1>

    <ConditionalContent :errors="errors" :expected-content="category">
        <div>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Category</h4>
                <hr>
                <Details :category="category!" />
                <form method="post">
                    <input @click.prevent="deleteCategory" type="submit" value="Delete" class="btn btn-danger"> |
                    <RouterLink :to="{name: 'Categories'}">Back to List</RouterLink>
                </form>
            </div>
        </div>
    </ConditionalContent>
</template>

<style scoped></style>