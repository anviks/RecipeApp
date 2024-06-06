import GenericService from '@/services/genericService';
import { Sample } from '@/types';

export default class SamplesService extends GenericService<Sample> {
    protected override getServiceUrl(): string {
        return 'Samples/';
    }
}