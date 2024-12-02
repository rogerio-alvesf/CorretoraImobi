## CorretoraImobiApi
CorretoraImobi é um projeto de exemplo que demonstra a implementação de um CRUD utilizando MongoDB em uma Minimal API com testes de integração.

## Funcionalidades 
- CRUD de Imóveis
- Conexão com MongoDB
- Testes de Integração

## Estrutura do Projeto 
- **API**: Contém os endpoints da API.
- **Aplicação**: Contém a lógica de aplicação e serviços.
- **Infraestrutura**: Contém a configuração do banco de dados e repositórios.
- **Domínio**: Contém as entidades e enums.
- **Testes**: Contém os testes de integração.

(`Docs\images\CorretoraImobi.Doc.jpg`)

## Tecnologias Utilizadas 
- .NET Core (8.0)
- MongoDB
- Docker

### Configuração do Ambiente do Banco de Dados
Para configurar o ambiente do banco de dados MongoDB para testar a API, siga os passos abaixo:

1. **Navegue até o diretório onde o arquivo `docker-compose.yml` está localizado:**
```
cd Docker/
```

2. **Execute o Docker Compose para levantar o banco de dados MongoDB:**
```
docker-compose up -d
```

**Isso irá inicializar o banco de dados MongoDB que a API utilizará.**

3. **Após o banco de dados estar em execução, você pode acessar o container do MongoDB para realizar operçõees adicionais, como selecionar ou criar o banco de dados `CorretoraImobi`:**

- **Acesse o container do MongoDB:**
```
docker exec -it imobi-mongo-db bash
```

- **Dentro do container, abra o Mongo Shell e selecione o banco de dados `CorretoraImobi`:**
```
mongosh --eval "use CorretoraImobi"
```