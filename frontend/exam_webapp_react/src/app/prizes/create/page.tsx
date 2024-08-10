'use client';

import { ChangeEvent, MouseEvent, useEffect, useState } from 'react';
import { Prize, Raffle, RaffleResult } from '@/types';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';
import { useRouter } from 'next/navigation';
import Link from 'next/link';

export default function Create() {
    const [prize, setPrize] = useState<Prize>({ prizeName: '', raffleId: '' });
    const [allRaffles, setAllRaffles] = useState<Raffle[]>([]);
    const [allRaffleResults, setAllRaffleResults] = useState<RaffleResult[]>();
    const [isLoading, setIsLoading] = useState(true);

    const { prizesService, rafflesService, raffleResultsService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    const loadRaffles = async () => {
        const result = await rafflesService.findAll();
        setAllRaffles(result.data!);

        const raffleResults = await raffleResultsService.findAll();
        setAllRaffleResults(raffleResults.data);
        
        setIsLoading(false);
    };

    useEffect(() => {
        loadRaffles().then();
    }, []);

    const updateInput = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setPrize({ ...prize!, [e.target.name]: e.target.value });
    };

    const submitCreate = async (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await prizesService.create(prize, userContext);
        router.push('/prizes');
    };

    return (
        <>
            <h1>Create</h1>

            <h4>Prize</h4>
            <hr />
            <div className="row">
                {isLoading ? <p>Loading...</p> :
                    <div className="col-md-4">
                        <form method="post">
                            <div className="form-group">
                                <label className="control-label" htmlFor="prizeName">Field</label>
                                <input className="form-control valid" id="prizeName" type="text" name="prizeName"
                                       value={prize.prizeName} onChange={updateInput} />
                                <span className="text-danger field-validation-valid"></span>
                            </div>
                            <div className="form-group">
                                <label className="control-label" htmlFor="raffleId">Raffle</label>
                                <select className="form-control" id="raffleId" name="raffleId" value={prize.raffleId}
                                        onChange={updateInput}>
                                    <option value="">Select a raffle</option>
                                    {
                                        allRaffles.map((raffle) => {
                                            return <option key={raffle.id}
                                                           value={raffle.id}>{raffle.raffleName}</option>;
                                        })
                                    }
                                </select>
                            </div>
                            <div className="form-group">
                                <label className="control-label" htmlFor="raffleResultId">Raffle Result</label>
                                <select className="form-control" id="raffleResultId" name="raffleResultId"
                                        value={prize.raffleResultId} onChange={updateInput}>
                                    <option value="">Select a raffle result</option>
                                    {
                                        allRaffleResults?.map((raffleResult) => {
                                            return <option key={raffleResult.id}
                                                           value={raffleResult.id}>{raffleResult.id}</option>;
                                        })
                                    }
                                </select>
                            </div>
                            <div className="form-group">
                                <button onClick={submitCreate} type="submit" className="btn btn-primary">Create</button>
                            </div>
                        </form>
                    </div>
                }
            </div>

            <div>
                <Link href={'/prizes'}>Back to List</Link>
            </div>
        </>
    );
}