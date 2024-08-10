'use client';

import { ChangeEvent, MouseEvent, useState } from 'react';
import { ActivityType } from '@/types';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';
import { useRouter } from 'next/navigation';
import Link from 'next/link';

export default function Create() {
    const [activityType, setActivityType] = useState<ActivityType>({ activityTypeName: '' });

    const { activityTypesService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    const updateInput = (e: ChangeEvent<HTMLInputElement>) => {
        setActivityType({...activityType!, [e.target.name]: e.target.value});
    }

    const submitCreate = async (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await activityTypesService.create(activityType, userContext);
        router.push('/activity-types');
    };

    return (
        <>
            <h1>Create</h1>

            <h4>ActivityType</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        <div className="form-group">
                            <label className="control-label" htmlFor="activityTypeName">Field</label>
                            <input className="form-control valid" id="activityTypeName" type="text" name="activityTypeName" value={activityType.activityTypeName} onChange={updateInput} />
                            <span className="text-danger field-validation-valid"></span>
                        </div>
                        <div className="form-group">
                            <button onClick={submitCreate} type="submit" className="btn btn-primary">Create</button>
                        </div>
                    </form>
                </div>
            </div>

            <div>
                <Link href={'/activity-types'}>Back to List</Link>
            </div>
        </>
    );
}