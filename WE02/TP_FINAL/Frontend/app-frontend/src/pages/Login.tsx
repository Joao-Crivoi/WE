import { useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const [Nome, setNome] = useState("");
  const [Senha, setPassword] = useState("");

  const { login } = useAuth();
  const navigate = useNavigate();

  const handleLogin = async () => {
    console.log("Enviando dados:", Nome, Senha);

    const ok = await login(Nome, Senha);

    console.log("Resultado:", ok);

    if (ok) navigate("/produtos");
    else alert("Login inválido");
  };

  return (
    <div>
      <h1>Login</h1>
      <input
        placeholder="Nome"
        value={Nome}
        onChange={(e) => setNome(e.target.value)}
      />
      <input
        placeholder="Senha"
        type="password"
        value={Senha}
        onChange={(e) => setPassword(e.target.value)}
      />
      <button onClick={handleLogin}>Entrar</button>
    </div>
  );
}
