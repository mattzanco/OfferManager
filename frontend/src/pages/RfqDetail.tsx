import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { rfqService, Rfq } from '../services/rfqService';
import '../styles/Detail.css';

export function RfqDetail() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [rfq, setRfq] = useState<Rfq | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchRfq = async () => {
      if (!id) return;
      try {
        setLoading(true);
        const response = await rfqService.getById(id);
        setRfq(response.data);
        setError(null);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load RFQ');
      } finally {
        setLoading(false);
      }
    };

    fetchRfq();
  }, [id]);

  const handleDelete = async () => {
    if (!id || !window.confirm('Are you sure you want to delete this RFQ?')) return;
    try {
      await rfqService.delete(id);
      navigate('/rfqs');
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to delete RFQ');
    }
  };

  if (loading) return <div className="loading">Loading RFQ...</div>;
  if (error) return <div className="error">Error: {error}</div>;
  if (!rfq) return <div className="not-found">RFQ not found</div>;

  return (
    <div className="detail-container">
      <div className="detail-header">
        <h2>RFQ Details</h2>
        <div className="detail-actions">
          <button onClick={() => navigate(`/rfqs/edit/${id}`)} className="btn-primary">Edit</button>
          <button onClick={handleDelete} className="btn-danger">Delete</button>
          <button onClick={() => navigate('/rfqs')} className="btn-secondary">Back</button>
        </div>
      </div>

      <div className="detail-content">
        <div className="detail-section">
          <h3>Information</h3>
          <div className="detail-row">
            <label>ID:</label>
            <span>{rfq.rfqId}</span>
          </div>
          <div className="detail-row">
            <label>Customer ID:</label>
            <span>{rfq.customerId}</span>
          </div>
          <div className="detail-row">
            <label>Created Date:</label>
            <span>{new Date(rfq.createdAt).toLocaleDateString()}</span>
          </div>
          {rfq.status && (
            <div className="detail-row">
              <label>Status:</label>
              <span>{rfq.status}</span>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
