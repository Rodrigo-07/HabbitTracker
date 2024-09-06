import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import authService from '../services/auth';

const Dashboard: React.FC = () => {
  const navigate = useNavigate(); 

  useEffect(() => {
    const authenticated = authService.isAuthenticated();
    if (!authenticated) {
      navigate('/login');
    }
  }, [navigate]);

  return (
    <div>
      <h1>Bem-vindo ao Dashboard!</h1>
      <p>Você está autenticado.</p>

      <button onClick={() => authService.logout()}>Logout</button>
    </div>
  );
};

export default Dashboard;
