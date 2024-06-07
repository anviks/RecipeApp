'use client';

import Link from 'next/link';
import React, { useEffect, useState } from 'react';
import { Company, Raffle } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Delete() {
    const [raffle, setRaffle] = useState<Raffle>();
    const [company, setCompany] = useState<Company>();
    const [isLoading, setIsLoading] = useState(true);

    const router = useRouter();
    const { userContext, setUserContext } = useUserContext();

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
    };

    useEffect(() => {
        loadRaffle().then();
    }, []);

    const submitDelete = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await rafflesService.delete(id, userContext);
        router.push('/raffles');
    };

    return (
        <>
            <h1>Delete</h1>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Raffle</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderRaffle(raffle!, submitDelete, company!)}
            </div>
        </>
    );
}

function renderRaffle(raffle: Raffle, submitDelete: (e: React.MouseEvent<HTMLButtonElement>) => void, company: Company) {
    return <>
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
        <form method="post">
            <button onClick={submitDelete} type="submit" className="btn btn-danger">Delete</button>
            |
            <Link href={'/raffles'}>Back to List</Link>
        </form>
    </>
}
