'use client';

import { useEffect, useState } from 'react';
import { IngredientTypeAssociation } from '@/types';
import { useServices } from '@/components/ServiceContext';
import Link from 'next/link';

export default function IngredientTypeAssociations() {
    const [isLoading, setIsLoading] = useState(true);
    const [ingredientTypeAssociations, setIngredientTypeAssociations] = useState<IngredientTypeAssociation[]>([]);

    const { ingredientsService, ingredientTypesService, ingredientTypeAssociationsService } = useServices();

    const loadIngredientTypeAssociations = async () => {
        const allIngredientTypeAssociations = await ingredientTypeAssociationsService.findAll();
        if (allIngredientTypeAssociations.data) {
            for (const association of allIngredientTypeAssociations.data) {
                const ingredient = await ingredientsService.findById(association.ingredientId);
                const ingredientType = await ingredientTypesService.findById(association.ingredientTypeId);
                association.ingredient = ingredient.data;
                association.ingredientType = ingredientType.data;
            }
            setIngredientTypeAssociations(allIngredientTypeAssociations.data);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        loadIngredientTypeAssociations().then();
    }, []);
    
    return (
        <>
            <h1>Index</h1>

            <p>
                <Link href={'/ingredient-type-associations/create'}>Create New</Link>
            </p>
            {(isLoading)
                ? <p>Loading...</p>
                : renderIngredientTypeAssociationsTable(ingredientTypeAssociations)}
        </>
    );
}

function renderIngredientTypeAssociationsTable(ingredientTypeAssociations: IngredientTypeAssociation[]) {
    return <table className="table">
        <thead>
        <tr>
            <th>
                Ingredient
            </th>
            <th>
                IngredientType
            </th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        {renderIngredientTypeAssociations(ingredientTypeAssociations)}
        </tbody>
    </table>;
}

function renderIngredientTypeAssociations(ingredientTypeAssociations: IngredientTypeAssociation[]) {
    return ingredientTypeAssociations.map(ingredientTypeAssociation => (
        <tr key={ingredientTypeAssociation.id}>
            <td>
                {ingredientTypeAssociation.ingredient!.name}
            </td>
            <td>
                {ingredientTypeAssociation.ingredientType!.name}
            </td>
            <td>
                <Link href={`/ingredient-type-associations/${ingredientTypeAssociation.id}/edit`}>Edit</Link> |
                <Link href={`/ingredient-type-associations/${ingredientTypeAssociation.id}`}>Details</Link> |
                <Link href={`/ingredient-type-associations/${ingredientTypeAssociation.id}/delete`}>Delete</Link>
            </td>
        </tr>
    ));
}