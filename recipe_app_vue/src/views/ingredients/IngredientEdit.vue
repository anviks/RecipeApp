<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { Ingredient, ResultObject } from '@/types';
import { useRoute, useRouter } from 'vue-router';
import IngredientsService from '@/services/ingredientsService';

let ingredient = ref<ResultObject<Ingredient>>({});
const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();

onMounted(async () => {
    ingredient.value = await IngredientsService.findById(id);
});

const submitEdit = async () => {
    await IngredientsService.update(id, ingredient.value.data!);
    await router.push({ name: 'Ingredients' });
};
</script>

<template>
    <h1>Edit</h1>

    <h4>Ingredient</h4>
    <hr>
    <div class="row">
        <div class="col-md-4">
            <form v-if="ingredient.data" method="post">
                <div class="form-group">
                    <label class="control-label" for="Name">Name</label>
                    <input class="form-control valid" type="text" v-model="ingredient.data.name">
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
        <RouterLink :to="{name: 'Ingredients'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>