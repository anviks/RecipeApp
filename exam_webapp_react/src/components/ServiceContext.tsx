'use client';
import React, { createContext, useContext } from 'react';
import AccountService from '@/services/accountService';
import SamplesService from '@/services/samplesService';

const accountService = new AccountService();
const samplesService = new SamplesService();

export const ServiceContext = createContext({
    accountService,
    samplesService
});

export const ServiceProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
    return (
        <ServiceContext.Provider value={{ accountService, samplesService }}>
            {children}
        </ServiceContext.Provider>
    );
};

export const useServices = () => useContext(ServiceContext);
