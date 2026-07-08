import { getProduction, setProduct } from "~/api/production"
import { ApiError } from "~/apiError"
import type { Product } from "~/types"

type ProductionData = {
  products: Product[]
}

export const useProduction = () => {
  const state = useState<ProductionData>('production', () => ({
    products: []
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

  function setProducts(products: Product[]) {
    state.value.products = products
  }

  async function loadProduction(id?: string) {
    const response = await getProduction()
    
    if (isApiError(response)) handleError(response)

    if (response.data) {
      setProducts(response.data)
    }
  }

  async function addProduct(product: Product, id?: string) {
    const response = await setProduct(product, id)

    if (isApiError(response)) handleError(response)
  }

  async function deleteProduct(id: string) {
    const response = await deleteProduct(id)

    if (isApiError(response)) handleError(response)
  }


  return {
    state,
    loadProduction,
    addProduct,
    deleteProduct
  }
}