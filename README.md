## CorretoraImobiApi

### Configura��o do Ambiente do Banco de Dados
Para configurar o ambiente do banco de dados MongoDB para testar a API, siga os passos abaixo:

1. **Navegue at� o diret�rio onde o arquivo `docker-compose.yml` est� localizado:**
```
cd Docker/
```

2. **Execute o Docker Compose para levantar o banco de dados MongoDB:**
```
docker-compose up -d
```

**Isso ir� inicializar o banco de dados MongoDB que a API utilizar�.**

3. **Ap�s o banco de dados estar em execu��o, voc� pode acessar o cont�iner do MongoDB para realizar opera��es adicionais, como selecionar ou criar o banco de dados `CorretoraImobi`:**

- **Acesse o cont�iner do MongoDB:**
```
docker exec -it imobi-mongo-db bash
```

- **Dentro do cont�iner, abra o Mongo Shell e selecione o banco de dados `CorretoraImobi`:**
```
mongosh --eval "use CorretoraImobi"
```