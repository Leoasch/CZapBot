import { getAuth, getLogout, postLogin, postRegister } from "~/api/auth"
import { ApiError } from "~/apiError"
import type { AuthData } from "~/types"

type AuthState = {
  authenticated: boolean | null
  user: AuthData | null
}

export const useAuth = () => {
  const state = useState<AuthState>('auth', () => ({
    authenticated: null,
    user: null
  }))

  function isApiError (response: any) {
    return response instanceof ApiError
  }

  function handleError (error: ApiError) {
    markRaw(useToast().add({
      description: error.message,
      color: "error"
    }))
  }

  function setUser(user: AuthData | null) {
    if (user) {
      state.value.authenticated = true
      state.value.user = user
    } else {
      state.value.authenticated = false
      state.value.user = null
    }
  }

  async function validate() {
    const response = await getAuth()

    if (isApiError(response)) {
      handleError(response)
      return
    }

    setUser(response.user)
  }

  async function login(login: string, password: string) {
    const response = await postLogin({
      email: login,
      senha: password
    })

    if (isApiError(response)) {
      handleError(response)
      return
    }

    setUser(response.user)
    if (response.user) {
      return navigateTo('/')
    }
  }

  async function logout() {
    const response = await getLogout()
    if (isApiError(response)) {
      handleError(response)
      return
    }
    setUser(response.user)
  }

  async function register(name: string, email: string, password: string) {
    const response = await postRegister({
      email,
      nome: name,
      senha: password
    })
    if (isApiError(response)) {
      handleError(response)
      return
    }

    return navigateTo('/auth/login')
  }

  return {
    state,
    validate,
    login,
    logout,
    register
  }
}