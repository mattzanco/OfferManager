import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { customerService, Customer } from '../services/customerService';
import '../styles/Form.css';

export function CustomerForm() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [formData, setFormData] = useState<Partial<Customer>>({});
  const [loading, setLoading] = useState(!!id);
  const [error, setError] = useState<string | null>(null);
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    if (!id) return;
    const fetchCustomer = async () => {
      try {
        const response = await customerService.getById(id);
        setFormData(response.data);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load customer');
      } finally {
        setLoading(false);
      }
    };
    fetchCustomer();
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
        await customerService.update(id, {
          ...formData,
          customerId: parseInt(id, 10),
        });
        navigate(`/customers/${id}`);
      } else {
        const response = await customerService.create(formData);
        navigate(`/customers/${response.data.customerId}`);
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to save customer');
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) return <div className="loading">Loading customer...</div>;

  return (
    <div className="form-container">
      <h2>{id ? 'Edit Customer' : 'Create New Customer'}</h2>
      {error && <div className="error">{error}</div>}

      <form onSubmit={handleSubmit} className="form">
        <div className="form-group">
          <label>Name *</label>
          <input
            type="text"
            name="name"
            value={formData.name || ''}
            onChange={handleChange}
            placeholder="Enter customer name"
            required
          />
        </div>

        <div className="form-actions">
          <button type="submit" disabled={submitting} className="btn-primary">
            {submitting ? 'Saving...' : id ? 'Update Customer' : 'Create Customer'}
          </button>
          <button
            type="button"
            onClick={() => navigate(id ? `/customers/${id}` : '/customers')}
            className="btn-secondary"
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  );
}
