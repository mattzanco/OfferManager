import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { offerService, Offer } from '../services/offerService';
import '../styles/List.css';

export function OfferList() {
  const navigate = useNavigate();
  const [offers, setOffers] = useState<Offer[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchOffers = async () => {
      try {
        setLoading(true);
        const response = await offerService.getAll();
        setOffers(response.data);
        setError(null);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load offers');
      } finally {
        setLoading(false);
      }
    };

    fetchOffers();
  }, []);

  if (loading) return <div className="loading">Loading offers...</div>;
  if (error) return <div className="error">Error: {error}</div>;

  return (
    <div className="list-container">
      <div className="list-header">
        <h2>Offers</h2>
        <button onClick={() => navigate('/offers/new')} className="btn-primary">
          + New Offer
        </button>
      </div>
      {offers.length === 0 ? (
        <p>No offers found. <button onClick={() => navigate('/offers/new')} className="link-btn">Create one</button></p>
      ) : (
        <table className="list-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Created Date</th>
              <th>Revision</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {offers.map((offer) => (
              <tr key={offer.id}>
                <td>{offer.id}</td>
                <td>{new Date(offer.createdAt ?? offer.createdDate ?? '').toLocaleDateString()}</td>
                <td>{offer.currentRevisionId || 'N/A'}</td>
                <td>
                  <button onClick={() => navigate(`/offers/${offer.id}`)} className="btn-small">View</button>
                  <button onClick={() => navigate(`/offers/edit/${offer.id}`)} className="btn-small">Edit</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}
