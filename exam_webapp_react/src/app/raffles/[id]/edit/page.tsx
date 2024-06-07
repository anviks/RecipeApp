'use client';

import Link from 'next/link';
import React, { ChangeEvent, useEffect, useState } from 'react';
import { Company, Raffle } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Edit() {
    const [raffle, setRaffle] = useState<Raffle>();
    const [allCompanies, setAllCompanies] = useState<Company[]>([]);
    const [isLoading, setIsLoading] = useState(true);

    const { rafflesService, companiesService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    let { id } = useParams();
    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadRaffle = async () => {
        const result = await rafflesService.findById(id);
        setRaffle(result.data);
        
        const companiesResult = await companiesService.findAll();
        setAllCompanies(companiesResult.data!);
        
        setIsLoading(false);
    };

    useEffect(() => {
        loadRaffle().then();
    }, []);

    const submitEdit = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await rafflesService.update(id, raffle!, userContext);
        router.push('/raffles');
    };

    return (
        <>
            <h1>Edit</h1>

            <h4>Raffle</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        {
                            (isLoading)
                                ? <p>Loading...</p>
                                : <>
                                    {renderRaffle(raffle!, setRaffle, allCompanies)}
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
                <Link href={'/raffles'}>Back to List</Link>
            </div>
        </>
    );
}

function renderRaffle(raffle: Raffle, setRaffle: (raffle: Raffle) => void, allCompanies: Company[]) {
    const updateInput = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setRaffle({ ...raffle!, [e.target.name]: e.target.value });
    };

    return (
        <>
            <div className="form-group">
                <label className="control-label" htmlFor="raffleName">Field</label>
                <input className="form-control valid" id="raffleName" type="text" name="raffleName"
                       value={raffle.raffleName} onChange={updateInput} />
                <span className="text-danger field-validation-valid"></span>
            </div>
            <div className="form-group">
                <label className="control-label" htmlFor="companyId">Company</label>
                <select className="form-control" name="companyId" value={raffle.companyId} onChange={updateInput}>
                    <option value="">Select</option>
                    {allCompanies.map((company, index) => (
                        <option key={index} value={company.id}>{company.companyName}</option>
                    ))}
                </select>
            </div>
            <div className="form-group">
                <label className="control-label" htmlFor="startDate">Start Date</label>
                <input className="form-control valid" id="startDate" type="text" name="startDate"
                       value={raffle.startDate} onChange={updateInput} />
                <span className="text-danger field-validation-valid"></span>
            </div>
            <div className="form-group">
                <label className="control-label" htmlFor="endDate">End Date</label>
                <input className="form-control valid" id="endDate" type="text" name="endDate" value={raffle.endDate}
                       onChange={updateInput} />
                <span className="text-danger field-validation-valid"></span>
            </div>
            <div className="form-group">
                <label className="control-label" htmlFor="allowAnonymousUsers">Allow Anonymous Users</label>
                <input className="form-control valid" id="allowAnonymousUsers" type="checkbox"
                       name="allowAnonymousUsers" checked={raffle.allowAnonymousUsers} onChange={updateInput} />
                <span className="text-danger field-validation-valid"></span>
            </div>
            <div className="form-group">
                <label className="control-label" htmlFor="visibleToPublic">Visible To Public</label>
                <input className="form-control valid" id="visibleToPublic" type="checkbox" name="visibleToPublic"
                       checked={raffle.visibleToPublic} onChange={updateInput} />
                <span className="text-danger field-validation-valid"></span>
            </div>
        </>
    );
}
