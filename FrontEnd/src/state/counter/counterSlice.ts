import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import axios from "axios";
import { RootState } from '../../state/store';

export interface Product {
    id: number;
    plu: string;
    product_category_id: number;
    active: boolean;
    created_user: string;
}

export interface ProductCategory {
    id: number;
    name: string;
    active: boolean;
    created_user: string;
}
export interface ProductVariant {
    id: number;
    product_id: number;
    code: string;
    name: string;
    qty: number;
    price: number;
    active: boolean;
    created_user: string;
}

export interface User {
    id: number;
    userid: string;
    email: string;
    // Tambahkan field lain jika diperlukan
}

export interface Menu {
    id: number;
    name: string;
    iconName: string;
    path: string;
    active: boolean;
    created_user: string;
    // Tambahkan field lain jika diperlukan
}

export interface MenuAccess {
    id: number;
    menu_id: number;
    menu_name: string;
    role_id: number;
    active: boolean;
    created_user: string;
    // Tambahkan field lain jika diperlukan
}

interface CounterState {
    name: string;
    roleid: number;
    isAutenticated: boolean;
    products: Product[];
    productCategory: ProductCategory[];
    productVariant: ProductVariant[];
    Menu: Menu[];
    menuAccess: MenuAccess[];
    menuAccessByRoleId: MenuAccess[]; // Tambahkan properti menuAccessByRoleId
    manageTransactions: Transaction[];
    transactions: TransactionDetail[];
    isLoading: boolean;
    error: string | null;
    user: string | null; // Simpan token pengguna
}

interface Transaction {
    id: number;
    transaction_no: string;
    total_amount: number;
    active: boolean;
    created_date: string;
    created_user: string;
}

interface TransactionDetail {
    transactionId: number;
    transactionNo: string;
    category: string;
    productName: string;
    qty: number;
    subtotal: number;
}

const initialState: CounterState = {
    name: "",
    roleid: 0,
    isAutenticated: false,
    products: [],
    productCategory: [],
    productVariant: [],
    Menu: [],
    menuAccess: [],
    menuAccessByRoleId: [],
    manageTransactions: [],
    transactions: [],
    isLoading: false,
    error: null,
    user: null,
};


const configureSlice = createSlice({
    name: "counter",
    initialState,
    reducers: {
        setAutenticated: (state, action: PayloadAction<boolean>) => {
            state.isAutenticated = action.payload;
        },
        startLoading: (state) => {
            state.isLoading = true;
        },
        stopLoading: (state) => {
            state.isLoading = false;
        },
        login: (state, action: PayloadAction<{ name: string; token: string; roleid: number }>) => {
            state.name = action.payload.name;
            state.isAutenticated = true;
            state.user = action.payload.token; // Simpan token pengguna
            state.roleid = action.payload.roleid;
        },
        logout: (state) => {
            state.name = '';
            state.isAutenticated = false;
            state.user = null;
        },
        toggleActive: (state, action: PayloadAction<number>) => {
            const product = state.products.find(p => p.id === action.payload);
            if (product) {
                product.active = !product.active;
            }
        },
        toggleActiveCategory: (state, action: PayloadAction<number>) => {
            const productCategory = state.productCategory.find(p => p.id === action.payload);
            if (productCategory) {
                productCategory.active = !productCategory.active;
            }
        },
        toggleActiveVariant: (state, action: PayloadAction<number>) => {
            const productVariant = state.productVariant.find(p => p.id === action.payload);
            if (productVariant) {
                productVariant.active = !productVariant.active;
            }
        },
    },
    extraReducers: (builder) => {
        builder
            //#region Products
            .addCase(fetchProducts.pending, (state) => {
                state.isLoading = true;
            })
            .addCase(fetchProducts.fulfilled, (state, action: PayloadAction<Product[]>) => {
                state.isLoading = false;
                state.products = action.payload;
            })
            .addCase(fetchProducts.rejected, (state, action) => {
                state.isLoading = false;
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
            //#endregion Products

            //#region ProductCategory
            .addCase(fetchProductCategory.pending, (state) => {
                state.isLoading = true;
            })
            .addCase(fetchProductCategory.fulfilled, (state, action: PayloadAction<ProductCategory[]>) => {
                state.isLoading = false;
                state.productCategory = action.payload;
            })
            .addCase(fetchProductCategory.rejected, (state, action) => {
                state.isLoading = false;
                state.error = action.error.message || 'Failed to fetch product categories';
            })
            .addCase(addProductCategory.fulfilled, (state, action: PayloadAction<ProductCategory>) => {
                state.productCategory.push(action.payload);
            })
            .addCase(updateProductCategory.fulfilled, (state, action: PayloadAction<ProductCategory>) => {
                const index = state.productCategory.findIndex(p => p.id === action.payload.id);
                if (index !== -1) {
                    state.productCategory[index] = action.payload;
                }
            })
            .addCase(deleteProductCategory.fulfilled, (state, action: PayloadAction<number>) => {
                state.productCategory = state.productCategory.filter(p => p.id !== action.payload);
            })
            .addCase(fetchUser.rejected, (state, action) => {
                state.isLoading = false;
                state.error = action.error.message || 'Failed to fetch user';
            })
            //#endregion ProductCategory

            //#region Menu
            .addCase(fetchMenu.pending, (state) => {
                state.isLoading = true;
                state.error = null;
            })
            .addCase(fetchMenu.fulfilled, (state, action) => {
                state.Menu = action.payload;
                state.isLoading = false;
            })
            .addCase(fetchMenu.rejected, (state, action) => {
                state.isLoading = false;
                state.error = action.error.message || 'Failed to fetch transactions';
            })
            //#endregion Menu

            //#region MenuAccess
            .addCase(fetchMenuAccess.pending, (state) => {
                state.isLoading = true;
            })
            .addCase(fetchMenuAccess.fulfilled, (state, action: PayloadAction<MenuAccess[]>) => {
                state.isLoading = false;
                state.menuAccess = action.payload;
            })
            .addCase(fetchMenuAccessByRoleId.fulfilled, (state, action: PayloadAction<MenuAccess[]>) => {
                state.isLoading = false;
                state.menuAccessByRoleId = action.payload;
            })
            //#endregion MenuAccess

            //#region ManageTransaction
            .addCase(fetchTransactions.pending, (state) => {
                state.isLoading = true;
                state.error = null;
            })
            .addCase(fetchTransactions.fulfilled, (state, action) => {
                state.manageTransactions = action.payload;
                state.isLoading = false;
            })
            .addCase(fetchTransactions.rejected, (state, action) => {
                state.isLoading = false;
                state.error = action.error.message || 'Failed to fetch transactions';
            })
            //#endregion ManageTransaction

            //#region Transaction
            .addCase(fetchTransactionsDetail.pending, (state) => {
                state.isLoading = true;
                state.error = null;
            })
            .addCase(fetchTransactionsDetail.fulfilled, (state, action) => {
                state.transactions = action.payload;
                state.isLoading = false;
            })
            .addCase(fetchTransactionsDetail.rejected, (state, action) => {
                state.isLoading = false;
                state.error = action.error.message || 'Failed to fetch transactions';
            });
        //#endregion Transaction
        ;
    },
});

// Thunks for async operations
export const fetchUser = createAsyncThunk('user/fetchUser', async (token: string) => {
    const response = await axios.get<User>('https://localhost:7073/api/Auth/user', {
        headers: {
            Authorization: `Bearer ${token}`,
        },
    });
    return response.data;
});

//#region Products
export const fetchProducts = createAsyncThunk('products/fetchProducts', async () => {
    const response = await axios.get<Product[]>('https://localhost:7073/api/Product/GetProduct');
    return response.data;
});

export const addProduct = createAsyncThunk('products/addProduct', async (product: Product, { getState }) => {
    const state = getState() as RootState;
    const response = await axios.post<Product>('https://localhost:7073/api/Product/CreateProduct', product, {
        headers: {
            'Authorization': `Bearer ${state.login.user}`, // Ensure token is added if needed
        }
    });
    return response.data;
});

export const updateProduct = createAsyncThunk('products/updateProduct', async (product: Product) => {
    const response = await axios.put<Product>(`https://localhost:7073/api/Product/UpdateProductById`, product);
    return response.data;
});

export const deleteProduct = createAsyncThunk('products/deleteProduct', async (id: number) => {
    await axios.delete(`https://localhost:7073/api/Product/DeleteProductById?id=${id}`);
    return id;
});
//#endregion Products

//#region ProductCategory
export const fetchProductCategory = createAsyncThunk('productCategory/fetchProductCategory', async () => {
    const response = await axios.get<ProductCategory[]>('https://localhost:7073/api/ProductCategory/GetProductCategory');
    return response.data;
});

export const addProductCategory = createAsyncThunk('productCategory/addProductCategory', async (productCategory: ProductCategory, { getState }) => {
    const state = getState() as RootState;
    const response = await axios.post<ProductCategory>('https://localhost:7073/api/ProductCategory/CreateProductCategory', productCategory, {
        headers: {
            'Authorization': `Bearer ${state.login.user}`, // Ensure token is added if needed
        }
    });
    return response.data;
});

export const updateProductCategory = createAsyncThunk('productCategory/updateProductCategory', async (productCategory: ProductCategory) => {
    const response = await axios.put<ProductCategory>(`https://localhost:7073/api/ProductCategory/UpdateProductCategoryById`, productCategory);
    return response.data;
});

export const deleteProductCategory = createAsyncThunk('productCategory/deleteProductCategory', async (id: number) => {
    await axios.delete(`https://localhost:7073/api/ProductCategory/DeleteProductCategoryById?id=${id}`);
    return id;
});
//#endregion ProductCategory

//#region ProductVariant
export const fetchProductVariant = createAsyncThunk('productVariant/fetchProductVariant', async () => {
    const response = await axios.get<ProductVariant[]>('https://localhost:7073/api/ProductVariant/GetProductVariant');
    return response.data;
});

export const addProductVariant = createAsyncThunk('productVariant/addProductVariant', async (productVariant: ProductVariant, { getState }) => {
    const state = getState() as RootState;
    const response = await axios.post<ProductVariant>('https://localhost:7073/api/ProductVariant/CreateProductVariant', productVariant, {
        headers: {
            'Authorization': `Bearer ${state.login.user}`, // Ensure token is added if needed
        }
    });
    return response.data;
});

export const updateProductVariant = createAsyncThunk('productVariant/updateProductVariant', async (productVariant: ProductVariant) => {
    const response = await axios.put<ProductVariant>(`https://localhost:7073/api/ProductVariant/UpdateProductVariantById`, productVariant);
    return response.data;
});

export const deleteProductVariant = createAsyncThunk('productVariant/deleteProductVariant', async (id: number) => {
    await axios.delete(`https://localhost:7073/api/ProductVariant/DeleteProductVariantById?id=${id}`);
    return id;
});
//#endregion ProductVariant

//#region Menu
export const fetchMenu = createAsyncThunk('menu/fetchMenu', async () => {
    const response = await axios.get<Menu[]>('https://localhost:7073/api/Menu/GetMenu');
    return response.data;
});
//#endregion Menu

//#region MenuAccess
export const fetchMenuAccess = createAsyncThunk('menuAccess/fetchMenuAccess', async () => {
    const response = await axios.get<MenuAccess[]>('https://localhost:7073/api/MenuAccess/GetMenuAccess');
    return response.data;
});

export const fetchMenuAccessByRoleId = createAsyncThunk('menuAccess/fetchMenuAccessByRoleId', async (roleId: number) => {
    console.log(roleId, 'roleId in fetchMenuAccessByRoleId');
    const response = await axios.get<MenuAccess[]>(`https://localhost:7073/api/MenuAccess/GetMenuAccessByRoleId?roleid=${roleId}`);
    console.log(response.data, 'response.data in fetchMenuAccessByRoleId');
    return response.data;
});
//#endregion MenuAccess

//#region transaction
export const fetchTransactions = createAsyncThunk('transaction/fetchTransactions', async () => {
    const response = await axios.get<Transaction[]>('https://localhost:7073/api/Transaction/GetTransaction');
    return response.data;
});

export const fetchTransactionsDetail = createAsyncThunk('transactions/fetchTransactionsDetail', async () => {
    const response = await axios.get<TransactionDetail[]>('https://localhost:7073/api/TransactionDetail/GetTransactionDetail'); // Update with the correct endpoint
    return response.data;
});

//#endregion transaction
export const { login, logout, setAutenticated, startLoading, stopLoading, toggleActive, toggleActiveCategory, toggleActiveVariant } = configureSlice.actions;

export default configureSlice.reducer;
