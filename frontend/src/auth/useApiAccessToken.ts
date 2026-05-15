import { InteractionRequiredAuthError } from '@azure/msal-browser';
import { useMsal } from '@azure/msal-react';
import { useCallback } from 'react';
import { apiTokenRequest, authEnabled } from './authConfig';

export function useApiAccessToken(): () => Promise<string | null> {
  const { instance, accounts } = useMsal();

  return useCallback(async () => {
    if (!authEnabled || accounts.length === 0) {
      return null;
    }

    const account = accounts[0];
    try {
      const result = await instance.acquireTokenSilent({
        ...apiTokenRequest,
        account,
      });
      return result.accessToken;
    } catch (error) {
      if (error instanceof InteractionRequiredAuthError) {
        await instance.acquireTokenRedirect({
          ...apiTokenRequest,
          account,
        });
      }
      throw error;
    }
  }, [instance, accounts]);
}
