'use client';
import React, { createContext, useContext } from 'react';
import IngredientsService from '@/services/ingredientsService';
import AccountService from '@/services/accountService';
import IngredientTypesService from '@/services/ingredientTypesService';
import IngredientTypeAssociationsService from '@/services/ingredientTypeAssociationsService';

const accountService = new AccountService();
const ingredientsService = new IngredientsService();
const ingredientTypesService = new IngredientTypesService();
const ingredientTypeAssociationsService = new IngredientTypeAssociationsService();

export const AccountContext = createContext(accountService);
export const IngredientsServiceContext = createContext(ingredientsService);
export const IngredientTypesServiceContext = createContext(ingredientTypesService);
export const IngredientTypeAssociationsServiceContext = createContext(ingredientTypeAssociationsService);

export const ServiceProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
    return (
        <AccountContext.Provider value={accountService}>
            <IngredientsServiceContext.Provider value={ingredientsService}>
                <IngredientTypesServiceContext.Provider value={ingredientTypesService}>
                    <IngredientTypeAssociationsServiceContext.Provider value={ingredientTypeAssociationsService}>
                        {children}
                    </IngredientTypeAssociationsServiceContext.Provider>
                </IngredientTypesServiceContext.Provider>
            </IngredientsServiceContext.Provider>
        </AccountContext.Provider>
    );
};

export const useAccountService = () => useContext(AccountContext);
export const useIngredientsService = () => useContext(IngredientsServiceContext);
export const useIngredientTypesService = () => useContext(IngredientTypesServiceContext);
export const useIngredientTypeAssociationsService = () => useContext(IngredientTypeAssociationsServiceContext);
