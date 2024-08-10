<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { Category, Optional } from '@/types';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import CreateEdit from '@/views/categories/partials/CreateEdit.vue';

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

const submitEdit = async () => {
    await handleApiResult<Category>({
        result: categoriesService.update(id, category.value!),
        dataRef: category,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Categories',
        successRedirect: 'Categories'
    });
};
</script>

<template>
    <h1>Edit</h1>

    <h4>Category</h4>
    <hr>
    <div class="row">
        <ConditionalContent :errors="errors" :expected-content="category">
            <div class="col-md-4">
                <form method="post">
                    <CreateEdit v-model="category!" />
                    <div class="form-group">
                        <button @click.prevent="submitEdit" type="submit" class="btn btn-primary">Save</button>
                    </div>
                </form>
            </div>
        </ConditionalContent>
    </div>

    <div>
        <RouterLink :to="{name: 'Categories'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>