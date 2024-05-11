import Service from '@/services/service';
import type { ResultObject } from '@/types';

export default abstract class GenericService<T> extends Service {
    async findAll(): Promise<ResultObject<T[]>> {
        return await this.makeRequest<T[]>('GET', '');
    }

    async findById(id: string): Promise<ResultObject<T>> {
        return await this.makeRequest<T>('GET', id);
    }

    async create(entity: T): Promise<ResultObject<T>> {
        return await this.makeRequest<T>('POST', '', entity);
    }

    async update(id: string, entity: T): Promise<ResultObject<T>> {
        return await this.makeRequest<T>('PUT', id, entity);
    }

    async delete(id: string): Promise<ResultObject<T>> {
        return await this.makeRequest<T>('DELETE', id);
    }
}