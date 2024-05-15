export type Optional<T> = T | null | undefined | never;

export interface ErrorResponse {
    status?: number;
    statusText?: string;
    message: string;
}

export interface ResultObject<TResponseData> {
    errors?: ErrorResponse[];
    data?: TResponseData;
}

export interface LoginData {
    usernameOrEmail: string;
    password: string;
}

export interface RegisterData {
    username: string;
    email: string;
    password: string;
    confirmPassword: string;
}

export interface UserInfo {
    jsonWebToken: string;
    refreshToken: string;
    username: string;
    email: string;
}

export interface User {
    id?: string;
    username: string;
}

export interface Category {
    id?: string;
    name: string;
    description?: string;
}

export interface Ingredient {
    id?: string;
    name: string;
    ingredientTypeAssociations?: IngredientTypeAssociation[];
}

export interface IngredientType {
    id?: string;
    name: string;
    description: string;
}

export interface IngredientTypeAssociation {
    id?: string;
    ingredientId: string;
    ingredientTypeId: string;
}

export interface Unit {
    id?: string;
    name: string;
    abbreviation: string;
    unitMultiplier: number;
    ingredientTypeId: string;
    ingredientType?: IngredientType;
}

export interface Recipe {
    id?: string;
    title: string;
    description: string;
    imageFileUrl: string;
    instructions: string[];
    preparationTime: number;
    cookingTime: number;
    servings: number;
    isVegetarian: boolean;
    isVegan: boolean;
    isGlutenFree: boolean;
    createdAt: Date;
    authorUser: User;
    updatedAt: Date;
    updatingUser: User;
}

export interface Review {
    id?: string;
    rating: number;
    comment: string;
    createdAt?: Date;
    recipeId: string;
    recipe?: Recipe;
    user?: User;
}
