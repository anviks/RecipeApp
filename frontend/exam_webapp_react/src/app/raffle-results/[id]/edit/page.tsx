'use client';

import Link from 'next/link';
import React, { ChangeEvent, useEffect, useState } from 'react';
import { AppUser, Raffle, RaffleResult } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Edit() {
    const [raffleResult, setRaffleResult] = useState<RaffleResult>();
    const [allRaffles, setAllRaffles] = useState<Raffle[]>([]);
    const [allUsers, setAllUsers] = useState<AppUser[]>([]);
    const [isLoading, setIsLoading] = useState(true);

    const { raffleResultsService, rafflesService, usersService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    let { id } = useParams();
    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadRaffleResult = async () => {
        const result = await raffleResultsService.findById(id);
        setRaffleResult(result.data);

        const allRafflesResult = await rafflesService.findAll();
        setAllRaffles(allRafflesResult.data!);

        const allUsersResult = await usersService.findAll();
        setAllUsers(allUsersResult.data!);
        
        setIsLoading(false);
    };

    useEffect(() => {
        loadRaffleResult().then();
    }, []);

    const submitEdit = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await raffleResultsService.update(id, raffleResult!, userContext);
        router.push('/raffle-results');
    };

    return (
        <>
            <h1>Edit</h1>

            <h4>RaffleResult</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        {
                            (isLoading)
                                ? <p>Loading...</p>
                                : <>
                                    {renderRaffleResult(raffleResult!, setRaffleResult, allRaffles, allUsers)}
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
                <Link href={'/raffle-results'}>Back to List</Link>
            </div>
        </>
    );
}

function renderRaffleResult(raffleResult: RaffleResult, setRaffleResult: (raffleResult: RaffleResult) => void, allRaffles: Raffle[], allUsers: AppUser[]) {
    const updateInput = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setRaffleResult({ ...raffleResult!, [e.target.name]: e.target.value });
    };

    return (
        <>
            <div className="form-group">
                <label className="control-label" htmlFor="raffleId">Raffle</label>
                <select className="form-control" name="raffleId" value={raffleResult.raffleId}
                        onChange={updateInput}>
                    <option value="">Select</option>
                    {allRaffles.map((raffle, index) => (
                        <option key={index} value={raffle.id}>{raffle.raffleName}</option>
                    ))}
                </select>
            </div>
            <div className="form-group">
                <label className="control-label" htmlFor="userId">User</label>
                <select className="form-control" name="userId" value={raffleResult.userId}
                        onChange={updateInput}>
                    <option value="">Select</option>
                    {allUsers.map((user, index) => (
                        <option key={index} value={user.id}>{user.username}</option>
                    ))}
                </select>
            </div>
            <div className="form-group">
                <label className="control-label" htmlFor="anonymousUserName">Field</label>
                <input className="form-control valid" id="anonymousUserName" type="text"
                       name="anonymousUserName" value={raffleResult.anonymousUserName}
                       onChange={updateInput} />
                <span className="text-danger field-validation-valid"></span>
            </div>
        </>
    );
}
