import apiClient from './api';

export interface Offer {
  id: string | number;
  currentRevisionId?: string | number;
  createdAt: string;
  createdDate?: string;
}

export const offerService = {
  getAll: () => apiClient.get<Offer[]>('/api/Offer'),
  getById: (id: string) => apiClient.get<Offer>(`/api/Offer/${id}`),
  create: (data: Partial<Offer>) => apiClient.post<Offer>('/api/Offer', data),
  update: (id: string, data: Partial<Offer>) => apiClient.put<Offer>(`/api/Offer/${id}`, data),
  delete: (id: string) => apiClient.delete(`/api/Offer/${id}`),
};
