import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';
import authService from '../services/auth';

const Login: React.FC = () => {
    const [userName, setuserName] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();
  
    const handleLogin = async (e: React.FormEvent) => {
      e.preventDefault();
      try {
        // const response = await api.post('/api/Account/login', { email, password });
        // const { token } = response.data;

        // console.log(token);
  
        // // Armazena o token JWT em localStorage
        // localStorage.setItem('token', token);
  
        // // Redireciona para outra página, ex: página principal
        // navigate('/dashboard');

        const user = await authService.login(userName, password);
    
        // Redireciona para o dashboard se o login for bem-sucedido
        if (user) {
        navigate('/dashboard');
        }
      } catch (err) {
        setError('Login failed. Check your credentials.');
      }
    };
  
    return (
      <div style={{ maxWidth: '400px', margin: 'auto' }}>
        <h2>Login</h2>
        <form onSubmit={handleLogin}>
          <div>
            <label>Username:</label>
            <input
              onChange={(e) => setuserName(e.target.value)}
              required
            />
          </div>
          <div>
            <label>Password:</label>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>
          {error && <p style={{ color: 'red' }}>{error}</p>}
          <button type="submit">Login</button>
        </form>
      </div>
    );
  };
  
  export default Login;