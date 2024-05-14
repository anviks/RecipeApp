import type { LoginData, RegisterData, ResultObject, UserInfo } from '@/types';
import { useAuthStore } from '@/stores/auth';
import Service from '@/services/service';

export default class AccountService extends Service {
    async register(registerData: RegisterData): Promise<ResultObject<UserInfo>> {
        if (registerData.password !== registerData.confirmPassword) {
            return {
                errors: [{
                    message: 'Passwords do not match.'
                }]
            };
        }

        return await this.makeRequest<UserInfo>('POST', 'register', registerData);
    }

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
        return 'Account/';
    }
}