import apiClient from './api';

export interface Customer {
  id: string;
  name: string;
  // Add other customer fields as needed
}

export const customerService = {
  getAll: () => apiClient.get<Customer[]>('/customers'),
  getById: (id: string) => apiClient.get<Customer>(`/customers/${id}`),
  create: (data: Partial<Customer>) => apiClient.post<Customer>('/customers', data),
  update: (id: string, data: Partial<Customer>) => apiClient.put<Customer>(`/customers/${id}`, data),
  delete: (id: string) => apiClient.delete(`/customers/${id}`),
};
