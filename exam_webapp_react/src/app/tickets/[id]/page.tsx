'use client';

import { useParams } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { Ticket, Optional, Raffle, AppUser } from '@/types';

export default function Details() {
    const [ticket, setTicket] = useState<Ticket>();
    const [raffle, setRaffle] = useState<Raffle>();
    const [user, setUser] = useState<AppUser>();
    const [isLoading, setIsLoading] = useState(true);

    let { id } = useParams();
    const { ticketsService, rafflesService, usersService } = useServices();

    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadTicket = async () => {
        const result = await ticketsService.findById(id);
        setTicket(result.data);
        
        const raffleResult = await rafflesService.findById(ticket!.raffleId);
        setRaffle(raffleResult.data);
        
        const userResult = await usersService.findById(ticket!.userId);
        setUser(userResult.data);
        
        setIsLoading(false);
    }

    useEffect(() => {
        loadTicket().then();
    }, []);

    return (
        <>
            <h1>Details</h1>
            <div>
                <h4>Ticket</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderTicket(ticket!, raffle!, user!)}
            </div>
            <div>
                <Link href={`/tickets/${id}/edit`}>Edit</Link>
                |
                <Link href={'/tickets'}>Back to List</Link>
            </div>
        </>
    );
}

function renderTicket(ticket: Ticket, raffle: Raffle, user: AppUser) {
    return (
        <dl className="row">
            <dt className="col-sm-2">
                Raffle
            </dt>
            <dd className="col-sm-10">
                {raffle.raffleName}
            </dd>
            <dt className="col-sm-2">
                User
            </dt>
            <dd className="col-sm-10">
                {user.username}
            </dd>
        </dl>
    );
}
