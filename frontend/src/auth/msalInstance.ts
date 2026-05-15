import { PublicClientApplication } from '@azure/msal-browser';
import { authEnabled, msalConfig } from './authConfig';

export const msalInstance = authEnabled ? new PublicClientApplication(msalConfig) : null;
