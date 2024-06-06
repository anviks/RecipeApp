import GenericService from '@/services/genericService';
import type { Sample } from '@/types';

export default class SamplesService extends GenericService<Sample, Sample> {
    protected override getServiceUrl(): string {
        return 'Samples/';
    }
}