'use client';

import Link from 'next/link';
import React, { ChangeEvent, useEffect, useState } from 'react';
import { ActivityType } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Edit() {
    const [activityType, setActivityType] = useState<ActivityType>();
    const [isLoading, setIsLoading] = useState(true);

    const { activityTypesService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    let { id } = useParams();
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

    const submitEdit = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await activityTypesService.update(id, activityType!, userContext);
        router.push('/activity-types');
    };

    return (
        <>
            <h1>Edit</h1>

            <h4>ActivityType</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        {
                            (isLoading)
                                ? <p>Loading...</p>
                                : <>
                                    {renderActivityType(activityType!, setActivityType)}
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
                <Link href={'/activity-types'}>Back to List</Link>
            </div>
        </>
    );
}

function renderActivityType(activityType: ActivityType, setActivityType: (activityType: ActivityType) => void) {
    const updateInput = (e: ChangeEvent<HTMLInputElement>) => {
        setActivityType({ ...activityType!, [e.target.name]: e.target.value });
    };

    return (
        <div className="form-group">
            <label className="control-label" htmlFor="activityTypeName">Activity type name</label>
            <input name="activityTypeName" className="form-control valid" id="activityTypeName" type="text"
                   value={activityType.activityTypeName} onChange={updateInput} />
            <span className="text-danger field-validation-valid"></span>
        </div>
    );
}
