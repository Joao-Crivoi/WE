const base =
  import.meta.env.VITE_API_URL ?? "https://localhost:7160/api/Phrases";

export async function fetchPhrases() {
  const res = await fetch(base);
  if (!res.ok) throw new Error((await res.text()) || "Erro ao buscar frases");
  return res.json();
}

export async function createPhrase(payload) {
  const res = await fetch(base, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });
  if (!res.ok) throw new Error((await res.text()) || "Erro ao criar");
  return res.json();
}

export async function updatePhrase(id, payload) {
  const res = await fetch(`${base}/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });
  if (!res.ok) throw new Error((await res.text()) || "Erro ao atualizar");
  return res.json();
}

export async function deletePhrase(id) {
  const res = await fetch(`${base}/${id}`, { method: "DELETE" });
  if (!res.ok) throw new Error((await res.text()) || "Erro ao deletar");
  return true;
}
