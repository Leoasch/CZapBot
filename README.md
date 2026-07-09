# CZapBot

## Como rodar a aplicação

1. Abra um terminal na raiz do projeto:

2. Execute o comando abaixo para subir todos os serviços via Docker:
   ```bash
   docker compose up --build
   ```

3. Aguarde o Docker baixar as imagens, compilar e iniciar os containers.

---

## O que já está configurado no `docker-compose.yml`

- `frontend`: aplicação Nuxt.js rodando na porta `3000`
- `api`: ASP.NET Web API rodando na porta `8080`
- `worker`: serviço Worker .NET
- `redis`: Redis como cache/banco, com volume persistente

---

## Variáveis e dependências

- `api` e `worker` usam a variável `REDIS_HOST=redis`
- `api` depende de `redis`
- `frontend` depende de `api`

---

## Acesso

- Frontend: `http://localhost:3000`
- API: `http://localhost:8080`

---

## Comandos úteis

- Subir em segundo plano:
  ```bash
  docker compose up --build -d
  ```

- Parar e remover containers:
  ```bash
  docker compose down
  ```

- Ver logs:
  ```bash
  docker compose logs -f
  ```
