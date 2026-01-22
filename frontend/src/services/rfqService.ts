import apiClient from './api';

export interface Rfq {
  id: string;
  customerId: string;
  createdDate: string;
  status?: string;
  // Add other RFQ fields as needed
}

export const rfqService = {
  getAll: () => apiClient.get<Rfq[]>('/api/Rfq'),
  getById: (id: string) => apiClient.get<Rfq>(`/api/Rfq/${id}`),
  create: (data: Partial<Rfq>) => apiClient.post<Rfq>('/api/Rfq', data),
  update: (id: string, data: Partial<Rfq>) => apiClient.put<Rfq>(`/api/Rfq/${id}`, data),
  delete: (id: string) => apiClient.delete(`/Rfq/${id}`),
};
