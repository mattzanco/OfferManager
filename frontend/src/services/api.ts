import axios, { AxiosInstance } from 'axios';
import { authEnabled } from '../auth/authConfig';

// Empty base URL uses Vite dev proxy (/api → local WebApi). Production builds set VITE_API_BASE_URL in CI.
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? '';
const API_KEY = import.meta.env.VITE_API_KEY;

let accessTokenProvider: (() => Promise<string | null>) | null = null;

export function setAccessTokenProvider(provider: () => Promise<string | null>) {
  accessTokenProvider = provider;
}

if (!authEnabled && !API_KEY) {
  throw new Error(
    'Missing VITE_API_KEY. Set it in your environment, or enable auth with VITE_AUTH_ENABLED=true.',
  );
}

const apiClient: AxiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

if (API_KEY) {
  apiClient.defaults.headers.common['Ocp-Apim-Subscription-Key'] = API_KEY;
}

apiClient.interceptors.request.use(async (config) => {
  if (accessTokenProvider) {
    const token = await accessTokenProvider();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
  }
  return config;
});

export default apiClient;
