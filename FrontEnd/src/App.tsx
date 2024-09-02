import React from 'react';
import './App.css';
import Navbar from './component/page/navbar'; // Import Navbar
import Login from './component/page/login';
import { useSelector } from 'react-redux';
import { RootState } from './state/store'; // Pastikan jalur ini benar
import Product from './component/page/product';

function App() {
  // Pastikan state.counter ada dan memiliki isAutenticated
  const isAutenticated = useSelector((state: RootState) => state.login.isAutenticated);

  return (
    <div className="App">
      <Navbar />
      <main style={{ minHeight: 'calc(100vh - 120px)', padding: '20px' }}>
        {/* Render Login only if not authenticated */}
        {!isAutenticated ? <Login /> : <Product />}
      </main>
    </div>
  );
}

export default App;
