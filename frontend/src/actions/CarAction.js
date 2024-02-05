import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";




export const getShoppingCar = createAsyncThunk(
    "shoppingCar/GetById",
    async({ rejectWithValue}) => {
        try {
            const shoppingCarId = localStorage.getItem("shoppingCarId") ?? '00000000-0000-0000-0000-000000000000';
            const {data} = await axios.get(`/api/v1/ShoppingCar/${shoppingCarId}`);
            return data;
        } catch (err) {
            return rejectWithValue(err.response.data.message);
        }
    }
);

export const addItemShoppingCar = createAsyncThunk(
    "shoppingCar/update",
    async(params, {rejectWithValue}) => {
        try {
            const { shoppingCarItems, item, cantidad } = params;
            let items = [];
            items = shoppingCarItems.slice();
            const indice = shoppingCarItems.findIndex(i => i.productId === item.productId);

            if (indice === -1)
            {
                items.push(item);
            }else{
                let cantidad_ = items[indice].cantidad;
                var total = cantidad_ + cantidad;
                let itemNewClone = {...items[indice]};
                itemNewClone.cantidad = total;
                items[indice] = itemNewClone;
            }

            var request = {
                ShoppingCarItems: items
            }

            const requestConfig = {
                headers: {
                    "Content-Type": "application/json",
                }
            }

            const {data} = await axios.put(
                `/api/v1/ShoppingCar/${params.shoppingCarId}`,
                request,
                requestConfig
            )

            return data;
        } catch (err) {
            rejectWithValue(err.response.data.message);
        }
    }
)

export const removeItemShoppingCar = createAsyncThunk(
    "shoppingCar/removeItem",
    async (params, {rejectWithValue}) => {
        try {
            const requestConfig = {
                headers: {
                    "Content-Type": "application/json"
                }
            };
            const {data} = await axios.delete(
                `/api/v1/ShoppingCar/item/${params.id}`,
                params,
                requestConfig
            );

            return data;
        } catch (err) {
            return rejectWithValue(err.response.data.message);
        }
    }
)




export const saveAddressInfo = createAsyncThunk(
    "shoppingCar/saveAddressInfo",
    async (params, {rejectWithValue}) => {
        try
        {
            const requestConfig = {
                headers : {
                    "Content-Type": "application/json",
                }
            };
            const { data } = await axios.post(
                `api/v1/order/address`,
                params,
                requestConfig
            );

            return data;
        }
        catch (err)
        {
            return rejectWithValue(err.response.data.message);
        }
    }
)