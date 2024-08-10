'use client';

import { useParams } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { Raffle, Optional, Company } from '@/types';

export default function Details() {
    const [raffle, setRaffle] = useState<Raffle>();
    const [company, setCompany] = useState<Company>();
    const [isLoading, setIsLoading] = useState(true);

    let { id } = useParams();
    const { rafflesService, companiesService } = useServices();

    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadRaffle = async () => {
        const result = await rafflesService.findById(id);
        setRaffle(result.data);
        
        const companyResult = await companiesService.findById(raffle!.companyId);
        setCompany(companyResult.data);
        
        setIsLoading(false);
    }

    useEffect(() => {
        loadRaffle().then();
    }, []);

    return (
        <>
            <h1>Details</h1>
            <div>
                <h4>Raffle</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderRaffle(raffle!, company!)}
            </div>
            <div>
                <Link href={`/raffles/${id}/edit`}>Edit</Link>
                |
                <Link href={'/raffles'}>Back to List</Link>
            </div>
        </>
    );
}

function renderRaffle(raffle: Raffle, company: Company) {
    return (
        <dl className="row">
            <dt className="col-sm-2">
                Raffle name
            </dt>
            <dd className="col-sm-10">
                {raffle.raffleName}
            </dd>
            <dt className="col-sm-2">
                Visible to public
            </dt>
            <dd className="col-sm-10">
                {raffle.visibleToPublic ? 'Yes' : 'No'}
            </dd>
            <dt className="col-sm-2">
                Allow anonymous users
            </dt>
            <dd className="col-sm-10">
                {raffle.allowAnonymousUsers ? 'Yes' : 'No'}
            </dd>
            <dt className="col-sm-2">
                Start date
            </dt>
            <dd className="col-sm-10">
                {raffle.startDate}
            </dd>
            <dt className="col-sm-2">
                End date
            </dt>
            <dd className="col-sm-10">
                {raffle.endDate}
            </dd>
            <dt className="col-sm-2">
                Company
            </dt>
            <dd className="col-sm-10">
                {company.companyName}
            </dd>
        </dl>
    );
}
