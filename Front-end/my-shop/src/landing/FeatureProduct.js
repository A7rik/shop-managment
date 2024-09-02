import React from "react";
import { Link } from "react-router-dom";
import Image from "../product-17.png";


function FeatureProduct({ product }) {
  return (
    <div className="col">
      <div className="card shadow-sm">
        <img
          className="card-img-top bg-dark cover"
          height="240"
          alt={product.name}
          src={Image}
        />
        <div className="card-body">
          <h5 className="card-title text-center">{product.name}</h5>
          <p className="card-text text-center text-muted">{product.price} Toman</p>
          <div className="d-grid gap-2">
            <Link to={`/products/${product.id}`} className="btn btn-outline-dark" replace>
              Detail
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
}

export default FeatureProduct;
