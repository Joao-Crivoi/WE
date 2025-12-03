import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./pages/Login";
import Produtos from "./pages/Produtos";
import ProtectedRoute from "./components/ProtectedRoute";

export default function App() {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route
        path="/produtos"
        element={
          <ProtectedRoute>
            <Produtos />
          </ProtectedRoute>
        }
      />
      <Route path="*" element={<Navigate to="/products" />} />
    </Routes>
  );
}
