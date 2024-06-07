import GenericService from '@/services/genericService';
import { ActivityType } from '@/types';

export default class ActivityTypeService extends GenericService<ActivityType> {
    protected override getServiceUrl(): string {
        return 'ActivityTypes/';
    }
}