/// <reference types="vite/client" />

interface ImportMetaEnv {
    readonly BASE_URL: string;
    readonly VITE_BACKEND_URL: string;
    readonly VITE_BACKEND_API_VERSION: string;
}

interface ImportMeta {
    readonly env: ImportMetaEnv;
}
  