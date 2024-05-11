import Service from '@/services/service';
import type { ResultObject } from '@/types';

export default abstract class GenericService<T> extends Service {
    async findAll(): Promise<ResultObject<T[]>> {
        return this.makeRequest<T[]>('GET', '');
    }

    async findById(id: string): Promise<ResultObject<T>> {
        return this.makeRequest<T>('GET', id);
    }

    async create(ingredient: T): Promise<ResultObject<T>> {
        return this.makeRequest<T>('POST', '', ingredient);
    }

    async update(id: string, ingredient: T): Promise<ResultObject<T>> {
        return this.makeRequest<T>('PUT', id, ingredient);
    }

    async delete(id: string): Promise<ResultObject<T>> {
        return this.makeRequest<T>('DELETE', id);
    }
}