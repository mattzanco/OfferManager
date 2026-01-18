import apiClient from './api';

export interface Rfq {
  id: string;
  customerId: string;
  createdDate: string;
  status?: string;
  // Add other RFQ fields as needed
}

export const rfqService = {
  getAll: () => apiClient.get<Rfq[]>('/rfq'),
  getById: (id: string) => apiClient.get<Rfq>(`/rfq/${id}`),
  create: (data: Partial<Rfq>) => apiClient.post<Rfq>('/rfq', data),
  update: (id: string, data: Partial<Rfq>) => apiClient.put<Rfq>(`/rfq/${id}`, data),
  delete: (id: string) => apiClient.delete(`/rfq/${id}`),
};
