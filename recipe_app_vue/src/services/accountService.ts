import type { LoginData, ResultObject, UserInfo } from '@/types';
import httpClient from '@/services/httpClient';
import { useAuthStore } from '@/stores/auth';
import Service from '@/services/service';

export default class AccountService extends Service {
    private constructor() {
        super();
    }

    static async login(loginData: LoginData): Promise<ResultObject<UserInfo>> {
        return await this.makeRequest<UserInfo>('POST', 'login?expiresInSeconds=5', loginData);
    }

    static async logout(): Promise<ResultObject<null>> {
        const authStore = useAuthStore();
        const result = await this.makeRequest<null>('POST', 'logout', {
            refreshToken: authStore.refreshToken
        });
        if (!result.errors) {
            authStore.clearUserDetails();
        }
        return result;
    }

    protected static override getServiceUrl(): string {
        return 'account/';
    }
}