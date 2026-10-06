# Afya Admin — Dashboard com Blazor WebAssembly e MudBlazor

## Identificação

| | |
|---|---|
| **Aluno(a)** | André Vinícius de Souza |
| **Matrícula** | **[ayfa caiu não lembro prof]** |
| **Faculdade** | São Lucas campus 2 Porto Velho |
| **Curso** | Bacharelado em Ciência da Computação |
| **Disciplina** | Programação para Sistemas Web |
| **Professor(a)** | **[mestre liluyoud cury]** |
| **Semestre** | 2026.2 |


## Tecnologias utilizadas

- .NET 10 / Blazor WebAssembly
- MudBlazor 9 (componentes, tema e classes utilitárias)
- C# e Razor
- Git e GitHub
- usei o meu terminal que linux só para falar mesmo

## Como executar

É necessário ter o **.NET SDK 10** instalado (`dotnet --version` deve mostrar 10.x).

```bash
git clone https://github.com/AndreAeusy/afya-admin.git
cd afya-admin
dotnet watch
```
O primeiro build restaura os pacotes necessários. Não é preciso instalar o template MudBlazor para executar este projeto já criado.

Use a URL informada no terminal. Para selecionar explicitamente o perfil HTTP configurado no projeto:
Depois, acesse o endereço local que aparecer no terminal  `http://localhost:5010/`esse e  o link aparece pelo terminal

## Telas

### Tema claro

![Dashboard — tema claro](docs/prints/tema-claro.png)

### Tema escuro

![Dashboard — tema escuro](docs/prints/tema-escuro.png)

### Versão mobile

![Dashboard — celular](docs/prints/mobile.png)

### HTML gerado (DevTools)

![Inspeção do HTML no DevTools](docs/prints/devtools.png)


## Estrutura do projeto

```text
afya-admin/
├── Components/   cards, gráficos, listas e tabela do dashboard + Ui.cs
├── Data/         dados fictícios e modelos (records), separados do visual
├── Layout/       MainLayout (tema, barra superior, menu lateral) e NavMenu
├── Pages/        Dashboard (rota "/") e NotFound
├── wwwroot/      index.html, app.css (sem regras) e imagens
└── docs/prints/  capturas de tela usadas neste README
```

## Componentes criados

| Componente | Responsabilidade | Parâmetros que recebe |
|---|---|---|
| `DashboardCard` | Card base com título, subtítulo, ações, menu e conteúdo. | `Titulo`, `Subtitulo`, `Acoes`, `Menu`, `ChildContent` |
| `CabecalhoPagina` | Título, subtítulo e ações no topo da página. | `Titulo`, `Subtitulo`, `ChildContent` |
| `SeletorPeriodo` | Menu para escolher o período exibido. | `Valor`, `ValorChanged` |
| `KpiCard` | Indicador com valor, variação e mini gráfico. | `Kpi` |
| `GraficoReceita` | Gráfico de linha de Receita x Meta. | nenhum (lê de `DashboardData`) |
| `GraficoDistribuicaoClientes` | Gráfico de rosca com o total e a legenda. | nenhum (lê de `DashboardData`) |
| `PerformanceProjetos` | Projetos com barra de progresso. | nenhum (lê de `DashboardData`) |
| `AtividadesRecentes` | Feed das últimas ações da equipe. | nenhum (lê de `DashboardData`) |
| `ProjetosRecentes` | Tabela de projetos com status e progresso. | nenhum (lê de `DashboardData`) |

## O que aprendi

### 1. Como o Blazor WebAssembly inicia?

Tudo começa no arquivo `wwwroot/index.html`, que mostra a tela de carregamento na tag `<div id="app">`. Enquanto isso, o navegador baixa o runtime do .NET em WebAssembly e as DLLs do nosso código. Quando termina, ele executa o `Program.cs`. É nesse momento que a instrução `builder.RootComponents.Add<App>("#app")` substitui o carregamento pela aplicação real e ativa as rotas.

### 2. Qual a diferença entre Layout, Page e Component?

* **Layout (`MainLayout.razor`):** é a "moldura" do sistema. A barra lateral e a barra superior ficam aqui, garantindo que não precisamos repeti-las em todas as páginas.
* **Page (`Dashboard.razor`):** é a tela inteira, acessada por uma URL específica (como o `@page "/"`). Ela serve para organizar e montar o quebra-cabeça.
* **Component (`KpiCard.razor`):** são as peças do quebra-cabeça. Um bloco visual isolado que eu crio uma vez e reutilizo várias vezes na tela, passando informações diferentes.

### 3. Para que serve o `RenderFragment`?

O `RenderFragment` funciona como um espaço reservado dentro de um componente. No `DashboardCard`, eu usei isso no `ChildContent`, nas `Acoes` e no `Menu`. Isso me permitiu colocar botões, tabelas ou gráficos diferentes dentro de cada card sem recriar as bordas e o cabeçalho toda vez.

### 4. Como funciona o `@bind-Valor` no `SeletorPeriodo`?

É uma "via de mão dupla". Eu passo o valor atual para dentro do componente e, quando o usuário escolhe outro período, o componente avisa a página principal disparando o `ValorChanged.InvokeAsync(opcao)`. Assim, a variável da página é atualizada e reflete a nova escolha.

### 5. Por que separar a pasta `Data`?

Deixar os dados (os `records` e as listas fictícias) separados do visual deixa tudo mais organizado. A maior vantagem é pensar no futuro: quando os dados vierem de uma API usando o `HttpClient`, só precisamos alterar a busca na pasta `Data`. O visual dos cards e gráficos continua intacto.

### 6. Como a responsividade funciona no `MudGrid` (`xs`, `sm`, `lg`)?

O `MudGrid` organiza a tela em 12 colunas. Ao colocar `xs="12" sm="6" lg="3"` nos cards de KPI, o layout fica assim: no celular, o card ocupa as 12 colunas (1 por linha); no tablet, ocupa 6 (2 por linha); e na tela grande, ocupa 3 (os 4 lado a lado).

### 7. Como foi feita a estilização sem CSS e qual o papel do `MudTheme`?

Eu não usei o `app.css`. Dei a "cara" do projeto com o `MudTheme` no `MainLayout`, que define de uma só vez as cores e a fonte do site, nos modos claro e escuro. O resto foi resolvido com classes utilitárias direto nas tags (como `pa-4` para espaçamento e `d-flex` para alinhar itens).

### 8. Por que o namespace é `afya_admin` e não `afya-admin`?

O compilador do C# não aceita hífens (`-`) em nomes de namespace, porque entenderia como uma subtração. Como a pasta do projeto se chama `afya-admin`, o `dotnet new` trocou o hífen por um sublinhado, e o namespace raiz ficou `afya_admin`.

## Dificuldades e soluções

Sendo bem sincero, enfrentei dois desafios principais onde acabei travando um pouco durante o desenvolvimento:

1. **Quebrando a cabeça com os gráficos (MudBlazor 9):** Quando fui tentar montar os gráficos e os *sparklines* dos KPIs, percebi que os códigos e exemplos que eu achava em tutoriais na internet simplesmente não compilavam. Acabei descobrindo que a API do MudBlazor mudou muito nessa versão 9 (agora eles exigem tipos genéricos, como `ChartSeries<double>`, e removeram vários parâmetros antigos). O jeito foi parar de pesquisar no Google, abrir a documentação oficial, ler a fundo e entender que eu precisava montar as séries de dados usando o ciclo de vida `OnParametersSet`.

2. **O "buraco" no Card de Performance:** Quando terminei o card de "Performance dos Projetos", notei que a lista não preenchia a altura toda do card, deixando um espaço em branco feio sobrando na parte de baixo. Como a regra do trabalho era não criar regras de CSS de jeito nenhum, fiquei pensando em como alinhar aquilo. A sacada foi usar as próprias classes utilitárias de flexbox do framework (`d-flex flex-column flex-grow-1`) direto nas tags. Isso forçou o container a esticar as linhas e dividir o espaço vertical perfeitamente, resolvendo o problema só com o HTML.

## Melhorias futuras e desafios

**O desafio que consegui implementar:**
Fiz questão de pôr o filtro de período a funcionar a sério nos KPIs. Agora, quando escolhemos uma opção no `SeletorPeriodo`, os quatro cards atualizam-se na hora. A página simplesmente chama o método `DashboardData.ObterKpis(periodo)`, que vai buscar a lista de dados correspondente àquele intervalo de tempo e atualiza a interface num piscar de olhos.

**O que gostava de implementar no futuro:**
O próximo passo óbvio para este projeto seria acabar com os dados estáticos (aqueles que deixei "mockados" à mão na pasta `Data`) e ligar o dashboard a um backend a sério. A ideia seria usar a classe `HttpClient` para ir buscar dados reais a uma API REST. 

Outra melhoria muito útil seria usar o `localStorage` do navegador para guardar a preferência de quem está a usar (se prefere o tema Claro ou Escuro). É um bocado frustrante recarregar a página e o tema voltar ao padrão do nada, por isso guardar essa escolha deixaria o projeto com um toque muito mais profissional!
