import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { customerService, Customer } from '../services/customerService';
import '../styles/Detail.css';

export function CustomerDetail() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [customer, setCustomer] = useState<Customer | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchCustomer = async () => {
      if (!id) return;
      try {
        setLoading(true);
        const response = await customerService.getById(id);
        setCustomer(response.data);
        setError(null);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load customer');
      } finally {
        setLoading(false);
      }
    };

    fetchCustomer();
  }, [id]);

  const handleDelete = async () => {
    if (!id || !window.confirm('Are you sure you want to delete this customer?')) return;
    try {
      await customerService.delete(id);
      navigate('/customers');
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to delete customer');
    }
  };

  if (loading) return <div className="loading">Loading customer...</div>;
  if (error) return <div className="error">Error: {error}</div>;
  if (!customer) return <div className="not-found">Customer not found</div>;

  return (
    <div className="detail-container">
      <div className="detail-header">
        <h2>Customer Details</h2>
        <div className="detail-actions">
          <button onClick={() => navigate(`/customers/edit/${id}`)} className="btn-primary">Edit</button>
          <button onClick={handleDelete} className="btn-danger">Delete</button>
          <button onClick={() => navigate('/customers')} className="btn-secondary">Back</button>
        </div>
      </div>

      <div className="detail-content">
        <div className="detail-section">
          <h3>Information</h3>
          <div className="detail-row">
            <label>ID:</label>
            <span>{customer.customerId}</span>
          </div>
          <div className="detail-row">
            <label>Name:</label>
            <span>{customer.name}</span>
          </div>
        </div>
      </div>
    </div>
  );
}
