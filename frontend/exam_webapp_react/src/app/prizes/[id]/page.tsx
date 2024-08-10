'use client';

import { useParams } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { Prize, Optional, Raffle, RaffleResult } from '@/types';

export default function Details() {
    const [prize, setPrize] = useState<Prize>();
    const [raffle, setRaffle] = useState<Raffle>();
    const [raffleResult, setRaffleResult] = useState<RaffleResult>();
    const [isLoading, setIsLoading] = useState(true);

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

    return (
        <>
            <h1>Details</h1>
            <div>
                <h4>Prize</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderPrize(prize!, raffle!, raffleResult)}
            </div>
            <div>
                <Link href={`/prizes/${id}/edit`}>Edit</Link>
                |
                <Link href={'/prizes'}>Back to List</Link>
            </div>
        </>
    );
}

function renderPrize(prize: Prize, raffle: Raffle, raffleResult: Optional<RaffleResult>) {
    return (
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
    );
}
