'use client';

import { createContext, useContext, useEffect, useState } from 'react';
import { parseJwt } from '@/helpers/jwtParser';
import { UserContext } from '@/types';
import IngredientsService from '@/services/ingredientsService';
import AccountService from '@/services/accountService';


const initialUserContext: UserContext = {
    jsonWebToken: '',
    refreshToken: '',
    username: '',
    email: '',
    expiresAt: 0,
    isAuthenticated: () => false
};

export const AuthContext = createContext<{
    userContext: UserContext,
    setUserContext: (userContext: UserContext) => void
}>({
    userContext: initialUserContext,
    setUserContext: () => {
    }
});

export default function AppState({ children }: Readonly<{ children: React.ReactNode }>) {
    const [userContext, setUserContext] = useState<UserContext>(initialUserContext);

    useEffect(() => {
        const details = parseJwt(userContext?.jsonWebToken ?? '');
        setUserContext({ ...userContext, ...details, isAuthenticated: () => !!userContext?.jsonWebToken });
    }, [userContext?.jsonWebToken]);

    return (
        <>
            <AuthContext.Provider value={{ userContext, setUserContext }}>
                {children}
            </AuthContext.Provider>
        </>
    );
}

export const useUserContext = () => useContext(AuthContext);
