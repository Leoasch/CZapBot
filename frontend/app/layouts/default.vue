-- Active: 1782586592277@@127.0.0.1@5432@leoaschDB
<script setup lang="ts">
import type { DropdownMenuItem, NavigationMenuItem } from '@nuxt/ui'

const open = ref(true)
const auth = useAuth()

function getItems(state: 'collapsed' | 'expanded') {
  return [
    {
      label: 'Inicio',
      icon: 'i-lucide-home',
      to: '/'
    },
    
  ] satisfies NavigationMenuItem[]
}

const user = ref({
  name: auth.state.value.user?.nome,
})

const userItems = computed<DropdownMenuItem[][]>(() => [
  
  [
    {
      label: 'Sair',
      icon: 'i-lucide-log-out',
      onSelect: async () => {
        await auth.logout()
        navigateTo("/auth/login")
      }
    }
  ]
])

</script>

<template>
  <div class="flex flex-1 h-screen">
    <USidebar
      v-model:open="open"
      collapsible="icon"
      rail
      :ui="{
        container: 'h-full',
        inner: 'bg-elevated/25 divide-transparent',
        body: 'py-0'
      }"
    >
      <template #header>
        <div>
          <div class="h-10 w-full">
            <h1 :class="`${open ? '' : 'text-center'} transition-all duration-400 overflow-hidden w-full text-ellipsis text-nowrap`">{{ open ? "Sistema Interno JTX" : "JTX" }}</h1>
          </div>
          <UColorModeSwitch />
        </div>
      </template>

      <template #default="{ state }">
        <UNavigationMenu
          :key="state"
          :items="getItems(state)"
          orientation="vertical"
          :ui="{ link: 'p-1.5 overflow-hidden' }"
        />
      </template>

      <template #footer>
        <UDropdownMenu
          :items="userItems"
          :content="{ align: 'center', collisionPadding: 12 }"
          :ui="{ content: 'w-(--reka-dropdown-menu-trigger-width) min-w-48' }"
        >
          <UButton
            v-bind="user"
            :label="user?.name"
            icon="lucide:user"
            trailing-icon="i-lucide-chevrons-up-down"
            color="neutral"
            variant="ghost"
            square
            class="w-full data-[state=open]:bg-elevated overflow-hidden"
            :ui="{
              trailingIcon: 'text-dimmed ms-auto'
            }"
          />
        </UDropdownMenu>
      </template>
    </USidebar>

    <div class="flex-1 flex flex-col h-full">
      <div class="h-(--ui-header-height) shrink-0 flex items-center px-4 border-b border-default">
        <UButton
          icon="i-lucide-panel-left"
          color="neutral"
          variant="ghost"
          aria-label="Toggle sidebar"
          @click="open = !open"
        />
      </div>

      <div class="flex-1 min-h-0">
        <div class="h-full">
          <slot />
        </div>
      </div>
    </div>
  </div>
</template>
