<script setup lang="ts">
import FormInput from '@/components/FormInput.vue';
import type { PropType } from 'vue';
import type { Ingredient, IngredientType } from '@/types';

defineProps({
    ingredientTypes: {
        type: Array as PropType<IngredientType[]>,
        required: true
    }
});

const ingredient = defineModel({
    type: Object as PropType<Ingredient>,
    required: true
});

const addType = () => {
    ingredient.value.ingredientTypeAssociations!.push({ ingredientTypeId: '', ingredientId: '' });
};

const removeType = () => {
    const length = ingredient.value.ingredientTypeAssociations!.length;
    ingredient.value.ingredientTypeAssociations!.splice(length - 1, 1);
};
</script>

<template>
    <FormInput id="Name" label="Name" v-model="ingredient.name"/>
    <div v-for="(association, index) in ingredient.ingredientTypeAssociations" :key="index"
         class="form-group">
        <label class="control-label" :for="'IngredientType' + index">Type</label>
        <select class="form-control" :id="'IngredientType' + index"
                v-model="ingredient.ingredientTypeAssociations![index].ingredientTypeId">
            <option v-for="type in ingredientTypes" :key="type.id" :value="type.id">{{ type.name }}
            </option>
        </select>
    </div>
    <div class="form-group">
        <button @click="addType" type="button" class="btn btn-primary">Add type</button>
        <button @click="removeType" type="button" class="btn btn-danger"
                :disabled="ingredient.ingredientTypeAssociations?.length === 0">Remove type
        </button>
    </div>
</template>

<style scoped></style>