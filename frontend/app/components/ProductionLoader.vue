<script setup lang="ts">
import type { ContextMenuItem } from '@nuxt/ui'
import type { Product } from '~/types'


const production = useProduction()

onMounted(() => {
  production.loadProduction()
})

function stateToFontColor (estado: string) {
  switch (estado) {
    case "pendente":
      return "text-amber-300"
    case "em produção":
      return "text-orange-400"
    case "concluido":
      return "text-emerald-500"
  }
  return "text-slate-100"
}

const organizedProducts = computed(() => {
  const products = production.state.value.products ?? []

  const organized: Record<string, any[]> = {
    pendente: [],
    "em produção": [],
    concluido: [],
    outros: [],
  }

  for (const product of products) {
    if (organized[product.estado]) {
      organized[product.estado]?.push(product)
    } else {
      organized.outros?.push(product)
    }
  }

  // Sort each state by priority (highest first)
  Object.values(organized).forEach(stateProducts => {
    stateProducts.sort((a, b) => (b.prioridade ?? 0) - (a.prioridade ?? 0))
  })

  return [
    ...organized.outros!,
    ...organized.pendente!,
    ...organized["em produção"]!,
    ...organized.concluido!,
  ]
})

function filterProduct (product: Product) {
  return {
    idProduto: product.idProduto,
    nome: product.nome,
    detalhes: product.detalhes,
    prioridade: product.prioridade,
    estado: product.estado
  }
}

const items = (product: Product) => ref<ContextMenuItem[]>([
  {
    label: 'Estado',
    children: [
      {
        label: 'Concluido',
        onSelect: async () => {
          if (product.id) {
            await production.addProduct({
              ...filterProduct(product),
              estado: 'concluido'
            }, product.id)
            production.loadProduction()
          }
        },
      },
      {
        label: 'Em Produção',
        onSelect: async () => {
          if (product.id) {
            await production.addProduct({
              ...filterProduct(product),
              estado: 'em produção'
            }, product.id)
            production.loadProduction()
          }
        },
      },
      {
        label: 'Pendente',
        onSelect: async () => {
          if (product.id) {
            await production.addProduct({
              ...filterProduct(product),
              estado: 'pendente'
            }, product.id)
            production.loadProduction()
          }
        },
      },
    ]
  }
])

</script>

<template>
  <div class="size-full flex flex-col gap-3 max-h-full overflow-y-auto">
    <UContextMenu v-for="(value, idx) in organizedProducts" :key="idx" :items="items(value).value">
      <div 
        :class="`
          rounded-md bg-accented/30 p-2 flex flex-col
          ${ value.estado === 'pendente' ? 'border-amber-300/50 border' : 
          value.estado === 'em produção' ? 'border-orange-400/50 border' : 
          value.estado === 'concluido' ? 'border-emerald-500/50 border opacity-70' : ''}
        `">
        <div class="flex gap-2 font-bold" v-if="value.nome">
          <h1>{{ value.nome }}</h1>
          <h1>({{ value.idProduto }})</h1>
        </div>
        <h1 v-else>{{ value.idProduto }}</h1>
        <p class="text-sm text-slate-200/60 font-light">Prioridade: {{ value.prioridade }}</p>
        <p class="text-sm text-slate-200/60 font-light">{{ value.detalhes }}</p>
        <h2 :class="`mr-0 ml-auto p-0.5 rounded font-light`"><i :class="`${stateToFontColor(value.estado)}`">{{ value.estado }}</i></h2>
      </div>
    </UContextMenu>
  </div>  
</template>