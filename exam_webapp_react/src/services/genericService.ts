import Service from '@/services/service';
import type { ResultObject, UserContext } from '@/types';

export default abstract class GenericService<T> extends Service {
    async findAll(): Promise<ResultObject<T[]>> {
        return await this.makeRequest<T[]>('GET', '');
    }

    async findById(id: string): Promise<ResultObject<T>> {
        return await this.makeRequest<T>('GET', id);
    }

    async create(entity: T, userContext?: UserContext): Promise<ResultObject<T>> {
        return await this.makeRequest<T>('POST', '', entity, undefined, userContext);
    }

    async update(id: string, entity: T, userContext?: UserContext): Promise<ResultObject<T>> {
        return await this.makeRequest<T>('PUT', id, entity, undefined, userContext);
    }

    async delete(id: string, userContext?: UserContext): Promise<ResultObject<T>> {
        return await this.makeRequest<T>('DELETE', id, undefined, undefined, userContext);
    }
}