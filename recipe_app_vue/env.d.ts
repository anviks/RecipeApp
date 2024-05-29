/// <reference types="vite/client" />

interface ImportMetaEnv {
    readonly VITE_BASE_URL: string;
    // add other environment variables here if needed
}

interface ImportMeta {
    readonly env: ImportMetaEnv;
}
