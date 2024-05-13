'use client';

import { useParams } from 'next/navigation';
import {
    useIngredientsService,
    useIngredientTypeAssociationsService,
    useIngredientTypesService
} from '@/components/ServiceContext';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { IngredientTypeAssociation } from '@/types';

export default function Details() {
    const [ingredientTypeAssociation, setIngredientTypeAssociation] = useState<IngredientTypeAssociation>();
    const [isLoading, setIsLoading] = useState(true);
    
    const ingredientsService = useIngredientsService();
    const ingredientTypesService = useIngredientTypesService();
    const ingredientTypeAssociationsService = useIngredientTypeAssociationsService();
    
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
    }

    useEffect(() => {
        loadIngredientTypeAssociation().then();
    }, []);

    return (
        <>
            <h1>Details</h1>
            <div>
                <h4>IngredientTypeAssociation</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderIngredientTypeAssociation(ingredientTypeAssociation!)}
            </div>
            <div>
                <Link href={`/ingredient-type-associations/${id}/edit`}>Edit</Link>
                |
                <Link href={'/ingredient-type-associations'}>Back to List</Link>
            </div>
        </>
    );
}

function renderIngredientTypeAssociation(ingredientTypeAssociation: IngredientTypeAssociation) {
    return (
        <>
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
        </>
    );
}
