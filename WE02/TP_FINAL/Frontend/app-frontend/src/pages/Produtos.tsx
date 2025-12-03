import React, { useEffect, useState } from "react";
import api from "../api";
import type { Product } from "../types/types";
import ProductForm from "../components/ProductForm";
import { useAuth } from "../contexts/AuthContext";

export default function Products() {
  const [products, setProducts] = useState<Product[]>([]);
  const [editing, setEditing] = useState<Product | null>(null);
  const [showForm, setShowForm] = useState(false);
  const [loading, setLoading] = useState(true);

  const auth = useAuth();

  const load = async () => {
    try {
      const res = await api.get("/Produtos");
      setProducts(res.data);
    } catch (err) {
      console.error("Erro ao carregar produtos:", err);
      alert("Erro ao carregar produtos.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    load();
  }, []);

  const handleCreate = () => {
    setEditing(null);
    setShowForm(true);
  };

  const handleEdit = (p: Product) => {
    setEditing(p);
    setShowForm(true);
  };

  const handleDelete = async (id?: number) => {
    if (!id) return;
    if (!confirm("Confirma exclusão?")) return;

    try {
      await api.delete(`Produtos/${id}`);
      await load();
    } catch (err) {
      console.error("Erro ao excluir:", err);
      alert("Erro ao excluir produto.");
    }
  };

  const onSaved = async () => {
    setShowForm(false);
    await load();
  };

  if (loading) return <p className="text-center mt-10">Carregando...</p>;

  return (
    <div className="p-6 max-w-4xl mx-auto">
      <div className="flex justify-between items-center mb-4">
        <h1 className="text-2xl font-bold">Produtos</h1>

        <div className="flex gap-2">
          <button
            onClick={handleCreate}
            className="bg-green-600 text-white px-3 py-1 rounded"
          >
            Novo
          </button>

          <button
            onClick={() => {
              auth.logout();
              window.location.replace("/login");
            }}
            className="bg-gray-600 text-white px-3 py-1 rounded"
          >
            Sair
          </button>
        </div>
      </div>

      <table className="w-full border">
        <thead>
          <tr className="bg-gray-100">
            <th className="p-2 border">Id</th>
            <th className="p-2 border">Nome</th>
            <th className="p-2 border">Preço</th>
            <th className="p-2 border">Status</th>
            <th className="p-2 border">Cadastro</th>
            <th className="p-2 border">Ações</th>
          </tr>
        </thead>

        <tbody>
          {products.map((p) => (
            <tr key={p.id}>
              <td className="p-2 border text-center">{p.id}</td>
              <td className="p-2 border">{p.nome}</td>
              <td className="p-2 border text-right">R$ {p.preco.toFixed(2)}</td>
              <td className="p-2 border text-center">
                {p.status ? "Ativo" : "Inativo"}
              </td>
              <td className="p-2 border text-center">
                {p.idUsuarioCadastro ?? "-"}
              </td>
              <td className="p-2 border text-center">
                <button
                  onClick={() => handleEdit(p)}
                  className="mr-2 text-blue-600"
                >
                  Editar
                </button>

                <button
                  onClick={() => handleDelete(p.id)}
                  className="text-red-600"
                >
                  Excluir
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {showForm && (
        <ProductForm
          product={editing}
          onSaved={onSaved}
          onCancel={() => setShowForm(false)}
        />
      )}
    </div>
  );
}
