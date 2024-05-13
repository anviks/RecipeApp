'use client';

import Link from 'next/link';
import { MouseEvent, useEffect, useState } from 'react';
import { IngredientType } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useIngredientTypesService } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Delete() {
    const [ingredientType, setIngredientType] = useState<IngredientType>();
    const [isLoading, setIsLoading] = useState(true);

    const router = useRouter();
    const { userContext, setUserContext } = useUserContext();

    let { id } = useParams();
    const ingredientTypesService = useIngredientTypesService();
    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadIngredientType = async () => {
        const result = await ingredientTypesService.findById(id);
        setIngredientType(result.data);
        setIsLoading(false);
    };

    useEffect(() => {
        loadIngredientType().then();
    }, []);

    const submitDelete = async (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await ingredientTypesService.delete(id, userContext);
        router.push('/ingredient-types');
    };

    return (
        <>
            <h1>Delete</h1>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>IngredientType</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderIngredientType(ingredientType!, submitDelete)}
            </div>
        </>
    );
}

function renderIngredientType(ingredientType: IngredientType, submitDelete: (e: MouseEvent<HTMLButtonElement>) => void) {
    return <>
        <dl className="row">
            <dt className="col-sm-2">
                Name
            </dt>
            <dd className="col-sm-10">
                {ingredientType!.name}
            </dd>
        </dl>
        <dl className="row">
            <dt className="col-sm-2">
                Description
            </dt>
            <dd className="col-sm-10">
                {ingredientType!.description}
            </dd>
        </dl>
        <form method="post">
            <button onClick={submitDelete} type="submit" className="btn btn-danger">Delete</button>
            |
            <Link href={'/ingredient-types'}>Back to List</Link>
        </form>
    </>
}
