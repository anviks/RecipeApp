import type { Metadata } from 'next';
import { Inter } from 'next/font/google';
import 'bootstrap/dist/css/bootstrap.css';
import './globals.css';
import BootstrapActivation from '@/components/BootstrapActivation';
import { ReactNode } from 'react';
import Footer from '@/components/Footer';
import Header from '@/components/Header';
import { ServiceProvider } from '@/components/ServiceContext';
import AppState from '@/components/AppState';

const inter = Inter({ subsets: ['latin'] });

export const metadata: Metadata = {
    title: 'Exam App',
    description: 'Some app done as an exam practical task'
};

export default function RootLayout({ children }: Readonly<{ children: ReactNode; }>) {
    return (
        <html lang="en">
        <body className={inter.className}>
        <AppState>
            <ServiceProvider>
                <Header />
                <div className="container">
                    <main role="main" className="pb-3">
                        {children}
                    </main>
                </div>
                <Footer />
            </ServiceProvider>
        </AppState>
        </body>
        <BootstrapActivation />
        </html>
    );
}
