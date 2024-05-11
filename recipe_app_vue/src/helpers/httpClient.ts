import axios from 'axios';
import { useAuthStore } from '@/stores/auth';
import type { ResultObject, UserInfo } from '@/types';


const httpClient = axios.create({
    baseURL: 'http://localhost:5211/api/v1/'
});

// Add a request interceptor to attach JWT token to outgoing requests
httpClient.interceptors.request.use(async (config) => {
    const authStore = useAuthStore();
    if (authStore.expiresAt && authStore.expiresAt < Date.now() / 1000) {
        console.log('JWT expired, refreshing...');
        const result: ResultObject<UserInfo> = await axios.post('http://localhost:5211/api/v1/account/refresh', {
            jsonWebToken: authStore.jsonWebToken,
            refreshToken: authStore.refreshToken
        });
        if (result.data) {
            authStore.jsonWebToken = result.data.jsonWebToken;
            authStore.refreshToken = result.data.refreshToken;
        }
    }
    if (authStore.jsonWebToken) {
        config.headers.Authorization = `Bearer ${authStore.jsonWebToken}`;
    }
    return config;
});

export default httpClient;
