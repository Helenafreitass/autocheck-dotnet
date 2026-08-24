# AutoCheck.ConsoleApp — Motor de Vistoria Veicular

Projeto do Mini-Projeto Avaliativo (Módulo 01 - Semana 08) do curso de Desenvolvedor(a) Back-End .NET.

## O que o sistema faz

É uma aplicação de console em C# que simula o processo de vistoria técnica usado por concessionárias antes de comprar ou aceitar um veículo usado. O usuário cadastra um veículo (Carro, Moto ou Caminhão), responde o checklist de inspeção específico daquele tipo de veículo, e o sistema:

- calcula a pontuação obtida com base no status de cada item ("Bom" = 10 pts, "Regular" = 5 pts, "Ruim" = 0 pts);
- calcula o percentual de aprovação em relação à pontuação máxima possível;
- classifica o veículo em **Aprovado com Excelência**, **Aprovado com Apontamentos** ou **Reprovado na Vistoria**;
- lista os itens críticos e os itens de atenção, com uma recomendação de serviço para a oficina.

## Como executar

Pré-requisito: .NET SDK instalado (o projeto usa `net8.0`).

```bash
git clone <link-do-seu-repositorio>
cd autocheck-dotnet/src/AutoCheck.ConsoleApp
dotnet run
```

O sistema abre um menu no terminal com as opções:

- `1` — Realizar nova vistoria
- `2` — Exibir relatório das vistorias já feitas
- `0` — Sair

## Regra de cálculo adotada

A pontuação de cada item é somada e comparada com a pontuação máxima possível (total de itens × 10):

```
Percentual (%) = (Pontuação Obtida / Pontuação Máxima Possível) × 100
```

Fiz o cast para `double` antes da divisão (`(double)pontuacaoObtida / pontuacaoMaxima`), porque em C# a divisão entre dois `int` trunca o resultado — sem isso o percentual sempre daria um número "quebrado" errado.

As faixas de classificação usadas foram:

| Percentual | Classificação |
|---|---|
| 90% a 100% | Aprovado com Excelência |
| 60% a 89% | Aprovado com Apontamentos |
| 0% a 59% | Reprovado na Vistoria |

## Conceitos do Módulo 01 aplicados e onde

- **Classes, atributos e construtores**: `Veiculo`, `ItemVistoria` e as subclasses, todas com construtor explícito usando `this`.
- **Encapsulamento**: propriedades com `{ get; set; }` em todas as classes de modelo.
- **Herança (`:`)**: `Carro`, `Moto` e `Caminhao` herdam de `Veiculo`.
- **Polimorfismo (`virtual` / `override`)**: `ObterChecklistObrigatorio()` é sobrescrito em cada subclasse pra devolver o checklist específico daquele tipo de veículo.
- **Coleções (`List<T>`)**: lista de itens de vistoria dentro de cada veículo, e a lista central de vistorias no `Program.cs`.
- **Laços tradicionais (`for`/`foreach`) e condicionais (`if`/`else`)**: usados em todo o `MotorVistoria` pra somar pontos, filtrar itens críticos/de atenção e montar as recomendações — sem LINQ, como pedido no enunciado.

## Sobre arquitetura cliente-servidor

Esse projeto é uma **aplicação de console standalone**: não existe separação entre cliente e servidor, tudo roda no mesmo processo, na mesma máquina, e os dados ficam só em memória (a lista de vistorias se perde quando o programa fecha).

Numa arquitetura cliente-servidor, o **cliente** (ex: um navegador, um app mobile, ou até outro console) faria requisições para um **servidor** (uma API, por exemplo, feita em ASP.NET Core), que processaria a lógica de negócio e devolveria a resposta — normalmente com os dados persistidos em um banco de dados, não em memória. Se eu fosse evoluir esse projeto, o próximo passo natural seria transformar o `MotorVistoria` numa API .NET, com o console (ou uma tela web) atuando como cliente que consome essa API.

## Vídeo de apresentação

`<colar aqui o link do vídeo no Drive/YouTube>`

## Uso de IA

`<descrever aqui, no vídeo e/ou no README, onde você usou apoio de IA e o que você validou/entendeu do código>`
