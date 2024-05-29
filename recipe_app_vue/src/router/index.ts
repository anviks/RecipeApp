import { createRouter, createWebHistory } from 'vue-router';
import Login from '@/views/account/Login.vue';
import Home from '@/views/Home.vue';
import Ingredients from '@/views/ingredients/Ingredients.vue';
import IngredientDetails from '@/views/ingredients/IngredientDetails.vue';
import IngredientEdit from '@/views/ingredients/IngredientEdit.vue';
import IngredientDelete from '@/views/ingredients/IngredientDelete.vue';
import IngredientCreate from '@/views/ingredients/IngredientCreate.vue';
import IngredientTypeCreate from '@/views/ingredient-types/IngredientTypeCreate.vue';
import IngredientTypeDelete from '@/views/ingredient-types/IngredientTypeDelete.vue';
import IngredientTypes from '@/views/ingredient-types/IngredientTypes.vue';
import IngredientTypeDetails from '@/views/ingredient-types/IngredientTypeDetails.vue';
import IngredientTypeEdit from '@/views/ingredient-types/IngredientTypeEdit.vue';
import Register from '@/views/account/Register.vue';
import Units from '@/views/units/Units.vue';
import UnitDetails from '@/views/units/UnitDetails.vue';
import UnitEdit from '@/views/units/UnitEdit.vue';
import UnitDelete from '@/views/units/UnitDelete.vue';
import UnitCreate from '@/views/units/UnitCreate.vue';
import Reviews from '@/views/reviews/Reviews.vue';
import ReviewDetails from '@/views/reviews/ReviewDetails.vue';
import ReviewEdit from '@/views/reviews/ReviewEdit.vue';
import ReviewDelete from '@/views/reviews/ReviewDelete.vue';
import ReviewCreate from '@/views/reviews/ReviewCreate.vue';
import Recipes from '@/views/recipes/Recipes.vue';
import RecipeDetails from '@/views/recipes/RecipeDetails.vue';
import RecipeEdit from '@/views/recipes/RecipeEdit.vue';
import RecipeDelete from '@/views/recipes/RecipeDelete.vue';
import RecipeCreate from '@/views/recipes/RecipeCreate.vue';
import CategoryDetails from '@/views/categories/CategoryDetails.vue';
import CategoryEdit from '@/views/categories/CategoryEdit.vue';
import CategoryDelete from '@/views/categories/CategoryDelete.vue';
import CategoryCreate from '@/views/categories/CategoryCreate.vue';
import Categories from '@/views/categories/Categories.vue';

const router = createRouter({
    history: createWebHistory(import.meta.env.VITE_BASE_URL),
    routes: [
        { path: '/', name: 'Home', component: Home },
        { path: '/account/login', name: 'Login', component: Login },
        { path: '/account/register', name: 'Register', component: Register },

        { path: '/categories', name: 'Categories', component: Categories },
        { path: '/categories/:id', name: 'CategoryDetails', component: CategoryDetails },
        { path: '/categories/:id/edit', name: 'CategoryEdit', component: CategoryEdit },
        { path: '/categories/:id/delete', name: 'CategoryDelete', component: CategoryDelete },
        { path: '/categories/create', name: 'CategoryCreate', component: CategoryCreate },
        
        { path: '/ingredients', name: 'Ingredients', component: Ingredients },
        { path: '/ingredients/:id', name: 'IngredientDetails', component: IngredientDetails },
        { path: '/ingredients/:id/edit', name: 'IngredientEdit', component: IngredientEdit },
        { path: '/ingredients/:id/delete', name: 'IngredientDelete', component: IngredientDelete },
        { path: '/ingredients/create', name: 'IngredientCreate', component: IngredientCreate },

        { path: '/ingredient-types', name: 'IngredientTypes', component: IngredientTypes },
        { path: '/ingredient-types/:id', name: 'IngredientTypeDetails', component: IngredientTypeDetails },
        { path: '/ingredient-types/:id/edit', name: 'IngredientTypeEdit', component: IngredientTypeEdit },
        { path: '/ingredient-types/:id/delete', name: 'IngredientTypeDelete', component: IngredientTypeDelete },
        { path: '/ingredient-types/create', name: 'IngredientTypeCreate', component: IngredientTypeCreate },

        { path: '/recipes', name: 'Recipes', component: Recipes },
        { path: '/recipes/:id', name: 'RecipeDetails', component: RecipeDetails },
        { path: '/recipes/:id/edit', name: 'RecipeEdit', component: RecipeEdit },
        { path: '/recipes/:id/delete', name: 'RecipeDelete', component: RecipeDelete },
        { path: '/recipes/create', name: 'RecipeCreate', component: RecipeCreate },

        { path: '/reviews', name: 'Reviews', component: Reviews },
        { path: '/reviews/:id', name: 'ReviewDetails', component: ReviewDetails },
        { path: '/reviews/:id/edit', name: 'ReviewEdit', component: ReviewEdit },
        { path: '/reviews/:id/delete', name: 'ReviewDelete', component: ReviewDelete },
        { path: '/reviews/create', name: 'ReviewCreate', component: ReviewCreate },

        { path: '/units', name: 'Units', component: Units },
        { path: '/units/:id', name: 'UnitDetails', component: UnitDetails },
        { path: '/units/:id/edit', name: 'UnitEdit', component: UnitEdit },
        { path: '/units/:id/delete', name: 'UnitDelete', component: UnitDelete },
        { path: '/units/create', name: 'UnitCreate', component: UnitCreate },
    ]
});

export default router;
