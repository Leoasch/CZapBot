
export type AuthData = {
  nome: string
  email: string
  role: string
}

export type Product = {
  id?: string | null
  idProduto: number
  nome: string
  detalhes: string
  prioridade: number
  estado: "pendente" | "em produção" | "concluido"
}

