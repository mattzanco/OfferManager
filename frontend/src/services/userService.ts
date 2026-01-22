import apiClient from './api';

export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  // Add other user fields as needed
}

export const userService = {
  getAll: () => apiClient.get<User[]>('/User'),
  getById: (id: string) => apiClient.get<User>(`/User/${id}`),
  create: (data: Partial<User>) => apiClient.post<User>('/User', data),
  update: (id: string, data: Partial<User>) => apiClient.put<User>(`/User/${id}`, data),
  delete: (id: string) => apiClient.delete(`/User/${id}`),
};
