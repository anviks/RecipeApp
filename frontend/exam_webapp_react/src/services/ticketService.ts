import GenericService from '@/services/genericService';
import { Ticket } from '@/types';

export default class TicketService extends GenericService<Ticket> {
    protected override getServiceUrl(): string {
        return 'Tickets/';
    }
}