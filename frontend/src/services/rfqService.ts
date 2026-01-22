import apiClient from './api';

export interface Rfq {
  id: string;
  customerId: string;
  createdDate: string;
  status?: string;
  // Add other RFQ fields as needed
}

export const rfqService = {
  getAll: () => apiClient.get<Rfq[]>('/Rfq'),
  getById: (id: string) => apiClient.get<Rfq>(`/Rfq/${id}`),
  create: (data: Partial<Rfq>) => apiClient.post<Rfq>('/Rfq', data),
  update: (id: string, data: Partial<Rfq>) => apiClient.put<Rfq>(`/Rfq/${id}`, data),
  delete: (id: string) => apiClient.delete(`/Rfq/${id}`),
};
