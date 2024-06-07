import GenericService from '@/services/genericService';
import { Prize } from '@/types';

export default class PrizeService extends GenericService<Prize> {
    protected override getServiceUrl(): string {
        return 'Prizes/';
    }
}