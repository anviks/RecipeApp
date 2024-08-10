import GenericService from '@/services/genericService';
import { Activity } from '@/types';

export default class ActivityService extends GenericService<Activity> {
    protected override getServiceUrl(): string {
        return 'Activities/';
    }
}