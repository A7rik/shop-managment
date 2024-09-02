import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:44382/api",
});

export const getProducts = async (
  pageNumber = 1,
  pageSize = 2,
  sortedBy = "updated"
) => {
  try {
    const response = await api.get(
      `/Products/GetProductsForAllCategory/${pageNumber},${pageSize},${sortedBy}`
    );
    if (response.data.isSuccess) {
      return response.data.data;
    }
  } catch (error) {
    console.error("Error fetching products:", error);
    throw error;
  }
};

export const getCategories = async () => {
  try {
    const response = await api.get("/Categories/GetCategories");
    if (response.data.isSuccess) {
      return response.data.data;
    }
  } catch (error) {
    console.error("Error fetching Categories:", error);
    throw error;
  }
};
export const getProductsByCategory = async (
  categoryName,
  pageNumber = 1,
  pageSize = 2,
  sortedBy = "updated"
) => {
  try {
    const response = await api.get(
      `/Products/ProductsByCategoryName/${categoryName},${pageNumber},${pageSize},${sortedBy}`
    );
    if (response.data.isSuccess) {
      return response.data.data;
    }
  } catch (error) {
    console.error("Error fetching products:", error);
    throw error;
  }
};

export const getProductDetail = async (id) => {
  try {
    const response = await api.get(`/products/GetProductById/${id}`);
    if (response.data.isSuccess) {
      return response.data.data;
    }
  } catch (error) {
    console.error("Error fetching products:", error);
    throw error;
  }
};

export const getCategoryById = async (id) => {
  try {
    const response = await api.get(`/Categories/GetCategoryById/${id}`);
    if (response.data.isSuccess) {
      return response.data.data.name;
    }
  } catch (error) {
    console.error("Error fetching Category:", error);
    throw error;
  }
};

export const ListProductsByName = async (
  searchTerm,
  pageNumber = 1,
  pageSize = 2,
  sortedBy = "updated"
) => {
  try {
    const response = await api.get(
      `/products/ListProductsByName/${searchTerm},${pageNumber},${pageSize},${sortedBy}`
    );
    if (response.data.isSuccess) {
      return response.data.data;
    }
  } catch (error) {
    console.error("Error fetching ListProductsByName:", error);
    throw error;
  }
};

export const ListLatestProducts = async (page) => {
  try {
    const response = await api.get(`/products/latest/${page}`);
    if (response.data.isSuccess) {
      return response.data.data;
    }
  } catch (error) {
    console.error("Error fetching ListLatestProducts:", error);
    throw error;
  }
};

export const TotalNumberOfProducts = async () => {
  try {
    const response = await api.get(`/Products/TotalNumberOfProducts`);
    if (response.data.isSuccess) {
      return response.data.data;
    }
  } catch (error) {
    console.error("Error fetching Category:", error);
    throw error;
  }
};

// localStorage.setItem('token', response.data.token); // or sessionStorage.setItem('token', ...);
// localStorage.removeItem('token'); // or sessionStorage.removeItem('token');

// axios.interceptors.request.use(
//   (config) => {
//     const token = localStorage.getItem('token');
//     if (token) {
//       config.headers.Authorization = `Bearer ${token}`;
//     }
//     return config;
//   },
//   (error) => {
//     return Promise.reject(error);
//   }
// );

export const login = async (email, password) => {
  try {
    const response = await api.post("/Auth/Login", { email, password });
    if (response.data.isSuccess) {
      const token = response.data.data.token;
      const user = response.data.data.user;
      localStorage.setItem("token", token);
      localStorage.setItem("myUserId", user.id);
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      return user;
    }
  } catch (error) {
    console.error("Error fetching products:", error);
    throw error;
  }
};

export const signup = async (
  firstName,
  lastName,
  phoneNumber,
  email,
  password
) => {
  try {
    const response = await axios.post("/Auth/SignUp", {
      firstName,
      lastName,
      phoneNumber,
      email,
      password,
    });
    if (response.data.isSuccess) {
      const { token, user } = response.data.data;
      localStorage.setItem("token", token);
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      return user;
    }
  } catch (error) {
    console.error("Error fetching products:", error);
    throw error;
  }
};

export const createCategory = async (name = "khorak") => {
  const response = await api.post("/Categories/CreateCategory", { name });
  return response.data;
};

export const getUserProfile = async () => {
  const userid = localStorage.getItem("myUserId");

  if (userid) {
    const response = await api.get(`/Users/GetUserById/${userid}`);
    if (response.data.isSuccess) {
      return response.data.data;
    }
  }
};

export const updateProfileInfo = async (
  firstName,
  lastName,
  phoneNumber,
  email,
  password
) => {
  const userid = localStorage.getItem("myUserId");
  if (userid) {
    const response = await api.put(`/Users/UpdateUser/${userid}`, {
      firstName,
      lastName,
      phoneNumber,
      email,
      password,
    });
    if (response.data.isSuccess) {
      return response.data.data;
    }
  }
};

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers["Authorization"] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);
