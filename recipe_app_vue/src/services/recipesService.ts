import GenericService from '@/services/genericService';
import type { Recipe, RecipeRequest } from '@/types';

export default class RecipesService extends GenericService<RecipeRequest, Recipe> {
    protected override getServiceUrl(): string {
        return 'Recipes/';
    }
}