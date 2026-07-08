<template>
  <UMain class="size-full flex flex-col items-center justify-center">
    <div class="w-120 mx-auto flex gap-2 p-2">
      <ULink type="button" to="/auth/login" class="m-auto text-xl w-full text-center hover:bg-accented/30">Login</ULink>
      <ULink type="button" to="/auth/register" class="m-auto text-xl w-full text-center hover:bg-accented/30">Registrar-se</ULink>
    </div>
    <slot/>
  </UMain>
</template>