'use client';
import React, { ChangeEvent, useContext, useState } from 'react';
import { useRouter } from 'next/navigation';
import { ErrorResponse, LoginData } from '@/types';
import { useUserContext } from '@/components/AppState';
import { AccountContext, useAccountService } from '@/components/ServiceContext';


export default function Login() {
    const router = useRouter();
    const [validationError, setValidationError] = useState<ErrorResponse>({ message: '' });
    const [formData, setFormData] = useState<LoginData>({
        usernameOrEmail: '',
        password: ''
    });
    const { userContext, setUserContext } = useUserContext();
    const accountService = useAccountService();
    
    const updateInput = (e: ChangeEvent<HTMLInputElement>) => {
        setFormData({...formData, [e.target.name]: e.target.value});
        setValidationError({ message: '' });
    }
    
    const login = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        
        if (formData.usernameOrEmail.length < 5 || formData.password.length < 6) {
            setValidationError({ message: 'Invalid input lengths' });
            return;
        }

        const response = await accountService.login(formData);
        if (response.data) {
            setUserContext({...userContext, jsonWebToken: response.data.jsonWebToken, refreshToken: response.data.refreshToken});
            router.push("/");
        }

        if (response.errors && response.errors.length > 0) {
            setValidationError(response.errors[0]);
        }
    }
    
    return (
        <div className="row">
            <div className="col-md-5">
                <h2>Log in</h2>
                <hr/>
                <div className="text-danger" role="alert">{validationError.message}</div>
                <div className="form-floating mb-3">
                    <input id="usernameOrEmail" type="username" className="form-control" autoComplete="username"
                           name="usernameOrEmail"
                           value={formData.usernameOrEmail} placeholder="name@example.com" onChange={updateInput}/>
                    <label htmlFor="usernameOrEmail" className="form-label">Username or email</label>
                </div>
                <div className="form-floating mb-3">
                    <input id="password" type="password" className="form-control" autoComplete="password"
                           name="password"
                           value={formData.password} placeholder="password" onChange={updateInput}/>
                    <label htmlFor="password" className="form-label">Password</label>
                </div>
                <div>
                    <button className="w-100 btn btn-lg btn-primary" onClick={login} type="submit">Log in</button>
                </div>
            </div>
        </div>
    );
}