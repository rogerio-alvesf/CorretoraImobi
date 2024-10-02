## CorretoraImobiApi

### Configuração do Ambiente do Banco de Dados
Para configurar o ambiente do banco de dados MongoDB para testar a API, siga os passos abaixo:

1. **Navegue até o diretório onde o arquivo `docker-compose.yml` está localizado:**
```
cd CorretoraImobi.Api/Docker/
```

2. **Execute o Docker Compose para levantar o banco de dados MongoDB:**
```
docker-compose up -d
```

**Isso irá inicializar o banco de dados MongoDB que a API utilizará.**

3. **Após o banco de dados estar em execução, você pode acessar o contêiner do MongoDB para realizar operações adicionais, como selecionar ou criar o banco de dados `CorretoraImobi`:

- **Acesse o contêiner do MongoDB:**
```
docker exec -it imobi-mongo-db bash
```

- **Dentro do contêiner, abra o Mongo Shell e selecione o banco de dados `CorretoraImobi`:**
```
mongosh --eval "use CorretoraImobi"
```