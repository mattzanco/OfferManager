import apiClient from './api';

export interface Customer {
  id: string;
  name: string;
  // Add other customer fields as needed
}

export const customerService = {
  getAll: () => apiClient.get<Customer[]>('/Customer'),
  getById: (id: string) => apiClient.get<Customer>(`/Customer/${id}`),
  create: (data: Partial<Customer>) => apiClient.post<Customer>('/Customer', data),
  update: (id: string, data: Partial<Customer>) => apiClient.put<Customer>(`/Customer/${id}`, data),
  delete: (id: string) => apiClient.delete(`/Customer/${id}`),
};
