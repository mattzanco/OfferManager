import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { offerService, Offer } from '../services/offerService';
import '../styles/Form.css';

export function OfferForm() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [formData, setFormData] = useState<Partial<Offer>>({});
  const [loading, setLoading] = useState(!!id);
  const [error, setError] = useState<string | null>(null);
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    if (!id) return;
    const fetchOffer = async () => {
      try {
        const response = await offerService.getById(id);
        setFormData(response.data);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load offer');
      } finally {
        setLoading(false);
      }
    };
    fetchOffer();
  }, [id]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitting(true);
    try {
      if (id) {
        await offerService.update(id, formData);
        navigate(`/offers/${id}`);
      } else {
        const response = await offerService.create(formData);
        navigate(`/offers/${response.data.id}`);
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to save offer');
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) return <div className="loading">Loading offer...</div>;

  return (
    <div className="form-container">
      <h2>{id ? 'Edit Offer' : 'Create New Offer'}</h2>
      {error && <div className="error">{error}</div>}

      <form onSubmit={handleSubmit} className="form">
        <div className="form-group">
          <label>Revision ID</label>
          <input
            type="text"
            name="currentRevisionId"
            value={formData.currentRevisionId || ''}
            onChange={handleChange}
            placeholder="Enter revision ID"
          />
        </div>

        <div className="form-actions">
          <button type="submit" disabled={submitting} className="btn-primary">
            {submitting ? 'Saving...' : id ? 'Update Offer' : 'Create Offer'}
          </button>
          <button
            type="button"
            onClick={() => navigate(id ? `/offers/${id}` : '/offers')}
            className="btn-secondary"
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  );
}
