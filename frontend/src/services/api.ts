import axios, { AxiosInstance } from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://offermanager-dev-apim.azure-api.net/api';
const API_KEY = import.meta.env.VITE_API_KEY || 'ff7205ef2f474a79a56bfc3f7f3ad8b1';

const apiClient: AxiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
    'Ocp-Apim-Subscription-Key': API_KEY,
  },
});

export default apiClient;
