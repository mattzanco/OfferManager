import { Configuration, LogLevel } from '@azure/msal-browser';

export const authEnabled = import.meta.env.VITE_AUTH_ENABLED === 'true';

const tenantId = import.meta.env.VITE_AZURE_TENANT_ID ?? '';
const clientId = import.meta.env.VITE_AZURE_CLIENT_ID ?? '';
const apiScope = import.meta.env.VITE_AZURE_API_SCOPE ?? '';

if (authEnabled && (!tenantId || !clientId || !apiScope)) {
  throw new Error(
    'VITE_AUTH_ENABLED is true but VITE_AZURE_TENANT_ID, VITE_AZURE_CLIENT_ID, or VITE_AZURE_API_SCOPE is missing.',
  );
}

export const msalConfig: Configuration = {
  auth: {
    clientId,
    authority: `https://login.microsoftonline.com/${tenantId}`,
    redirectUri: window.location.origin,
    postLogoutRedirectUri: window.location.origin,
  },
  cache: {
    cacheLocation: 'sessionStorage',
  },
  system: {
    loggerOptions: {
      logLevel: LogLevel.Warning,
    },
  },
};

export const loginRequest = {
  scopes: [apiScope],
};

export const apiTokenRequest = {
  scopes: [apiScope],
};
