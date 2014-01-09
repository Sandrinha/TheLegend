/*****************************
Jogo do Galo
"jogar." para começar o jogo
*****************************/

jogar:- 
	regras,
	jogada([v,v,v,v,v,v,v,v,v]).
	
regras:-
	write('Coloque x inserindo o inteiro da posicao seguido de .'),
	nl,
	mostra([1,2,3,4,5,6,7,8,9]).

jogada(Tabuleiro):-
	vitoria(Tabuleiro,x),
	write('Parabéns, ganhas-te o jogo!').

jogada(Tabuleiro):-
	vitoria(Tabuleiro,c),
	write('Perdes-te o jogo!').
	
jogada(Tabuleiro):-
	read(N),
	mover(Tabuleiro, N, NovoTabuleiro),
	mostra(NovoTabuleiro),
	respostapc(NovoTabuleiro, NovoTabuleiro2),
	mostra(NovoTabuleiro2),
	jogada(NovoTabuleiro2).

/****************************
Mostrador
****************************/	

mostra([A,B,C,D,E,F,G,H,I]):-
	write([A,B,C]),
	nl,
	write([D,E,F]),
	nl,
	write([G,H,I]),
	nl,nl.

/*****************************
Condiões de vitória
*****************************/

vitoria(Tabuleiro, Jogador):-
	vitoriaLinha(Tabuleiro, Jogador).

vitoria(Tabuleiro, Jogador):-
	vitoriaColuna(Tabuleiro, Jogador).

vitoria(Tabuleiro, Jogador):-
	vitoriaDiagonal(Tabuleiro, Jogador).


vitoriaLinha(Tabuleiro, Jogador):-
	Tabuleiro = [Jogador,Jogador,Jogador,_,_,_,_,_,_].

vitoriaLinha(Tabuleiro, Jogador):-
	Tabuleiro = [_,_,_,Jogador,Jogador,Jogador,_,_,_].

vitoriaLinha(Tabuleiro, Jogador):-
	Tabuleiro = [_,_,_,_,_,_,Jogador,Jogador,Jogador].


vitoriaColuna(Tabuleiro, Jogador):-
	Tabuleiro = [Jogador,_,_,Jogador,_,_,Jogador,_,_].

vitoriaColuna(Tabuleiro, Jogador):-
	Tabuleiro = [_,Jogador,_,_,Jogador,_,_,Jogador,_].

vitoriaColuna(Tabuleiro, Jogador):-
	Tabuleiro = [_,_,Jogador,_,_,Jogador,_,_,Jogador].


vitoriaDiagonal(Tabuleiro, Jogador):-
	Tabuleiro = [Jogador,_,_,_,Jogador,_,_,_,Jogador].

vitoriaDiagonal(Tabuleiro, Jogador):-
	Tabuleiro = [_,_,Jogador,_,Jogador,_,Jogador,_,_].


/*********************************
*********************************/

mover([v,B,C,D,E,F,G,H,I], 1, [x,B,C,D,E,F,G,H,I]).
mover([A,v,C,D,E,F,G,H,I], 2, [A,x,C,D,E,F,G,H,I]).
mover([A,B,v,D,E,F,G,H,I], 3, [A,B,x,D,E,F,G,H,I]).
mover([A,B,C,v,E,F,G,H,I], 4, [A,B,C,x,E,F,G,H,I]).
mover([A,B,C,D,v,F,G,H,I], 5, [A,B,C,D,x,F,G,H,I]).
mover([A,B,C,D,E,v,G,H,I], 6, [A,B,C,D,E,x,G,H,I]).
mover([A,B,C,D,E,F,v,H,I], 7, [A,B,C,D,E,F,x,H,I]).
mover([A,B,C,D,E,F,G,v,I], 8, [A,B,C,D,E,F,G,x,I]).
mover([A,B,C,D,E,F,G,H,v], 9, [A,B,C,D,E,F,G,H,x]).

mover(Tabuleiro, N, Tabuleiro):-
	write(N),
	nl,
	write('Essa Jogada não é válida'),
	nl.


/**********************************
**********************************/

respostapc(Tabuleiro, NovoTabuleiro):-
	moverpc(Tabuleiro, c, NovoTabuleiro),
	vitoria(NovoTabuleiro, c),
	!.

respostapc(Tabuleiro, NovoTabuleiro):-
	moverpc(Tabuleiro, c, NovoTabuleiro),
	not(jogador_ganha_1_jogada(NovoTabuleiro)).

respostapc(Tabuleiro, NovoTabuleiro):-
	moverpc(Tabuleiro, c, NovoTabuleiro).

respostapc(Tabuleiro, NovoTabuleiro):-
	not(member(v,Tabuleiro)),
	!,
	write('O jogo ficou empatado!'),
	nl,
	NovoTabuleiro = Tabuleiro.

/**************************************
**************************************/

moverpc([v,B,C,D,E,F,G,H,I], Jogador, [Jogador,B,C,D,E,F,G,H,I]).
moverpc([A,v,C,D,E,F,G,H,I], Jogador, [A,Jogador,C,D,E,F,G,H,I]).
moverpc([A,B,v,D,E,F,G,H,I], Jogador, [A,B,Jogador,D,E,F,G,H,I]).
moverpc([A,B,C,v,E,F,G,H,I], Jogador, [A,B,C,Jogador,E,F,G,H,I]).
moverpc([A,B,C,D,v,F,G,H,I], Jogador, [A,B,C,D,Jogador,F,G,H,I]).
moverpc([A,B,C,D,E,v,G,H,I], Jogador, [A,B,C,D,E,Jogador,G,H,I]).
moverpc([A,B,C,D,E,F,v,H,I], Jogador, [A,B,C,D,E,F,Jogador,H,I]).
moverpc([A,B,C,D,E,F,G,v,I], Jogador, [A,B,C,D,E,F,G,Jogador,I]).
moverpc([A,B,C,D,E,F,G,H,v], Jogador, [A,B,C,D,E,F,G,H,Jogador]).


/*************************************
*************************************/

jogador_ganha_1_jogada(Tabuleiro):-
	moverpc(Tabuleiro, x, NovoTabuleiro),
	vitoria(NovoTabuleiro, x).
	











