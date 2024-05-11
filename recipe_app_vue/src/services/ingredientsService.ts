import type { Ingredient, ResultObject } from '@/types';
import GenericService from '@/services/genericService';

export default class IngredientsService extends GenericService<Ingredient> {
    override async create(ingredient: Ingredient): Promise<ResultObject<Ingredient>> {
        ingredient.ingredientTypeAssociations = undefined;
        return await this.makeRequest<Ingredient>('POST', '', ingredient);
    }
    
    protected override getServiceUrl(): string {
        return 'ingredients/';
    }
}