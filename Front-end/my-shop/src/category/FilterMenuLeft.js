import React from "react";
import { Link } from "react-router-dom";

function FilterMenuLeft({ categories }) {
  return (
    <div>
      {categories.map((category) => (
        <Link
          key={category.id}
          to={`/categories/${category.name}`}
          className="btn btn-sm btn-outline-dark rounded-pill me-2 mb-2"
        >
          {category.name}
        </Link>
      ))}
    </div>
  );
}

export default FilterMenuLeft;
