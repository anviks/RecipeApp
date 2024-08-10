'use client';

import Link from 'next/link';
import React, { ChangeEvent, useEffect, useState } from 'react';
import { Ingredient } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Edit() {
    const [ingredient, setIngredient] = useState<Ingredient>();
    const [isLoading, setIsLoading] = useState(true);

    const { ingredientsService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    let { id } = useParams();
    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadIngredient = async () => {
        const result = await ingredientsService.findById(id);
        setIngredient(result.data);
        setIsLoading(false);
    };

    useEffect(() => {
        loadIngredient().then();
    }, []);

    const submitEdit = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await ingredientsService.update(id, ingredient!, userContext);
        router.push('/ingredients');
    };

    return (
        <>
            <h1>Edit</h1>

            <h4>Ingredient</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        {
                            (isLoading)
                                ? <p>Loading...</p>
                                : <>
                                    {renderIngredient(ingredient!, setIngredient)}
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
                <Link href={'/ingredients'}>Back to List</Link>
            </div>
        </>
    );
}

function renderIngredient(ingredient: Ingredient, setIngredient: (ingredient: Ingredient) => void) {
    const updateInput = (e: ChangeEvent<HTMLInputElement>) => {
        setIngredient({ ...ingredient!, [e.target.name]: e.target.value });
    };

    return <div className="form-group">
        <label className="control-label" htmlFor="Name">Name</label>
        <input name="name" className="form-control valid" type="text" value={ingredient.name} onChange={updateInput} />
        <span className="text-danger field-validation-valid"></span>
    </div>;
}
