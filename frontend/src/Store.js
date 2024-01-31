import { configureStore } from "@reduxjs/toolkit";
import { productsReducer } from "./slices/ProductsSlice";
import { productByIdReducer } from "./slices/ProductByIdSlice";
import { productPaginationReducer } from "./slices/ProductPaginationSlice";
import { categoryReducer } from "./slices/CategorySlice";
import { securityReducer } from "./slices/SecuritySlice";
import { forgotPasswordReducer } from "./slices/ForgotPasswordSlice";
import { resetPasswordReducer } from "./slices/ResetPasswordSlice";


export default configureStore({
    reducer: {
        products: productsReducer,
        product: productByIdReducer,
        productPagination: productPaginationReducer,
        category: categoryReducer,
        security: securityReducer,
        forgotPassword: forgotPasswordReducer,
        resetPassword: resetPasswordReducer
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware({serializableCheck: false})
})