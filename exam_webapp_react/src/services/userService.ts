import GenericService from '@/services/genericService';
import { AppUser } from '@/types';

export default class UserService extends GenericService<AppUser> {
    protected override getServiceUrl(): string {
        return 'Users/';
    }
}