import type { IngredientTypeAssociation } from '@/types';
import GenericService from '@/services/genericService';

export default class IngredientTypeAssociationsService extends GenericService<IngredientTypeAssociation> {
    protected override getServiceUrl(): string {
        return 'IngredientTypeAssociations/';
    }
}