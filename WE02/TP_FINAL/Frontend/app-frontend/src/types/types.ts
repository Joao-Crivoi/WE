export type Usuario = {
  id?: number;
  nome: string;
  senha?: string;
  status: boolean;
};

export type Product = {
  id?: number;
  nome: string;
  preco: number;
  status: boolean;
  idUsuarioCadastro?: number;
  idUsuarioUpdate?: number | null;
};
