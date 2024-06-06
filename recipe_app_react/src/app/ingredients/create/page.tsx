'use client';

import { ChangeEvent, MouseEvent, useState } from 'react';
import { Ingredient } from '@/types';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';
import { useRouter } from 'next/navigation';
import Link from 'next/link';

export default function Create() {
    const [ingredient, setIngredient] = useState<Ingredient>({ name: '' });

    const { ingredientsService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    const updateInput = (e: ChangeEvent<HTMLInputElement>) => {
        setIngredient({...ingredient!, [e.target.name]: e.target.value});
    }

    const submitCreate = async (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await ingredientsService.create(ingredient, userContext);
        router.push('/ingredients');
    };

    return (
        <>
            <h1>Create</h1>

            <h4>Ingredient</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        <div className="form-group">
                            <label className="control-label" htmlFor="Name">Name</label>
                            <input className="form-control valid" type="text" name="name" value={ingredient.name} onChange={updateInput} />
                            <span className="text-danger field-validation-valid"></span>
                        </div>
                        <div className="form-group">
                            <button onClick={submitCreate} type="submit" className="btn btn-primary">Create</button>
                        </div>
                    </form>
                </div>
            </div>

            <div>
                <Link href={'/ingredients'}>Back to List</Link>
            </div>
        </>
    );
}