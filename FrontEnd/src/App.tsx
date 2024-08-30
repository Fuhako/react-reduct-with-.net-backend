import React from 'react';
import logo from './logo.svg';
import './App.css';
import Navbar from './component/page/navbar'; // Import Navbar
import Login from './component/page/login';
import { useSelector } from 'react-redux';
import { RootState } from './state/store';

function App() {
  const { isAutenticated } = useSelector((state: RootState) => state.counter);

  return (
    <div className="App">
      <Navbar />
      <main style={{ minHeight: 'calc(100vh - 120px)', padding: '20px' }}>
        {/* Render Login only if not authenticated */}
        {!isAutenticated ? <Login /> : <div>Welcome Back!</div>}
      </main>
    </div>
  );
}

export default App;
