'use client';

import Link from 'next/link';
import React, { useEffect, useState } from 'react';
import { AppUser, Raffle, RaffleResult } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Delete() {
    const [raffleResult, setRaffleResult] = useState<RaffleResult>();
    const [raffle, setRaffle] = useState<Raffle>();
    const [user, setUser] = useState<AppUser>();
    const [isLoading, setIsLoading] = useState(true);

    const router = useRouter();
    const { userContext, setUserContext } = useUserContext();

    let { id } = useParams();
    const { raffleResultsService, rafflesService, usersService } = useServices();

    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadRaffleResult = async () => {
        const result = await raffleResultsService.findById(id);
        setRaffleResult(result.data);

        const raffleRes = await rafflesService.findById(raffleResult!.raffleId);
        setRaffle(raffleRes.data);

        let userId = raffleResult!.userId;
        if (userId !== undefined) {
            const userResult = await usersService.findById(userId);
            setUser(userResult.data);
        }
        
        setIsLoading(false);
    };

    useEffect(() => {
        loadRaffleResult().then();
    }, []);

    const submitDelete = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await raffleResultsService.delete(id, userContext);
        router.push('/raffle-results');
    };

    return (
        <>
            <h1>Delete</h1>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>RaffleResult</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderRaffleResult(raffleResult!, submitDelete, raffle!, user!)}
            </div>
        </>
    );
}

function renderRaffleResult(raffleResult: RaffleResult, submitDelete: (e: React.MouseEvent<HTMLButtonElement>) => void, raffle: Raffle, user: AppUser) {
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
            <dt className="col-sm-2">
                Anonymous user name
            </dt>
            <dd className="col-sm-10">
                {raffleResult.anonymousUserName || 'N/A'}
            </dd>
        </dl>
        <form method="post">
            <button onClick={submitDelete} type="submit" className="btn btn-danger">Delete</button>
            |
            <Link href={'/raffle-results'}>Back to List</Link>
        </form>
    </>
}
