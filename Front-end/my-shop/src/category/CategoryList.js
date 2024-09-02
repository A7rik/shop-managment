import React, { useEffect, useState } from "react";
import { Link, useParams, useNavigate, useLocation } from "react-router-dom";
import PaginationComponent from "../template/PaginationComponent";
import {
  getCategories,
  getProducts,
  getProductsByCategory,
  ListProductsByName,
  TotalNumberOfProducts,
} from "../api";
import FilterMenuLeft from "./FilterMenuLeft";
import Product from "../products/Product";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import ProductH from "../products/ProductH";
import ScrollToTopOnMount from "../template/ScrollToTopOnMount";

function CategoryList() {
  const { categoryName } = useParams();
  const [searchTerm, setSearchTerm] = useState("");
  const [numberOfProducts, setNumberOfProducts] = useState("");
  const [categories, setCategories] = useState([]);
  const [products, setProducts] = useState([]);
  const [viewType, setViewType] = useState({ grid: true });
  const [page, setPage] = useState(1);
  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    const query = new URLSearchParams(location.search);

    const searchQuery = query.get("q");
    const pageQuery = query.get("page") || 1;
    setPage(pageQuery);
    if (searchQuery) {
      setSearchTerm(searchQuery);
      fetchProducts(searchQuery, pageQuery);
    } else {
      fetchProducts("", pageQuery);
    }
  }, [page, location.search]);

  useEffect(() => {
    setPage(1);
    new URLSearchParams(location.search).delete("q");
    fetchProducts("", 1);
    setSearchTerm("");
  }, [categoryName]);

  useEffect(() => {
    const fetchCategories = async () => {
      const response = await getCategories(page);
      setCategories(response);
    };

    fetchCategories();
  }, []);

  function changeViewType() {
    setViewType({
      grid: !viewType.grid,
    });
  }

  const fetchProducts = async (term, page = 1) => {
    let response;
    if (!term) {
      if (!categoryName) {
        response = await getProducts(page);
      } else {
        response = await getProductsByCategory(categoryName);
      }
    } else {
      response = await ListProductsByName(term, page);
    }

    setProducts(response);
    const response1 = await TotalNumberOfProducts();
    setNumberOfProducts(response1);
  };

  const handleSearchClick = async () => {
    if (!searchTerm) {
      navigate("/");
      return;
    }
    navigate(`?q=${searchTerm}&page=1`);
  };

  const handlePageChange = (newPage) => {
    navigate(`?q=${searchTerm}&page=${newPage}`);
  };

  return (
    <div className="container mt-5 py-4 px-xl-5">
      <ScrollToTopOnMount />
      <nav aria-label="breadcrumb" className="bg-custom-light rounded">
        <ol className="breadcrumb p-3 mb-0">
          <li className="breadcrumb-item">
            <Link
              className="text-decoration-none link-secondary"
              to="/categories"
            >
              All Categories
            </Link>
          </li>
          <li className="breadcrumb-item active" aria-current="page">
            {categoryName ? categoryName : "All Products"}
          </li>
        </ol>
      </nav>

      <div className="h-scroller d-block d-lg-none">
        <nav className="nav h-underline">
          {categories.map((category) => (
            <div className="h-link me-2">
              <Link
                key={category.id}
                to={`/categories/${category.name}`}
                className="btn btn-sm btn-outline-dark rounded-pill"
                replace
              >
                {category.name}
              </Link>
            </div>
          ))}
        </nav>
      </div>

      <div className="row mb-4 mt-lg-3">
        <div className="d-none d-lg-block col-lg-3">
          <div className="border rounded shadow-sm">
            <ul className="list-group list-group-flush rounded">
              <li className="list-group-item d-none d-lg-block">
                <h5 className="mt-1 mb-2">Browse</h5>
                <div className="d-flex flex-wrap my-2">
                  <FilterMenuLeft categories={categories} />
                </div>
              </li>
            </ul>
          </div>
        </div>
        <div className="col-lg-9">
          <div className="d-flex flex-column h-100">
            <div className="row mb-3">
              <div className="col-lg-3 d-none d-lg-block">
                <select
                  className="form-select"
                  aria-label="Default select example"
                  defaultValue=""
                >
                  <option value="">All Models</option>
                </select>
              </div>
              <div className="col-lg-9 col-xl-5 offset-xl-4 d-flex flex-row">
                <div className="input-group mb-3">
                  <input
                    className="form-control"
                    type="text"
                    placeholder="Search products..."
                    aria-label="search input"
                    value={searchTerm}
                    onChange={(e) =>
                      setSearchTerm(e.target.value.toLowerCase())
                    }
                  />
                  <button
                    className="btn btn-outline-dark"
                    onClick={handleSearchClick}
                  >
                    <FontAwesomeIcon icon={["fas", "search"]} />
                  </button>
                </div>

                <button
                  className="btn btn-outline-dark ms-2 d-none d-lg-inline"
                  onClick={changeViewType}
                >
                  <FontAwesomeIcon
                    icon={["fas", viewType.grid ? "th-list" : "th-large"]}
                  />
                </button>
              </div>
            </div>
            <div
              className={
                "row row-cols-1 row-cols-md-2 row-cols-lg-2 g-3 mb-4 flex-shrink-0 " +
                (viewType.grid ? "row-cols-xl-3" : "row-cols-xl-2")
              }
            >
              {Array.isArray(products) && products.length > 0 ? (
                products.map((product) =>
                  viewType.grid ? (
                    <Product key={product.id} product={product} />
                  ) : (
                    <ProductH key={product.id} product={product} />
                  )
                )
              ) : (
                <p>No products available.</p>
              )}
            </div>
            <div className="d-flex align-items-center mt-auto">
              <span className="text-muted small d-none d-md-inline">
                Showing {products.length} products
              </span>
              <PaginationComponent
                currentPage={page}
                totalProducts={numberOfProducts}
                onPageChange={handlePageChange}
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CategoryList;
