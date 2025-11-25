import React, { useEffect, useState } from 'react';
import { fetchPhrases } from './api';
import PhraseForm from './components/PhraseForm';
import PhraseList from './components/PhraseList';
import FlyingPhrases from './components/FlyingPhrases';

export default function App(){
  const [phrases, setPhrases] = useState([]);
  const [error, setError] = useState(null);

  const load = async ()=> {
    try {
      const data = await fetchPhrases();
      setPhrases(data);
    } catch(err) {
      setError(err.message);
    }
  };

  useEffect(()=>{ load(); }, []);

  return (
    <div className="min-h-screen p-6">
      <div className="max-w-4xl mx-auto bg-white rounded-xl shadow p-6">
        <h1 className="text-2xl font-bold mb-4">Phrases - Cadastro</h1>
        {error && <div className="text-red-600 mb-2">{error}</div>}
        <PhraseForm onCreated={load} />
        <hr className="my-6" />
        <PhraseList phrases={phrases} onChanged={load} />
      </div>
      <div className="flying-area">
        <FlyingPhrases phrases={phrases} />
      </div>
    </div>
  );
}
