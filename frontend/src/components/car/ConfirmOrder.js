import React, { Fragment, useEffect } from "react";
import MetaData from "../layout/MetaData";
import { CheckoutSteps } from "./CheckoutSteps";
import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { resetUpdateStatus } from "../../slices/OrderSlice";
import { useAlert } from "react-alert";
import { saveOrder } from "../../actions/OrderAction";

const ConfirmOrder = () => {
  const alert = useAlert();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const {
    shoppingCarItems,
    shoppingCarId,
    total,
    cantidad,
    subtotal,
    precioEnvio,
    impuesto,
  } = useSelector((state) => state.car);

  const item = shoppingCarItems.slice();

  const { isUpdated, errores } = useSelector((state) => state.order);

  const { user, dirEnvio } = useSelector((state) => state.security);

  useEffect(() => {
    if (isUpdated) {
      navigate("/payment");
      dispatch(resetUpdateStatus({}));
    }
    if (errores) {
      errores.map((error) => alert.error(error));
    }
  }, [dispatch, navigate, isUpdated, errores, alert]);

  const submitHandler = () => {
    const request = {
        shoppingCarId,
    };
    dispatch(saveOrder(request));
  };

  return (
    <Fragment>
      <MetaData titulo="Confirmacion De Orden" />
      <CheckoutSteps envio confirmacion />
      <div className="row d-flex justify-content-between">
        <div className="col-12 col-lg-8 mt-5 order-confirm">
          <h4 className="mb-3">Informacion De Envio</h4>
          <p>
            <b>Name:</b> {user.nombre + " " + user.apellido}
          </p>
          <p className="mb-4">
            <b>Address:</b>
            {(dirEnvio ? dirEnvio.direccion : "") +
              ", " +
              (dirEnvio ? dirEnvio.ciudad : "") +
              ", " +
              (dirEnvio ? dirEnvio.departamento : "") +
              ", " +
              (dirEnvio ? dirEnvio.codigoPostal : "") +
              ", " +
              (dirEnvio ? dirEnvio.pais : "")}{" "}
          </p>

          <hr />
          <h4 className="mt-4">Carro De Compras: </h4>

          {shoppingCarItems.map((item) => (
            <Fragment key={item.id}>
              <hr />
              <div className="cart-item my-1">
                <div className="row">
                  <div className="col-4 col-lg-2">
                    <img
                      src={item.imagen}
                      alt={item.producto}
                      height="45"
                      width="65"
                    />
                  </div>

                  <div className="col-5 col-lg-6">
                    <Link to={`/product/${item.productoId}`}>
                        {item.producto}
                    </Link>
                  </div>

                  <div className="col-4 col-lg-4 mt-4 mt-lg-0">
                    <p>
                      {item.cantidad} x ${item.precio} = <b>${item.totalLine}</b>
                    </p>
                  </div>
                </div>
              </div>
              <hr />
            </Fragment>
          ))}
        </div>

        <div className="col-12 col-lg-3 my-4">
          <div id="order_summary">
            <h4>Orden De Compra</h4>
            <hr />
            <p>
              SubTotal: <span className="order-summary-values">${subtotal}</span>
            </p>
            <p>
              Precio De Envio: <span className="order-summary-values">${precioEnvio}</span>
            </p>
            <p>
              Impuesto: <span className="order-summary-values">${impuesto}</span>
            </p>

            <hr />

            <p>
              Total: <span className="order-summary-values">${total}</span>
            </p>

            <hr />
            <button id="checkout_btn" className="btn btn-primary btn-block" onClick={submitHandler}>
              Confirmar Orden De Compra
            </button>
          </div>
        </div>
      </div>
    </Fragment>
  );
};

export default ConfirmOrder;
