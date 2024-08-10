'use client';

import { useEffect, useState } from 'react';
import { Ingredient } from '@/types';
import { useServices } from '@/components/ServiceContext';
import Link from 'next/link';

export default function Ingredients() {
    const [isLoading, setIsLoading] = useState(true);
    const [ingredients, setIngredients] = useState<Ingredient[]>([]);
    const { ingredientsService } = useServices();

    const loadIngredients = async () => {
        const allIngredients = await ingredientsService.findAll();
        if (allIngredients.data) {
            setIngredients(allIngredients.data);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        loadIngredients().then();
    }, []);
    
    return (
        <>
            <h1>Index</h1>

            <p>
                <Link href={'/ingredients/create'}>Create New</Link>
            </p>
            {(isLoading)
                ? <p>Loading...</p>
                : renderIngredientsTable(ingredients)}
        </>
    );
}

function renderIngredientsTable(ingredients: Ingredient[]) {
    return <table className="table">
        <thead>
        <tr>
            <th>
                Name
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        {renderIngredients(ingredients)}
        </tbody>
    </table>;
}

function renderIngredients(ingredients: Ingredient[]) {
    return ingredients.map(ingredient => (
        <tr key={ingredient.id}>
            <td>
                {ingredient.name}
            </td>
            <td>
                <Link href={`/ingredients/${ingredient.id}/edit`}>Edit</Link> |
                <Link href={`/ingredients/${ingredient.id}`}>Details</Link> |
                <Link href={`/ingredients/${ingredient.id}/delete`}>Delete</Link>
            </td>
        </tr>
    ));
}