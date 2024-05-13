'use client';

import Link from 'next/link';
import React, { ChangeEvent, useEffect, useState } from 'react';
import { Ingredient, IngredientType, IngredientTypeAssociation } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import {
    useIngredientsService,
    useIngredientTypeAssociationsService,
    useIngredientTypesService
} from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Edit() {
    const [ingredients, setIngredients] = useState<Ingredient[]>([]);
    const [ingredientTypes, setIngredientTypes] = useState<IngredientType[]>([]);
    const [ingredientTypeAssociation, setIngredientTypeAssociation] = useState<IngredientTypeAssociation>();
    const [isLoading, setIsLoading] = useState(true);

    const ingredientsService = useIngredientsService();
    const ingredientTypesService = useIngredientTypesService();
    const ingredientTypeAssociationsService = useIngredientTypeAssociationsService();

    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    let { id } = useParams();
    if (typeof id !== 'string') {
        id = id.join('');
    }

    const loadIngredientTypeAssociation = async () => {
        const result = await ingredientTypeAssociationsService.findById(id);
        let association = result.data;
        if (association) {
            const ingredientsResult = await ingredientsService.findAll();
            const ingredientTypesResult = await ingredientTypesService.findAll();
            setIngredients(ingredientsResult.data!);
            setIngredientTypes(ingredientTypesResult.data!);
            setIngredientTypeAssociation(association);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        loadIngredientTypeAssociation().then();
    }, []);

    const submitEdit = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await ingredientTypeAssociationsService.update(id, ingredientTypeAssociation!, userContext);
        router.push('/ingredient-type-associations');
    };

    const updateInput = (e: ChangeEvent<HTMLSelectElement>) => {
        setIngredientTypeAssociation({ ...ingredientTypeAssociation!, [e.target.name]: e.target.value });
    };

    return (
        <>
            <h1>Edit</h1>

            <h4>IngredientTypeAssociation</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        {(isLoading)
                            ? <p>Loading...</p>
                            : <>
                                {renderIngredientSelect(ingredients, ingredientTypeAssociation!.ingredientId, updateInput)}
                                {renderIngredientTypeSelect(ingredientTypes, ingredientTypeAssociation!.ingredientTypeId, updateInput)}
                                <div className="form-group">
                                    <button onClick={submitEdit} type="submit" className="btn btn-primary">Save</button>
                                </div>
                            </>
                        }
                    </form>
                </div>
            </div>

            <div>
                <Link href={'/ingredient-type-associations'}>Back to List</Link>
            </div>
        </>
    );
}

function renderIngredientSelect(ingredients: Ingredient[], selectedIngredientId: string, updateInput: (e: ChangeEvent<HTMLSelectElement>) => void) {
    return <div className="form-group">
        <label className="control-label" htmlFor="IngredientId">Ingredient</label>
        <select name="ingredientId" className="form-control" onChange={updateInput} value={selectedIngredientId}>
            {ingredients.map((ingredient, i) => <option key={i} value={ingredient.id}>{ingredient.name}</option>)}
        </select>
        <span className="text-danger field-validation-valid"></span>
    </div>;
}

function renderIngredientTypeSelect(ingredientTypes: IngredientType[], selectedIngredientTypeId: string, updateInput: (e: ChangeEvent<HTMLSelectElement>) => void) {
    return <div className="form-group">
        <label className="control-label" htmlFor="IngredientTypeId">IngredientType</label>
        <select name="ingredientTypeId" className="form-control" onChange={updateInput}
                value={selectedIngredientTypeId}>
            {ingredientTypes.map((ingredientType, i) => <option key={i}
                                                                value={ingredientType.id}>{ingredientType.name}</option>)}
        </select>
        <span className="text-danger field-validation-valid"></span>
    </div>;
}
