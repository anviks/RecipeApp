import { ref, computed, watch } from 'vue';
import { defineStore } from 'pinia';
import type { Optional } from '@/types';

export const useAuthStore = defineStore('auth', () => {
    // ref - state variables
    const jsonWebToken = ref<Optional<string>>(localStorage.getItem('jsonWebToken') ?? null);
    const refreshToken = ref<Optional<string>>(localStorage.getItem('refreshToken') ?? null);
    const username = ref<Optional<string>>(null);
    const email = ref<Optional<string>>(null);
    const expiresAt = ref<Optional<number>>(null);

    // computed - getters
    const isAuthenticated = computed<boolean>(() => !!jsonWebToken.value);

    watch(jsonWebToken, (newValue) => {
        if (!newValue) {
            localStorage.removeItem('jsonWebToken');
        } else {
            localStorage.setItem('jsonWebToken', newValue);
            const parsedJwt = parseJwt(newValue);
            username.value = parsedJwt.username;
            email.value = parsedJwt.email;
            expiresAt.value = parsedJwt.expiresAt;
        }
    });

    watch(refreshToken, (newValue) => {
        if (!newValue) {
            localStorage.removeItem('refreshToken');
        } else {
            localStorage.setItem('refreshToken', newValue);
        }
    });
    
    if (jsonWebToken.value) {
        const parsedJwt = parseJwt(jsonWebToken.value);
        username.value = parsedJwt.username;
        email.value = parsedJwt.email;
        expiresAt.value = parsedJwt.expiresAt;
    }

    // functions - actions
    const clearUserDetails = () => {
        jsonWebToken.value = null;
        refreshToken.value = null;
        username.value = null;
        email.value = null;
        expiresAt.value = null;
    }

    return { jsonWebToken, refreshToken, username, email, expiresAt, isAuthenticated, clearUserDetails };
});

function parseJwt(token: string): any {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
    const parsedPayload = JSON.parse(jsonPayload);

    return {
        username: parsedPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
        email: parsedPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
        expiresAt: parsedPayload.exp
    };
}
