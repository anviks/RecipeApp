import GenericService from '@/services/genericService';
import { Company } from '@/types';

export default class CompanyService extends GenericService<Company> {
    protected override getServiceUrl(): string {
        return 'Companies/';
    }
}