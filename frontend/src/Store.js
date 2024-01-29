import { configureStore } from "@reduxjs/toolkit";
import { productsReducer } from "./slices/ProductsSlice";

export default configureStore({
    reducer: {
        products: productsReducer
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware({serializableCheck: false})
})