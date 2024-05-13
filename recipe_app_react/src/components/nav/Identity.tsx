'use client';
import Link from 'next/link';
import { UserContext } from '@/types';
import { useUserContext } from '@/components/AppState';
import { useAccountService } from '@/components/ServiceContext';

export default function Identity() {
    const { userContext, setUserContext } = useUserContext();
    return !userContext.isAuthenticated() ? <LoggedOut /> : <LoggedIn context={userContext} setContext={setUserContext} />;
}

const LoggedIn = ({ context, setContext }: { context: UserContext, setContext: (userContext: UserContext) => void }) => {
    const accountService = useAccountService();
    
    const logOut = () => {
        accountService.logout(context, setContext).then();
    };

    return (
        <>
            <li className="nav-item">
                <Link href={'/'} className="nav-link text-dark" title="Manage">Hello, {context.username}</Link>
            </li>
            <li className="nav-item">
                <Link onClick={logOut} href={'/'} className="nav-link text-dark" title="Logout">Logout</Link>
            </li>
        </>
    );
};

const LoggedOut = () => {
    return (
        <>
            <li className="nav-item">
                <Link href={'/register'} className="nav-link text-dark">Register</Link>
            </li>
            <li className="nav-item">
                <Link href={'/login'} className="nav-link text-dark">Login</Link>
            </li>
        </>
    );
};
