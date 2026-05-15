import { InteractionStatus } from '@azure/msal-browser';
import { useIsAuthenticated, useMsal } from '@azure/msal-react';
import { ReactNode, useEffect, createElement } from 'react';
import { loginRequest } from './authConfig';

interface ProtectedRouteProps {
  children: ReactNode;
}

export function ProtectedRoute({ children }: ProtectedRouteProps) {
  const { instance, inProgress } = useMsal();
  const isAuthenticated = useIsAuthenticated();

  useEffect(() => {
    if (!isAuthenticated && inProgress === InteractionStatus.None) {
      instance.loginRedirect(loginRequest);
    }
  }, [instance, isAuthenticated, inProgress]);

  if (inProgress !== InteractionStatus.None) {
    return createElement('div', { className: 'loading' }, 'Signing you in...');
  }

  if (!isAuthenticated) {
    return null;
  }

  return <>{children}</>;
}
