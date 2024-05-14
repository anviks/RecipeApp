import { inject } from 'vue';
import type UnitsService from '@/services/unitsService';
import type IngredientTypesService from '@/services/ingredientTypesService';
import type IngredientsService from '@/services/ingredientsService';
import type IngredientTypeAssociationsService from '@/services/ingredientTypeAssociationsService';
import type AccountService from '@/services/accountService';

export default function useServices() {
    const accountService = inject('accountService') as AccountService;
    const ingredientsService = inject('ingredientsService') as IngredientsService;
    const ingredientTypesService = inject('ingredientTypesService') as IngredientTypesService;
    const ingredientTypeAssociationsService = inject('ingredientTypeAssociationsService') as IngredientTypeAssociationsService;
    const unitsService = inject('unitsService') as UnitsService;

    return { accountService, ingredientsService, ingredientTypesService, ingredientTypeAssociationsService, unitsService };
}