import { ApiError } from "~/apiError"

const API_BASE_URL = 'http://localhost:8080'

export async function apiRequest<T>(url: string, options: any = {}): Promise<T | ApiError> {
  try {
    return await $fetch<T>(url, { baseURL: API_BASE_URL, ...options, credentials: 'include' })
  } catch (e: any) {
    console.error(`Erro ao se comunicar com o backend na rota ${url}:`, e?.message)
    return new ApiError(e)
  }
}