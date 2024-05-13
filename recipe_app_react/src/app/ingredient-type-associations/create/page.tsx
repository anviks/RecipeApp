'use client';

import { ChangeEvent, MouseEvent, useEffect, useState } from 'react';
import { Ingredient, IngredientType, IngredientTypeAssociation } from '@/types';
import {
    useIngredientsService,
    useIngredientTypeAssociationsService,
    useIngredientTypesService
} from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';
import { useRouter } from 'next/navigation';
import Link from 'next/link';

export default function Create() {
    const [ingredientTypeAssociation, setIngredientTypeAssociation] = useState<IngredientTypeAssociation>({
        ingredientId: '',
        ingredientTypeId: ''
    });
    const [ingredients, setIngredients] = useState<Ingredient[]>([]);
    const [ingredientTypes, setIngredientTypes] = useState<IngredientType[]>([]);
    const [isLoading, setIsLoading] = useState(true);

    const { userContext, setUserContext } = useUserContext();

    const ingredientsService = useIngredientsService();
    const ingredientTypesService = useIngredientTypesService();
    const ingredientTypeAssociationsService = useIngredientTypeAssociationsService();

    const router = useRouter();

    useEffect(() => {
        const loadIngredients = async () => {
            const result = await ingredientsService.findAll();
            setIngredients(result.data!);
        };

        const loadIngredientTypes = async () => {
            const result = await ingredientTypesService.findAll();
            setIngredientTypes(result.data!);
        };

        loadIngredients().then();
        loadIngredientTypes().then();
        
        setIsLoading(false);
    }, []);

    const updateInput = (e: ChangeEvent<HTMLSelectElement>) => {
        setIngredientTypeAssociation({ ...ingredientTypeAssociation!, [e.target.name]: e.target.value });
    };

    const submitCreate = async (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await ingredientTypeAssociationsService.create(ingredientTypeAssociation, userContext);
        router.push('/ingredient-type-associations');
    };

    return (
        <>
            <h1>Create</h1>

            <h4>IngredientTypeAssociation</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        {(isLoading)
                            ? <p>Loading...</p>
                            : <>
                                {renderIngredientSelect(ingredients, updateInput)}
                                {renderIngredientTypeSelect(ingredientTypes, updateInput)}
                                <div className="form-group">
                                    <button onClick={submitCreate} type="submit" className="btn btn-primary">Create
                                    </button>
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

function renderIngredientSelect(ingredients: Ingredient[], updateInput: (e: ChangeEvent<HTMLSelectElement>) => void) {
    return <div className="form-group">
        <label className="control-label" htmlFor="IngredientId">Ingredient</label>
        <select name="ingredientId" className="form-control" onChange={updateInput}>
            {ingredients.map((ingredient, i) => <option key={i} value={ingredient.id}>{ingredient.name}</option>)}
        </select>
        <span className="text-danger field-validation-valid"></span>
    </div>;
}

function renderIngredientTypeSelect(ingredientTypes: IngredientType[], updateInput: (e: ChangeEvent<HTMLSelectElement>) => void) {
    return <div className="form-group">
        <label className="control-label" htmlFor="IngredientTypeId">IngredientType</label>
        <select name="ingredientTypeId" className="form-control" onChange={updateInput}>
            {ingredientTypes.map((ingredientType, i) => <option key={i}
                                                                value={ingredientType.id}>{ingredientType.name}</option>)}
        </select>
        <span className="text-danger field-validation-valid"></span>
    </div>;
}
