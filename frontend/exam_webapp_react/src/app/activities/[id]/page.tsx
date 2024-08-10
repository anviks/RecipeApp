'use client';

import { useParams } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { Activity, ActivityType, AppUser, Optional } from '@/types';

export default function Details() {
    const [activity, setActivity] = useState<Activity>();
    const [activityType, setActivityType] = useState<ActivityType>();
    const [user, setUser] = useState<AppUser>();
    const [isLoading, setIsLoading] = useState(true);

    let { id } = useParams();
    const { activitiesService, activityTypeService, usersService } = useServices();

    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadActivity = async () => {
        const result = await activitiesService.findById(id);
        setActivity(result.data);
        
        const activityTypeResult = await activityTypeService.findById(activity!.activityTypeId!);
        setActivityType(activityTypeResult.data);
        
        const userResult = await usersService.findById(activity!.userId!);
        setUser(userResult.data);
        
        setIsLoading(false);
    }

    useEffect(() => {
        loadActivity().then();
    }, []);

    return (
        <>
            <h1>Details</h1>
            <div>
                <h4>Activity</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderActivity(activity!, activityType!, user!)}
            </div>
            <div>
                <Link href={`/activities/${id}/edit`}>Edit</Link>
                |
                <Link href={'/activities'}>Back to List</Link>
            </div>
        </>
    );
}

function renderActivity(activity: Activity, activityType: ActivityType, user: AppUser) {
    return (
        <dl className="row">
            <dt className="col-sm-2">
                Duration (minutes)
            </dt>
            <dd className="col-sm-10">
                {activity.durationInMinutes}
            </dd>
            <dt className="col-sm-2">
                Date
            </dt>
            <dd className="col-sm-10">
                {activity.date}
            </dd>
            <dt className="col-sm-2">
                Activity Type
            </dt>
            <dd className="col-sm-10">
                {activityType.activityTypeName}
            </dd>
            <dt className="col-sm-2">
                User
            </dt>
            <dd className="col-sm-10">
                {user.username}
            </dd>
        </dl>
    );
}
