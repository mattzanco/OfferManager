import { useEffect } from 'react';
import { setAccessTokenProvider } from '../services/api';
import { authEnabled } from './authConfig';
import { useApiAccessToken } from './useApiAccessToken';

/** Wires MSAL token acquisition into the shared Axios client. */
export function ApiTokenBridge({ children }: { children: React.ReactNode }) {
  const getAccessToken = useApiAccessToken();

  useEffect(() => {
    if (authEnabled) {
      setAccessTokenProvider(getAccessToken);
    }
  }, [getAccessToken]);

  return <>{children}</>;
}
