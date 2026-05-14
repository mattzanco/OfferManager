import apiClient from './api';

export interface Rfq {
  rfqId: number;
  organizationId: number;
  customerId: number;
  requestedByContactId?: number | null;
  originLocationId: number;
  destinationLocationId: number;
  mode: string;
  equipmentType?: string | null;
  serviceLevel?: string | null;
  pickupEarliestAt?: string | null;
  pickupLatestAt?: string | null;
  deliveryEarliestAt?: string | null;
  deliveryLatestAt?: string | null;
  commodity?: string | null;
  weightLbs?: number | null;
  palletCount?: number | null;
  pieceCount?: number | null;
  hazmat: boolean;
  temperatureControlled: boolean;
  notes?: string | null;
  status: string;
  createdByUserId: number;
  createdAt: string;
  updatedAt: string;
}

export const rfqService = {
  getAll: () => apiClient.get<Rfq[]>('/api/Rfq'),
  getById: (id: string) => apiClient.get<Rfq>(`/api/Rfq/${id}`),
  create: (data: Partial<Rfq>) => apiClient.post<Rfq>('/api/Rfq', data),
  update: (id: string, data: Partial<Rfq>) => apiClient.put<Rfq>(`/api/Rfq/${id}`, data),
  delete: (id: string) => apiClient.delete(`/api/Rfq/${id}`),
};
