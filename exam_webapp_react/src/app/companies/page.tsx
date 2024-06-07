'use client';

import { useEffect, useState } from 'react';
import { useServices } from '@/components/ServiceContext';
import { Company } from '@/types';
import Link from 'next/link';

export default function Companies() {
    const [isLoading, setIsLoading] = useState(true);
    const [companies, setCompanies] = useState<Company[]>([]);
    const { companiesService } = useServices();

    const loadCompanies = async () => {
        const allCompanies = await companiesService.findAll();
        if (allCompanies.data) {
            setCompanies(allCompanies.data);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        loadCompanies().then();
    }, []);
    
    return (
        <>
            <h1>Index</h1>

            <p>
                <Link href={'/companies/create'}>Create New</Link>
            </p>
            {(isLoading)
                ? <p>Loading...</p>
                : renderCompaniesTable(companies)}
        </>
    );
}

function renderCompaniesTable(companies: Company[]) {
    return <table className="table">
        <thead>
        <tr>
            <th>
                Company name
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        {renderCompanies(companies)}
        </tbody>
    </table>;
}

function renderCompanies(companies: Company[]) {
    return companies.map(company => (
        <tr key={company.id}>
            <td>
                {company.companyName}
            </td>
            <td>
                <Link href={`/companies/${company.id}/edit`}>Edit</Link> |
                <Link href={`/companies/${company.id}`}>Details</Link> |
                <Link href={`/companies/${company.id}/delete`}>Delete</Link>
            </td>
        </tr>
    ));
}
