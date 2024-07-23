import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

import "./App.css";
import { getCategories } from "./actions/CategoryAction";
import { getCountries } from "./actions/CountryAction";
import { getShoppingCar } from "./actions/CarAction";
import { loadUser } from "./actions/UserAction";
import { useDispatch } from "react-redux";
import { useEffect } from "react";
import Car from "./components/car/Car";
import CarouselEdit from "./components/carousel/carouselEdit.js";
import ConfirmOrder from "./components/car/ConfirmOrder";
// import Dashboard from "./components/dashboard/dashboardMain";
import DashboardRoutes from "./components/dashboard/dashboardRoutes.js";
import Footer from "./components/layout/Footer";
import ForgotPassword from "./components/security/ForgotPassword";
import Header from "./components/layout/Header";
import Home from "./components/Home";
import Login from "./components/security/Login";
import NewPassword from "./components/security/NewPassword";
import OrderSuccess from "./components/car/OrderSuccess";
import Payment from "./components/car/Payment";
import ProductDetail from "./components/product/ProductDetail";
import Profile from "./components/security/Profile";
import ProtectedRoute from "./components/route/ProtectedRoute";
import Register from "./components/security/Register";
import Shipping from "./components/car/Shipping";
import UpdatePassword from "./components/security/UpdatePassword";
import UpdateProfile from "./components/security/UpdateProfile";

function App() {
  const dispatch = useDispatch();

  const token = localStorage.getItem("token");

  useEffect(() => {
    dispatch(getCategories({}));
    dispatch(getShoppingCar({}));
    dispatch(getCountries({}));

    if (token) {
      dispatch(loadUser({}));
    }
  }, [dispatch, token]);
  return (
    <Router>
      <div class="parent">
        <div class="div1">
          <Header />
        </div>

        <div class="div2">
          <div className="App">
            <div className="w-100">
              <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/product/:id" element={<ProductDetail />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/car" element={<Car />} />
                {/* <Route path="/dashboard/carousel" element={<CarouselEdit />} /> */}
                <Route exact path="/me" element={<ProtectedRoute />}>
                  <Route path="/me" element={<Profile />} />
                </Route>
                <Route path="/dashboard/*" element={<ProtectedRoute />}>
                  <Route path="*" element={<DashboardRoutes />} />
                </Route>
                {/* <Route
              exact
              path="/dashboard/carousel"
              element={<ProtectedRoute />}
            >
              <Route path="/dashboard/carousel" element={<CarouselEdit />} />
            </Route> */}
                <Route exact path="/me/update" element={<ProtectedRoute />}>
                  <Route path="/me/update" element={<UpdateProfile />} />
                </Route>
                <Route path="/password/forgot" element={<ForgotPassword />} />
                <Route
                  path="/password/reset/:token"
                  element={<NewPassword />}
                />
                <Route
                  exact
                  path="/password/update"
                  element={<ProtectedRoute />}
                >
                  <Route path="/password/update" element={<UpdatePassword />} />
                </Route>
                <Route exact path="/shipping" element={<ProtectedRoute />}>
                  <Route path="/shipping" element={<Shipping />} />
                </Route>
                <Route exact path="/order/confirm" element={<ProtectedRoute />}>
                  <Route path="/order/confirm" element={<ConfirmOrder />} />
                </Route>
                <Route exact path="/payment" element={<ProtectedRoute />}>
                  <Route path="/payment" element={<Payment />} />
                </Route>
                <Route exact path="/success" element={<ProtectedRoute />}>
                  <Route path="/success" element={<OrderSuccess />} />
                </Route>
              </Routes>
            </div>
          </div>
        </div>

        <div class="div3">
          <Footer />
        </div>
      </div>
    </Router>
  );
}

export default App;
