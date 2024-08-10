import type { App } from 'vue';
import AccountService from '@/services/accountService';
import SamplesService from '@/services/samplesService';

export default {
    install(app: App) {
        app.provide('accountService', new AccountService());
        app.provide('samplesService', new SamplesService());
    }
}

