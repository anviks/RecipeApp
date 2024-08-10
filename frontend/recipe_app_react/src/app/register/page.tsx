'use client';

import Link from 'next/link';
import React, { ChangeEvent, useState } from 'react';
import { RegisterData } from '@/types';
import { useServices } from '@/components/ServiceContext';
import { useRouter } from 'next/navigation';
import { useUserContext } from '@/components/AppState';

export default function Register() {
    const [registerIsOngoing, setRegisterIsOngoing] = useState(false);
    const [errors, setErrors] = useState<string[]>([]);
    const [registerData, setRegisterData] = useState<RegisterData>({
        username: '',
        email: '',
        password: '',
        confirmPassword: ''
    });

    const { accountService } = useServices();
    const { userContext, setUserContext } = useUserContext();
    const router = useRouter();

    const updateInput = (e: ChangeEvent<HTMLInputElement>) => {
        setRegisterData({ ...registerData, [e.target.name]: e.target.value });
    }

    const submitRegister = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        
        setRegisterIsOngoing(true);
        const result = await accountService.register(registerData);
        setRegisterIsOngoing(false);
        if (result.errors) {
            setErrors(result.errors.map(e => e.message));
        } else {
            setErrors([]);
            setUserContext({ ...userContext, jsonWebToken: result.data!.jsonWebToken, refreshToken: result.data!.refreshToken });
            router.push('/');
        }
    };

    return (
        <>
            <div className="row">
                <div className="col-md-6">
                    <h1>Register as a user</h1>
                    <form method="post">
                        <h2>Create a new account.</h2>
                        <hr />
                        <div className="text-danger">{errors.join('\n')}</div>
                        <div className="form-floating mb-3">
                            <input className="form-control" placeholder="" type="text"
                                   id="Input_Username" maxLength={64}
                                   name="username" value={registerData.username} onChange={updateInput} />
                            <label htmlFor="Input_Username">Username</label>
                        </div>
                        <div className="form-floating mb-3">
                            <input className="form-control" placeholder="" type="email"
                                   id="Input_Email" name="email" value={registerData.email} onChange={updateInput} />
                            <label htmlFor="Input_Email">Email</label>
                        </div>
                        <div className="form-floating mb-3">
                            <input className="form-control" placeholder=""
                                   type="password" id="Input_Password" maxLength={100}
                                   name="password" value={registerData.password} onChange={updateInput} />
                            <label htmlFor="Input_Password">Password</label>
                        </div>
                        <div className="form-floating mb-3">
                            <input className="form-control" placeholder=""
                                   type="password" id="Input_ConfirmPassword" name="confirmPassword"
                                   value={registerData.confirmPassword} onChange={updateInput} />
                            <label htmlFor="Input_ConfirmPassword">Confirm password</label>
                        </div>
                        <button onClick={submitRegister} disabled={registerIsOngoing} id="registerSubmit" type="submit"
                                className="w-100 btn btn-lg btn-primary">
                            {renderButtonContent(registerIsOngoing)}
                        </button>
                        <p>Already have an account?&nbsp;
                            <Link href={'/login'}>Login</Link>
                        </p>
                    </form>
                </div>
            </div>
        </>
    );
}

function renderButtonContent(registerIsOngoing: boolean): React.ReactNode {
    if (registerIsOngoing) {
        return (
            <>
                <span className="spinner-border spinner-border-sm" role="status"></span>
                Loading...
            </>
        );
    } else {
        return <>Register</>;
    }
};
