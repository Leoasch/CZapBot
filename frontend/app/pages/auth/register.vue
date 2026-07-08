<script setup lang="ts">
definePageMeta({
  layout: 'no-auth',
})

const auth = useAuth()

const form = ref({
  login: '',
  senha: '',
  nome: ''
})
const show = ref(false)

async function handleRegister() {
  await auth.register(form.value.nome, form.value.login, form.value.senha)
}

</script>
<template>
  <UForm class="w-120 mx-auto flex flex-col gap-4 p-2 rounded bg-accented/40 shadow-2xl">
    <h1 class="mx-auto my-1 text-2xl font-bold w-3/4">Registrar-se</h1>
    <UFormField label="Nome" class="mx-auto w-3/4">
      <UInput v-model="form.nome" class="w-full"/>
    </UFormField>
    <UFormField label="E-Mail" class="mx-auto w-3/4">
      <UInput v-model="form.login" class="w-full"/>
    </UFormField>
    <UFormField label="Senha" class="mx-auto w-3/4">
      <UInput v-model="form.senha" class="w-full" :type="show ? 'text' : 'password'">
        <template #trailing>
          <UButton
            color="neutral"
            variant="link"
            size="sm"
            :icon="show ? 'i-lucide-eye-off' : 'i-lucide-eye'"
            :aria-label="show ? 'Hide password' : 'Show password'"
            :aria-pressed="show"
            aria-controls="password"
            @click="show = !show"
          />
        </template>
      </UInput>
    </UFormField>
    <UButton class="mx-auto cursor-pointer my-2" trailing-icon="lucide:log-in" @click="handleRegister">Entrar</UButton>
  </UForm>
</template>