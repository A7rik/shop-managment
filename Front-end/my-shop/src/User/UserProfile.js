import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import {getUserProfile} from "../api"; 

const UserProfile = () => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const fetchUserData = async () => {
        const response = await getUserProfile();
        setUser(response);
    };
    fetchUserData();
  }, []);

  if (!user) return <div>Loading...</div>;

  return (
    <div className="container">
      <h2 className="mt-4">User Profile</h2>
      <div className="card p-4">
        <p><strong>First Name:</strong> {user.firstName}</p>
        <p><strong>Last Name:</strong> {user.lastName}</p>
        <p><strong>Email:</strong> {user.email}</p>
        {user.phoneNumber && <p><strong>Phone Number:</strong> {user.phoneNumber}</p>}
        <Link to="/update-profile" className="btn btn-outline-dark mt-3">Update Profile</Link>
      </div>
    </div>
  );
};

export default UserProfile;
