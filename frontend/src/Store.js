import { configureStore } from "@reduxjs/toolkit";
import { productsReducer } from "./slices/ProductsSlice";
import { productByIdReducer } from "./slices/ProductByIdSlice";
import { productPaginationReducer } from "./slices/ProductPaginationSlice";
import { categoryReducer } from "./slices/CategorySlice";
import { securityReducer } from "./slices/SecuritySlice";


export default configureStore({
    reducer: {
        products: productsReducer,
        product: productByIdReducer,
        productPagination: productPaginationReducer,
        category: categoryReducer,
        security: securityReducer
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware({serializableCheck: false})
})