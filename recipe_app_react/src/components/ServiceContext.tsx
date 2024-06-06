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

export const ServiceContext = createContext({
    accountService,
    ingredientsService,
    ingredientTypesService,
    ingredientTypeAssociationsService
});

export const ServiceProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
    return (
        <ServiceContext.Provider
            value={{ accountService, ingredientsService, ingredientTypesService, ingredientTypeAssociationsService }}>
            {children}
        </ServiceContext.Provider>
    );
};

export const useServices = () => useContext(ServiceContext);
