import apiClient from './api';

export interface Offer {
  id: string;
  currentRevisionId?: string;
  createdDate: string;
  // Add other offer fields as needed
}

export const offerService = {
  getAll: () => apiClient.get<Offer[]>('/offers'),
  getById: (id: string) => apiClient.get<Offer>(`/offers/${id}`),
  create: (data: Partial<Offer>) => apiClient.post<Offer>('/offers', data),
  update: (id: string, data: Partial<Offer>) => apiClient.put<Offer>(`/offers/${id}`, data),
  delete: (id: string) => apiClient.delete(`/offers/${id}`),
};
