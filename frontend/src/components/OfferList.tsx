import { useState, useEffect } from 'react';
import { offerService, Offer } from '../services/offerService';
import './OfferList.css';

export function OfferList() {
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
    <div className="offer-list">
      <h2>Offers</h2>
      {offers.length === 0 ? (
        <p>No offers found.</p>
      ) : (
        <table>
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
                <td>{new Date(offer.createdDate).toLocaleDateString()}</td>
                <td>{offer.currentRevisionId || 'N/A'}</td>
                <td>
                  <button>View</button>
                  <button>Edit</button>
                  <button>Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}
