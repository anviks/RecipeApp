import GenericService from '@/services/genericService';
import type { Recipe } from '@/types';

export default class RecipesService extends GenericService<Recipe> {
    protected override getServiceUrl(): string {
        return 'Recipes/';
    }
}