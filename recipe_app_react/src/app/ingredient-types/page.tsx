'use client';

import { useEffect, useState } from 'react';
import { IngredientType } from '@/types';
import { useServices } from '@/components/ServiceContext';
import Link from 'next/link';

export default function IngredientTypes() {
    const [isLoading, setIsLoading] = useState(true);
    const [ingredientTypes, setIngredientTypes] = useState<IngredientType[]>([]);
    const { ingredientTypesService } = useServices();

    const loadIngredientTypes = async () => {
        const allIngredientTypes = await ingredientTypesService.findAll();
        if (allIngredientTypes.data) {
            setIngredientTypes(allIngredientTypes.data);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        loadIngredientTypes().then();
    }, []);

    return (
        <>
            <h1>Index</h1>

            <p>
                <Link href={'/ingredient-types/create'}>Create New</Link>
            </p>
            {(isLoading)
                ? <p>Loading...</p>
                : renderIngredientTypesTable(ingredientTypes)}
        </>
    );
}

function renderIngredientTypesTable(ingredientTypes: IngredientType[]) {
    return <table className="table">
        <thead>
        <tr>
            <th>
                Name
            </th>
            <th>
                Description
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        {renderIngredientTypes(ingredientTypes)}
        </tbody>
    </table>;
}

function renderIngredientTypes(ingredientTypes: IngredientType[]) {
    return ingredientTypes.map(type => (
        <tr key={type.id}>
            <td>
                {type.name}
            </td>
            <td>
                {type.description}
            </td>
            <td>
                <Link href={`/ingredient-types/${type.id}/edit`}>Edit</Link> |
                <Link href={`/ingredient-types/${type.id}`}>Details</Link> |
                <Link href={`/ingredient-types/${type.id}/delete`}>Delete</Link>
            </td>
        </tr>
    ));
}