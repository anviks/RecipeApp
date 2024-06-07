import GenericService from '@/services/genericService';
import { RaffleResult } from '@/types';

export default class RaffleResultService extends GenericService<RaffleResult> {
    protected override getServiceUrl(): string {
        return 'RaffleResults/';
    }
}