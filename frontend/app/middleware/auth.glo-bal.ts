export default defineNuxtRouteMiddleware( async (to, _) => {
  const to_path = to.path
  
  const user = useAuth()

  if (user.state.value.authenticated === null) {
    await user.validate()
  }
  
  const login_routes = [
    '/auth/login',
    '/auth/register',
  ]
  
  
  for (const route of login_routes) {
    if (to_path.match(new RegExp(route))) {
      if (user.state.value.authenticated) {
        return navigateTo('/')
      }
      return
    }
  }

  if (!user.state.value.authenticated) {
    return navigateTo('/auth/login')
  }
})