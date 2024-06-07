'use client';

import Link from 'next/link';
import React, { ChangeEvent, useEffect, useState } from 'react';
import { AppUser, Raffle, Ticket } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Edit() {
    const [ticket, setTicket] = useState<Ticket>();
    const [allRaffles, setAllRaffles] = useState<Raffle[]>([]);
    const [allUsers, setAllUsers] = useState<AppUser[]>([]);
    const [isLoading, setIsLoading] = useState(true);

    const { ticketsService, rafflesService, usersService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    let { id } = useParams();
    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadTicket = async () => {
        const result = await ticketsService.findById(id);
        setTicket(result.data);

        const allRafflesResult = await rafflesService.findAll();
        setAllRaffles(allRafflesResult.data!);

        const allUsersResult = await usersService.findAll();
        setAllUsers(allUsersResult.data!);
        
        setIsLoading(false);
    };

    useEffect(() => {
        loadTicket().then();
    }, []);

    const submitEdit = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await ticketsService.update(id, ticket!, userContext);
        router.push('/tickets');
    };

    return (
        <>
            <h1>Edit</h1>

            <h4>Ticket</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        {
                            (isLoading)
                                ? <p>Loading...</p>
                                : <>
                                    {renderTicket(ticket!, setTicket, allRaffles, allUsers)}
                                    <div className="form-group">
                                        <button onClick={submitEdit} type="submit" className="btn btn-primary">Save
                                        </button>
                                    </div>
                                </>
                        }
                    </form>
                </div>
            </div>

            <div>
                <Link href={'/tickets'}>Back to List</Link>
            </div>
        </>
    );
}

function renderTicket(ticket: Ticket, setTicket: (ticket: Ticket) => void, allRaffles: Raffle[], allUsers: AppUser[]) {
    const updateInput = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setTicket({ ...ticket!, [e.target.name]: e.target.value });
    };

    return (
        <>
            <div className="form-group">
                <label className="control-label" htmlFor="raffleId">Raffle</label>
                <select className="form-control" name="raffleId" value={ticket.raffleId} onChange={updateInput}>
                    <option value="">Select</option>
                    {allRaffles.map((raffle, index) => (
                        <option key={index} value={raffle.id}>{raffle.raffleName}</option>
                    ))}
                </select>
            </div>
            <div className="form-group">
                <label className="control-label" htmlFor="userId">User</label>
                <select className="form-control" name="userId" value={ticket.userId} onChange={updateInput}>
                    <option value="">Select</option>
                    {allUsers.map((user, index) => (
                        <option key={index} value={user.id}>{user.username}</option>
                    ))}
                </select>
            </div>
        </>
    );
}
