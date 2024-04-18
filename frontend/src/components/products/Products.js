import React from "react";
import Loader from "../layout/Loader";
import Product from "../product/Product";

const Products = ({ products, col, loading }) => {
    return loading ? (
      <Loader />
    ) : (
      <React.Fragment>
        {products ? (
          products.map((productElement) => (
            <Product
              key={productElement.id}
              product={productElement}
              col={col}
            />
          ))
        ) : (
          <Loader />
        )}
      </React.Fragment>
    );
};

export default Products;
