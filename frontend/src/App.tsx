import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
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

// Static Web Apps deployment test
function App() {
  return (
    <BrowserRouter>
      <div className="app">
        <nav className="navbar">
          <div className="navbar-brand">
            <h1>📊 OfferManager</h1>
          </div>
          <ul className="navbar-links">
            <li><Link to="/">Dashboard</Link></li>
            <li><Link to="/offers">Offers</Link></li>
            <li><Link to="/customers">Customers</Link></li>
            <li><Link to="/rfqs">RFQs</Link></li>
          </ul>
        </nav>

        <main className="container">
          <Routes>
            <Route path="/" element={<Dashboard />} />
            
            {/* Offers */}
            <Route path="/offers" element={<OfferList />} />
            <Route path="/offers/new" element={<OfferForm />} />
            <Route path="/offers/:id" element={<OfferDetail />} />
            <Route path="/offers/edit/:id" element={<OfferForm />} />
            
            {/* Customers */}
            <Route path="/customers" element={<CustomerList />} />
            <Route path="/customers/new" element={<CustomerForm />} />
            <Route path="/customers/:id" element={<CustomerDetail />} />
            <Route path="/customers/edit/:id" element={<CustomerForm />} />
            
            {/* RFQs */}
            <Route path="/rfqs" element={<RfqList />} />
            <Route path="/rfqs/new" element={<RfqForm />} />
            <Route path="/rfqs/:id" element={<RfqDetail />} />
            <Route path="/rfqs/edit/:id" element={<RfqForm />} />
          </Routes>
        </main>

        <footer className="footer">
          <p>&copy; {new Date().getFullYear()} Matt Zanco. All rights reserved.</p>
        </footer>
      </div>
    </BrowserRouter>
  );
}

export default App;
