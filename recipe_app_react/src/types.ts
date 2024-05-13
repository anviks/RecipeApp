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

// Field name must be the same as the according html input element's name attribute
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

export interface UserContext {
    jsonWebToken: string;
    refreshToken: string;
    username: string;
    email: string;
    expiresAt: number | null;
    isAuthenticated: () => boolean;
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
