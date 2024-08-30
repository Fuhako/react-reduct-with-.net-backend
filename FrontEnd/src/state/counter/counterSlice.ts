import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import exp from "constants";

interface CounterState {
    value: number;
};

const initialState: CounterState = {
    value: 0,
};

const configureSlice = createSlice({
    name: "counter",
    initialState,
    reducers: {
        increment: (state) => {
            state.value += 1;
        },
        decrement: (state) => {
            state.value -= 1;
        },
        incrementByAmount: (state, action: PayloadAction<number>) => {
            state.value += action.payload;
        }
    },
    extraReducers: (builder) => {
        builder.addCase(incrementAsync.pending, (state) => {
            console.log("IncrementAsync is pending");
        })
            .addCase(incrementAsync.fulfilled, (state, action) => {
                state.value += action.payload
            })
    }
});

export const incrementAsync = createAsyncThunk(
    "counter/incrementAsync",
    async (amount: number) => {
        await new Promise((resolve) => setTimeout(resolve, 1000));
        return amount;
    }
)

export const { increment, decrement, incrementByAmount } = configureSlice.actions;

export default configureSlice.reducer;