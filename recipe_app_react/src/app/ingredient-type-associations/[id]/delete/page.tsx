'use client';

import Link from 'next/link';
import { MouseEvent, useEffect, useState } from 'react';
import { IngredientTypeAssociation } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import {
    useIngredientsService,
    useIngredientTypeAssociationsService,
    useIngredientTypesService
} from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Delete() {
    const [ingredientTypeAssociation, setIngredientTypeAssociation] = useState<IngredientTypeAssociation>();
    const [isLoading, setIsLoading] = useState(true);

    const ingredientsService = useIngredientsService();
    const ingredientTypesService = useIngredientTypesService();
    const ingredientTypeAssociationsService = useIngredientTypeAssociationsService();

    const router = useRouter();
    const { userContext, setUserContext } = useUserContext();

    let { id } = useParams();
    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadIngredientTypeAssociation = async () => {
        const result = await ingredientTypeAssociationsService.findById(id);
        let association = result.data;
        if (association) {
            const ingredient = await ingredientsService.findById(association.ingredientId);
            const ingredientType = await ingredientTypesService.findById(association.ingredientTypeId);
            association.ingredient = ingredient.data;
            association.ingredientType = ingredientType.data;
            setIngredientTypeAssociation(association);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        loadIngredientTypeAssociation().then();
    }, []);

    const submitDelete = async (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await ingredientTypeAssociationsService.delete(id, userContext);
        router.push('/ingredient-type-associations');
    };

    return (
        <>
            <h1>Delete</h1>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>IngredientTypeAssociation</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderIngredientTypeAssociation(ingredientTypeAssociation!, submitDelete)}
            </div>
        </>
    );
}

function renderIngredientTypeAssociation(ingredientTypeAssociation: IngredientTypeAssociation, submitDelete: (e: MouseEvent<HTMLButtonElement>) => void) {
    return <>
        <dl className="row">
            <dt className="col-sm-2">
                Ingredient
            </dt>
            <dd className="col-sm-10">
                {ingredientTypeAssociation.ingredient!.name}
            </dd>
        </dl>
        <dl className="row">
            <dt className="col-sm-2">
                IngredientType
            </dt>
            <dd className="col-sm-10">
                {ingredientTypeAssociation.ingredientType!.name}
            </dd>
        </dl>
        <form method="post">
            <button onClick={submitDelete} type="submit" className="btn btn-danger">Delete</button>
            |
            <Link href={'/ingredient-type-associations'}>Back to List</Link>
        </form>
    </>
}
