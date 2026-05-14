import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { rfqService, Rfq } from '../services/rfqService';
import '../styles/Form.css';

export function RfqForm() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [formData, setFormData] = useState<Partial<Rfq>>({});
  const [loading, setLoading] = useState(!!id);
  const [error, setError] = useState<string | null>(null);
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    if (!id) return;
    const fetchRfq = async () => {
      try {
        const response = await rfqService.getById(id);
        setFormData(response.data);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load RFQ');
      } finally {
        setLoading(false);
      }
    };
    fetchRfq();
  }, [id]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitting(true);
    try {
      const normalizedCustomerId =
        typeof formData.customerId === 'string'
          ? parseInt(formData.customerId, 10)
          : formData.customerId;
      if (id) {
        const rfqId = parseInt(id, 10);
        await rfqService.update(id, {
          ...formData,
          rfqId,
          customerId: normalizedCustomerId,
        });
        navigate(`/rfqs/${id}`);
      } else {
        const response = await rfqService.create({
          ...formData,
          customerId: normalizedCustomerId,
        });
        navigate(`/rfqs/${response.data.rfqId}`);
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to save RFQ');
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) return <div className="loading">Loading RFQ...</div>;

  return (
    <div className="form-container">
      <h2>{id ? 'Edit RFQ' : 'Create New RFQ'}</h2>
      {error && <div className="error">{error}</div>}

      <form onSubmit={handleSubmit} className="form">
        <div className="form-group">
          <label>Customer ID *</label>
          <input
            type="text"
            name="customerId"
            value={formData.customerId != null ? String(formData.customerId) : ''}
            onChange={handleChange}
            placeholder="Enter customer ID"
            required
          />
        </div>

        {formData.status && (
          <div className="form-group">
            <label>Status</label>
            <input
              type="text"
              name="status"
              value={formData.status || ''}
              onChange={handleChange}
              placeholder="Enter status"
            />
          </div>
        )}

        <div className="form-actions">
          <button type="submit" disabled={submitting} className="btn-primary">
            {submitting ? 'Saving...' : id ? 'Update RFQ' : 'Create RFQ'}
          </button>
          <button
            type="button"
            onClick={() => navigate(id ? `/rfqs/${id}` : '/rfqs')}
            className="btn-secondary"
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  );
}
