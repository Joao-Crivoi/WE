import React from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import api from "../api";
import type { Product } from "../types/types";
import { useAuth } from "../contexts/AuthContext";

const schema = z.object({
  nome: z.string().min(1, "Nome obrigatório"),
  preco: z.number().min(0),
  status: z.boolean(),
});

type FormData = z.infer<typeof schema>;

export default function ProductForm({
  product,
  onSaved,
  onCancel,
}: {
  product?: Product | null;
  onSaved: () => void;
  onCancel: () => void;
}) {
  const auth = useAuth();

  const { register, handleSubmit } = useForm<FormData>({
    resolver: zodResolver(schema),
    defaultValues: {
      nome: product?.nome ?? "",
      preco: product?.preco ?? 0,
      status: product?.status ?? true,
    },
  });

  const submit = async (data: FormData) => {
    if (!auth.user) {
      alert("Usuário não autenticado.");
      return;
    }

    try {
      if (product?.id) {
        // EDITAR
        await api.put(`Produtos/${product.id}`, {
          ...data,
          IdUsuarioUpdate: auth.user.id,
        });
      } else {
        // CRIAR
        await api.post("Produtos", {
          ...data,
          IdUsuarioCadastro: auth.user.id,
        });
      }

      onSaved();
    } catch (err: any) {
      alert("Erro: " + (err?.response?.data || err.message));
    }
  };

  return (
    <div className="fixed inset-0 bg-black/30 flex items-center justify-center">
      <form
        onSubmit={handleSubmit(submit)}
        className="bg-white p-6 rounded w-full max-w-lg"
      >
        <h3 className="text-lg font-semibold mb-4">
          {product ? "Editar Produto" : "Novo Produto"}
        </h3>

        <div className="mb-3">
          <label className="block text-sm">Nome</label>
          <input
            {...register("nome")}
            className="w-full border rounded px-3 py-2"
          />
        </div>

        <div className="mb-3">
          <label className="block text-sm">Preço</label>
          <input
            {...register("preco", { valueAsNumber: true })}
            type="number"
            step="0.01"
            className="w-full border rounded px-3 py-2"
          />
        </div>

        <div className="mb-4">
          <label className="inline-flex items-center gap-2">
            <input
              {...register("status")}
              type="checkbox"
              className="w-4 h-4"
              defaultChecked={product?.status ?? true}
            />
            <span>Ativo</span>
          </label>
        </div>

        <div className="flex justify-end gap-2">
          <button
            type="button"
            onClick={onCancel}
            className="px-3 py-1 border rounded"
          >
            Cancelar
          </button>
          <button
            type="submit"
            className="px-3 py-1 bg-blue-600 text-white rounded"
          >
            Salvar
          </button>
        </div>
      </form>
    </div>
  );
}
