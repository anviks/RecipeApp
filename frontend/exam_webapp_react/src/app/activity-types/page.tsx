'use client';

import { useEffect, useState } from 'react';
import { useServices } from '@/components/ServiceContext';
import { ActivityType } from '@/types';
import Link from 'next/link';

export default function ActivityTypes() {
    const [isLoading, setIsLoading] = useState(true);
    const [activityTypes, setActivityTypes] = useState<ActivityType[]>([]);
    const { activityTypesService } = useServices();

    const loadActivityTypes = async () => {
        const allActivityTypes = await activityTypesService.findAll();
        if (allActivityTypes.data) {
            setActivityTypes(allActivityTypes.data);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        loadActivityTypes().then();
    }, []);
    
    return (
        <>
            <h1>Index</h1>

            <p>
                <Link href={'/activityTypes/create'}>Create New</Link>
            </p>
            {(isLoading)
                ? <p>Loading...</p>
                : renderActivityTypesTable(activityTypes)}
        </>
    );
}

function renderActivityTypesTable(activityTypes: ActivityType[]) {
    return <table className="table">
        <thead>
        <tr>
            <th>
                Activity Type Name
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        {renderActivityTypes(activityTypes)}
        </tbody>
    </table>;
}

function renderActivityTypes(activityTypes: ActivityType[]) {
    return activityTypes.map(activityType => (
        <tr key={activityType.id}>
            <td>
                {activityType.activityTypeName}
            </td>
            <td>
                <Link href={`/activityTypes/${activityType.id}/edit`}>Edit</Link> |
                <Link href={`/activityTypes/${activityType.id}`}>Details</Link> |
                <Link href={`/activityTypes/${activityType.id}/delete`}>Delete</Link>
            </td>
        </tr>
    ));
}
