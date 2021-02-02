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

            Envolvido no Unity no MVC.
            Responsável pela organização do projeto e implementação do MVC 
            no GitHub.
            Envolvido no desenvolvimento da implementação original do Felli.
            Realizou o UML.
            Envolvido no Markdown.

    * João Dias:

            Responsável pelo Unity no MVC.
            Envolvido no desenvolvimento da implementação original do Felli.
            Contribuiu para a correção e restruturação da implementação do 
            Felli de modo a acomodar o MVC.

    * Pedro Fernandes:

            Responsável pela Consola no MVC.
            Encarregado de corrigir e reconstruir a implementação do Felli 
            de modo a acomodar o MVC.
            Envolvido no Markdown.

[Repositório Git](https://github.com/l1nkh/Projeto3_LP2_2020)

---

## Arquitetura da Solução

### Design Patterns utilizados

* **Model-View-Controller (MVC)** - Este pattern é constituido por 3 peças
  fundamentais, o *Model*, o *View* e o *Controller*.

  * O **Model** é o contentor do código comum (o 'Common') entre as duas
    aplicações diferentes do jogo _Felli_.
  * A **View** é responsável pela renderização do jogo no ecrã e receber
    o *input* do jogador. Com este, chama os métodos da `class` **Controller**
    apropriados que por sua vez vão aceder ao _Common_ para progredir com o
    jogo. Como está construido, de acordo com o resultado dos métodos de
    **Controller**, **ConsoleView** sabe se a jogada foi válida e portanto pode
    passar a "ouvir" o _input_ relacionado com a próxima fase, realizando
    as ações associadas à mesma.
  * O **Controller**, de acordo com o *input* do utilizador - recebido e
    interpretado no *View* - comunica com o *Common* para que este realize as
    ações de jogo pretendidas, interpretando por sua vez o resultados dos
    mesmos e retornando uma resposta ao **ConsoleView** (que chamou o seu
    método).
  
* **Facade** - Este pattern está presente através da `class` **GameManager**,
  que é o ponto de contacto entre o _Felli_ e as versões de consola e Unity.
  Os metodos de **GameManager** compreendem as ações gerais do jogo, sendo
  estas: verificar se a peça selecionada - através do *input* do jogador -
  está viva _e_ tem movimentos possíveis ('IsPieceAvailable()'), se o movimento
  selecionado para a peça escolhida é válido, realizando-o se for
  ('CanDoMove()'), e por fim, verificando se - após um jogador realizar uma
  jogada - as peças do adversário foram todas derrotadas, indicando que o jogo
  terminou. **GameManager** faz isto chamando a sequência de métodos da `class`
  **Board** necessários para a ação requisitada a ser realizada.

---

### Jogo (Felli)

Foi utilizada como base para a implemetnação do _Felli_ uma versão
desenvolvida por um grupo do qual Diogo Henriques e João Dias faziam parte.
Começando com este, foi feita depois uma restruturação do mesmo, de modo a
garantir o funcionamento com o MVC Pattern, corrigir quaisquer erros existentes
e garantir que o jogo possuia todas as funcionalidades esperadas.

> Como referência para o projeto tem-se as regras disponíveis na página de
> Wikipedia sobre o mesmo (link nas referências), estas são as mesmas que as
> propostas no
> [enunciado do projeto](https://github.com/VideojogosLusofona/lp2_2020_p3).

#### O tabuleiro é um array de Pieces

* A `class` **Piece** representa uma peça do tabuleiro, tendo esta uma
  propriedade que indica o seu tipo, um espaço vazio é representado no `array`
  que representa o tabuleiro como _null_. Desta maneira, distingue-se um
  espaço vazio de um ocupado por uma peça branca, de um ocupado por uma peça
  preta, ou se é um espaço "bloqueado". Este último é o que nos permite,
  num `array bi-dimensional`, simular o tabuleiro de _Felli_, com o seu formato
  de "ampulheta".

#### Piece sets

* **Piece** é utilizado como tipo de *referência* de modo a utilizar-se um
  `array` *pieceSet* para as peças de cada jogador (para cada cor). Isto é
  feito para que se consiga indentificar e interagir com peças específicas de
  um jogador de forma eficiente. Transformações feitas nas **Pieces**
  específicas dentro dos *pieceSets* são reletidas no *boardArray* por
  partilharem referências às mesmas instâncias. Um exemplo disto é quando se
  altera a posição de uma **Piece** após verificar-se que o movimento
  selecionado para ela é válido. Isto facilita também verificações como a
  realizada para identificar uma vitória, sendo que isto é feito
  verificando-se se a *pieceSet* do adversário do jogador atual só tem *nulls*.

### MVC - Consola

#### Sistema de Input

* A `function` GetInput() está construida de modo a prestar atenção apenas às
  teclas que são consideradas válidas para cada fase do jogo, repetindo um loop
  até receber algo correto. Esta distingue os *inputs* a fazer e as ações a
  fazer com eles através de uma `enum` **GameState**:
  * **Menu** (em que o jogador decide se quer jogar, ver a ajuda ou sair do
  jogo)
  * **TurnSelection** (em que se decide quem vai primeiro)
  * **SelectPiece** (onde se escolhe a peça que se quer mexer)
  * **SelectDirection** (onde se escolhe em que direção essa peça se vai mexer)
  * **VictoryScreen** (para escolher regressar a "StartGame" ou fechar o jogo).

* "ChoosePiece" e "ChooseDirection" constituem o gameloop, com o turno
  alternando após uma transformação da posição de uma peça é feita com sucesso.
  Ao fim de cada turno, é feita uma verificação para identificar se existe um
  vencedor, passando para o estado de "AnnounceWinner", sendo que aqui, os
  jogadores podem escolher regressar a "StartGame" ou fechar o jogo.

#### A classe Controller é a ligação de ConsoleView com a 'Common'

* `class` **Controller** tem os métodos que interagem com o jogo em si,
  presente no *Common*, desta maneira isolando a `class` **ConsoleView**,
  que somente recebe input e escreve mensagens na consola, do jogo em si.
  **ConsoleView** recebe o *input* dado pelo jogador e chama a classe
  **Controller** para esta verificar a validez dos *inputs* no contexto do
  *Felli* em si (verificar se a peça selecionada está 'viva', por exemplo), e
  realiza as operações requesitadas pelo jogador, caso **Controller** verifique
  que estas são validas.

* Os métodos da `class` **Controller** compreendem múltiplos metodos da
  *Common* em si. Por exemplo, a `function` CheckForDirection() verifica se a
  direção em que a peça selecionada é pretendida mover-se é válida, e se o
  for, realiza a transformação, retornando um `boolean` verdadeiro para informar
  a `function` GetInput() que a transformação foi feita com sucesso e que
  pode passar para a próxima fase do jogo. **Controller** faz isto comunicando
  com a `class` **GameManager**, que funciona como uma façade da 'Common' para
  **Controller**, sendo esta a ponte entre a mesma e a `class`
  **ConsoleView**.

### MVC - Unity

#### Container

* A `class` **Container** serve como contentor da informação do **Model**, para
  ser usado nas `classes` **Controller** e **Viewer**, não tendo assim de criar
  múltiplas cópias das instâncias que queremos usar.

#### Controller

* A classe `Controller` recebe o `Scriptable Object` **Container**, ganhando
  assim acesso às classes vindas do model para as poder manipular o *input* do
  utilizador e mover as peças dentro do jogo.

#### View e Update Piece

* A `class` **UnityView** recebe o `Scriptable Object` **Container** e é
  responsável por atualizar os butões (representativos do mapa e das peças),
  a `class` possui uma condição que que verifica o estado da **Piece**, para
  saber o que imprimir no butão. A `class` **UpdatePiece** usa a `function`
  UpdatePiece() da `class` **UnityView** e atualiza-o todos os *frames* através
  do `FixedUpdate()`.

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
