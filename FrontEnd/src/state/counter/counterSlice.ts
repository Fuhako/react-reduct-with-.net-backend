import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import exp from "constants";
import axios from "axios";
import { RootState } from '../../state/store';

interface CounterState {
    name: string;
    isAutenticated: boolean;
    products: Product[];
    loading: boolean;
    error: string;
    user: string | null; // Tambahkan field user

};

export interface Product {
    id: number;
    plu: string;
    product_category_id: number;
    created_user: string;
}

export interface User {
    id: number;
    userid: string;
    email: string;
    // tambahkan field lain jika diperlukan
}

const initialState: CounterState = {
    name: "",
    isAutenticated: false,
    products: [],
    loading: false,
    error: '',
    user: ''
};

const configureSlice = createSlice({
    name: "counter",
    initialState,
    reducers: {
        setAutenticated: (state, action: PayloadAction<boolean>) => {
            state.isAutenticated = action.payload;
        },

        login: (state, action: PayloadAction<{ name: string, token: string }>) => {
            state.name = action.payload.name;
            state.isAutenticated = true;
            state.user = action.payload.token; // Simpan token pengguna
        },
        logout: (state) => {
            state.name = '';
            state.isAutenticated = false;
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchProducts.pending, (state) => {
                state.loading = true;
            })
            .addCase(fetchProducts.fulfilled, (state, action: PayloadAction<Product[]>) => {
                state.loading = false;
                state.products = action.payload;
            })
            .addCase(fetchProducts.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message || 'Failed to fetch products';
            })
            .addCase(addProduct.fulfilled, (state, action: PayloadAction<Product>) => {
                state.products.push(action.payload);
            })
            .addCase(updateProduct.fulfilled, (state, action: PayloadAction<Product>) => {
                const index = state.products.findIndex(p => p.id === action.payload.id);
                if (index !== -1) {
                    state.products[index] = action.payload;
                }
            })
            .addCase(deleteProduct.fulfilled, (state, action: PayloadAction<number>) => {
                state.products = state.products.filter(p => p.id !== action.payload);
            })
            .addCase(fetchUser.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message || 'Failed to fetch user';
            });;
    },
});

// Thunks untuk operasi async
export const fetchUser = createAsyncThunk('user/fetchUser', async (token: string) => {
    const response = await axios.get('https://localhost:7073/api/Auth/user', {
        headers: {
            Authorization: `Bearer ${token}`,
        },
    });
    return response.data;
});

export const fetchProducts = createAsyncThunk('products/fetchProducts', async () => {
    const response = await axios.get('https://localhost:7073/api/Product/GetProduct');
    return response.data;
});

export const addProduct = createAsyncThunk('products/addProduct', async (product: Product, { getState }) => {
    const state = getState() as RootState;

    const response = await axios.post('https://localhost:7073/api/Product/CreateProduct', product, {
        headers: {
            'Authorization': `Bearer ${state.login.user}`, // If needed
        }
    });

    return response.data;
});

export const updateProduct = createAsyncThunk('products/updateProduct', async (product: Product) => {
    const response = await axios.put(`https://localhost:7073/api/Product/UpdateProductById/${product.id}`);
    return response.data;
});

export const deleteProduct = createAsyncThunk('products/deleteProduct', async (product: Product) => {
    const response = await axios.delete(`https://localhost:7073/api/Product/DeleteProductById/${product.id}`);
    return response.data;
});

export const { login, logout, setAutenticated } = configureSlice.actions;

export default configureSlice.reducer;