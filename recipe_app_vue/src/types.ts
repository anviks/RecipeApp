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

export interface UserInfo {
    jsonWebToken: string;
    refreshToken: string;
    username: string;
    email: string;
}

export interface Ingredient {
    id?: string;
    name: string;
}

export interface IngredientType {
    id?: string;
    name: string;
    description: string;
}
