import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { rfqService, Rfq } from '../services/rfqService';
import '../styles/List.css';

export function RfqList() {
  const navigate = useNavigate();
  const [rfqs, setRfqs] = useState<Rfq[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchRfqs = async () => {
      try {
        setLoading(true);
        const response = await rfqService.getAll();
        setRfqs(response.data);
        setError(null);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load RFQs');
      } finally {
        setLoading(false);
      }
    };

    fetchRfqs();
  }, []);

  if (loading) return <div className="loading">Loading RFQs...</div>;
  if (error) return <div className="error">Error: {error}</div>;

  return (
    <div className="list-container">
      <div className="list-header">
        <h2>RFQs</h2>
        <button onClick={() => navigate('/rfqs/new')} className="btn-primary">
          + New RFQ
        </button>
      </div>
      {rfqs.length === 0 ? (
        <p>No RFQs found. <button onClick={() => navigate('/rfqs/new')} className="link-btn">Create one</button></p>
      ) : (
        <table className="list-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Customer ID</th>
              <th>Created Date</th>
              {rfqs.some(r => r.status) && <th>Status</th>}
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {rfqs.map((rfq) => (
              <tr key={rfq.rfqId}>
                <td>{rfq.rfqId}</td>
                <td>{rfq.customerId}</td>
                <td>{new Date(rfq.createdAt).toLocaleDateString()}</td>
                {rfqs.some(r => r.status) && <td>{rfq.status || 'N/A'}</td>}
                <td>
                  <button onClick={() => navigate(`/rfqs/${rfq.rfqId}`)} className="btn-small">View</button>
                  <button onClick={() => navigate(`/rfqs/edit/${rfq.rfqId}`)} className="btn-small">Edit</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}
