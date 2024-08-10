'use client';

import { ChangeEvent, MouseEvent, useState } from 'react';
import { AppUser, Raffle, RaffleResult } from '@/types';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';
import { useRouter } from 'next/navigation';
import Link from 'next/link';

export default function Create() {
    const [raffleResult, setRaffleResult] = useState<RaffleResult>({ anonymousUserName: '', raffleId: '' });
    const [allRaffles, setAllRaffles] = useState<Raffle[]>([]);
    const [allUsers, setAllUsers] = useState<AppUser[]>([]);
    const [isLoading, setIsLoading] = useState(true);

    const { raffleResultsService, rafflesService, usersService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    const loadRaffleResult = async () => {
        const allRafflesResult = await rafflesService.findAll();
        setAllRaffles(allRafflesResult.data!);

        const allUsersResult = await usersService.findAll();
        setAllUsers(allUsersResult.data!);

        setIsLoading(false);
    };

    const updateInput = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setRaffleResult({ ...raffleResult!, [e.target.name]: e.target.value });
    };

    const submitCreate = async (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await raffleResultsService.create(raffleResult, userContext);
        router.push('/raffle-results');
    };

    return (
        <>
            <h1>Create</h1>

            <h4>RaffleResult</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
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
                        <div className="form-group">
                            <button onClick={submitCreate} type="submit" className="btn btn-primary">Create</button>
                        </div>
                    </form>
                </div>
            </div>

            <div>
                <Link href={'/raffle-results'}>Back to List</Link>
            </div>
        </>
    );
}