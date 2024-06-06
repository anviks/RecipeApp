'use client';

import Link from 'next/link';
import React, { ChangeEvent, useEffect, useState } from 'react';
import { Sample } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Edit() {
    const [sample, setSample] = useState<Sample>();
    const [isLoading, setIsLoading] = useState(true);

    const { samplesService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    let { id } = useParams();
    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadSample = async () => {
        const result = await samplesService.findById(id);
        setSample(result.data);
        setIsLoading(false);
    };

    useEffect(() => {
        loadSample().then();
    }, []);

    const submitEdit = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await samplesService.update(id, sample!, userContext);
        router.push('/samples');
    };

    return (
        <>
            <h1>Edit</h1>

            <h4>Sample</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        {
                            (isLoading)
                                ? <p>Loading...</p>
                                : <>
                                    {renderSample(sample!, setSample)}
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
                <Link href={'/samples'}>Back to List</Link>
            </div>
        </>
    );
}

function renderSample(sample: Sample, setSample: (sample: Sample) => void) {
    const updateInput = (e: ChangeEvent<HTMLInputElement>) => {
        setSample({ ...sample!, [e.target.name]: e.target.value });
    };

    return <div className="form-group">
        <label className="control-label" htmlFor="field">Field</label>
        <input name="field" className="form-control valid" id="field" type="text" value={sample.field} onChange={updateInput} />
        <span className="text-danger field-validation-valid"></span>
    </div>;
}
