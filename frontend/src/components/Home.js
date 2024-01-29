
import React, { Fragment, useEffect } from "react";
import MetaData from "./layout/MetaData";
import { useDispatch, useSelector } from "react-redux";
import { getProducts } from "../actions/ProductsAction";
import Product from "./product/Product";
import Loader from "./layout/Loader";
import { useAlert } from "react-alert";


const Home = () => {
  const dispatch = useDispatch();
  const { products, loading, error } = useSelector((state) => state.products);
  const alert = useAlert();
  console.log('error===>', error);



  useEffect(()=> {
    if (error != null)
    {
      return alert.error(error);
    }
    dispatch(getProducts());
  }, [dispatch, alert, error])


  if(loading)
  {
    return (<Loader/>);
  }

  return (
    <Fragment>
      <MetaData titulo={'Productos'}/>
      <section id="products" className="container mt-5">
        <div className="row">


        {
          products ? products.map(
            (productElement) => (<Product key = {productElement.id} product = {productElement} col = {4}/>)
          ): 'No hay product'
        }

        </div>
      </section>
    </Fragment>
      
  );
};

export default Home;
