'use client';

import { useParams } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { RaffleResult, Optional, Raffle, AppUser } from '@/types';

export default function Details() {
    const [raffleResult, setRaffleResult] = useState<RaffleResult>();
    const [raffle, setRaffle] = useState<Raffle>();
    const [user, setUser] = useState<AppUser>();
    const [isLoading, setIsLoading] = useState(true);

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

    return (
        <>
            <h1>Details</h1>
            <div>
                <h4>RaffleResult</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderRaffleResult(raffleResult!, raffle!, user!)}
            </div>
            <div>
                <Link href={`/raffleResults/${id}/edit`}>Edit</Link>
                |
                <Link href={'/raffle-results'}>Back to List</Link>
            </div>
        </>
    );
}

function renderRaffleResult(raffleResult: RaffleResult, raffle: Raffle, user: AppUser) {
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
            <dt className="col-sm-2">
                Anonymous user name
            </dt>
            <dd className="col-sm-10">
                {raffleResult.anonymousUserName || 'N/A'}
            </dd>
        </dl>
    );
}
