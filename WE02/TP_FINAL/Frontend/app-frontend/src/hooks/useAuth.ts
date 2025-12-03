import { useContext } from "react";

export function useAuth() {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error("useAuth precisa estar dentro do AuthProvider");
  return ctx;
}
