import { Link } from "react-router-dom";
import Localimg from "../../product-17.png";

function RelatedProduct({ product }) {
  const { id, name, price, imageUrl, percentOff } = product;
  let offPrice = `${price} Toman`;

  let discountBadge = null;

  if (percentOff && percentOff > 0) {
    discountBadge = (
      <div
        className="badge bg-dim py-2 text-white position-absolute"
        style={{ top: "0.5rem", right: "0.5rem" }}
      >
        {percentOff}% OFF
      </div>
    );
    offPrice = (
      <>
        <del>{price} Toman</del> {price - (percentOff * price) / 100} Toman
      </>
    );
  }

  return (
    <Link
      to={`/products/${id}`}
      className="col text-decoration-none"
      replace
    >
      <div className="card shadow-sm">
        {discountBadge}
        <img
          className="card-img-top bg-dark cover"
          height="200"
          alt={name}
          src={Localimg}
        />
        <div className="card-body">
          <h5 className="card-title text-center text-dark text-truncate">
            {name}
          </h5>
          <p className="card-text text-center text-muted">{offPrice}</p>
        </div>
      </div>
    </Link>
  );
}

export default RelatedProduct;
