import type { AuthData, Product } from "~/types"
import { apiRequest } from "./base"

export async function getProduction(id?: string) {
  return await apiRequest<{ data: Product[] | null, message: string }>(`/producao${id ? `/${id}` : ''}`)
}

export async function setProduct(body: Product, id?: string) {
  return await apiRequest<{ user: AuthData | null }>(`/producao${id ? `/${id}` : ''}`, {
    method: id ? 'PATCH' : 'POST',
    body,
    headers: {
      'Content-Type': 'application/json',
    },
  })
}

export async function deleteProduct(id?: string) {
  return await apiRequest<{ user: AuthData | null }>(`/producao${id ? `/${id}` : ''}`, {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json',
    },
  })
}