import type { Ingredient, ResultObject } from '@/types';
import GenericService from '@/services/genericService';

export default class IngredientsService extends GenericService<Ingredient, Ingredient> {
    override async create(ingredient: Ingredient): Promise<ResultObject<Ingredient>> {
        ingredient.ingredientTypeAssociations = undefined;
        return await super.create(ingredient);
    }
    
    override async update(id: string, ingredient: Ingredient): Promise<ResultObject<Ingredient>> {
        ingredient.ingredientTypeAssociations = undefined;
        return await super.update(id, ingredient);
    }
    
    protected override getServiceUrl(): string {
        return 'Ingredients/';
    }
}