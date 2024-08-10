import GenericService from '@/services/genericService';
import type { RecipeIngredient } from '@/types';

export default class RecipeIngredientsService extends GenericService<RecipeIngredient, RecipeIngredient> {
    protected override getServiceUrl(): string {
        return 'RecipeIngredients/';
    }
}