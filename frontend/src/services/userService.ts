import apiClient from './api';

export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  // Add other user fields as needed
}

export const userService = {
  getAll: () => apiClient.get<User[]>('/api/User'),
  getById: (id: string) => apiClient.get<User>(`/api/User/${id}`),
  create: (data: Partial<User>) => apiClient.post<User>('/api/User', data),
  update: (id: string, data: Partial<User>) => apiClient.put<User>(`/api/User/${id}`, data),
  delete: (id: string) => apiClient.delete(`/api/User/${id}`),
};
