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
            Responsável pela organização do projeto e implementação do MVC 
            no GitHub.
            Envolvido no desenvolvimento da implementação original do Felli.
            Realizou o UML
            Envolvido no Markdown.

    * João Dias:

            Responsável pelo lado de Unity.
            Envolvido no desenvolvimento da implementação original do Felli.
            Contribuiu para a correção e restruturação da implementação do 
            Felli de modo a acomodar o MVC.

    * Pedro Fernandes:

            Responsável pelo lado de Consola.
            Encarregado de corrigir e reconstruir a implementação do Felli 
            de modo a acomodar o MVC.
            Envolvido no Markdown.

[Repositório Git](https://github.com/l1nkh/Projeto3_LP2_2020)

---

## Arquitetura da Solução

### Design Patterns utilizados

* **Model-View-Controller (MVC)** - Este pattern é constituido por 3 peças
  fundamentais, o View, o Model e o Controller. O modelo é o contentor do código
  comum entre as duas aplicações diferentes do jogo Felli. O Controller manipula
  o input do utilizador e comunica com o modelo para o saber o que fazer com
  esse input. A View é responsável pela renderização do jogo no ecrã e é sempre
  atualizado através do controller e do model quando o utilizador faz inputs e
  altera o estado do jogo.
  
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

* **Piece** representa uma peça do tabuleiro, sendo um espaço vazio representado
  como null. Desta maneira percebe-se se um espaço está vazio, ocupado por uma
  peça branca, se está ocupado por uma peça preta, ou se é um espaço
  "bloqueado". Este último é o que nos permite, através de um array
  bi-dimensional, simular o tabuleiro de Felli, com o seu formato de
  "ampulheta".

### MVC - Consola

#### Sistema de Input

* A `function` GetInput() está construida de modo a prestar atenção apenas às 
  teclas que são consideradas válidas para cada fase do jogo, repetindo um loop
  até receber algo correto. Esta distingue os inputs a fazer e as ações a fazer
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
  vencedor, passando para o estado de "AnnounceWinner", sendo que aqui, os
  jogadores podem escolher regressar a "StartGame" ou fechar o jogo.

#### A classe Controller é a ligação de ConsoleView com a 'Common'

* `class` **Controller** tem os métodos que interagem com o jogo em si,
  presente no Common, desta maneira isolando a `class` **ConsoleView**,
  que somente recebe input e escreve mensagens na consola, do jogo em si.
  **ConsoleView** recebe o input dado pelo jogador e chama a classe
  **Controller** para esta verificar a validez dos inputs no contexto do *Felli*
  em si (verificar se a peça selecionada está 'viva', por exemplo), e realiza as
  operações requesitadas pelo jogador, caso **Controller** verifique que estas
  são validas.

* Os métodos da `class` **Controller** compreendem múltiplos metodos da
  'Common' em si. Por exemplo, a `function` CheckForDirection() verifica se a
  direção em que a peça selecionada é pretendida mover-se é válida, e se o
  for, realiza a transformação, retornando um boolean verdadeiro para informar
  a `function` GetInput() que a transformação foi feita com sucesso e que
  pode passar para a próxima fase do jogo. Controller faz isto comunicando com
  a `class` **GameManager**, que funciona como uma façade da 'Common' para 
  **Controller**, sendo esta a ponte entre a mesma e a `class` 
  **ConsoleView**.

### Unity

#### Container

* A classe `Container` serve como contentor da informação do model, para ser
  usado nas classes `Controller` e `Viewer`, não tendo assim de criar múltiplas
  cópias das instâncias que queremos usar.

#### Controller

* A classe `Controller` recebe o Scriptable Object `Container`, ganhando assim
  acesso às classes vindas do model para as poder manipular o input do
  utilizador e mover as peças dentro do jogo.

#### View e Update Piece

* A classe `UnityView` recebe o Scriptable Object `Container` e é responsável
  por atualizar os butões (representativos do mapa e das peças), a classe possui
  uma condição que que verifica o estado da peça, para saber o que imprimir no
  butão. A classe `UpdatePiece` usa o método `UpdatePiece()` da classe
  `UnityView` e atualiza-o todos os frames através do `FixedUpdate()`.

### Diagrama UML

![Diagrama UML](/images/uml.png)

---

## Referências

* Regras de jogo usadas para referência.
  [(link)](https://en.wikipedia.org/wiki/Felli)

* Base para a implementação do jogo _Felli_.
  [(link)](https://github.com/FPTheFluffyPawed/Project2_LP2019)

* Aula dada pelo professor Nuno Fachada em relação ao Model-View-Controller
  [(link)](https://www.youtube.com/watch?v=_z_iRUjmvzE&feature=youtu.be)
