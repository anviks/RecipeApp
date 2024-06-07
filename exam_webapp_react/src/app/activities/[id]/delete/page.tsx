'use client';

import Link from 'next/link';
import React, { useEffect, useState } from 'react';
import { Activity, ActivityType, AppUser } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Delete() {
    const [activity, setActivity] = useState<Activity>();
    const [activityType, setActivityType] = useState<ActivityType>();
    const [user, setUser] = useState<AppUser>();
    const [isLoading, setIsLoading] = useState(true);

    const router = useRouter();
    const { userContext, setUserContext } = useUserContext();

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
    };

    useEffect(() => {
        loadActivity().then();
    }, []);

    const submitDelete = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await activitiesService.delete(id, userContext);
        router.push('/activities');
    };

    return (
        <>
            <h1>Delete</h1>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Activity</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderActivity(activity!, submitDelete, activityType!, user!)}
            </div>
        </>
    );
}

function renderActivity(activity: Activity, submitDelete: (e: React.MouseEvent<HTMLButtonElement>) => void, activityType: ActivityType, user: AppUser) {
    return <>
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
        <form method="post">
            <button onClick={submitDelete} type="submit" className="btn btn-danger">Delete</button>
            |
            <Link href={'/activities'}>Back to List</Link>
        </form>
    </>
}
