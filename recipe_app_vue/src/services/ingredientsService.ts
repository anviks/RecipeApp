import type { Ingredient, ResultObject } from '@/types';
import Service from '@/services/service';

export default class IngredientsService extends Service {
    private constructor() {
        super();
    }

    static async findAll(): Promise<ResultObject<Ingredient[]>> {
        return this.makeRequest<Ingredient[]>('GET', '');
    }

    static async findById(id: string): Promise<ResultObject<Ingredient>> {
        return this.makeRequest<Ingredient>('GET', id);
    }

    static async create(ingredient: Ingredient): Promise<ResultObject<Ingredient>> {
        return this.makeRequest<Ingredient>('POST', '', ingredient);
    }

    static async update(id: string, ingredient: Ingredient): Promise<ResultObject<Ingredient>> {
        return this.makeRequest<Ingredient>('PUT', id, ingredient);
    }
    
    static async delete(id: string): Promise<ResultObject<Ingredient>> {
        return this.makeRequest<Ingredient>('DELETE', id);
    }

    protected static override getServiceUrl(): string {
        return 'ingredients/';
    }
}