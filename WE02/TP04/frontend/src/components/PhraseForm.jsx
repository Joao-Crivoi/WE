import React, { useState } from 'react';
import { createPhrase } from '../api';

export default function PhraseForm({ onCreated }) {
  const [content, setContent] = useState('');
  const [loading, setLoading] = useState(false);
  const [msg, setMsg] = useState(null);

  async function submit(e) {
    e.preventDefault();
    setLoading(true);
    setMsg(null);
    try {
      await createPhrase({ content });
      setContent('');
      setMsg('Frase criada com sucesso');
      if (onCreated) onCreated();
    } catch(err) {
      setMsg(err.message);
    } finally { setLoading(false); }
  }

  return (
    <form onSubmit={submit} className="space-y-3">
      <textarea value={content} onChange={e=>setContent(e.target.value)}
        className="w-full p-3 border rounded resize-none h-24" placeholder="Escreva a frase aqui..." required />
      <div className="flex items-center gap-3">
        <button type="submit" disabled={loading} className="px-4 py-2 bg-blue-600 text-white rounded">
          {loading ? 'Enviando...' : 'Enviar'}
        </button>
        {msg && <span className="text-sm text-gray-600">{msg}</span>}
      </div>
    </form>
  );
}
