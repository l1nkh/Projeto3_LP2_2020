# Projeto3_LP2_2020

## Autoria

* Diogo Henriques, nº 21802132
* João Dias, nº 21803573
* Pedro Fernandes, nº 21803791

### Distribuição de Tarefas

_Todos os membros do grupo estiveram envolvidos no projeto desde o seu
início até à sua entrega_

1. O que cada membro fez
    * Diogo Henriques:

            Responsável pelo lado de Unity.
            Envolvido no desenvolvimento da base de Felli.
            Envolvido no Markdown.

    * João Dias:

            Responsável pelo lado de Unity.
            Envolvido no desenvolvimento da base de Felli.

    * Pedro Fernandes:

            Responsável pelo lado de consola.
            Envolvido no Markdown.

[Repositório Git](https://github.com/l1nkh/Projeto3_LP2_2020)

---

## Arquitetura da Solução

### Design Patterns utilizados

* **Model-View-Controller (MVC)** -
  
* **Observer** -

---

### Jogo (Felli)

Foi utilizada uma implemetnação do Felli desenvolvida no passado por parte de
um grupo do qual Diogo Henriques e João Dias faziam parte. Começando com este,
foi feita depois uma restruturação do mesmo, de modo a garantir o funcionamento
com o MVC Pattern.

> Como referência para o projeto tem-se as regras disponíveis na página de
> wikipedia sobre o mesmo (link nas referências), estas são as mesmas que as
> propostas no
> [enunciado do projeto](https://github.com/VideojogosLusofona/lp2_2020_p3).

#### O tabuleiro é um array de Pieces

* **Piece** é uma `struct` que representa um espaço no tabuleiro e as peças do
  mesmo. Estes servem para identificar se um espaço está vazio, ocupado por uma
  peça amarela, se está ocupado por uma peça vermelha, ou se é um espaço
  "bloqueado". Este último é o que nos permite, através de um array
  bi-dimensional, representar um tabuleiro de Felli.

#### Sistema de Input (Consola)

* A `function` GetInput() está construido de modo a prestar atenção apenas às 
  teclas que são consideradas válidas para cada fase do jogo, repetindo um loop
  até receber algo correto. Este distingue os inputs a fazer e as ações a fazer
  com eles através de uma `enum` GameState:
  * **Menu** (em que o jogador decide se quer jogar, ver a ajuda ou sair do
  jogo)
  * **TurnSelection** (em que se decide quem vai primeiro)
  * **SelectPiece** (onde se escolhe a peça que se quer mexer)
  * **SelectDirection** (onde se escolhe em que direção essa peça se vai mexer)
  * **VictoryScreen** (para escolher regressar a "StartGame" ou fechar o jogo).

* "ChoosePiece" e "ChooseDirection" constituem o GameLoop, com o turno
  alternando após uma transformação da posição de uma peça é feita com sucesso.
  Ao fim de cada turno, é feita uma verificação para identificar se existe um
  vencedor, passando para o estado de "AnnounceWinner", sendo que aqui os
  jogadores podem escolher regressar a "StartGame" ou fechar o jogo.

#### A classe Controller é a ligação de ConsoleView com a 'Common'

* `class` **Controller** tem os métodos que interagem com o jogo em si,
  presente no Commonm, desta maneira isolando a `class` **ConsoleView**,
  que somente recebe input e escreve mensagens na consola, do jogo em si.
  **ConsoleView** recebe o input dado pelo jogador e chama a classe
  **Controller** para esta verificar a validez dos inputs no contexto do *Felli*
  em si (verificar se a peça selecionada está 'viva', por exemplo), e realiza as
  operações requesitadas pelo jogador, caso controller verifique que estas
  são validas.

* Os métodos da `class` **Controller** compreendem múltiplos metodos da
  'Common' em si. Por exemplo, a `function` CheckForDirection verifica se a
  direção em que a peça selecionada é pretendida mover-se é válida, e se o
  for, realiza a transformação, retornando um boolean verdadeiro para informar
  a `function` GetInput() que a transformação foi feita com sucesso e que
  pode-se passar para a próxima fase. Controller, com os seus métodos,
  funciona como uma façade da 'Common' para GetInput e - efetivamente - para
  a `class` **ConsoleView**.

### Unity

#### Interesse 1

* A B C D

#### Interesse 2

* 1 2 3 4

#### Input

* Ponham o que acharem interessante.

### Diagrama UML

![Diagrama UML](/images/uml.png)

---

### Fluxograma

![Fluxograma](/images/flowchart.png)

---

## Referências

* Regras de jogo usadas para referência.
  [(link)](https://en.wikipedia.org/wiki/Felli)

* Base para a implementação do jogo _Felli_.
  [(link)](https://github.com/FPTheFluffyPawed/Project2_LP2019)

* Aula dada pelo professor Nuno Fachada em relação ao Model-View-Controller
  [(link)](https://www.youtube.com/watch?v=_z_iRUjmvzE&feature=youtu.be)