<script setup lang="ts">
import { inject, ref } from 'vue';
import AccountService from '@/services/accountService';
import { useAuthStore } from '@/stores/auth';
import { useRoute, useRouter } from 'vue-router';

const accountService = inject('accountService') as AccountService;
const authStore = useAuthStore();
const route = useRoute();
const router = useRouter();
const returnUrl = route.query.returnUrl as string | undefined;
const registerData = ref({
    username: '',
    email: '',
    password: '',
    confirmPassword: ''
});
const registerIsOngoing = ref(false);
const errors = ref<string[]>([]);

const submitRegister = async () => {
    registerIsOngoing.value = true;

    const result = await accountService.register(registerData.value);
    console.log(result);

    if (result.data) {
        authStore.jsonWebToken = result.data.jsonWebToken;
        authStore.refreshToken = result.data.refreshToken;
        errors.value = [];
        await router.push(returnUrl ?? '/');
    } else {
        errors.value = result.errors?.map(e => e.message) ?? [];
    }

    registerIsOngoing.value = false;
};
</script>

<template>
    <div class="row">
        <div class="col-md-6">
            <h1>Register as a user</h1>
            <form method="post">
                <h2>Create a new account.</h2>
                <hr>
                <div class="text-danger">{{ errors.join('\n') }}</div>
                <div class="form-floating mb-3">
                    <input class="form-control" autocomplete="username" placeholder="" type="text"
                           id="Input_Username" :maxlength="64"
                           name="Input.Username" v-model="registerData.username">
                    <label for="Input_Username">Username</label>
                </div>
                <div class="form-floating mb-3">
                    <input class="form-control" autocomplete="username" placeholder="" type="email"
                           id="Input_Email" name="Input.Email" v-model="registerData.email">
                    <label for="Input_Email">Email</label>
                </div>
                <div class="form-floating mb-3">
                    <input class="form-control" autocomplete="new-password" placeholder=""
                           type="password" id="Input_Password" :maxlength="100"
                           name="Input.Password" v-model="registerData.password">
                    <label for="Input_Password">Password</label>
                </div>
                <div class="form-floating mb-3">
                    <input class="form-control" autocomplete="new-password" placeholder=""
                           type="password" id="Input_ConfirmPassword" name="Input.ConfirmPassword"
                           v-model="registerData.confirmPassword">
                    <label for="Input_ConfirmPassword">Confirm password</label>
                </div>
                <button @click.prevent="submitRegister" :disabled="registerIsOngoing" id="registerSubmit" type="submit"
                        class="w-100 btn btn-lg btn-primary">
                    <span v-if="registerIsOngoing">
                        <span class="spinner-border spinner-border-sm" role="status"></span>
                        Loading...
                    </span>
                    <span v-else>Register</span>
                </button>
                <p>Already have an account?
                    <RouterLink :to="{name: 'Login', query: route.query}">Login</RouterLink>
                </p>
            </form>
        </div>
    </div>
</template>

<style scoped></style>