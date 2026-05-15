import { useMsal } from '@azure/msal-react';
import './UserMenu.css';

export function UserMenu() {
  const { instance, accounts } = useMsal();
  const account = accounts[0];

  const handleLogout = () => {
    instance.logoutRedirect();
  };

  return (
    <div className="user-menu">
      <span className="user-menu-name" title={account?.username}>
        {account?.name ?? account?.username ?? 'Signed in'}
      </span>
      <button type="button" className="user-menu-logout" onClick={handleLogout}>
        Sign out
      </button>
    </div>
  );
}
