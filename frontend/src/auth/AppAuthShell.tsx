import { MsalProvider } from '@azure/msal-react';
import { createElement, ReactNode, useEffect, useState } from 'react';
import { ApiTokenBridge } from './ApiTokenBridge';
import { authEnabled } from './authConfig';
import { msalInstance } from './msalInstance';
import { ProtectedRoute } from './ProtectedRoute';

interface AppAuthShellProps {
  children: ReactNode;
}

export function AppAuthShell({ children }: AppAuthShellProps) {
  const [ready, setReady] = useState(!authEnabled);

  useEffect(() => {
    if (!authEnabled || !msalInstance) {
      return;
    }

    msalInstance
      .initialize()
      .then(() => msalInstance!.handleRedirectPromise())
      .then(() => setReady(true))
      .catch((err) => {
        console.error('MSAL initialization failed', err);
        setReady(true);
      });
  }, []);

  if (!authEnabled) {
    return <>{children}</>;
  }

  if (!msalInstance || !ready) {
    return createElement('div', { className: 'loading' }, 'Loading authentication...');
  }

  return (
    <MsalProvider instance={msalInstance}>
      <ApiTokenBridge>
        <ProtectedRoute>{children}</ProtectedRoute>
      </ApiTokenBridge>
    </MsalProvider>
  );
}
