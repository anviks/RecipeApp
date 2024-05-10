<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { IngredientType, ResultObject } from '@/types';
import { useRoute, useRouter } from 'vue-router';
import IngredientTypesService from '@/services/ingredientTypesService';

let ingredientType = ref<ResultObject<IngredientType>>({});
const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();

onMounted(async () => {
    ingredientType.value = await IngredientTypesService.findById(id);
});

const submitEdit = async () => {
    await IngredientTypesService.update(id, ingredientType.value.data!);
    await router.push({ name: 'IngredientTypes' });
};
</script>

<template>
    <h1>Edit</h1>

    <h4>IngredientType</h4>
    <hr>
    <div class="row">
        <div class="col-md-4">
            <form v-if="ingredientType.data" method="post">
                <div class="form-group">
                    <label class="control-label" for="Name">Name</label>
                    <input class="form-control" type="text" id="Name" v-model="ingredientType.data.name">
                    <span class="text-danger field-validation-valid"></span>
                </div>
                <div class="form-group">
                    <label class="control-label" for="Description">Description</label>
                    <input class="form-control" type="text" id="Description" v-model="ingredientType.data.description">
                    <span class="text-danger field-validation-valid"></span>
                </div>
                <div class="form-group">
                    <button @click.prevent="submitEdit" type="submit" class="btn btn-primary">Save</button>
                </div>
            </form>
            <span v-else>Loading...</span>
        </div>
    </div>

    <div>
        <RouterLink :to="{name: 'IngredientTypes'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>