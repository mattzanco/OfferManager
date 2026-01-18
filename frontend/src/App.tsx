import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import { OfferList } from './components/OfferList';
import { CustomerList } from './components/CustomerList';
import './App.css';

function App() {
  return (
    <BrowserRouter>
      <div className="app">
        <nav className="navbar">
          <h1>OfferManager</h1>
          <ul>
            <li><Link to="/">Home</Link></li>
            <li><Link to="/offers">Offers</Link></li>
            <li><Link to="/customers">Customers</Link></li>
          </ul>
        </nav>

        <main className="container">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/offers" element={<OfferList />} />
            <Route path="/customers" element={<CustomerList />} />
          </Routes>
        </main>
      </div>
    </BrowserRouter>
  );
}

function Home() {
  return (
    <div className="home">
      <h1>Welcome to OfferManager</h1>
      <p>Select an option from the menu to get started.</p>
    </div>
  );
}

export default App;
