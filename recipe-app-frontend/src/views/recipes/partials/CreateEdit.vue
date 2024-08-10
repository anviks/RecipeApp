<script setup lang="ts">
import FormInput from '@/components/FormInput.vue';
import { onMounted, type PropType, ref } from 'vue';
import type { Category, Ingredient, RecipeRequest, Unit } from '@/types';
import useServices from '@/helpers/useServices';

const {
    categoriesService,
    ingredientsService,
    unitsService
} = useServices();

const categories = ref<Category[]>([]);
const ingredients = ref<Ingredient[]>([]);
const units = ref<Unit[]>([]);

onMounted(async () => {
    const categoriesResult = await categoriesService.findAll();
    const ingredientsResult = await ingredientsService.findAll();
    const unitsResult = await unitsService.findAll();

    categories.value = categoriesResult.data!;
    ingredients.value = ingredientsResult.data!;
    units.value = unitsResult.data!;
});

const recipeRequest = defineModel({
    type: Object as PropType<RecipeRequest>,
    required: true
});

const imageUrl = ref<string | null>(null);

const handleImageUpload = (event: Event) => {
    const target = event.target as HTMLInputElement;
    const file = target.files?.[0];
    if (file) {
        imageUrl.value = URL.createObjectURL(file);
        recipeRequest.value.imageFile = file;
    }
};

const addIngredient = () => {
    recipeRequest.value.recipeIngredients ??= [];
    recipeRequest.value.recipeIngredients!.push({
        customUnit: '',
        ingredientId: '',
        ingredientModifier: '',
        quantity: 0,
        recipeId: '',
        unitId: ''
    });
};

const removeIngredient = () => {
    const length = recipeRequest.value.recipeIngredients!.length;
    recipeRequest.value.recipeIngredients!.splice(length - 1, 1);
};

const addStep = () => {
    recipeRequest.value.instructions ??= [];
    recipeRequest.value.instructions.push('');
};

const removeStep = () => {
    const length = recipeRequest.value.instructions.length;
    recipeRequest.value.instructions.splice(length - 1, 1);
};

const checkUnitPlurality = (unit: Unit, index: number) => {
    return recipeRequest.value.recipeIngredients![index - 1].quantity === 1 ? unit.name : unit.name + 's';
};
</script>

<template>
    <FormInput id="Title" label="Title" v-model="recipeRequest.title" />
    <FormInput id="Description" label="Description" v-model="recipeRequest.description" type="textarea" />
    <div class="form-group">
        <label class="control-label" for="Image">Image</label>
        <input type="file" class="form-control" id="Image" @change="handleImageUpload">
        <span class="text-danger"></span>
        <img v-if="imageUrl" :src="imageUrl" alt="Uploaded Image">
    </div>
    <FormInput id="PreparationTime" label="Preparation time" v-model="recipeRequest.preparationTime" type="number" />
    <FormInput id="CookingTime" label="Cooking time" v-model="recipeRequest.cookingTime" type="number" />
    <FormInput id="Servings" label="Servings" v-model="recipeRequest.servings" type="number" />
    <FormInput id="IsVegetarian" label="Vegetarian" v-model="recipeRequest.isVegetarian" type="checkbox" />
    <FormInput id="IsVegan" label="Vegan" v-model="recipeRequest.isVegan" type="checkbox" />
    <FormInput id="IsGlutenFree" label="Gluten free" v-model="recipeRequest.isGlutenFree" type="checkbox" />
    Ingredients
    <div class="form-group">
        <div v-for="i in recipeRequest.recipeIngredients?.length" :key="i" class="form-group">
            <!-- Input field for quantity -->
            <input :id="`Ingredient${i}Quantity`" v-model="recipeRequest.recipeIngredients![i - 1].quantity"
                   class="form-control d-inline-block" type="number" style="width: 15%">
            <!-- Select field for unit of measurement -->
            <select :id="`Ingredient${i}Unit`" v-model="recipeRequest.recipeIngredients![i - 1].unitId"
                    class="form-control w-25 d-inline-block" title="Unit of measurement">
                <option :value="null">Custom unit</option>
                <option v-for="unit in units" :key="unit.id" :value="unit.id">{{ checkUnitPlurality(unit, i) + ` (${unit.abbreviation})` }}</option>
            </select>
            <!-- Input field for custom unit -->
            <input v-if="recipeRequest.recipeIngredients![i - 1].unitId === null" v-model="recipeRequest.recipeIngredients![i - 1].customUnit"
                   class="form-control w-25 d-inline-block" placeholder="Enter custom unit">
            of
            <!-- Input field for ingredient modifier -->
            <input v-model="recipeRequest.recipeIngredients![i - 1].ingredientModifier"
                   class="form-control d-inline-block" style="width: 15%" placeholder="Modifier" title="Optional modifier (e.g. sliced/chopped/small/large)">
            <!-- Select field for ingredient -->
            <select :id="`Ingredient${i}`" v-model="recipeRequest.recipeIngredients![i - 1].ingredientId"
                    class="form-control d-inline-block" style="width: 30%">
                <option value="">---Select ingredient---</option>
                <option v-for="ingredient in ingredients" :key="ingredient.id" :value="ingredient.id">{{ ingredient.name }}</option>
            </select>
        </div>
    </div>
    <div class="form-group">
        <button @click="addIngredient" type="button" class="btn btn-primary">Add ingredient</button>
        <button @click="removeIngredient" type="button" class="btn btn-danger"
                :disabled="(recipeRequest.recipeIngredients?.length ?? 0) === 0">Remove ingredient
        </button>
    </div>
    Instructions
    <div class="form-group">
        <template v-for="i in recipeRequest.instructions.length" :key="i">
            <label class="control-label" :for="'Instructions' + i">Step {{ i }}</label>
            <textarea v-model="recipeRequest.instructions[i - 1]"
                      class="form-control" :id="'Instructions' + i"
                      :placeholder="'Enter instructions for step ' + i" />
        </template>
    </div>
    <div class="form-group">
        <button @click="addStep" type="button" class="btn btn-primary">Add step</button>
        <button @click="removeStep" type="button" class="btn btn-danger"
                :disabled="recipeRequest.instructions.length < 2">Remove step
        </button>
    </div>
    Categories
    <div class="form-group">
        <div class="form-check d-inline-block me-3" v-for="category in categories" :key="category.id">
            <input :id="`Category${category.id}`" v-model="recipeRequest.categoryIds" :value="category.id" @click="() => recipeRequest.categoryIds ??= []"
                   class="form-check-input" type="checkbox">
            <label :for="`Category${category.id}`" class="form-check-label">{{ category.name }}</label>
        </div>
    </div>
</template>

<style scoped></style>