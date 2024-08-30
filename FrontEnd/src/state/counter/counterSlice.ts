import { createSlice } from "@reduxjs/toolkit";
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
    },
});

export const { increment, decrement } = configureSlice.actions;

export default configureSlice.reducer;