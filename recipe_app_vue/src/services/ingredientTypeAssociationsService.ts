import GenericService from '@/services/genericService';
import type { IngredientTypeAssociation } from '@/types';

export default class IngredientTypeAssociationsService extends GenericService<IngredientTypeAssociation> {
    protected getServiceUrl(): string {
        return 'ingredientTypeAssociations/';
    }
}