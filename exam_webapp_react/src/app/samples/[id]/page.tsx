'use client';

import { useParams } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { Sample, Optional } from '@/types';

export default function Details() {
    const [sample, setSample] = useState<Sample>();
    const [isLoading, setIsLoading] = useState(true);

    let { id } = useParams();
    const { samplesService } = useServices();

    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadSample = async () => {
        const result = await samplesService.findById(id);
        setSample(result.data);
        setIsLoading(false);
    }

    useEffect(() => {
        loadSample().then();
    }, []);

    return (
        <>
            <h1>Details</h1>
            <div>
                <h4>Sample</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderSample(sample!)}
            </div>
            <div>
                <Link href={`/samples/${id}/edit`}>Edit</Link>
                |
                <Link href={'/samples'}>Back to List</Link>
            </div>
        </>
    );
}

function renderSample(sample: Sample) {
    return (
        <dl className="row">
            <dt className="col-sm-2">
                Field
            </dt>
            <dd className="col-sm-10">
                {sample.field}
            </dd>
        </dl>
    );
}
