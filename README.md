# Afya Admin — Dashboard com Blazor WebAssembly e MudBlazor

## Identificação
| | |
|---|---|
| **Aluno(a)** | André Vinícius de Souza |
| **Faculdade** | São Lucas campus 2 Porto Velho |
| **Curso** | Bacharelado em Ciência da Computação |
| **Disciplina** | Programação para Sistemas Web |
| **Professor(a)** | mestre liluyourd cury |
| **Semestre** | 2026.2 |

Dashboard administrativo feito com **Blazor WebAssembly (.NET 10)** e **MudBlazor 9**, sem nenhuma linha de CSS próprio.

## Como executar

```bash
dotnet restore
dotnet run
```

Abra o endereço exibido no terminal (por padrão `https://localhost:xxxx`).

## Estrutura

```text
afya-admin/
├── Components/   componentes reutilizáveis (cards, gráficos, listas) + Ui.cs
├── Data/         modelos (records) e dados fictícios do dashboard
├── Layout/       MainLayout (tema, AppBar, Drawer) e NavMenu
├── Pages/        Dashboard (rota "/") e NotFound
├── wwwroot/      index.html, app.css (sem regras) e imagens
└── docs/prints/  tema-claro.png, tema-escuro.png, mobile.png, devtools.png
```
## Componentes criados

| Componente | Responsabilidade | Parâmetros que recebe |
| --- | --- | --- |
| `CabecalhoPagina` | Renderiza o título da página, subtítulo e ações no topo. | `Titulo`, `Subtitulo`, `Acoes` (`RenderFragment`) |
| `SeletorPeriodo` | Dropdown estilizado para escolha do intervalo de datas. | `Opcoes`, `Valor`, `ValorChanged` |
| `DashboardCard` | Card base padronizado com cabeçalho, ações, menu e slot para conteúdo. | `Titulo`, `Subtitulo`, `Acoes`, `Menu`, `ChildContent` |
| `KpiCard` | Exibe o indicador de desempenho (KPI) com valor, variação e sparkline. | `Kpi` |
| `GraficoReceita` | Exibe o gráfico de linhas comparando Receita e Meta mensal. | `Meses`, `Receita`, `Meta` |
| `GraficoDistribuicaoClientes` | Gráfico de rosca (Donut) com o total no centro e legenda por segmento. | `Total`, `Segmentos` |
| `PerformanceProjetos` | Lista os projetos com barras de progresso alinhadas e tarefas concluídas. | `Projetos` |
| `AtividadesRecentes` | Timeline/feed com as últimas ações dos usuários na plataforma. | `Atividades` |
| `ProjetosRecentes` | Tabela interativa de projetos com status em chips e porcentagem. | `Projetos` |


## Telas

### Tema Claro
![Tema Claro](docs/prints/tema-claro.png)

### Tema Escuro
![Tema Escuro](docs/prints/tema-escuro.png)

### Mobile
![Mobile](docs/prints/mobile.png)

### DevTools
![DevTools](docs/prints/devtools.png)

## O que aprendi

### 1. Como o Blazor WebAssembly inicia?
O navegador carrega o `wwwroot/index.html`, que exibe a tela de carregamento inicial no elemento `<div id="app">`. Em seguida, o runtime .NET via WebAssembly baixa as DLLs compiladas do C# e executa o `Program.cs`. A instrução `builder.RootComponents.Add<App>("#app")` substitui o indicador de carregamento pela aplicação Blazor e pelo roteador configurado no `App.razor`.

### 2. Qual a diferença entre Layout, Page e Component?
- **Layout (`MainLayout.razor`):** define a estrutura visual comum a várias telas, como o menu lateral (`MudDrawer`) e a barra superior (`MudAppBar`).
- **Page (`Dashboard.razor`):** é a página mapeada por uma rota `@page "/"`, responsável por estruturar os elementos da tela e consumir componentes.
- **Component (`KpiCard.razor`):** um bloco visual encapsulado, reutilizável e parametrizado para exibir informações específicas de forma modular.

### 3. Para que serve o `RenderFragment`?
O `RenderFragment` permite que um componente Blazor receba um bloco completo de marcação HTML e subcomponentes como parâmetro. O `DashboardCard` utiliza esse recurso no `ChildContent`, no `Menu` e nas `Acoes`, permitindo personalizar o conteúdo interno sem ter que recriar o cabeçalho e os cantos do card.

### 4. Como funciona o `@bind-Valor` no `SeletorPeriodo`?
O parâmetro `@bind-Valor` faz um bind bidirecional. O componente recebe o valor atual pela propriedade `Valor`. Quando uma opção é selecionada, o componente dispara o manipulador de eventos `ValorChanged.InvokeAsync(opcao)`, notificando a página pai para atualizar a variável associada.

### 5. Por que separar a pasta `Data`?
A pasta `Data` isola os dados das regras visuais dos componentes. Essa separação facilita a manutenção e possibilita trocar os dados mockados por chamadas REST a uma API backend no futuro (usando `HttpClient`), mantendo os componentes visuais totalmente intactos.

### 6. Como a responsividade funciona no `MudGrid` (`xs`, `sm`, `lg`)?
O `MudGrid` baseia-se no sistema flexbox de 12 colunas. Ao definir `xs="12" sm="6" lg="3"` nos cards de KPI, dizemos ao layout para ocupar 12 colunas em telas pequenas (1 card/linha), 6 colunas em tablets (2 cards/linha) e 3 colunas em telas grandes (4 cards/linha).

### 7. Como foi feita a estilização sem CSS e qual o papel do `MudTheme`?
Toda a estilização foi feita aproveitando o sistema de temas (`MudTheme`) e as classes utilitárias nativas do MudBlazor. O `MudTheme` centraliza as paletas de cores (modo claro e escuro), fontes e raios de borda, enquanto classes como `pa-4`, `d-flex` e `mud-text-secondary` resolvem alinhamentos, margens e cores pontuais.

### 8. Por que o namespace é `afya_admin` e não `afya-admin`?
O C# não permite o uso do caractere de hífen `-` em identificadores de código (pois o compilador interpreta como operador de subtração). Por essa razão, o .NET substitui o hífen pelo caractere de sublinhado `_`, definindo o namespace padrão do projeto como `afya_admin`.

Aqui tens o documento `README.md` completo, formatado exatamente com as marcações de títulos (`##`, `###`) e subtítulos que pediste, pronto a copiar e colar:

```markdown
# Afya Admin — Dashboard com Blazor WebAssembly e MudBlazor

Painel administrativo desenvolvido para a disciplina de Programação para Sistemas Web, utilizando **Blazor WebAssembly (.NET 10)** e componentes **MudBlazor 9**. O projeto foi construído inteiramente com componentes prontos e classes utilitárias, sem a necessidade de escrever regras de CSS personalizadas.

## Identificação

| Campo | Dados do Aluno |
| --- | --- |
| **Aluno(a)** | André Vinícius de Souza |
| **Matrícula** | *[Sua Matrícula]* |
| **Faculdade** | São Lucas campus 2 Porto Velho |
| **Curso** | Bacharelado em Ciência da Computação |
| **Disciplina** | Programação para Sistemas Web |
| **Professor(a)** | mestre liluyourd cury |
| **Semestre** | 2026.2 |

---

## Como executar o projeto

Para rodar a aplicação na sua máquina, certifique-se de ter o **.NET 10 SDK** instalado e siga os passos abaixo no terminal:

```bash
# Entre na pasta do projeto
cd afya-admin

# Restaure as dependências e inicie o servidor de desenvolvimento
dotnet restore
dotnet watch

```

Abra no navegador o endereço local fornecido pelo terminal (geralmente `https://localhost:xxxx`).

---

## Estrutura do Projeto

A organização dos diretórios foi estruturada para separar claramente os dados da interface visual:

```text
afya-admin/
├── Components/    componentes visuais reutilizáveis (cards, gráficos, tabelas) + Ui.cs
├── Data/          modelos (records) e dados fictícios do dashboard
├── Layout/        MainLayout (AppBar, Drawer, tema claro/escuro) e NavMenu
├── Pages/         Dashboard (rota raiz "/") e página de erro NotFound
├── wwwroot/       index.html, app.css global e assets/imagens
└── docs/prints/   capturas de tela (tema-claro.png, tema-escuro.png, mobile.png, devtools.png)

```

---

## Componentes Criados

| Componente | Responsabilidade | Parâmetros Principais |
| --- | --- | --- |
| `CabecalhoPagina` | Exibe o título principal, subtítulo e ações no topo. | `Titulo`, `Subtitulo`, `Acoes` |
| `SeletorPeriodo` | Menu suspenso para alternar o intervalo de exibição dos dados. | `Opcoes`, `Valor`, `ValorChanged` |
| `DashboardCard` | Estrutura base de card reutilizável com cabeçalho, ações e menu de contexto. | `Titulo`, `Subtitulo`, `Acoes`, `Menu`, `ChildContent` |
| `KpiCard` | Mostra os indicadores de desempenho com valores, variações e mini gráficos (*sparklines*). | `Kpi` |
| `GraficoReceita` | Gráfico de linhas dinâmico comparando a Receita versus a Meta mensal. | `Meses`, `Receita`, `Meta` |
| `GraficoDistribuicaoClientes` | Gráfico de rosca (*Donut*) com o total centralizado e legenda por segmento. | `Total`, `Segmentos` |
| `PerformanceProjetos` | Lista de projetos com barras de progresso alinhadas e contagem de tarefas. | `Projetos` |
| `AtividadesRecentes` | Feed em formato de timeline com as últimas ações realizadas na plataforma. | `Atividades` |
| `ProjetosRecentes` | Tabela interativa com status em chips coloridos e progresso detalhado. | `Projetos` |

---

## Telas da Aplicação

### Tema Claro

### Tema Escuro

### Versão Mobile

### Inspeção no DevTools

---

## O que aprendi

### 1. Como o Blazor WebAssembly inicia?

O navegador carrega inicialmente o ficheiro `wwwroot/index.html`, que exibe uma animação de carregamento dentro da tag `<div id="app">`. Em seguida, o runtime do .NET compilado em WebAssembly descarrega as DLLs do projeto e executa o `Program.cs`. A linha `builder.RootComponents.Add<App>("#app")` substitui o carregamento inicial pela aplicação Blazor e ativa o sistema de rotas do `App.razor`.

### 2. Qual a diferença entre Layout, Page e Component?

* **Layout (`MainLayout.razor`):** Define a estrutura comum da interface que se mantém fixa entre as páginas, como o menu lateral (`MudDrawer`) e a barra superior (`MudAppBar`).
* **Page (`Dashboard.razor`):** É o componente de nível de página associado a uma rota específica (`@page "/"`), servindo para organizar os blocos visuais.
* **Component (`KpiCard.razor`):** Um elemento isolado, modular e reutilizável que recebe dados via parâmetros para renderizar partes específicas da tela.

### 3. Para que serve o `RenderFragment`?

O `RenderFragment` funciona como um "espaço reservado" (*slot*) que permite injetar blocos de marcação HTML ou outros componentes dentro de um componente reutilizável. O `DashboardCard` utiliza esse recurso no `ChildContent`, nas `Acoes` e no `Menu` para manter uma estrutura padrão sem engessar o conteúdo interno.

### 4. Como funciona o `@bind-Valor` no `SeletorPeriodo`?

O vínculo bidirecional (`@bind-Valor`) passa o valor atual para dentro do componente através do parâmetro `Valor`. Quando o utilizador escolhe uma nova opção no menu, o componente dispara o evento `ValorChanged.InvokeAsync(opcao)`, atualizando automaticamente a variável correspondente na página pai.

### 5. Por que separar a pasta `Data`?

Isolar os dados e modelos (*records*) na pasta `Data` separa a regra de negócio e a fonte de dados da camada visual. Isso torna o código mais limpo e facilita futuras manutenções, como a substituição dos dados estáticos por chamadas a uma API REST real utilizando `HttpClient`.

### 6. Como a responsividade funciona no `MudGrid` (`xs`, `sm`, `lg`)?

O `MudGrid` divide a largura do ecrã com base em um sistema de 12 colunas. Ao configurar propriedades como `xs="12" sm="6" lg="3"`, determinamos que o elemento ocupará a linha inteira em telemóveis (`12` colunas), metade do espaço em tablets (`6` colunas) e um quarto em monitores de desktop (`3` colunas).

### 7. Como foi feita a estilização sem CSS e qual o papel do `MudTheme`?

Toda a identidade visual foi construída utilizando o sistema de temas do framework (`MudTheme`) e classes utilitárias nativas (como `pa-4`, `d-flex` e `mud-text-secondary`). O `MudTheme` centraliza as paletas de cores para os modos claro e escuro, tipografia e espaçamentos, eliminando a necessidade de ficheiros de folha de estilo customizados.

### 8. Por que o namespace é `afya_admin` e não `afya-admin`?

A linguagem C# proíbe o uso de hífens (`-`) em identificadores de código, pois interpretaria o caractere como uma operação matemática de subtração. Por essa razão, o compilador substitui automaticamente hífens por sublinhados (`_`), definindo o namespace raiz do projeto como `afya_admin`.

```

```
