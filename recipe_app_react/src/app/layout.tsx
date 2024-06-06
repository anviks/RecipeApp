import type { Metadata } from 'next';
import { Inter } from 'next/font/google';
import 'bootstrap/dist/css/bootstrap.css';
import './globals.css';
import BootstrapActivation from '@/components/BootstrapActivation';
import Footer from '@/components/Footer';
import Header from '@/components/Header';
import AppState from '@/components/AppState';
import { ServiceProvider } from '@/components/ServiceContext';

const inter = Inter({ subsets: ['latin'] });

export const metadata: Metadata = {
    title: 'Recipe App',
    description: 'Yet another recipe app'
};

export default function RootLayout({ children }: Readonly<{ children: React.ReactNode; }>) {
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
