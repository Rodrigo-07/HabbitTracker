import axios from 'axios';
import { jwtDecode } from 'jwt-decode';
import {DecodedUser, AuthResponse} from '../interfaces/auth';

const API_URL = 'http://localhost:5292';

// Método de login
const login = async (userName: string, password: string): Promise<DecodedUser> => {
  try {
    // Fazer a chamada para o backend
    console.log(userName);
    console.log(password);
    const response = await axios.post<AuthResponse>(`${API_URL}/api/Account/login`, { userName, password });
    console.log(response);

    // Extrair o token do corpo da resposta
    if (response.data.token) {
      localStorage.setItem('token', response.data.token); // Salvar o token no local storage
      return jwtDecode<DecodedUser>(response.data.token); // petornar o usuário decodificado
    } else {
      throw new Error('Token não foi encontrado');
    }
  } catch (error) {
    throw new Error('Erro ao fazer login: ' + error);
  }
};

// Método de logout
const logout = (): void => {
  localStorage.removeItem('token'); // para fazer logout é só remover o token do local storage
};

// Método para pegar o usuário logado
const getLoggedInUser = (): DecodedUser | null => {
  const token = localStorage.getItem('token');

  if (token) {
    return jwtDecode<DecodedUser>(token);
  }

  return null;
};

// Método para verificar se está autenticado
const isAuthenticated = (): boolean => {
  const token = localStorage.getItem('token');

  if (!token) {
    return false;
  }

  const decodedToken = jwtDecode<DecodedUser>(token);
  const currentTime = Date.now() / 1000;

  return decodedToken.exp > currentTime; // Se o token ainda não expirou, retorna true
};

const hasRole = (role: string): boolean => {
  const user = getLoggedInUser();

  if (user) {
    return user.roles.includes(role);
  }

  return false;
}

const getToken = (): string | null => {
  return localStorage.getItem('token');
}

// Exportar os métodos
const authService = {
  login,
  logout,
  getLoggedInUser,
  isAuthenticated,
  hasRole,
  getToken
};

export default authService;
