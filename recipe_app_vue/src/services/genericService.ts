import Service from '@/services/service';
import type { ResultObject } from '@/types';

export default abstract class GenericService<TRequest, TResponse> extends Service {
    async findAll(): Promise<ResultObject<TResponse[]>> {
        return await this.makeRequest<TResponse[]>('GET', '');
    }

    async findById(id: string): Promise<ResultObject<TResponse>> {
        return await this.makeRequest<TResponse>('GET', id);
    }

    async create(entity: TRequest | FormData): Promise<ResultObject<TResponse>> {
        return await this.makeRequest<TResponse>('POST', '', entity);
    }

    async update(id: string, entity: TRequest | FormData): Promise<ResultObject<TResponse>> {
        return await this.makeRequest<TResponse>('PUT', id, entity);
    }

    async delete(id: string): Promise<ResultObject<TResponse>> {
        return await this.makeRequest<TResponse>('DELETE', id);
    }
}