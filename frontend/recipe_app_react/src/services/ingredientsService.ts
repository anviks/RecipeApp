import type { Ingredient, ResultObject, UserContext } from '@/types';
import GenericService from '@/services/genericService';

export default class IngredientsService extends GenericService<Ingredient> {
    override async create(ingredient: Ingredient, userContext?: UserContext): Promise<ResultObject<Ingredient>> {
        ingredient.ingredientTypeAssociations = undefined;
        return await super.create(ingredient, userContext);
    }

    override async update(id: string, ingredient: Ingredient, userContext?: UserContext): Promise<ResultObject<Ingredient>> {
        ingredient.ingredientTypeAssociations = undefined;
        return await super.update(id, ingredient, userContext);
    }

    protected override getServiceUrl(): string {
        return 'Ingredients/';
    }
}