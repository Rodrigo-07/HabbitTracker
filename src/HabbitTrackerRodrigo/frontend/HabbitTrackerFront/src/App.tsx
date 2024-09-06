// src/App.tsx
import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Login from './pages/Login';
import Dashboard from './pages/Dashboard';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        {/* Redireciona a rota padrão ("/") para a página de login */}
        <Route path="/" element={<Navigate to="/login" />} />
        
        {/* Rota para página de login */}
        <Route path="/login" element={<Login />} />
        
        {/* Rota para página do dashboard */}
        <Route path="/dashboard" element={<Dashboard />} />
      </Routes>
    </Router>
  );
};

export default App;
