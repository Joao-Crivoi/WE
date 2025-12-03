/* eslint-disable react-refresh/only-export-components */
import { createContext, useState, useContext, type ReactNode } from "react";
import api from "../api";

interface User {
  id: number;
  nome: string;
  status: boolean;
}

interface AuthContextType {
  user: User | null;
  isAuthenticated: boolean;
  login: (nome: string, password: string) => Promise<boolean>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<User | null>(null);
  const [token, setToken] = useState<string | null>(() =>
    localStorage.getItem("token")
  );

  const login = async (nome: string, password: string) => {
    try {
      const res = await api.post("/usuarios/auth", {
        Nome: nome,
        Senha: password,
      });

      const t = res.data.token;
      if (!t) return false;

      localStorage.setItem("token", t);
      setToken(t);
      setUser(res.data.user);

      return true;
    } catch (err) {
      console.error("Erro no login:", err);
      return false;
    }
  };

  const logout = () => {
    localStorage.removeItem("token");
    setToken(null);
    setUser(null);
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        isAuthenticated: !!token,
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error("useAuth deve estar dentro de AuthProvider");
  return ctx;
}
