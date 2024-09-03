import { configureStore } from '@reduxjs/toolkit';
import counterReducer from "./counter/counterSlice";

export const store = configureStore({
    reducer: {
        login: counterReducer,
        products: counterReducer,
        loading: counterReducer,
        productCategory: counterReducer,
        productVariant: counterReducer,
        menu: counterReducer,
        menuAccessByRoleId: counterReducer,
        manageTransactions: counterReducer,
        transactions: counterReducer
    },
});

export type RootState = ReturnType<typeof store.getState>;

export type AppDispatch = typeof store.dispatch;