import axios, { AxiosInstance } from 'axios';
import type { ResultObject, UserContext, UserInfo } from '@/types';

const apiUrl: string = `${process.env.NEXT_PUBLIC_BACKEND_URL}/api/v1/`;

export default abstract class Service {
    private httpClient: AxiosInstance;
    
    public constructor() {
        this.httpClient = axios.create({
            baseURL: apiUrl
        });
    }

    protected async makeRequest<T>(
        method: string,
        url: string,
        data?: any,
        headers?: any,
        userContext?: UserContext
    ): Promise<ResultObject<T>> {
        url = this.getServiceUrl() + url;
        try {
            headers = headers || {};
            if (userContext) {
                await this.attachValidToken(headers, userContext);
            }
            const response = await this.httpClient.request<T>({
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
                    status: error.response?.status,
                    statusText: error.response?.statusText,
                    message: error.response?.data?.error || JSON.stringify(error.response?.data?.errors)
                }]
            };
        }
    }
    
    private async attachValidToken(headers: any, userContext: UserContext) {
        if (userContext.expiresAt && userContext.expiresAt < Date.now() / 1000) {
            console.log('JWT expired, refreshing...');
            const result: ResultObject<UserInfo> = await axios.post(`${apiUrl}account/refresh`, {
                jsonWebToken: userContext!.jsonWebToken,
                refreshToken: userContext!.refreshToken
            });
            if (result.data) {
                userContext.jsonWebToken = result.data.jsonWebToken;
                userContext.refreshToken = result.data.refreshToken;
            }
        }
        if (userContext.jsonWebToken) {
            headers.Authorization = `Bearer ${userContext.jsonWebToken}`;
        }
    }

    protected abstract getServiceUrl(): string;
}
