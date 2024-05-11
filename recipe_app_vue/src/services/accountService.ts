import type { LoginData, ResultObject, UserInfo } from '@/types';
import { useAuthStore } from '@/stores/auth';
import Service from '@/services/service';

export default class AccountService extends Service {
    async login(loginData: LoginData): Promise<ResultObject<UserInfo>> {
        return await this.makeRequest<UserInfo>('POST', 'login', loginData);
    }

    async logout(): Promise<ResultObject<null>> {
        const authStore = useAuthStore();
        const result = await this.makeRequest<null>('POST', 'logout', {
            refreshToken: authStore.refreshToken
        });
        if (!result.errors) {
            authStore.clearUserDetails();
        }
        return result;
    }

    protected override getServiceUrl(): string {
        return 'account/';
    }
}