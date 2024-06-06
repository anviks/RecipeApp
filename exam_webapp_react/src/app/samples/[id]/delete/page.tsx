'use client';

import Link from 'next/link';
import React, { useEffect, useState } from 'react';
import { Sample } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Delete() {
    const [sample, setSample] = useState<Sample>();
    const [isLoading, setIsLoading] = useState(true);

    const router = useRouter();
    const { userContext, setUserContext } = useUserContext();

    let { id } = useParams();
    const { samplesService } = useServices();

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

    const submitDelete = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await samplesService.delete(id, userContext);
        router.push('/samples');
    };

    return (
        <>
            <h1>Delete</h1>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Sample</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderSample(sample!, submitDelete)}
            </div>
        </>
    );
}

function renderSample(sample: Sample, submitDelete: (e: React.MouseEvent<HTMLButtonElement>) => void) {
    return <>
        <dl className="row">
            <dt className="col-sm-2">
                Field
            </dt>
            <dd className="col-sm-10">
                {sample!.field}
            </dd>
        </dl>
        <form method="post">
            <button onClick={submitDelete} type="submit" className="btn btn-danger">Delete</button>
            |
            <Link href={'/samples'}>Back to List</Link>
        </form>
    </>
}
