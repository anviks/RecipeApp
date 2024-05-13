'use client';
import React, { createContext, useContext } from 'react';
import IngredientsService from '@/services/ingredientsService';
import AccountService from '@/services/accountService';

const accountService = new AccountService();
const ingredientsService = new IngredientsService();

export const AccountContext = createContext(accountService);
export const IngredientsServiceContext = createContext(ingredientsService);

export const ServiceProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
    return (
        <AccountContext.Provider value={accountService}>
            <IngredientsServiceContext.Provider value={ingredientsService}>
                {children}
            </IngredientsServiceContext.Provider>
        </AccountContext.Provider>
    );
};

export const useIngredientsService = () => useContext(IngredientsServiceContext);
export const useAccountService = () => useContext(AccountContext);
