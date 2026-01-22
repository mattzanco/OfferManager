import apiClient from './api';

export interface Customer {
  id: string;
  name: string;
  // Add other customer fields as needed
}

export const customerService = {
  getAll: () => apiClient.get<Customer[]>('/api/Customer'),
  getById: (id: string) => apiClient.get<Customer>(`/api/Customer/${id}`),
  create: (data: Partial<Customer>) => apiClient.post<Customer>('/api/Customer', data),
  update: (id: string, data: Partial<Customer>) => apiClient.put<Customer>(`/api/Customer/${id}`, data),
  delete: (id: string) => apiClient.delete(`/api/Customer/${id}`),
};
