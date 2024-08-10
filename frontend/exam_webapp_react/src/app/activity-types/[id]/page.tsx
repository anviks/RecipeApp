'use client';

import { useParams } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { ActivityType, Optional } from '@/types';

export default function Details() {
    const [activityType, setActivityType] = useState<ActivityType>();
    const [isLoading, setIsLoading] = useState(true);

    let { id } = useParams();
    const { activityTypesService } = useServices();

    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadActivityType = async () => {
        const result = await activityTypesService.findById(id);
        setActivityType(result.data);
        setIsLoading(false);
    }

    useEffect(() => {
        loadActivityType().then();
    }, []);

    return (
        <>
            <h1>Details</h1>
            <div>
                <h4>ActivityType</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderActivityType(activityType!)}
            </div>
            <div>
                <Link href={`/activityTypes/${id}/edit`}>Edit</Link>
                |
                <Link href={'/activity-types'}>Back to List</Link>
            </div>
        </>
    );
}

function renderActivityType(activityType: ActivityType) {
    return (
        <dl className="row">
            <dt className="col-sm-2">
                Activity type name
            </dt>
            <dd className="col-sm-10">
                {activityType.activityTypeName}
            </dd>
        </dl>
    );
}
