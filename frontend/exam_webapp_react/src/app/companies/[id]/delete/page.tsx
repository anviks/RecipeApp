'use client';

import Link from 'next/link';
import React, { useEffect, useState } from 'react';
import { Company } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Delete() {
    const [company, setCompany] = useState<Company>();
    const [isLoading, setIsLoading] = useState(true);

    const router = useRouter();
    const { userContext, setUserContext } = useUserContext();

    let { id } = useParams();
    const { companiesService } = useServices();

    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadCompany = async () => {
        const result = await companiesService.findById(id);
        setCompany(result.data);
        setIsLoading(false);
    };

    useEffect(() => {
        loadCompany().then();
    }, []);

    const submitDelete = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await companiesService.delete(id, userContext);
        router.push('/companies');
    };

    return (
        <>
            <h1>Delete</h1>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Company</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderCompany(company!, submitDelete)}
            </div>
        </>
    );
}

function renderCompany(company: Company, submitDelete: (e: React.MouseEvent<HTMLButtonElement>) => void) {
    return <>
        <dl className="row">
            <dt className="col-sm-2">
                Company name
            </dt>
            <dd className="col-sm-10">
                {company!.companyName}
            </dd>
        </dl>
        <form method="post">
            <button onClick={submitDelete} type="submit" className="btn btn-danger">Delete</button>
            |
            <Link href={'/companies'}>Back to List</Link>
        </form>
    </>
}
