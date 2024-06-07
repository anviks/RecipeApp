'use client';

import { useEffect, useState } from 'react';
import { useServices } from '@/components/ServiceContext';
import { Ticket } from '@/types';
import Link from 'next/link';

export default function Tickets() {
    const [isLoading, setIsLoading] = useState(true);
    const [tickets, setTickets] = useState<Ticket[]>([]);
    const { ticketsService } = useServices();

    const loadTickets = async () => {
        const allTickets = await ticketsService.findAll();
        if (allTickets.data) {
            setTickets(allTickets.data);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        loadTickets().then();
    }, []);
    
    return (
        <>
            <h1>Index</h1>

            <p>
                <Link href={'/tickets/create'}>Create New</Link>
            </p>
            {(isLoading)
                ? <p>Loading...</p>
                : renderTicketsTable(tickets)}
        </>
    );
}

function renderTicketsTable(tickets: Ticket[]) {
    return <table className="table">
        <thead>
        <tr>
            <th>
                User Id
            </th>
            <th>
                Raffle Id
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        {renderTickets(tickets)}
        </tbody>
    </table>;
}

function renderTickets(tickets: Ticket[]) {
    return tickets.map(ticket => (
        <tr key={ticket.id}>
            <td>
                {ticket.userId}
            </td>
            <td>
                {ticket.raffleId}
            </td>
            <td>
                <Link href={`/tickets/${ticket.id}/edit`}>Edit</Link> |
                <Link href={`/tickets/${ticket.id}`}>Details</Link> |
                <Link href={`/tickets/${ticket.id}/delete`}>Delete</Link>
            </td>
        </tr>
    ));
}
