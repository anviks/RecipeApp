import { inject } from 'vue';
import type AccountService from '@/services/accountService';
import type SamplesService from '@/services/samplesService';

export default function useServices() {
    const accountService = inject('accountService') as AccountService;
    const samplesService = inject('samplesService') as SamplesService;

    return {
        accountService,
        samplesService,
    };
}