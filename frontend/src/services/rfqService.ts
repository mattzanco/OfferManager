import apiClient from './api';

export interface Rfq {
  id: string;
  customerId: string;
  createdDate: string;
  status?: string;
  // Add other RFQ fields as needed
}

export const rfqService = {
  getAll: () => apiClient.get<Rfq[]>('/rfqs'),
  getById: (id: string) => apiClient.get<Rfq>(`/rfqs/${id}`),
  create: (data: Partial<Rfq>) => apiClient.post<Rfq>('/rfqs', data),
  update: (id: string, data: Partial<Rfq>) => apiClient.put<Rfq>(`/rfqs/${id}`, data),
  delete: (id: string) => apiClient.delete(`/rfqs/${id}`),
};
