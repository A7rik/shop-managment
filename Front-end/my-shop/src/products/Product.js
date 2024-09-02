import { Link } from "react-router-dom";
import Image from "../product-17.png";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

function Product({ product }) {
  const { id, name, price, imageUrl, percentOff } = product;
  let finalPrice = price;
  let discountBadge = null;

  if (percentOff && percentOff > 0) {
    finalPrice = price - (percentOff * price) / 100;
    discountBadge = (
      <div
        className="badge bg-dim py-2 text-white position-absolute"
        style={{ top: "0.5rem", right: "0.5rem" }}
      >
        {percentOff}% OFF
      </div>
    );
  }

  return (
    <div className="col">
      <div className="card shadow-sm">
        <Link to={`/products/${id}`} replace>
          {discountBadge}
          <img
            className="card-img-top bg-dark cover"
            height="200"
            alt={name}
            src ={Image}
            // src={imageUrl || "/product-17.png"}
          />
        </Link>
        <div className="card-body">
          <h5 className="card-title text-center text-dark text-truncate">
            {name}
          </h5>
          <p className="card-text text-center text-muted mb-0">
            {finalPrice.toLocaleString()} Toman
          </p>
          <div className="d-grid d-block">
            <button className="btn btn-outline-dark mt-3">
              <FontAwesomeIcon icon={["fas", "cart-plus"]} /> Add to cart
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Product;
