/* eslint-disable jsx-a11y/alt-text */
import React, { Fragment } from "react";
import "../../App.css";
import Search from "./Search";
import { useDispatch, useSelector } from "react-redux";
import { useAlert } from "react-alert";
import { logout } from "../../slices/SecuritySlice";
import { Link } from "react-router-dom";

const Header = () => {

  const { shoppingCarItems } = useSelector(state => state.car);
const { user, loading } = useSelector((state) => state.security);
const dispatch = useDispatch();
const alert = useAlert();

const logoutHandler = () => {
  dispatch(logout());
  alert.success('Cerro Sesion Exitosamente');
}




  return (
    <Fragment>
      <nav className="navbar row">
        <div className="col-12 col-md-3">
          <div className="navbar-brand">
            <Link to="/">
              <img src="/images/logo1.png" />
            </Link>
          </div>
        </div>

        <div className="col-12 col-md-6 mt-2 mt-md-0">
          <Search/>
        </div>

        <div className="col-12 col-md-3 mt-4 mt-md-0 text-center">

        <Link to="/car">
        <span id="car" className="ml-3">
          Car
        </span>
        <span className="ml-1" id="car_count">
          { shoppingCarItems.length }
        </span>
        </Link>

        {
        user ?
          (
            <div className="ml-4 dropdown d-inline">
          <Link
            to = "#!"
            className = "btn dropdown-toggle text-white mr-4"
            type = "button"
            data-toggle = "dropdown"
            aria-haspopup = "true"
            aria-expanded = "false"
          >
            <figure className = "avatar avatar-nav">
              <img
                src = { user && user.avatar }
                alt = { user && user.nombre }
                className = "rounded-circle"
              />
            </figure>
            <span>{ user && user.nombre }</span>
          </Link>

          <div className="dropdown-menu" aria-labelledby="dropDownMenuButton">
            { user && user.roles.includes("ADMIN") && (
            <Link className="dropdown-item" to="/dashboard">
              Dashboard
            </Link>
            )
            }

            <Link className="dropdown-item" to="/orders/me">
              Ordenes
            </Link>
            <Link className="dropdown-item" to="/me">
              Perfil
            </Link>
            <Link className="dropdown-item" to="/" onClick={logoutHandler}>
              LogOut
            </Link>
          </div>
        </div>
          )
          :  (
            !loading && (
              <Link className="btn ml-4" id="login_btn" to="/login">
                Login
              </Link>
            )
          
          )
        }

        

          
        </div>
      </nav>
    </Fragment>
  );
};

export default Header;
