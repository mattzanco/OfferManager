import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { customerService, Customer } from '../services/customerService';
import '../styles/List.css';

export function CustomerList() {
  const navigate = useNavigate();
  const [customers, setCustomers] = useState<Customer[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchCustomers = async () => {
      try {
        setLoading(true);
        const response = await customerService.getAll();
        setCustomers(response.data);
        setError(null);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load customers');
      } finally {
        setLoading(false);
      }
    };

    fetchCustomers();
  }, []);

  if (loading) return <div className="loading">Loading customers...</div>;
  if (error) return <div className="error">Error: {error}</div>;

  return (
    <div className="list-container">
      <div className="list-header">
        <h2>Customers</h2>
        <button onClick={() => navigate('/customers/new')} className="btn-primary">
          + New Customer
        </button>
      </div>
      {customers.length === 0 ? (
        <p>No customers found. <button onClick={() => navigate('/customers/new')} className="link-btn">Create one</button></p>
      ) : (
        <table className="list-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {customers.map((customer) => (
              <tr key={customer.id}>
                <td>{customer.id}</td>
                <td>{customer.name}</td>
                <td>
                  <button onClick={() => navigate(`/customers/${customer.id}`)} className="btn-small">View</button>
                  <button onClick={() => navigate(`/customers/edit/${customer.id}`)} className="btn-small">Edit</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}
