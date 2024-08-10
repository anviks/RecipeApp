'use client';

import Link from 'next/link';
import React, { ChangeEvent, useEffect, useState } from 'react';
import { Activity, ActivityType, AppUser } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Edit() {
    const [activity, setActivity] = useState<Activity>();
    const [allUsers, setAllUsers] = useState<AppUser[]>([]);
    const [allActivityTypes, setAllActivityTypes] = useState<ActivityType[]>([]);
    const [isLoading, setIsLoading] = useState(true);

    const { activitiesService, usersService, activityTypesService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    let { id } = useParams();
    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadActivity = async () => {
        const result = await activitiesService.findById(id);
        setActivity(result.data);

        const allUsersResult = await usersService.findAll();
        setAllUsers(allUsersResult.data!);

        const allActivityTypesResult = await activityTypesService.findAll();
        setAllActivityTypes(allActivityTypesResult.data!);

        setIsLoading(false);
    };

    useEffect(() => {
        loadActivity().then();
    }, []);

    const submitEdit = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await activitiesService.update(id, activity!, userContext);
        router.push('/activities');
    };

    return (
        <>
            <h1>Edit</h1>

            <h4>Activity</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        {
                            (isLoading)
                                ? <p>Loading...</p>
                                : <>
                                    {renderActivity(activity!, setActivity, allUsers, allActivityTypes)}
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
                <Link href={'/activities'}>Back to List</Link>
            </div>
        </>
    );
}

function renderActivity(activity: Activity, setActivity: (activity: Activity) => void, allUsers: AppUser[], allActivityTypes: ActivityType[]) {
    const updateInput = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setActivity({ ...activity!, [e.target.name]: e.target.value });
    };

    return (
        <>
            <div className="form-group">
                <label className="control-label" htmlFor="durationInMinutes">Duration (minutes)</label>
                <input className="form-control valid" id="durationInMinutes" type="number" name="durationInMinutes"
                       value={activity.durationInMinutes} onChange={updateInput} />
                <span className="text-danger field-validation-valid"></span>
            </div>
            <div className="form-group">
                <label className="control-label" htmlFor="date">Date</label>
                <input className="form-control valid" id="date" type="date" name="date"
                       value={activity.date} onChange={updateInput} />
                <span className="text-danger field-validation-valid"></span>
            </div>
            <div className="form-group">
                <label className="control-label" htmlFor="user">User</label>
                <select className="form-control" id="user" name="userId"
                        value={activity.userId} onChange={updateInput}>
                    <option value="" disabled hidden>-- Choose --</option>
                    {allUsers.map((user) => {
                        return <option key={user.id}
                                       value={user.id}>{user.username}</option>;
                    })}
                </select>
                <span className="text-danger field-validation-valid"></span>
            </div>
            <div className="form-group">
                <label className="control-label" htmlFor="activityType">Activity Type</label>
                <select className="form-control" id="activityType" name="activityTypeId"
                        value={activity.activityTypeId} onChange={updateInput}>
                    <option value="" disabled hidden>-- Choose --</option>
                    {allActivityTypes.map((activityType) => {
                        return <option key={activityType.id}
                                       value={activityType.id}>{activityType.activityTypeName}</option>;
                    })}
                </select>
                <span className="text-danger field-validation-valid"></span>
            </div>
        </>
    );
}
