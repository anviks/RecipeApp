'use client';

import { useEffect, useState } from 'react';
import { useServices } from '@/components/ServiceContext';
import { Raffle } from '@/types';
import Link from 'next/link';

export default function Raffles() {
    const [isLoading, setIsLoading] = useState(true);
    const [raffles, setRaffles] = useState<Raffle[]>([]);
    const { rafflesService } = useServices();

    const loadRaffles = async () => {
        const allRaffles = await rafflesService.findAll();
        if (allRaffles.data) {
            setRaffles(allRaffles.data);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        loadRaffles().then();
    }, []);
    
    return (
        <>
            <h1>Index</h1>

            <p>
                <Link href={'/raffles/create'}>Create New</Link>
            </p>
            {(isLoading)
                ? <p>Loading...</p>
                : renderRafflesTable(raffles)}
        </>
    );
}

function renderRafflesTable(raffles: Raffle[]) {
    return <table className="table">
        <thead>
        <tr>
            <th>
                Raffle name
            </th>
            <th>
                Visible to public
            </th>
            <th>
                Allow anonymous users
            </th>
            <th>
                Start date
            </th>
            <th>
                End date
            </th>
            <th>
                Company ID
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        {renderRaffles(raffles)}
        </tbody>
    </table>;
}

function renderRaffles(raffles: Raffle[]) {
    return raffles.map(raffle => (
        <tr key={raffle.id}>
            <td>
                {raffle.raffleName}
            </td>
            <td>
                {raffle.visibleToPublic ? 'Yes' : 'No'}
            </td>
            <td>
                {raffle.allowAnonymousUsers ? 'Yes' : 'No'}
            </td>
            <td>
                {raffle.startDate}
            </td>
            <td>
                {raffle.endDate}
            </td>
            <td>
                {raffle.companyId}
            </td>
            <td>
                <Link href={`/raffles/${raffle.id}/edit`}>Edit</Link> |
                <Link href={`/raffles/${raffle.id}`}>Details</Link> |
                <Link href={`/raffles/${raffle.id}/delete`}>Delete</Link>
            </td>
        </tr>
    ));
}
