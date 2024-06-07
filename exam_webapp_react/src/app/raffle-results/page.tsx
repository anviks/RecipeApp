'use client';

import { useEffect, useState } from 'react';
import { useServices } from '@/components/ServiceContext';
import { AppUser, Raffle, RaffleResult } from '@/types';
import Link from 'next/link';

export default function RaffleResults() {
    const [raffleResults, setRaffleResults] = useState<RaffleResult[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    
    const { raffleResultsService } = useServices();

    const loadRaffleResults = async () => {
        const allRaffleResults = await raffleResultsService.findAll();
        if (allRaffleResults.data) {
            setRaffleResults(allRaffleResults.data);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        loadRaffleResults().then();
    }, []);
    
    return (
        <>
            <h1>Index</h1>

            <p>
                <Link href={'/raffleResults/create'}>Create New</Link>
            </p>
            {(isLoading)
                ? <p>Loading...</p>
                : renderRaffleResultsTable(raffleResults)}
        </>
    );
}

function renderRaffleResultsTable(raffleResults: RaffleResult[]) {
    return <table className="table">
        <thead>
        <tr>
            <th>
                Raffle Id
            </th>
            <th>
                User Id
            </th>
            <th>
                Anonymous user name
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        {renderRaffleResults(raffleResults)}
        </tbody>
    </table>;
}

function renderRaffleResults(raffleResults: RaffleResult[]) {
    return raffleResults.map(raffleResult => (
        <tr key={raffleResult.id}>
            <td>
                {raffleResult.raffleId}
            </td>
            <td>
                {raffleResult.userId}
            </td>
            <td>
                {raffleResult.anonymousUserName || 'N/A'}
            </td>
            <td>
                <Link href={`/raffleResults/${raffleResult.id}/edit`}>Edit</Link> |
                <Link href={`/raffleResults/${raffleResult.id}`}>Details</Link> |
                <Link href={`/raffleResults/${raffleResult.id}/delete`}>Delete</Link>
            </td>
        </tr>
    ));
}
