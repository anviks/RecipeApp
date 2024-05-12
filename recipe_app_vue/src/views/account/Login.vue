<script setup lang="ts">
import { inject, ref } from 'vue';
import { useAuthStore } from '@/stores/auth';
import AccountService from '@/services/accountService';
import type { LoginData, ResultObject, UserInfo } from '@/types';
import { useRoute, useRouter } from 'vue-router';

const accountService = inject('accountService') as AccountService;
const authStore = useAuthStore();
const router = useRouter();
const route = useRoute();
const returnUrl = route.query.returnUrl as string | undefined;
const loginData = ref<LoginData>({
    usernameOrEmail: '',
    password: ''
});
const loginIsOngoing = ref(false);
const errors = ref<string[]>([]);

const submitLogin = async () => {
    loginIsOngoing.value = true;

    const result: ResultObject<UserInfo> = await accountService.login(loginData.value);

    if (result.data) {
        authStore.jsonWebToken = result.data.jsonWebToken;
        authStore.refreshToken = result.data.refreshToken;
        errors.value = [];
        await router.push(returnUrl ?? '/');
    } else {
        errors.value = result.errors?.map(e => e.message) ?? [];
    }

    loginIsOngoing.value = false;
};
</script>

<template>
    <h1 v-if="!authStore.isAuthenticated">Log in</h1>
    <div class="row">
        <div class="col-md-6">
            <div v-if="!authStore.isAuthenticated">
                <form method="post">
                    <hr />
                    <div class="text-danger">{{ errors.join('\n') }}</div>
                    <div class="form-floating mb-3">
                        <input v-model="loginData.usernameOrEmail" class="form-control" autocomplete="username"
                               placeholder=""
                               type="text"
                               id="Input_UsernameOrEmail"
                               name="Input.UsernameOrEmail">
                        <label class="form-label" for="Input_UsernameOrEmail">UsernameOrEmail</label>
                    </div>
                    <div class="form-floating mb-3">
                        <input v-model="loginData.password" class="form-control" autocomplete="current-password"
                               placeholder="" type="password" id="Input_Password"
                               name="Input.Password">
                        <label class="form-label" for="Input_Password">Password</label>
                    </div>
                    <div>
                        <button @click="submitLogin" :disabled="loginIsOngoing" id="login-submit" type="submit"
                                class="w-100 btn btn-lg btn-primary">
                            <span v-if="loginIsOngoing">
                                <span class="spinner-border spinner-border-sm" role="status"></span>
                                Loading...
                            </span>
                            <span v-else>Log in</span>
                        </button>
                        <p>Don't have an account?
                            <RouterLink :to="{name: 'Register', query: route.query}">Register</RouterLink>
                        </p>
                    </div>
                </form>
            </div>
            <span v-else>You're already logged in!</span>
        </div>
    </div>
</template>

<style scoped>

</style>