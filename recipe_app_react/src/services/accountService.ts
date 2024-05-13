import type { LoginData, RegisterData, ResultObject, UserContext, UserInfo } from '@/types';
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

    async logout(userContext: UserContext, setUserContext: (userContext: UserContext) => void): Promise<ResultObject<null>> {
        const result = await this.makeRequest<null>('POST', 'logout', {
            refreshToken: userContext.refreshToken
        }, undefined, userContext);
        if (!result.errors) {
            setUserContext({
                expiresAt: null, 
                isAuthenticated: () => false,
                jsonWebToken: '', 
                refreshToken: '',
                email: '', 
                username: ''
            })
        }
        return result;
    }

    protected override getServiceUrl(): string {
        return 'account/';
    }
}