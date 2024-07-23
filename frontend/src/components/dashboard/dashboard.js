// Dashboard.js
import React from "react";
import { Outlet, Link } from "react-router-dom";

const Sidebar = () => {
  return (
    <div className="sidebar h-auto">
      <nav>
        <ul>
          <li>
            <Link id="view_btn" className="btn" to="/dashboard/carousel">
              Anuncios
            </Link>
          </li>
        </ul>
      </nav>
    </div>
  );
};

const Dashboard = () => {
  return (
    <div className="container-fluid">
      <div className="row">
        <div className="col-md-2 p-0">
        <Sidebar />
        </div>
        <div className="col-md-10">
          <Outlet />
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
