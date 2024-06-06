'use client';

import { useParams } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { IngredientType } from '@/types';

export default function Details() {
    const [ingredientType, setIngredientType] = useState<IngredientType>();
    const [isLoading, setIsLoading] = useState(true);

    let { id } = useParams();
    const { ingredientTypesService } = useServices();
    
    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadIngredientType = async () => {
        const result = await ingredientTypesService.findById(id);
        setIngredientType(result.data);
        setIsLoading(false);
    }

    useEffect(() => {
        loadIngredientType().then();
    }, []);

    return (
        <>
            <h1>Details</h1>
            <div>
                <h4>IngredientType</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderIngredientType(ingredientType!)}
            </div>
            <div>
                <Link href={`/ingredient-types/${id}/edit`}>Edit</Link>
                |
                <Link href={'/ingredient-types'}>Back to List</Link>
            </div>
        </>
    );
}

function renderIngredientType(ingredientType: IngredientType) {
    return (
        <>
            <dl className="row">
                <dt className="col-sm-2">
                    Name
                </dt>
                <dd className="col-sm-10">
                    {ingredientType.name}
                </dd>
            </dl>
            <dl className="row">
                <dt className="col-sm-2">
                    Description
                </dt>
                <dd className="col-sm-10">
                    {ingredientType.description}
                </dd>
            </dl>
        </>
    );
}
