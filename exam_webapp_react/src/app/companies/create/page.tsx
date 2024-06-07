'use client';

import { ChangeEvent, MouseEvent, useState } from 'react';
import { Company } from '@/types';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';
import { useRouter } from 'next/navigation';
import Link from 'next/link';

export default function Create() {
    const [company, setCompany] = useState<Company>({ companyName: ''});

    const { companiesService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    const updateInput = (e: ChangeEvent<HTMLInputElement>) => {
        setCompany({...company!, [e.target.name]: e.target.value});
    }

    const submitCreate = async (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await companiesService.create(company, userContext);
        router.push('/companies');
    };

    return (
        <>
            <h1>Create</h1>

            <h4>Company</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        <div className="form-group">
                            <label className="control-label" htmlFor="companyName">Company name</label>
                            <input className="form-control valid" id="companyName" type="text" name="companyName" value={company.companyName} onChange={updateInput} />
                            <span className="text-danger field-validation-valid"></span>
                        </div>
                        <div className="form-group">
                            <button onClick={submitCreate} type="submit" className="btn btn-primary">Create</button>
                        </div>
                    </form>
                </div>
            </div>

            <div>
                <Link href={'/companies'}>Back to List</Link>
            </div>
        </>
    );
}