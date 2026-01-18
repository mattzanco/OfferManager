import apiClient from './api';

export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  // Add other user fields as needed
}

export const userService = {
  getAll: () => apiClient.get<User[]>('/users'),
  getById: (id: string) => apiClient.get<User>(`/users/${id}`),
  create: (data: Partial<User>) => apiClient.post<User>('/users', data),
  update: (id: string, data: Partial<User>) => apiClient.put<User>(`/users/${id}`, data),
  delete: (id: string) => apiClient.delete(`/users/${id}`),
};
