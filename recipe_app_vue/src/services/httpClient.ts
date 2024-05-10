import axios from 'axios';
import { useAuthStore } from '@/stores/auth';
import type { ResultObject, UserInfo } from '@/types';
import AccountService from '@/services/accountService';


const httpClient = axios.create({
    baseURL: 'http://localhost:5211/api/v1/'
});

// Add a request interceptor to attach JWT token to outgoing requests
httpClient.interceptors.request.use(async (config) => {
    const authStore = useAuthStore();
    console.log('interceptor');
    console.log(authStore.expiresAt);
    if (authStore.expiresAt && authStore.expiresAt < Date.now() / 1000) {
        console.log('refreshing...');
        const result: ResultObject<UserInfo> = await axios.post('http://localhost:5211/api/v1/account/refresh?expiresInSeconds=5', {
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
}, (error) => {
    console.log(error);
    return Promise.reject(error);
});

httpClient.interceptors.response.use((response) => {
    console.log(response);
    return response;
}, async (error) => {
    const authStore = useAuthStore();
    console.log(error);
    console.log('logging out...');
    authStore.clearUserDetails();
    return Promise.reject(error);
});

export default httpClient;
