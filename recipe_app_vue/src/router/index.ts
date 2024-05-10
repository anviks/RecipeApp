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

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        { path: '/', name: 'Home', component: Home },
        { path: '/account/login', name: 'Login', component: Login },
        { path: '/account/register', name: 'Register', component: Register },
        
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
    ]
});

export default router;
