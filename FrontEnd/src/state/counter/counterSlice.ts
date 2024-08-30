import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import exp from "constants";

interface CounterState {
    name: string;
    isAutenticated: boolean;
};

const initialState: CounterState = {
    name: "",
    isAutenticated: false
};

const configureSlice = createSlice({
    name: "counter",
    initialState,
    reducers: {
        login: (state, action: PayloadAction<string>) => {
            state.name = action.payload;
            state.isAutenticated = true;
        },
        logout: (state) => {
            state.name = '';
            state.isAutenticated = false;
        },
    },
    // extraReducers: (builder) => {
    //     builder.addCase(incrementAsync.pending, (state) => {
    //         console.log("IncrementAsync is pending");
    //     })
    //         .addCase(incrementAsync.fulfilled, (state, action) => {
    //             state.value += action.payload
    //         })
    // }
});

export const incrementAsync = createAsyncThunk(
    "counter/incrementAsync",
    async (amount: number) => {
        await new Promise((resolve) => setTimeout(resolve, 1000));
        return amount;
    }
)

export const { login, logout } = configureSlice.actions;

export default configureSlice.reducer;