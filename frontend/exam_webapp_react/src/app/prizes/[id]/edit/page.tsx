'use client';

import Link from 'next/link';
import React, { ChangeEvent, useEffect, useState } from 'react';
import { Prize, Raffle, RaffleResult } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Edit() {
    const [prize, setPrize] = useState<Prize>();
    const [allRaffles, setAllRaffles] = useState<Raffle[]>([]);
    const [allRaffleResults, setAllRaffleResults] = useState<RaffleResult[]>();
    const [isLoading, setIsLoading] = useState(true);

    const { prizesService, rafflesService, raffleResultsService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    let { id } = useParams();
    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadPrize = async () => {
        const result = await prizesService.findById(id);
        setPrize(result.data);
        
        const allRafflesResult = await rafflesService.findAll();
        setAllRaffles(allRafflesResult.data!);
        
        const allRaffleResultsResult = await raffleResultsService.findAll();
        setAllRaffleResults(allRaffleResultsResult.data!);
        
        setIsLoading(false);
    };

    useEffect(() => {
        loadPrize().then();
    }, []);

    const submitEdit = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await prizesService.update(id, prize!, userContext);
        router.push('/prizes');
    };

    return (
        <>
            <h1>Edit</h1>

            <h4>Prize</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        {
                            (isLoading)
                                ? <p>Loading...</p>
                                : <>
                                    {renderPrize(prize!, setPrize, allRaffles, allRaffleResults!)}
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
                <Link href={'/prizes'}>Back to List</Link>
            </div>
        </>
    );
}

function renderPrize(prize: Prize, setPrize: (prize: Prize) => void, allRaffles: Raffle[], allRaffleResults: RaffleResult[]) {
    const updateInput = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setPrize({ ...prize!, [e.target.name]: e.target.value });
    };

    return (
        <>
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
        </>
    )
}
