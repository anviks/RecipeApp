import type { Router } from 'vue-router';
import type { Ref } from 'vue';
import type { Optional, ResultObject } from '@/types';

export interface ApiResultHandlerOptions<T> {
    result: Promise<ResultObject<T>>,
    dataRef?: Ref<Optional<T>>,
    errorsRef: Ref<string[]>,
    router: Router,
    fallbackRedirect: string,
    successRedirect?: string
}

export async function handleApiResult<T>(
    {
        result,
        dataRef,
        errorsRef,
        router,
        fallbackRedirect,
        successRedirect
    }: ApiResultHandlerOptions<T>) {
    const response = await result;
    if (!response.errors && successRedirect) {
        await router.push({ name: successRedirect });
    }
    if (response.data) {
        if (dataRef) {
            dataRef.value = response.data;
        }
    } else if (response.errors && response.errors.some(item => item !== undefined!)) {
        for (const error of response.errors) {
            if (error.status === 401) {
                errorsRef.value = [];
                errorsRef.value.push('You must be logged in to perform this action.');
            } else if (error.status === 403) {
                errorsRef.value = [];
                errorsRef.value.push('You do not have permission to perform this action.');
            } else if (error.message) {
                errorsRef.value.push(error.message);
                //     'An error occurred. Please try again.'
            } else {
                await router.push({ name: fallbackRedirect });
            }
        }
    } else if (response.errors) {
        await router.push({ name: fallbackRedirect });
    }
}
