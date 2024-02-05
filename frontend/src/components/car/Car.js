import React, { Fragment } from "react";
import MetaData from "../layout/MetaData";
import { Link, useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { addItemShoppingCar, removeItemShoppingCar } from "../../actions/CarAction";

const Car = () => {
  const { shoppingCarItems, shoppingCarId, total, cantidad } = useSelector(
    (state) => state.car
  );
  const { isAuthenticated } = useSelector((state) => state.security);

  const navigate = useNavigate();
  const dispatch = useDispatch();


    const increaseQty = (item) => {
      const newItem = {...item};
      const newCantidadItem = item.cantidad + 1;
      if(newCantidadItem > item.stock) return;

      newItem.cantidad = newCantidadItem;

      let params = {
        cantidad: 1,
        productId: item.productId,
        shoppingCarItems,
        shoppingCarId,
        item: newItem
      };
      dispatch(addItemShoppingCar(params));
    }
    const decreaseQty = (item) => {
      const newItem = {...item};
      const newCantidadItem = item.cantidad - 1;
      if(newCantidadItem <= 0) return;

      newItem.cantidad = newCantidadItem;

      let params = {
        cantidad: -1,
        productId: item.productId,
        shoppingCarItems,
        shoppingCarId,
        item: newItem
      };
      dispatch(addItemShoppingCar(params));
    }

    const removeCarItemHandler = (item) => {
      const request = {
        id: item.id
      }
      dispatch(removeItemShoppingCar(request));
    }

    const checkoutHandler = () => {
      if(isAuthenticated)
      {
        navigate("/shipping");
      }
      else
      {
        navigate("/login?redirect=shipping");
      }
    }




  return (
    <Fragment>
      <MetaData title={"El Carrito de compras"} />

      {shoppingCarItems.length === 0 ? (
        <h2 className="mt-5"> El Carrito esta vacio</h2>
      ) : (
        <Fragment>
          <h2 className="mt-5">
            {" "}
            Tu Carro Tiene: <b> {shoppingCarItems.length} items</b>
          </h2>
          <div className="row d-flex justify-content-between">
            <div className="col-12 col-lg-8">
              {shoppingCarItems.map((item) => (
                <Fragment key={item.productId}>
                  <hr />
                  <div className="car-item">
                    <div className="row">
                      <div className="col-4 col-lg-3">
                        <img
                          src={item.imagen}
                          alt={item.producto}
                          height="90"
                          width="115"
                        />
                      </div>

                      <div className="col-5 col-lg-3">
                        <Link to={`/product/${item.productId}`} > {item.producto}</Link>
                      </div>

                      <div className="col-4 col-lg-2 mt-4 mt-lg-0">
                        <p id="card_item_price">{item.precio}</p>
                      </div>

                      <div className="col-4 col-lg-3 mt-4 mt-lg-0">
                        <div className="stockCounter d-inline">
                          <span className="btn btn-danger minus" onClick={() => decreaseQty(item)}>-</span>
                          <input
                            type="number"
                            className="form-control count d-inline"
                            value={item.cantidad}
                            readOnly
                          />

                          <span className="btn btn-primary plus" onClick={() => increaseQty(item)}>+</span>
                        </div>
                      </div>

                      <div className="col-4 col-lg-1 mt-4 mt-lg-0">
                        <i
                          id="delete_cart_item"
                          className="fa fa-trash btn btn-danger"
                          onClick={()=> removeCarItemHandler(item)}
                        ></i>
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
                  Subtotal:{" "}
                  <span className="order-summary-values">{cantidad} (Unidades)</span>
                </p>
                <p>
                  Est. total:{" "}
                  <span className="order-summary-values">$ {total} </span>
                </p>

                <hr />
                <button id="checkout_btn" className="btn btn-primary btn-block" conClick={checkoutHandler}>
                  Check out
                </button>
              </div>
            </div>
          </div>
        </Fragment>
      )}
    </Fragment>
  );
};

export default Car;
