'use client';

import { ChangeEvent, MouseEvent, useState } from 'react';
import { IngredientType } from '@/types';
import { useIngredientTypesService } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';
import { useRouter } from 'next/navigation';
import Link from 'next/link';

export default function Create() {
    const [ingredientType, setIngredientType] = useState<IngredientType>({ name: '', description: '' });

    const ingredientTypesService = useIngredientTypesService();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    const updateInput = (e: ChangeEvent<HTMLInputElement>) => {
        setIngredientType({...ingredientType!, [e.target.name]: e.target.value});
    }

    const submitCreate = async (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await ingredientTypesService.create(ingredientType, userContext);
        router.push('/ingredient-types');
    };

    return (
        <>
            <h1>Create</h1>

            <h4>IngredientType</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        <div className="form-group">
                            <label className="control-label" htmlFor="Name">Name</label>
                            <input className="form-control valid" type="text" name="name" value={ingredientType.name} onChange={updateInput} />
                            <span className="text-danger field-validation-valid"></span>
                        </div>
                        <div className="form-group">
                            <label className="control-label" htmlFor="Description">Description</label>
                            <input className="form-control valid" type="text" name="description" value={ingredientType.description} onChange={updateInput} />
                            <span className="text-danger field-validation-valid"></span>
                        </div>
                        <div className="form-group">
                            <button onClick={submitCreate} type="submit" className="btn btn-primary">Create</button>
                        </div>
                    </form>
                </div>
            </div>

            <div>
                <Link href={'/ingredient-types'}>Back to List</Link>
            </div>
        </>
    );
}