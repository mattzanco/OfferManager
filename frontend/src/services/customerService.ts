import apiClient from './api';

export interface Customer {
  customerId: number;
  organizationId: number;
  name: string;
  accountCode?: string | null;
  billingTerms?: string | null;
  status: string;
  createdAt: string;
}

export const customerService = {
  getAll: () => apiClient.get<Customer[]>('/api/Customer'),
  getById: (id: string) => apiClient.get<Customer>(`/api/Customer/${id}`),
  create: (data: Partial<Customer>) => apiClient.post<Customer>('/api/Customer', data),
  update: (id: string, data: Partial<Customer>) => apiClient.put<Customer>(`/api/Customer/${id}`, data),
  delete: (id: string) => apiClient.delete(`/api/Customer/${id}`),
};
