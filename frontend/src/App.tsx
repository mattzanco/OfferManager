import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import { AppAuthShell } from './auth/AppAuthShell';
import { authEnabled } from './auth/authConfig';
import { UserMenu } from './components/UserMenu';
import { OfferList } from './components/OfferList';
import { CustomerList } from './components/CustomerList';
import { OfferDetail } from './pages/OfferDetail';
import { OfferForm } from './pages/OfferForm';
import { CustomerDetail } from './pages/CustomerDetail';
import { CustomerForm } from './pages/CustomerForm';
import { RfqList } from './pages/RfqList';
import { RfqDetail } from './pages/RfqDetail';
import { RfqForm } from './pages/RfqForm';
import { Dashboard } from './pages/Dashboard';
import './App.css';

function App() {
  return (
    <AppAuthShell>
      <BrowserRouter>
        <div className="app">
          <nav className="navbar">
            <div className="navbar-brand">
              <h1>OfferManager</h1>
            </div>
            <ul className="navbar-links">
              <li><Link to="/">Dashboard</Link></li>
              <li><Link to="/offers">Offers</Link></li>
              <li><Link to="/customers">Customers</Link></li>
              <li><Link to="/rfqs">RFQs</Link></li>
            </ul>
            {authEnabled && (
              <div className="navbar-user">
                <UserMenu />
              </div>
            )}
          </nav>

          <main className="container">
            <Routes>
              <Route path="/" element={<Dashboard />} />
              <Route path="/offers" element={<OfferList />} />
              <Route path="/offers/new" element={<OfferForm />} />
              <Route path="/offers/edit/:id" element={<OfferForm />} />
              <Route path="/offers/:id" element={<OfferDetail />} />
              <Route path="/customers" element={<CustomerList />} />
              <Route path="/customers/new" element={<CustomerForm />} />
              <Route path="/customers/edit/:id" element={<CustomerForm />} />
              <Route path="/customers/:id" element={<CustomerDetail />} />
              <Route path="/rfqs" element={<RfqList />} />
              <Route path="/rfqs/new" element={<RfqForm />} />
              <Route path="/rfqs/edit/:id" element={<RfqForm />} />
              <Route path="/rfqs/:id" element={<RfqDetail />} />
            </Routes>
          </main>

          <footer className="footer">
            <p>&copy; {new Date().getFullYear()} Matt Zanco. All rights reserved.</p>
          </footer>
        </div>
      </BrowserRouter>
    </AppAuthShell>
  );
}

export default App;
