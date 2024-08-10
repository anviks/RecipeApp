'use client';

import { useEffect, useState } from 'react';
import { useServices } from '@/components/ServiceContext';
import { Prize } from '@/types';
import Link from 'next/link';

export default function Prizes() {
    const [isLoading, setIsLoading] = useState(true);
    const [prizes, setPrizes] = useState<Prize[]>([]);
    const { prizesService } = useServices();

    const loadPrizes = async () => {
        const allPrizes = await prizesService.findAll();
        if (allPrizes.data) {
            setPrizes(allPrizes.data);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        loadPrizes().then();
    }, []);
    
    return (
        <>
            <h1>Index</h1>

            <p>
                <Link href={'/prizes/create'}>Create New</Link>
            </p>
            {(isLoading)
                ? <p>Loading...</p>
                : renderPrizesTable(prizes)}
        </>
    );
}

function renderPrizesTable(prizes: Prize[]) {
    return <table className="table">
        <thead>
        <tr>
            <th>
                Prize name
            </th>
            <th>
                Raffle ID
            </th>
            <th>
                Raffle Result ID
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        {renderPrizes(prizes)}
        </tbody>
    </table>;
}

function renderPrizes(prizes: Prize[]) {
    return prizes.map(prize => (
        <tr key={prize.id}>
            <td>
                {prize.prizeName}
            </td>
            <td>
                {prize.raffleId}
            </td>
            <td>
                {prize.raffleResultId}
            </td>
            <td>
                <Link href={`/prizes/${prize.id}/edit`}>Edit</Link> |
                <Link href={`/prizes/${prize.id}`}>Details</Link> |
                <Link href={`/prizes/${prize.id}/delete`}>Delete</Link>
            </td>
        </tr>
    ));
}
