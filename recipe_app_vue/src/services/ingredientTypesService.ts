import type { IngredientType, ResultObject } from '@/types';
import Service from '@/services/service';

export default class IngredientTypesService extends Service {
    private constructor() {
        super();
    }

    static async findAll(): Promise<ResultObject<IngredientType[]>> {
        return this.makeRequest<IngredientType[]>('GET', '');
    }

    static async findById(id: string): Promise<ResultObject<IngredientType>> {
        return this.makeRequest<IngredientType>('GET', id);
    }

    static async create(ingredient: IngredientType): Promise<ResultObject<IngredientType>> {
        return this.makeRequest<IngredientType>('POST', '', ingredient);
    }

    static async update(id: string, ingredient: IngredientType): Promise<ResultObject<IngredientType>> {
        return this.makeRequest<IngredientType>('PUT', id, ingredient);
    }

    static async delete(id: string): Promise<ResultObject<IngredientType>> {
        return this.makeRequest<IngredientType>('DELETE', id);
    }
    
    protected static override getServiceUrl(): string {
        return 'ingredientTypes/';
    }
}