import GenericService from '@/services/genericService';
import type { Category } from '@/types';

export default class CategoriesService extends GenericService<Category, Category> {
    protected override getServiceUrl(): string {
        return 'Categories/';
    }
}