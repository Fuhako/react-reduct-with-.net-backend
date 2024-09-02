import React from 'react';
import './App.css';
import Navbar from './component/page/navbar'; // Import Navbar
import Sidebar from './component/page/sidebar'; // Import Sidebar
import Login from './component/page/login';
import Product from './component/page/product';
import ProductCategory from './component/page/productCategory';
import ProductVariant from './component/page/productVariant';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { RootState } from './state/store'; // Pastikan jalur ini benar

function App() {
  // Pastikan state.login ada dan memiliki isAuthenticated
  const isAuthenticated = useSelector((state: RootState) => state.login.isAutenticated);

  return (
    <Router>
      <div className="App" style={{ display: 'flex' }}>
        {isAuthenticated && <Sidebar />} {/* Conditionally render Sidebar */}
        <div style={{ flexGrow: 1 }}>
          <Navbar />
          <main style={{ minHeight: 'calc(100vh - 120px)', padding: '20px' }}>
            {!isAuthenticated ? (
              <Login />
            ) : (
              <Routes>
                <Route path="/products" element={<Product />} />
                <Route path="/product-category" element={<ProductCategory />} />
                <Route path="/product-variant" element={<ProductVariant />} />
                {/* Add other routes here */}
              </Routes>
            )}
          </main>
        </div>
      </div>
    </Router>
  );
}

export default App;
