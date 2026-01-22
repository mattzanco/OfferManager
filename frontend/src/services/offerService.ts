import apiClient from './api';

export interface Offer {
  id: string;
  currentRevisionId?: string;
  createdDate: string;
  // Add other offer fields as needed
}

export const offerService = {
  getAll: () => apiClient.get<Offer[]>('/Offer'),
  getById: (id: string) => apiClient.get<Offer>(`/Offer/${id}`),
  create: (data: Partial<Offer>) => apiClient.post<Offer>('/Offer', data),
  update: (id: string, data: Partial<Offer>) => apiClient.put<Offer>(`/Offer/${id}`, data),
  delete: (id: string) => apiClient.delete(`/Offer/${id}`),
};
