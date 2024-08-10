import IngredientsService from '@/services/ingredientsService';
import IngredientTypesService from '@/services/ingredientTypesService';
import AccountService from '@/services/accountService';
import IngredientTypeAssociationsService from '@/services/ingredientTypeAssociationsService';
import UnitsService from '@/services/unitsService';
import ReviewsService from '@/services/reviewsService';
import RecipesService from '@/services/recipesService';
import CategoriesService from '@/services/categoriesService';
import RecipeCategoriesService from '@/services/recipeCategoriesService';
import RecipeIngredientsService from '@/services/recipeIngredientsService';
import type { App } from 'vue';

export default {
    install(app: App) {
        app.provide('accountService', new AccountService());
        app.provide('categoriesService', new CategoriesService());
        app.provide('ingredientsService', new IngredientsService());
        app.provide('ingredientTypesService', new IngredientTypesService());
        app.provide('ingredientTypeAssociationsService', new IngredientTypeAssociationsService());
        app.provide('recipeCategoriesService', new RecipeCategoriesService());
        app.provide('recipeIngredientsService', new RecipeIngredientsService());
        app.provide('recipesService', new RecipesService());
        app.provide('reviewsService', new ReviewsService());
        app.provide('unitsService', new UnitsService());
    }
}

