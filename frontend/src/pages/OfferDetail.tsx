import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { offerService, Offer } from '../services/offerService';
import '../styles/Detail.css';

export function OfferDetail() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [offer, setOffer] = useState<Offer | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchOffer = async () => {
      if (!id) return;
      try {
        setLoading(true);
        const response = await offerService.getById(id);
        setOffer(response.data);
        setError(null);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load offer');
      } finally {
        setLoading(false);
      }
    };

    fetchOffer();
  }, [id]);

  const handleDelete = async () => {
    if (!id || !window.confirm('Are you sure you want to delete this offer?')) return;
    try {
      await offerService.delete(id);
      navigate('/offers');
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to delete offer');
    }
  };

  if (loading) return <div className="loading">Loading offer...</div>;
  if (error) return <div className="error">Error: {error}</div>;
  if (!offer) return <div className="not-found">Offer not found</div>;

  return (
    <div className="detail-container">
      <div className="detail-header">
        <h2>Offer Details</h2>
        <div className="detail-actions">
          <button onClick={() => navigate(`/offers/edit/${id}`)} className="btn-primary">Edit</button>
          <button onClick={handleDelete} className="btn-danger">Delete</button>
          <button onClick={() => navigate('/offers')} className="btn-secondary">Back</button>
        </div>
      </div>

      <div className="detail-content">
        <div className="detail-section">
          <h3>Information</h3>
          <div className="detail-row">
            <label>ID:</label>
            <span>{offer.id}</span>
          </div>
          <div className="detail-row">
            <label>Created Date:</label>
            <span>{new Date(offer.createdDate).toLocaleDateString()}</span>
          </div>
          <div className="detail-row">
            <label>Current Revision:</label>
            <span>{offer.currentRevisionId || 'N/A'}</span>
          </div>
        </div>
      </div>
    </div>
  );
}
