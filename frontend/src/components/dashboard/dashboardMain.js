// src/components/Dashboard.js
import React from "react";
import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import CarouselEdit from "../carousel/carouselEdit";
/*import Carousel from '../carousel/carouselMain';
import Navbar from './navbar';
import Inventory from '../inventory/inventoryMain';*/



const Carousel = () => {
  return (
    <div className="carousel">
      <h2>Anuncios</h2>
      <p>Carousel</p>
    </div>
  );
};

const Dashboard = () => {
  return <>

  <CarouselEdit/>
  </>
  
};

export default Dashboard;

/* const Sidebar = () => {
    return (
      <div className="sidebar">
        <h2>Dashboard</h2>
        <nav>
          <ul>
            <li><Link id="view_btn" className="btn" to = "/dashboard/carousel">Anuncios</Link></li>
          </ul>
        </nav>
      </div>
    );
  }; */

/* <div className='w-100'>
    <Sidebar/>
      {/*<Carousel/>
      <Inventory/> 
    </div>*/
