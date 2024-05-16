import type { Recipe, RecipeRequest } from '@/types';


function mapObject<TSource, TDestination, TCommon extends keyof TSource & keyof TDestination>(source: TSource, keys: TCommon[]): TDestination {
    return keys.reduce((result, key) => {
        if (Object.prototype.hasOwnProperty.call(source, key)) {
            result[key] = source[key];
        }
        return result;
    }, {} as Pick<TSource, TCommon>) as TDestination;
}

type RecipeIntersection = keyof Recipe & keyof RecipeRequest;

const recipeKeysToMap: (RecipeIntersection)[] =
    ['title', 'description', 'instructions', 'preparationTime', 'cookingTime', 'servings', 'isVegetarian', 'isVegan', 'isGlutenFree', 'recipeIngredients'];

export function recipeResponseToRequest(recipeResponse: Recipe): RecipeRequest {
    const recipeRequest = mapObject<Recipe, RecipeRequest, RecipeIntersection>(recipeResponse, recipeKeysToMap);
    recipeRequest.categoryIds = recipeResponse.recipeCategories?.map(category => category.categoryId);
    return recipeRequest;
}

