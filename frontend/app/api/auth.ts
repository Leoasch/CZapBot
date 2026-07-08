import type { AuthData } from "~/types"
import { apiRequest } from "./base"


export async function getAuth() {
  return await apiRequest<{ user: AuthData | null }>('/auth/check')
}

export async function getLogout() {
  return await apiRequest<{ user: AuthData | null }>('/auth/logout')
}

export async function postLogin(body: { email: string, senha: string }) {
  return await apiRequest<{ user: AuthData | null }>('/auth/login', {
    method: 'POST',
    body,
    headers: {
      'Content-Type': 'application/json',
    },
  })
}

export async function postRegister(body: { email: string, senha: string, nome: string }) {
  return await apiRequest<{ user: AuthData | null }>('/auth/registrar', {
    method: 'POST',
    body,
    headers: {
      'Content-Type': 'application/json',
    },
  })
}