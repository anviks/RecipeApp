'use client';

import Link from 'next/link';
import React, { useEffect, useState } from 'react';
import { ActivityType } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Delete() {
    const [activityType, setActivityType] = useState<ActivityType>();
    const [isLoading, setIsLoading] = useState(true);

    const router = useRouter();
    const { userContext, setUserContext } = useUserContext();

    let { id } = useParams();
    const { activityTypesService } = useServices();

    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadActivityType = async () => {
        const result = await activityTypesService.findById(id);
        setActivityType(result.data);
        setIsLoading(false);
    };

    useEffect(() => {
        loadActivityType().then();
    }, []);

    const submitDelete = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await activityTypesService.delete(id, userContext);
        router.push('/activity-types');
    };

    return (
        <>
            <h1>Delete</h1>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>ActivityType</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderActivityType(activityType!, submitDelete)}
            </div>
        </>
    );
}

function renderActivityType(activityType: ActivityType, submitDelete: (e: React.MouseEvent<HTMLButtonElement>) => void) {
    return <>
        <dl className="row">
            <dt className="col-sm-2">
                Activity type name
            </dt>
            <dd className="col-sm-10">
                {activityType!.activityTypeName}
            </dd>
        </dl>
        <form method="post">
            <button onClick={submitDelete} type="submit" className="btn btn-danger">Delete</button>
            |
            <Link href={'/activity-types'}>Back to List</Link>
        </form>
    </>
}
