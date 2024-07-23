// DashboardRoutes.js
import React from "react";
import { Routes, Route } from "react-router-dom";
import Dashboard from "./dashboard";
import CarouselEdit from "../carousel/carouselEdit";

const DashboardRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<Dashboard />}>
        <Route path="carousel" element={<CarouselEdit />} />
      </Route>
    </Routes>
  );
};

export default DashboardRoutes;
