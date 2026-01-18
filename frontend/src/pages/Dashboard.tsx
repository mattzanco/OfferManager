import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { offerService } from '../services/offerService';
import { customerService } from '../services/customerService';
import { rfqService } from '../services/rfqService';
import '../styles/Dashboard.css';

interface DashboardStats {
  totalOffers: number;
  totalCustomers: number;
  totalRfqs: number;
}

export function Dashboard() {
  const navigate = useNavigate();
  const [stats, setStats] = useState<DashboardStats>({
    totalOffers: 0,
    totalCustomers: 0,
    totalRfqs: 0,
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchStats = async () => {
      try {
        setLoading(true);
        const [offerRes, customerRes, rfqRes] = await Promise.all([
          offerService.getAll(),
          customerService.getAll(),
          rfqService.getAll(),
        ]);

        setStats({
          totalOffers: offerRes.data.length,
          totalCustomers: customerRes.data.length,
          totalRfqs: rfqRes.data.length,
        });
        setError(null);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load dashboard');
      } finally {
        setLoading(false);
      }
    };

    fetchStats();
  }, []);

  if (loading) return <div className="loading">Loading dashboard...</div>;

  return (
    <div className="dashboard">
      <div className="dashboard-header">
        <h1>Dashboard</h1>
        {error && <div className="error">{error}</div>}
      </div>

      <div className="stats-grid">
        <div className="stat-card">
          <h3>Total Offers</h3>
          <div className="stat-number">{stats.totalOffers}</div>
          <button onClick={() => navigate('/offers')} className="stat-link">View Offers →</button>
        </div>

        <div className="stat-card">
          <h3>Total Customers</h3>
          <div className="stat-number">{stats.totalCustomers}</div>
          <button onClick={() => navigate('/customers')} className="stat-link">View Customers →</button>
        </div>

        <div className="stat-card">
          <h3>Total RFQs</h3>
          <div className="stat-number">{stats.totalRfqs}</div>
          <button onClick={() => navigate('/rfqs')} className="stat-link">View RFQs →</button>
        </div>
      </div>

      <div className="quick-actions">
        <h2>Quick Actions</h2>
        <div className="action-buttons">
          <button onClick={() => navigate('/customers/new')} className="btn-primary">
            + New Customer
          </button>
          <button onClick={() => navigate('/rfqs/new')} className="btn-primary">
            + New RFQ
          </button>
          <button onClick={() => navigate('/offers/new')} className="btn-primary">
            + New Offer
          </button>
        </div>
      </div>
    </div>
  );
}
