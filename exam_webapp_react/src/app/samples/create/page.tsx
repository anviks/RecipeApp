'use client';

import { ChangeEvent, MouseEvent, useState } from 'react';
import { Sample } from '@/types';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';
import { useRouter } from 'next/navigation';
import Link from 'next/link';

export default function Create() {
    const [sample, setSample] = useState<Sample>({ field: '' });

    const { samplesService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    const updateInput = (e: ChangeEvent<HTMLInputElement>) => {
        setSample({...sample!, [e.target.name]: e.target.value});
    }

    const submitCreate = async (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await samplesService.create(sample, userContext);
        router.push('/samples');
    };

    return (
        <>
            <h1>Create</h1>

            <h4>Sample</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        <div className="form-group">
                            <label className="control-label" htmlFor="field">Field</label>
                            <input className="form-control valid" id="field" type="text" name="field" value={sample.field} onChange={updateInput} />
                            <span className="text-danger field-validation-valid"></span>
                        </div>
                        <div className="form-group">
                            <button onClick={submitCreate} type="submit" className="btn btn-primary">Create</button>
                        </div>
                    </form>
                </div>
            </div>

            <div>
                <Link href={'/samples'}>Back to List</Link>
            </div>
        </>
    );
}