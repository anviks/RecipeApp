import GenericService from '@/services/genericService';
import type { RecipeCategory } from '@/types';

export default class RecipeCategoriesService extends GenericService<RecipeCategory, RecipeCategory> {
    protected override getServiceUrl(): string {
        return 'RecipeCategories/';
    }
}