'use client';

import Link from 'next/link';
import React, { ChangeEvent, useEffect, useState } from 'react';
import { Company } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Edit() {
    const [company, setCompany] = useState<Company>();
    const [isLoading, setIsLoading] = useState(true);

    const { companiesService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    let { id } = useParams();
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

    const submitEdit = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await companiesService.update(id, company!, userContext);
        router.push('/companies');
    };

    return (
        <>
            <h1>Edit</h1>

            <h4>Company</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        {
                            (isLoading)
                                ? <p>Loading...</p>
                                : <>
                                    {renderCompany(company!, setCompany)}
                                    <div className="form-group">
                                        <button onClick={submitEdit} type="submit" className="btn btn-primary">Save
                                        </button>
                                    </div>
                                </>
                        }
                    </form>
                </div>
            </div>

            <div>
                <Link href={'/companies'}>Back to List</Link>
            </div>
        </>
    );
}

function renderCompany(company: Company, setCompany: (company: Company) => void) {
    const updateInput = (e: ChangeEvent<HTMLInputElement>) => {
        setCompany({ ...company!, [e.target.name]: e.target.value });
    };

    return <div className="form-group">
        <label className="control-label" htmlFor="companyName">Company name</label>
        <input name="companyName" className="form-control valid" id="companyName" type="text" value={company.companyName} onChange={updateInput} />
        <span className="text-danger field-validation-valid"></span>
    </div>;
}
