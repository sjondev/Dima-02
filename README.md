# Projeto Financeiro DIMA

No meu segundo projeto, aprendi como funciona um sistema financeiro em C#/.NET. Estruturei o sistema em 3 camadas: Core, API e Web (Blazor), referenciando entre elas.

A camada Core é a mais importante, pois contém a lógica de negócio e não pode ser substituída. A API lida com a conexão ao banco de dados, endpoints e mapeamentos. Utilizei FluentMapping para automatizar a criação de tabelas e colunas, mantendo a lógica de negócio organizada e pronta para ser consumida pela API e pelos dados do sistema.

Para a autenticação de usuários, usei o Identity do Entity Framework, que protege os dados criptografando informações sensíveis. Também podemos trabalhar com cookies pré-configurados pelo EF, o que facilita a implementação de novos sistemas.

Além disso, aprendi a utilizar User Secrets, uma forma nativa do .NET para armazenar informações sensíveis como connection strings, tokens e chaves de API sem expor esses dados no código ou no Git. Isso ajuda a manter a segurança do sistema em ambiente de desenvolvimento.

Após configurar a autenticação, modelei e configurei Categorias e Transações, aplicando regras de entrada e saída usando Enums (por exemplo, 1 = entrada, 2 = saída). Também trabalhei com Orders (pedidos), Checkouts (compras), Products (produtos) e Vouchers (descontos), aprendendo bastante sobre regras de negócio.

Aprendi a trabalhar com contratos e interfaces para padronizar a lógica da API, além de uniformizar Requests e Responses, tornando o sistema mais organizado e escalável.

Atualmente, estou planejando integrar o Strapi ao meu aplicativo para gerenciar conteúdo de forma mais eficiente.

PS: Para configurar os User Secrets, basta executar dotnet user-secrets init no projeto e adicionar suas chaves com dotnet user-secrets set "Chave:SubChave" "Valor". Depois, elas podem ser acessadas pelo Configuration no Program.cs sem precisar colocar dados sensíveis no appsettings.json.
