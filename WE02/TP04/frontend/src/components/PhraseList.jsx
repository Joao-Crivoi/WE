import React, { useState } from 'react';
import { deletePhrase, updatePhrase } from '../api';

export default function PhraseList({ phrases, onChanged }) {
  const [editingId, setEditingId] = useState(null);
  const [editValue, setEditValue] = useState('');

  async function handleDelete(id) {
    if (!confirm('Deletar esta frase?')) return;
    try {
      await deletePhrase(id);
      if (onChanged) onChanged();
    } catch(err) {
      alert(err.message || 'Erro ao deletar');
    }
  }

  function startEdit(ph) {
    setEditingId(ph.id);
    setEditValue(ph.content);
  }

  async function saveEdit(id) {
    try {
      await updatePhrase(id, { content: editValue });
      setEditingId(null);
      setEditValue('');
      if (onChanged) onChanged();
    } catch(err) {
      alert(err.message || 'Erro ao atualizar');
    }
  }

  return (
    <div>
      <h2 className="text-xl font-semibold mb-3">Lista de frases</h2>
      <div className="space-y-2">
        {phrases.length === 0 && <div className="text-sm text-gray-500">Nenhuma frase.</div>}
        {phrases.map(ph => (
          <div key={ph.id} className="p-3 border rounded flex justify-between items-start">
            <div>
              <div className="text-sm text-gray-500">#{ph.id} — {new Date(ph.createdAt).toLocaleString()}</div>
              {editingId === ph.id ? (
                <textarea value={editValue} onChange={e=>setEditValue(e.target.value)} className="w-full p-2 border rounded mt-1" />
              ) : (
                <div className="mt-1">{ph.content}</div>
              )}
            </div>
            <div className="flex flex-col gap-2 ml-4">
              {editingId === ph.id ? (
                <>
                  <button onClick={()=>saveEdit(ph.id)} className="text-sm text-green-600">Salvar</button>
                  <button onClick={()=>{ setEditingId(null); setEditValue(''); }} className="text-sm text-gray-600">Cancelar</button>
                </>
              ) : (
                <>
                  <button onClick={()=>startEdit(ph)} className="text-sm text-blue-600">Editar</button>
                  <button onClick={()=>handleDelete(ph.id)} className="text-sm text-red-600">Deletar</button>
                </>
              )}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
