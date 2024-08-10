'use client';
import React, { createContext, useContext } from 'react';
import AccountService from '@/services/accountService';
import ActivitiesService from '@/services/activityService';
import ActivityTypesService from '@/services/activityTypeService';
import CompaniesService from '@/services/companyService';
import PrizesService from '@/services/prizeService';
import RaffleResultsService from '@/services/raffleResultService';
import RafflesService from '@/services/raffleService';
import TicketsService from '@/services/ticketService';
import UsersService from '@/services/userService';

const accountService = new AccountService();
const activitiesService = new ActivitiesService();
const activityTypesService = new ActivityTypesService();
const companiesService = new CompaniesService();
const prizesService = new PrizesService();
const raffleResultsService = new RaffleResultsService();
const rafflesService = new RafflesService();
const ticketsService = new TicketsService();
const usersService = new UsersService();

export const ServiceContext = createContext({
    accountService,
    activitiesService,
    activityTypesService,
    companiesService,
    prizesService,
    raffleResultsService,
    rafflesService,
    ticketsService,
    usersService
});

export const ServiceProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
    return (
        <ServiceContext.Provider value={{
            accountService,
            activitiesService,
            activityTypesService,
            companiesService,
            prizesService,
            raffleResultsService,
            rafflesService,
            ticketsService,
            usersService
        }}>
            {children}
        </ServiceContext.Provider>
    );
};

export const useServices = () => useContext(ServiceContext);
