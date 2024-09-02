import Template from "./template/Template";
import ProductDetail from "./products/detail/ProductDetail";
import Landing from "./landing/Landing";
import Categories from "./category/CategoryList";
import SignupPage from "./User/SignupPage";
import LoginPage from "./User/LoginPage";
import UserProfile from "./User/UserProfile";
import UpdateProfile from "./User/UpdateProfile";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

function App() {
  return (
    <Template>
        <Routes>
          <Route path="/user-profile" element={<UserProfile />} />
          <Route path="/update-profile" element={<UpdateProfile />} />
          <Route path="/Signup" element={<SignupPage />} />
          <Route path="/Login" element={<LoginPage />} />
          <Route path="/categories/:categoryName" element={<Categories />} />
          <Route path="/categories" element={<Categories />} />
          <Route path="/products/:slug" element={<ProductDetail />} />
          <Route path="/" element={<Landing />} />
        </Routes>
    </Template>
  );
}

export default App;
