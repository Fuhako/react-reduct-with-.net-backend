import React, { useEffect, useState } from 'react';
import './App.css';
import Navbar from './component/page/navbar';
import Sidebar from './component/page/sidebar';
import Login from './component/page/login';
import Product from './component/page/product';
import ProductCategory from './component/page/productCategory';
import ProductVariant from './component/page/productVariant';
import { BrowserRouter as Router } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { RootState, AppDispatch } from './state/store'; // Pastikan import AppDispatch dari store
import { fetchMenuAccessByRoleId } from './state/counter/counterSlice';
import CircularProgress from '@mui/material/CircularProgress'; // Import CircularProgress for loading indicator

function App() {
  const dispatch: AppDispatch = useDispatch(); // Use the correct dispatch type
  const isAuthenticated = useSelector((state: RootState) => state.login.isAutenticated);
  const roleid = useSelector((state: RootState) => state.login.roleid);
  const menuAccess = useSelector((state: RootState) => state.login.menuAccessByRoleId);
  const [loading, setLoading] = useState(true); // State to track loading

  useEffect(() => {
    if (isAuthenticated && roleid) {
      setLoading(true); // Start loading
      dispatch(fetchMenuAccessByRoleId(roleid))
        .unwrap()
        .finally(() => {
          setLoading(false); // Stop loading
        });
    } else {
      setLoading(false); // Stop loading if not authenticated or no role ID
    }
  }, [isAuthenticated, roleid, dispatch]);

  return (
    <Router>
      <div className="App" style={{ display: 'flex' }}>
        {isAuthenticated && menuAccess.length > 0 && <Sidebar />}
        <div style={{ flexGrow: 1 }}>
          <Navbar />
          <main style={{ minHeight: 'calc(100vh - 120px)', padding: '20px' }}>
            {loading ? (
              <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100%' }}>
                <CircularProgress />
              </div>
            ) : !isAuthenticated ? (
              <Login />
            ) : (
              <>
                {menuAccess.some((menu) => menu.menuname === 'Products') && <Product />}
                {menuAccess.some((menu) => menu.menuname === 'Product Category') && <ProductCategory />}
                {menuAccess.some((menu) => menu.menuname === 'Product Variant') && <ProductVariant />}
              </>
            )}
          </main>
        </div>
      </div>
    </Router>
  );
}

export default App;
