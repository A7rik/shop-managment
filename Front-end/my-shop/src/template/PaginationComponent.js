function PaginationComponent({ currentPage, totalProducts, onPageChange }) {
  const pageSize = 2;
  const totalPages = Math.ceil(totalProducts / pageSize);

  // Convert currentPage to a number to ensure arithmetic operations work correctly
  const currentPageNumber = Number(currentPage);

  return (
    <nav aria-label="Page navigation example" className="ms-auto">
      <ul className="pagination my-0">
        <li className={`page-item ${currentPageNumber === 1 ? "disabled" : ""}`}>
          <button
            className="page-link"
            onClick={() => onPageChange(Math.max(currentPageNumber - 1, 1))}
            disabled={currentPageNumber === 1}
          >
            Previous
          </button>
        </li>
        {Array.from({ length: totalPages }, (_, i) => (
          <li
            key={i + 1}
            className={`page-item ${currentPageNumber === i + 1 ? "active" : ""}`}
          >
            <button
              className="page-link"
              onClick={() => onPageChange(i + 1)}
            >
              {i + 1}
            </button>
          </li>
        ))}
        <li
          className={`page-item ${currentPageNumber === totalPages ? "disabled" : ""}`}
        >
          <button
            className="page-link"
            onClick={() => onPageChange(Math.min(currentPageNumber + 1, totalPages))}
            disabled={currentPageNumber === totalPages}
          >
            Next
          </button>
        </li>
      </ul>
    </nav>
  );
}

export default PaginationComponent;
