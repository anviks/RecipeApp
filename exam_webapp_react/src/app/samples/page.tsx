'use client';

import { useEffect, useState } from 'react';
import { useServices } from '@/components/ServiceContext';
import { Sample } from '@/types';
import Link from 'next/link';

export default function Samples() {
    const [isLoading, setIsLoading] = useState(true);
    const [samples, setSamples] = useState<Sample[]>([]);
    const { samplesService } = useServices();

    const loadSamples = async () => {
        const allSamples = await samplesService.findAll();
        if (allSamples.data) {
            setSamples(allSamples.data);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        loadSamples().then();
    }, []);
    
    return (
        <>
            <h1>Index</h1>

            <p>
                <Link href={'/samples/create'}>Create New</Link>
            </p>
            {(isLoading)
                ? <p>Loading...</p>
                : renderSamplesTable(samples)}
        </>
    );
}

function renderSamplesTable(samples: Sample[]) {
    return <table className="table">
        <thead>
        <tr>
            <th>
                Field
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        {renderSamples(samples)}
        </tbody>
    </table>;
}

function renderSamples(samples: Sample[]) {
    return samples.map(sample => (
        <tr key={sample.id}>
            <td>
                {sample.field}
            </td>
            <td>
                <Link href={`/samples/${sample.id}/edit`}>Edit</Link> |
                <Link href={`/samples/${sample.id}`}>Details</Link> |
                <Link href={`/samples/${sample.id}/delete`}>Delete</Link>
            </td>
        </tr>
    ));
}
