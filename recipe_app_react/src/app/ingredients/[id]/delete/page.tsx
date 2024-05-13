'use client';

import Link from 'next/link';
import React, { useEffect, useState } from 'react';
import { Ingredient } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useIngredientsService } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Delete() {
    const [ingredient, setIngredient] = useState<Ingredient>();
    const [isLoading, setIsLoading] = useState(true);

    const router = useRouter();
    const { userContext, setUserContext } = useUserContext();

    let { id } = useParams();
    const ingredientsService = useIngredientsService();
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

    const submitDelete = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await ingredientsService.delete(id, userContext);
        router.push('/ingredients');
    };

    return (
        <>
            <h1>Delete</h1>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Ingredient</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderIngredient(ingredient!, submitDelete)}
            </div>
        </>
    );
}

function renderIngredient(ingredient: Ingredient, submitDelete: (e: React.MouseEvent<HTMLButtonElement>) => void) {
    return <>
        <dl className="row">
            <dt className="col-sm-2">
                Name
            </dt>
            <dd className="col-sm-10">
                {ingredient!.name}
            </dd>
        </dl>
        <form method="post">
            <button onClick={submitDelete} type="submit" className="btn btn-danger">Delete</button>
            |
            <Link href={'/ingredients'}>Back to List</Link>
        </form>
    </>
}
