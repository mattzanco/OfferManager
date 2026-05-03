import axios, { AxiosInstance } from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://offermanager-dev-apim.azure-api.net';
const API_KEY = import.meta.env.VITE_API_KEY;

if (!API_KEY) {
  throw new Error('Missing VITE_API_KEY. Set it in your environment or CI secrets.');
}

const apiClient: AxiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
    'Ocp-Apim-Subscription-Key': API_KEY,
  },
});

export default apiClient;
