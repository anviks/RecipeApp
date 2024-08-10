'use client';

import { useEffect, useState } from 'react';
import { useServices } from '@/components/ServiceContext';
import { Activity } from '@/types';
import Link from 'next/link';

export default function Activities() {
    const [isLoading, setIsLoading] = useState(true);
    const [activities, setActivities] = useState<Activity[]>([]);
    const { activitiesService } = useServices();

    const loadActivities = async () => {
        const allActivities = await activitiesService.findAll();
        if (allActivities.data) {
            setActivities(allActivities.data);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        loadActivities().then();
    }, []);
    
    return (
        <>
            <h1>Index</h1>

            <p>
                <Link href={'/activities/create'}>Create New</Link>
            </p>
            {(isLoading)
                ? <p>Loading...</p>
                : renderActivitiesTable(activities)}
        </>
    );
}

function renderActivitiesTable(activities: Activity[]) {
    return <table className="table">
        <thead>
        <tr>
            <th>
                Duration (minutes)
            </th>
            <th>
                Date
            </th>
            <th>
                Activity Type
            </th>
            <th>
                User
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        {renderActivities(activities)}
        </tbody>
    </table>;
}

function renderActivities(activities: Activity[]) {
    return activities.map(activity => (
        <tr key={activity.id}>
            <td>
                {activity.durationInMinutes}
            </td>
            <td>
                {activity.date}
            </td>
            <td>
                {activity.activityTypeId}
            </td>
            <td>
                {activity.userId}
            </td>
            <td>
                <Link href={`/activities/${activity.id}/edit`}>Edit</Link> |
                <Link href={`/activities/${activity.id}`}>Details</Link> |
                <Link href={`/activities/${activity.id}/delete`}>Delete</Link>
            </td>
        </tr>
    ));
}
