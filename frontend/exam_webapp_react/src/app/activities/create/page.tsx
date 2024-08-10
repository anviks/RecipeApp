'use client';

import { ChangeEvent, MouseEvent, useEffect, useState } from 'react';
import { Activity, ActivityType, AppUser } from '@/types';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';
import { useRouter } from 'next/navigation';
import Link from 'next/link';

export default function Create() {
    const [activity, setActivity] = useState<Activity>({
        activityTypeId: '',
        date: '',
        durationInMinutes: 0,
        userId: ''
    });
    const [allUsers, setAllUsers] = useState<AppUser[]>([]);
    const [allActivityTypes, setAllActivityTypes] = useState<ActivityType[]>([]);
    const [isLoading, setIsLoading] = useState(true);

    const { activitiesService, usersService, activityTypesService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    const loadActivity = async () => {
        const allUsersResult = await usersService.findAll();
        setAllUsers(allUsersResult.data!);

        const allActivityTypesResult = await activityTypesService.findAll();
        setAllActivityTypes(allActivityTypesResult.data!);

        setIsLoading(false);
    };

    useEffect(() => {
        loadActivity().then();
        setIsLoading(false);
    }, []);

    const updateInput = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        console.log(e.target.name, e.target.value);
        setActivity({ ...activity!, [e.target.name]: e.target.value });
    };

    const submitCreate = async (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await activitiesService.create(activity, userContext);
        router.push('/activities');
    };

    return (
        <>
            <h1>Create</h1>

            <h4>Activity</h4>
            <hr />
            <div className="row">
                {isLoading ? <p>Loading...</p> :
                    <div className="col-md-4">
                        <form method="post">
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
                            <div className="form-group">
                                <button onClick={submitCreate} type="submit" className="btn btn-primary">Create</button>
                            </div>
                        </form>
                    </div>
                }
            </div>

            <div>
                <Link href={'/activities'}>Back to List</Link>
            </div>
        </>
    );
}