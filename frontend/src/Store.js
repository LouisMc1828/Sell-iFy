import { configureStore } from "@reduxjs/toolkit";
import { productsReducer } from "./slices/ProductsSlice";
import { productByIdReducer } from "./slices/ProductByIdSlice";
import { productPaginationReducer } from "./slices/ProductPaginationSlice";
import { categoryReducer } from "./slices/CategorySlice";
import { securityReducer } from "./slices/SecuritySlice";
import { forgotPasswordReducer } from "./slices/ForgotPasswordSlice";
import { resetPasswordReducer } from "./slices/ResetPasswordSlice";
import { carReducer } from "./slices/CarSlice";
import { countryReducer } from "./slices/CountrySlice";
import { orderReducer } from "./slices/OrderSlice";


export default configureStore({
    reducer: {
        products: productsReducer,
        product: productByIdReducer,
        productPagination: productPaginationReducer,
        category: categoryReducer,
        security: securityReducer,
        forgotPassword: forgotPasswordReducer,
        resetPassword: resetPasswordReducer,
        car: carReducer,
        country: countryReducer,
        order: orderReducer
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware({serializableCheck: false})
})