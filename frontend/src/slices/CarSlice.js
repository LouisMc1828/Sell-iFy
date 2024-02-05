import { createSlice } from "@reduxjs/toolkit";
import { addItemShoppingCar, getShoppingCar, removeItemShoppingCar } from "../actions/CarAction";
import { confirmPayment } from "../actions/OrderAction";

const initialState = {
    shoppingCarId: '',
    shoppingCarItems:[],
    loading: false,
    error: null,
    total: 0,
    cantidad: 0,
    subtotal: 0,
    impuesto: 0,
    precioEnvio: 0,
};

export const carSlice = createSlice({
    name: "carItems",
    initialState,
    extraReducers: {
        [getShoppingCar.pending]: (state) => {
            state.loading = true;
            state.error = null;
        },
        [getShoppingCar.fulfilled]: (state, { payload }) => {
            console.log('payload', payload);
            localStorage.setItem('shoppingCarId', payload.shoppingCarId);
            state.shoppingCarId = payload.shoppingCarId;
            state.shoppingCarItems = payload.shoppingCarItems ?? [];
            state.loading = false;
            state.error = null;
            state.total = payload.total;
            state.cantidad = payload.cantidad;
            state.subtotal = payload.subtotal;
            state.impuesto = payload.impuesto;
            state.precioEnvio = payload.precioEnvio;
        },
        [getShoppingCar.rejected]: (state, action) => {
            state.loading = false;
            state.error = action.payload;
        },

        [addItemShoppingCar.pending]: (state) => {
            state.loading = true;
            state.error = null;
        },
        [addItemShoppingCar.fulfilled]: (state, { payload }) => {
            state.shoppingCarId = payload.shoppingCarId;
            state.shoppingCarItems = payload.shoppingCarItems ?? [];
            state.loading = false;
            state.error = null;
            state.total = payload.total;
            state.cantidad = payload.cantidad;
            state.subtotal = payload.subtotal;
            state.impuesto = payload.impuesto;
            state.precioEnvio = payload.precioEnvio;
        },
        [addItemShoppingCar.rejected]: (state, action) => {
            state.loading = false;
            state.error = action.payload;
        },

        [removeItemShoppingCar.pending]: (state) => {
            state.loading = true;
            state.error = null;
        },
        [removeItemShoppingCar.fulfilled]: (state, { payload }) => {
            state.shoppingCarId = payload.shoppingCarId;
            state.shoppingCarItems = payload.shoppingCarItems ?? [];
            state.loading = false;
            state.error = null;
            state.total = payload.total;
            state.cantidad = payload.cantidad;
            state.subtotal = payload.subtotal;
            state.impuesto = payload.impuesto;
            state.precioEnvio = payload.precioEnvio;
        },
        [removeItemShoppingCar.rejected]: (state, action) => {
            state.loading = false;
            state.error = action.payload;
        },

        [confirmPayment.pending]: (state) => {
            state.loading = true;
            state.error = null;
        },
        [confirmPayment.fulfilled]: (state) => {
            state.shoppingCarItems = [];
            state.loading = false;
            state.error = null;
            state.total = 0;
            state.cantidad = 0;
            state.subtotal = 0;
            state.impuesto = 0;
            state.precioEnvio = 0;
        },
        [confirmPayment.rejected]: (state, action) => {
            state.loading = false;
            state.error = action.payload;
        },
    },
});



export const carReducer = carSlice.reducer;