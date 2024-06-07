'use client';

import { ChangeEvent, MouseEvent, useEffect, useState } from 'react';
import { Company, Raffle } from '@/types';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';
import { useRouter } from 'next/navigation';
import Link from 'next/link';

export default function Create() {
    const [raffle, setRaffle] = useState<Raffle>({
        allowAnonymousUsers: false,
        companyId: '',
        endDate: '',
        raffleName: '',
        startDate: '',
        visibleToPublic: false
    });
    const [allCompanies, setAllCompanies] = useState<Company[]>([]);
    const [isLoading, setIsLoading] = useState(true);

    const { rafflesService, companiesService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    const loadRaffle = async () => {
        const allCompaniesResult = await companiesService.findAll();
        setAllCompanies(allCompaniesResult.data!);

        setIsLoading(false);
    };

    useEffect(() => {
        loadRaffle().then();
    }, []);

    const updateInput = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setRaffle({ ...raffle!, [e.target.name]: e.target.value });
    };
    
    const submitCreate = async (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await rafflesService.create(raffle, userContext);
        router.push('/raffles');
    };

    return (
        <>
            <h1>Create</h1>

            <h4>Raffle</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        <div className="form-group">
                            <label className="control-label" htmlFor="raffleName">Field</label>
                            <input className="form-control valid" id="raffleName" type="text" name="raffleName"
                                   value={raffle.raffleName} onChange={updateInput} />
                            <span className="text-danger field-validation-valid"></span>
                        </div>
                        <div className="form-group">
                            <label className="control-label" htmlFor="companyId">Company</label>
                            <select className="form-control" name="companyId" value={raffle.companyId}
                                    onChange={updateInput}>
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
                            <input className="form-control valid" id="endDate" type="text" name="endDate"
                                   value={raffle.endDate} onChange={updateInput} />
                            <span className="text-danger field-validation-valid"></span>
                        </div>
                        <div className="form-group">
                            <label className="control-label" htmlFor="allowAnonymousUsers">Allow Anonymous Users</label>
                            <input className="form-check-input" id="allowAnonymousUsers" type="checkbox"
                                   name="allowAnonymousUsers" checked={raffle.allowAnonymousUsers}
                                   onChange={updateInput} />
                            <span className="text-danger field-validation-valid"></span>
                        </div>
                        <div className="form-group">
                            <label className="control-label" htmlFor="visibleToPublic">Visible To Public</label>
                            <input className="form-check-input" id="visibleToPublic" type="checkbox"
                                   name="visibleToPublic" checked={raffle.visibleToPublic} onChange={updateInput} />
                            <span className="text-danger field-validation-valid"></span>
                        </div>
                        <div className="form-group">
                            <button onClick={submitCreate} type="submit" className="btn btn-primary">Create</button>
                        </div>
                    </form>
                </div>
            </div>

            <div>
                <Link href={'/raffles'}>Back to List</Link>
            </div>
        </>
    );
}