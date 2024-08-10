import GenericService from '@/services/genericService';
import type { Review } from '@/types';

export default class ReviewsService extends GenericService<Review, Review> {
    protected override getServiceUrl(): string {
        return 'Reviews/';
    }
}