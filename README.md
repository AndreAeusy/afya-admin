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
- usei o terminal linux

## Como executar

É necessário ter o **.NET SDK 10** instalado (`dotnet --version` deve mostrar 10.x).

```bash
git clone https://github.com/AndreAeusy/afya-admin.git
cd afya-admin
dotnet watch
```

Depois, acesse o endereço local que aparecer no terminal (por exemplo, `http://localhost:xxxx`). o link aparece pelo terminal

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

Enfrentei dois desafios principais durante o desenvolvimento:

1. **Atualização dos Gráficos no MudBlazor 9:** Quando fui montar os gráficos e os *sparklines* nos cards de KPI, percebi que muitos exemplos da internet não compilavam mais. A API do MudBlazor mudou bastante na versão 9 (passou a exigir tipos genéricos como `ChartSeries<double>` e removeu parâmetros antigos). A solução foi ler a fundo a documentação atualizada e montar as séries dentro do ciclo de vida `OnParametersSet`.
2. **Alinhamento do Card de Performance:** No card "Performance dos Projetos", o conteúdo inicial não estava a preencher toda a altura disponível do card, deixando um "buraco" em branco no final. Para resolver isso sem usar CSS, apliquei as classes utilitárias de flexbox (`d-flex flex-column flex-grow-1`) nos containers, forçando as linhas dos projetos a dividirem o espaço vertical de forma igual.

## Melhorias futuras e desafios

**Desafio implementado: período funcional nos KPIs.** O `SeletorPeriodo` altera os quatro cards de KPI. A página chama `DashboardData.ObterKpis(periodo)`, que devolve a nova lista de dados correspondente àquele intervalo e atualiza a interface instantaneamente.

**Melhorias para o futuro:**
Num próximo passo, a evolução natural deste dashboard seria substituir os dados estáticos (mockados na pasta `Data`) por uma integração real com backend, utilizando a classe `HttpClient` para consumir dados de uma API REST. Também seria interessante usar o `localStorage` do navegador para salvar a preferência do utilizador pelo tema (Claro/Escuro), para que a página não volte ao tema padrão após um recarregamento.
