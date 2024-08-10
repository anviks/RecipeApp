'use client';

import Link from 'next/link';
import { ChangeEvent, MouseEvent, useEffect, useState } from 'react';
import { IngredientType } from '@/types';
import { useParams, useRouter } from 'next/navigation';
import { useServices } from '@/components/ServiceContext';
import { useUserContext } from '@/components/AppState';

export default function Edit() {
    const [ingredientType, setIngredientType] = useState<IngredientType>();
    const [isLoading, setIsLoading] = useState(true);

    const { ingredientTypesService } = useServices();
    const { userContext, setUserContext } = useUserContext();

    const router = useRouter();

    let { id } = useParams();
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

    const submitEdit = async (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await ingredientTypesService.update(id, ingredientType!, userContext);
        router.push('/ingredient-types');
    };

    return (
        <>
            <h1>Edit</h1>

            <h4>IngredientType</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <form method="post">
                        {(isLoading)
                            ? <p>Loading...</p>
                            : renderIngredientType(ingredientType!, setIngredientType)}
                        <div className="form-group">
                            <button onClick={submitEdit} type="submit" className="btn btn-primary">Save</button>
                        </div>
                    </form>
                </div>
            </div>

            <div>
                <Link href={'/ingredient-types'}>Back to List</Link>
            </div>
        </>
    );
}

function renderIngredientType(ingredientType: IngredientType, setIngredientType: (ingredientType: IngredientType) => void) {
    const updateInput = (e: ChangeEvent<HTMLInputElement>) => {
        setIngredientType({...ingredientType!, [e.target.name]: e.target.value});
    }

    return <>
        <div className="form-group">
            <label className="control-label" htmlFor="Name">Name</label>
            <input name="name" className="form-control valid" type="text" value={ingredientType.name}
                   onChange={updateInput} />
            <span className="text-danger field-validation-valid"></span>
        </div>
        <div className="form-group">
            <label className="control-label" htmlFor="Description">Description</label>
            <input name="description" className="form-control valid" type="text" value={ingredientType.description}
                   onChange={updateInput} />
            <span className="text-danger field-validation-valid"></span>
        </div>
    </>;
}
