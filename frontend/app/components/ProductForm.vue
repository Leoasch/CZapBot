<script setup lang="ts">
import type { Product } from '~/types'


const props = defineProps({
  id: {
    type: String,
    default: () => null,
    required: false
  }
})

const production = useProduction()

const form = ref<Product>({
  idProduto: 0,
  nome: '',
  detalhes: '',
  prioridade: 0,
  estado: 'pendente'
})

const selectItems = ref([
  { label: 'Pendente', value: 'pendente' },
  { label: 'Em Produção', value: 'em produção' },
  { label: 'Concluído', value: 'concluido' }
])

async function handleProductionAdd () {
  await production.addProduct(form.value, props?.id)
  await production.loadProduction()
  form.value = {
    idProduto: 0,
    nome: '',
    detalhes: '',
    prioridade: 0,
    estado: 'pendente'
  }
}

async function loadProductFromId () {

}

onMounted(async () => {
  if (props.id) {
    await loadProductFromId()
  }
})

</script>

<template>
  <div class="size-full flex flex-col gap-3">
    <h1 class="w-full text-center font-bold text-xl mb-4">Formulário de Produto</h1>
    <UFormField label="ID Produto" class="mx-auto w-full">
      <UInput v-model="form.idProduto" class="w-full" type="number"/>
    </UFormField>
    <UFormField label="Nome" class="mx-auto w-full">
      <UInput v-model="form.nome" class="w-full"/>
    </UFormField>
    <UFormField label="Detalhes" class="mx-auto w-full">
      <UTextarea v-model="form.detalhes" class="w-full"/>
    </UFormField>
    <UFormField label="Prioridade" class="mx-auto w-full">
      <UInput v-model="form.prioridade" class="w-full" type="number"/>
    </UFormField>
    <UFormField label="Estado" class="mx-auto w-full">
      <USelect v-model="form.estado" class="w-full" :items="selectItems"/>
    </UFormField>
    <UButton class="mx-auto cursor-pointer my-2" trailing-icon="lucide:plus" @click="handleProductionAdd">Adicionar</UButton>
  </div>
</template>