import GenericService from '@/services/genericService';
import { Raffle } from '@/types';

export default class RaffleService extends GenericService<Raffle> {
    protected override getServiceUrl(): string {
        return 'Raffles/';
    }
}