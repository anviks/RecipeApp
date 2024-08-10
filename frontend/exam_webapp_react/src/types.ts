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

export interface ActivityType {
    id?: string;
    activityTypeName: string;
}

export interface Activity {
    id?: string;
    durationInMinutes: number;
    date: string;
    userId: string;
    activityTypeId: string;
}

export interface Company {
    id?: string;
    companyName: string;
}

export interface Prize {
    id?: string;
    prizeName: string;
    raffleResultId?: string;
    raffleId: string;
}

export interface Raffle {
    id?: string;
    raffleName: string;
    visibleToPublic: boolean;
    allowAnonymousUsers: boolean;
    startDate: string;
    endDate: string;
    companyId: string;
}

export interface RaffleResult {
    id?: string;
    raffleId: string;
    userId?: string;
    anonymousUserName: string;
}

export interface Ticket {
    id?: string;
    userId: string;
    raffleId: string;
}

export interface AppUser {
    id?: string;
    username: string;
    email: string;
    companyId: string;
}
