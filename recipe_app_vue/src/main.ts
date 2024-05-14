import './assets/main.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'font-awesome/css/font-awesome.min.css';
import 'bootstrap';

import { createApp } from 'vue';
import { createPinia } from 'pinia';

import App from './App.vue';
import router from './router';
import IngredientsService from '@/services/ingredientsService';
import IngredientTypesService from '@/services/ingredientTypesService';
import AccountService from '@/services/accountService';
import IngredientTypeAssociationsService from '@/services/ingredientTypeAssociationsService';
import UnitsService from '@/services/unitsService';
import ReviewsService from '@/services/reviewsService';
import RecipesService from '@/services/recipesService';

const app = createApp(App);

app.provide('accountService', new AccountService());
app.provide('ingredientsService', new IngredientsService());
app.provide('ingredientTypesService', new IngredientTypesService());
app.provide('ingredientTypeAssociationsService', new IngredientTypeAssociationsService());
app.provide('recipesService', new RecipesService());
app.provide('reviewsService', new ReviewsService());
app.provide('unitsService', new UnitsService());

app.use(createPinia());
app.use(router);

app.mount('#app');
