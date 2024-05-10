import type { ResultObject } from '@/types';
import httpClient from '@/services/httpClient';

export default abstract class Service {
    protected constructor() {}

    protected static async makeRequest<T>(
        method: string,
        url: string,
        data?: any,
        headers?: any
    ): Promise<ResultObject<T>> {
        url = this.getServiceUrl() + url;
        try {
            const response = await httpClient.request<T>({
                url,
                method,
                headers,
                data
            });
            if (response.status < 300) {
                return {
                    data: response.data
                };
            }
            return {
                errors: [response.status.toString() + ' ' + response.statusText]
            };
        } catch (error: any) {
            return {
                errors: [JSON.stringify(error)]
            };
        }
    }
    
    protected static getServiceUrl(): string {
        return '';
    }
}
