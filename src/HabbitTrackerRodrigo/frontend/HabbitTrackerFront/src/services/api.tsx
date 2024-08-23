import axios from 'axios';
import { getToken, isAuthenticated } from './auth';


const api = axios.create({
  baseURL: 'http://localhost:5292',
  headers: {
    'Content-Type': 'application/json',
  },
});

api.interceptors.request.use(async (config) => {
    if (isAuthenticated()) {
      config.headers['Authorization'] = `Bearer ${getToken()}`;
    }

    return co

