'use client';

import Link from 'next/link';
import React, { useEffect, useState } from 'react';
import { Optional, Prize, Raffle, RaffleResult } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Delete() {
    const [prize, setPrize] = useState<Prize>();
    const [raffle, setRaffle] = useState<Raffle>();
    const [raffleResult, setRaffleResult] = useState<RaffleResult>();
    const [isLoading, setIsLoading] = useState(true);

    const router = useRouter();
    const { userContext, setUserContext } = useUserContext();

    let { id } = useParams();
    const { prizesService, rafflesService, raffleResultsService } = useServices();

    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadPrize = async () => {
        const result = await prizesService.findById(id);
        setPrize(result.data);

        const raffleResult = await rafflesService.findById(prize!.raffleId);
        setRaffle(raffleResult.data);

        let raffleResultId = prize!.raffleResultId;
        if (raffleResultId !== undefined) {
            const raffleResultResult = await raffleResultsService.findById(raffleResultId);
            setRaffleResult(raffleResultResult.data);
        }
        
        setIsLoading(false);
    };

    useEffect(() => {
        loadPrize().then();
    }, []);

    const submitDelete = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await prizesService.delete(id, userContext);
        router.push('/prizes');
    };

    return (
        <>
            <h1>Delete</h1>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Prize</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderPrize(prize!, submitDelete, raffle!, raffleResult)}
            </div>
        </>
    );
}

function renderPrize(prize: Prize, submitDelete: (e: React.MouseEvent<HTMLButtonElement>) => void, raffle: Raffle, raffleResult: Optional<RaffleResult>) {
    return <>
        <dl className="row">
            <dt className="col-sm-2">
                Prize name
            </dt>
            <dd className="col-sm-10">
                {prize.prizeName}
            </dd>
            <dt className="col-sm-2">
                Raffle
            </dt>
            <dd className="col-sm-10">
                {raffle.raffleName}
            </dd>
            <dt className="col-sm-2">
                Raffle result
            </dt>
            <dd className="col-sm-10">
                {raffleResult?.id ?? 'N/A'}
            </dd>
        </dl>
        <form method="post">
            <button onClick={submitDelete} type="submit" className="btn btn-danger">Delete</button>
            |
            <Link href={'/prizes'}>Back to List</Link>
        </form>
    </>
}
