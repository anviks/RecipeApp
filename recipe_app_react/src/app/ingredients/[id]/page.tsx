'use client';

import { useParams } from 'next/navigation';
import { useIngredientsService } from '@/components/ServiceContext';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { Ingredient, Optional } from '@/types';

export default function Details() {
    const [ingredient, setIngredient] = useState<Ingredient>();
    const [isLoading, setIsLoading] = useState(true);
    
    let { id } = useParams();
    const ingredientsService = useIngredientsService();
    if (typeof id !== 'string') {
        id = id.join('');
    }
    
    const loadIngredient = async () => {
        const result = await ingredientsService.findById(id);
        setIngredient(result.data);
        setIsLoading(false);
    }

    useEffect(() => {
        loadIngredient().then();
    }, []);

    return (
        <>
            <h1>Details</h1>
            <div>
                <h4>Ingredient</h4>
                <hr />

                {(isLoading)
                    ? <p>Loading...</p>
                    : renderIngredient(ingredient!)}
            </div>
            <div>
                <Link href={`/ingredients/${id}/edit`}>Edit</Link>
                |
                <Link href={'/ingredients'}>Back to List</Link>
            </div>
        </>
    );
}

function renderIngredient(ingredient: Ingredient) {
    return (
        <dl className="row">
            <dt className="col-sm-2">
                Name
            </dt>
            <dd className="col-sm-10">
                {ingredient.name}
            </dd>
        </dl>
    );
}
