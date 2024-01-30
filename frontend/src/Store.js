import { configureStore } from "@reduxjs/toolkit";
import { productsReducer } from "./slices/ProductsSlice";
import { productByIdReducer } from "./slices/ProductByIdSlice";
import { productPaginationReducer } from "./slices/ProductPaginationSlice";
import { categoryReducer } from "./slices/CategorySlice";


export default configureStore({
    reducer: {
        products: productsReducer,
        product: productByIdReducer,
        productPagination: productPaginationReducer,
        category: categoryReducer
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware({serializableCheck: false})
})