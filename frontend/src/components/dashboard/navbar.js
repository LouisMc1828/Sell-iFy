// src/components/Navbar.js
import React from 'react';
import { Link } from "react-router-dom";


const Navbar = () => {
  return (
    <nav className="navbar">
      <ul>
        <Link id="view_btn" className="btn" to = "">
          Inventario
        </Link>
        <Link id="view_btn" className="btn" to = "/dashboard/carousel">
          Anuncios
        </Link>
        {/* <li><a href="#dashboard">Inventario</a></li>
        <li><a href="#reports">Anucios</a></li> */}
      </ul>
    </nav>
  );
};

export default Navbar;
