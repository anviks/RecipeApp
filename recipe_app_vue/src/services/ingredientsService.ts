import type { Ingredient } from '@/types';
import GenericService from '@/services/genericService';

export default class IngredientsService extends GenericService<Ingredient> {
    protected override getServiceUrl(): string {
        return 'ingredients/';
    }
}