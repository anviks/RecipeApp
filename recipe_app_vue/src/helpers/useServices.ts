import { inject } from 'vue';
import type UnitsService from '@/services/unitsService';
import type IngredientTypesService from '@/services/ingredientTypesService';
import type IngredientsService from '@/services/ingredientsService';
import type IngredientTypeAssociationsService from '@/services/ingredientTypeAssociationsService';
import type AccountService from '@/services/accountService';
import type ReviewsService from '@/services/reviewsService';
import type RecipesService from '@/services/recipesService';
import type CategoriesService from '@/services/categoriesService';
import type RecipeCategoriesService from '@/services/recipeCategoriesService';
import type RecipeIngredientsService from '@/services/recipeIngredientsService';

export default function useServices() {
    const accountService = inject('accountService') as AccountService;
    const categoriesService = inject('categoriesService') as CategoriesService;
    const ingredientsService = inject('ingredientsService') as IngredientsService;
    const ingredientTypesService = inject('ingredientTypesService') as IngredientTypesService;
    const ingredientTypeAssociationsService = inject('ingredientTypeAssociationsService') as IngredientTypeAssociationsService;
    const recipeCategoriesService = inject('recipeCategoriesService') as RecipeCategoriesService;
    const recipeIngredientsService = inject('recipeIngredientsService') as RecipeIngredientsService;
    const recipesService = inject('recipesService') as RecipesService;
    const reviewsService = inject('reviewsService') as ReviewsService;
    const unitsService = inject('unitsService') as UnitsService;

    return {
        accountService,
        categoriesService,
        ingredientsService,
        ingredientTypesService,
        ingredientTypeAssociationsService,
        recipeCategoriesService,
        recipeIngredientsService,
        recipesService,
        reviewsService,
        unitsService
    };
}