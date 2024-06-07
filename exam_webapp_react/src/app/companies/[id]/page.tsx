'use client';

import { useParams } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { Company, Optional } from '@/types';

export default function Details() {
    const [company, setCompany] = useState<Company>();
    const [isLoading, setIsLoading] = useState(true);

    let { id } = useParams();
    const { companiesService } = useServices();

    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadCompany = async () => {
        const result = await companiesService.findById(id);
        setCompany(result.data);
        setIsLoading(false);
    }

    useEffect(() => {
        loadCompany().then();
    }, []);

    return (
        <>
            <h1>Details</h1>
            <div>
                <h4>Company</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderCompany(company!)}
            </div>
            <div>
                <Link href={`/companies/${id}/edit`}>Edit</Link>
                |
                <Link href={'/companies'}>Back to List</Link>
            </div>
        </>
    );
}

function renderCompany(company: Company) {
    return (
        <dl className="row">
            <dt className="col-sm-2">
                Company name
            </dt>
            <dd className="col-sm-10">
                {company.companyName}
            </dd>
        </dl>
    );
}
