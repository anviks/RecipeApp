import type { ResultObject } from '@/types';
import httpClient from '@/helpers/httpClient';

export default abstract class Service {
    public constructor() {
    }

    protected async makeRequest<T>(
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
            return {
                data: response.data
            };
        } catch (error: any) {
            console.error(error);
            return {
                errors: [{
                    status: error.response.status,
                    statusText: error.response.statusText,
                    message: error.response.data?.error || JSON.stringify(error.response.data?.errors)
                }]
            };
        }
    }

    protected abstract getServiceUrl(): string;
}
