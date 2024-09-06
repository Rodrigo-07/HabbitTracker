import axios from 'axios';
import  authService  from './auth';


const api = axios.create({
  baseURL: 'http://localhost:5292',
  headers: {
    'Content-Type': 'application/json',
  },
});

api.interceptors.request.use(async (config) => {
    if (authService.isAuthenticated()) {
      config.headers['Authorization'] = `Bearer ${authService.getToken()}`;
    }

    return config;
  }, (error) => {
    return Promise.reject(error);
  });

export default api;

