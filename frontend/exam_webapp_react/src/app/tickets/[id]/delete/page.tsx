'use client';

import Link from 'next/link';
import React, { useEffect, useState } from 'react';
import { AppUser, Raffle, Ticket } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Delete() {
    const [ticket, setTicket] = useState<Ticket>();
    const [raffle, setRaffle] = useState<Raffle>();
    const [user, setUser] = useState<AppUser>();
    const [isLoading, setIsLoading] = useState(true);

    const router = useRouter();
    const { userContext, setUserContext } = useUserContext();

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
    };

    useEffect(() => {
        loadTicket().then();
    }, []);

    const submitDelete = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await ticketsService.delete(id, userContext);
        router.push('/tickets');
    };

    return (
        <>
            <h1>Delete</h1>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Ticket</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderTicket(ticket!, submitDelete, raffle!, user!)}
            </div>
        </>
    );
}

function renderTicket(ticket: Ticket, submitDelete: (e: React.MouseEvent<HTMLButtonElement>) => void, raffle: Raffle, user: AppUser) {
    return <>
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
        <form method="post">
            <button onClick={submitDelete} type="submit" className="btn btn-danger">Delete</button>
            |
            <Link href={'/tickets'}>Back to List</Link>
        </form>
    </>
}
