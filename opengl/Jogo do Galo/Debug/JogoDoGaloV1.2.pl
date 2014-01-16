/*******************************
Jogo do Galo v1.2- TheLegend
José Vilela
13-01-2014

Implementaçao do Jogo do Galo
usando método minimax para interacao com C++
**********************************/

/**************************
como jogar jogo do galo:

inciar jogo:

inicio(c,Q).

c= x ou X para jogador 1 = humano
c =o ou O para jogador 1 = pc
em Q é retornado a jogada do pc caso seja o 1º a jogar	

jogada de humano:

jogar(n,p,Q).

n = 1 ou 2 (conforme o jogador humano seja 1 ou 2)
p = posiçao da prox jogada do humano	
em Q é retornada a jogada do pc

fim(V). -> mostra o vencedor V = 1 (Jogador 1 vence), V = 2 (Jogador 2 vence), V = 0(Empate)


**************************/

/***************************
			FACTOS
****************************/

%%% Determina o próximo jogador depois do jogador actual

prox_jogador(1,2). 
prox_jogador(2,1).


%%% Determina o marcador oposto ao marcador actual

inverte_marcador('x', 'o').
inverte_marcador('o', 'x').


%%% Define o marcador de cada jogador

marcador_jogador(1, 'x').
marcador_jogador(2, 'o').


%%% Atalho para o marcador oposto de cada jogador

marcador_oponente(1, 'o').
marcador_oponente(2, 'x').


%%% Marcador usado para espaço vazio

marcador_vazio('v').


%%% O jogador que está a jogar o X está sempre a tentar maximizar a utilidade da posiçao do tabuleiro

maximizar('x').


%%% O jogador que está a jogar o O está sempre a tentar minimizar a utilidade da posiçao do tabuleiro

minimizar('o').



/************************************
			MAIN PROGRAM
************************************/

%%% Inicio do programa

inicio(M,Q):-
	iniciar(M,Q).
	
%%% Mostra mensagem de boas vindas e inicializa o jogo	

iniciar(M,Q):-
	inicializar,
	nl,nl,nl,
	write('Benvindo ao Jogo do Galo'),nl,
	humano_joga(M),!,
	mostra_jogadores,!,
	(M == 'o' ; M == 'O'),
	tabuleiro(T),!,
	jogada(1,T,Q),!,
	tabuleiro(T2),!,
	mostra_tabuleiro(T2).

	
%%% Cria um Tabuleiro vazio

inicializar:-
	marcador_vazio(V),
	asserta( tabuleiro([V,V,V, V,V,V, V,V,V])).
	
	
%%% Fim do Jogo, mostra o vencedor e permite jogar novamente
	
fim(X):-
	tabuleiro(T),
	nl,nl,
	write('Fim do Jogo: '),
	mostra_tabuleiro(T),nl,
	mostra_vencedor(T,X),
	retract(tabuleiro(_)),
	retract(jogador(_,_)).

	
%%% Verifica se o utilizador deseja jogar novamente
	
ler_jogar_outravez(V):-
	nl,nl,
	write('Jogar novamente (S/N)? '),
	read(V),
	(V == 'S' ; V == 's' ; V == 'N' ; V == 'n'), ! .
	
ler_jogar_outravez(V):-
	nl,nl,
	write('Por favor insira S ou N. '),
	ler_jogar_outravez(V).

	
%%% Define quem joga primeiro

humano_joga(M):-
	(M == 'x' ; M == 'X'),
	asserta( jogador(1, humano) ),
	asserta( jogador(2, pc) ), !.
	
humano_joga(M):-
	(M == 'o' ; M == 'O'),
	asserta( jogador(1, pc) ),
	asserta( jogador(2, humano) ), !.
	
humano_joga(M):-
	nl,
	write('Por favor insira X ou O (X joga primeiro)'),
	read(V),
	humano_joga(V).

	
%%% 
	
jogar(J,Q,Q2):-
	jogador(J, Tipo),
	Tipo == humano,
	tabuleiro(T), !,
	not(fim_do_jogo(J, T)), !,
	jogada1(J,T,Q,Q2),!,
	tabuleiro(T2), !,
	mostra_tabuleiro(T2), !.
	

	
%%% quadrado - 

quadrado([M,_,_, _,_,_, _,_,_],1,M).
quadrado([_,M,_, _,_,_, _,_,_],2,M).
quadrado([_,_,M, _,_,_, _,_,_],3,M).
quadrado([_,_,_, M,_,_, _,_,_],4,M).
quadrado([_,_,_, _,M,_, _,_,_],5,M).
quadrado([_,_,_, _,_,M, _,_,_],6,M).
quadrado([_,_,_, _,_,_, M,_,_],7,M).
quadrado([_,_,_, _,_,_, _,M,_],8,M).
quadrado([_,_,_, _,_,_, _,_,M],9,M).
	
	
%%% Condições de Vitória	
	
vitoria([M,M,M, _,_,_, _,_,_],M).
vitoria([_,_,_, M,M,M, _,_,_],M).
vitoria([_,_,_, _,_,_, M,M,M],M).
vitoria([M,_,_, M,_,_, M,_,_],M).
vitoria([_,M,_, _,M,_, _,M,_],M).
vitoria([_,_,M, _,_,M, _,_,M],M).
vitoria([M,_,_, _,M,_, _,_,M],M).
vitoria([_,_,M, _,M,_, M,_,_],M).


%%% mover - 
%%% mete o marcador M no quadrado Q do tabuleiro T e retorna o novo tabuleiro T2

mover(T,Q,M,T2):-
	insere_item(T,Q,M,T2).

	
%%% Fim do Jogo - Determina quando se chega ao fim do jogo

fim_do_jogo(J,T):-
	fim_do_jogo2(J,T).
	
fim_do_jogo2(J,T):-
	marcador_oponente(J,M),
	vitoria(T, M).
	
fim_do_jogo2(J,T):-
	marcador_vazio(V),
	not(quadrado(T,Q,V)).
	
	
%%% jogada - pede a proxima jogado do humano ou pc, depois aplica essa jogada

jogada(J,T,Q2):-
	jogador(J, Tipo),
	jogada2(Tipo, J, T, T2,Q2),
	retract( tabuleiro(_) ),
	asserta( tabuleiro(T2) ).
	
jogada1(J,T,Q,Q2):-
	jogador(J, Tipo),
	jogada22(Tipo, J, T, Q, T2),
	retract( tabuleiro(_) ),
	asserta( tabuleiro(T2) ),
	prox_jogador(J,J2),!,
	jogada(J2,T2,Q2),!.

jogada22(humano,J,T,Q,T2):-
	nl,nl,
	marcador_vazio(V),
	quadrado(T,Q,V),
	marcador_jogador(J,M),
	mover(T,Q,M,T2), !.
	
jogada2(pc,J,T,T2,Q2):-	
	nl,nl,
	tabuleiro(T), !,
	not(fim_do_jogo(J, T)), !,
	write('O computador esta a pensar na proxima jogada...'),
	marcador_jogador(J,M),
	minimax(0,T,M,Q,U),
	mover(T,Q,M,T2),
	nl,nl,
	write('O computador coloca '),
	write(M),
	write(' no quadrado '),
	write(Q),
	Q2 = Q,
	write('.').
	
	
%%% jogadas - lista de jogadas possíveis nos quadrados vazios do tabuleiro
	
jogadas(T,L):-
	not(vitoria(T,x)),
	not(vitoria(T,o)),
	marcador_vazio(V),
	findall(N,quadrado(T,N,V),L),
	L \= [] .
	
	
%%% utilidade - determina o valor de uma determinada posição no tabuleiro	
	
utilidade(T,U):-
	vitoria(T,'x'),
	U = 1, !.
	
utilidade(T,U):-
	vitoria(T,'o'),
	U = (-1), !.
	
utilidade(T,U):-
	U = 0.
	
	
%%% minimax
%%% 

minimax(P,[V,V,V, V,V,V, V,V,V],M,Q,U):-
	marcador_vazio(V),
	aleatorio_int_1n(9,Q),!.
	
minimax(P,T,M,Q,U):-
	P2 is P + 1,
	jogadas(T,L),!,
	melhor(P2,T,M,L,Q,U),!.
	
minimax(P,T,M,Q,U):-
	utilidade(T,U).
	
	
%%% melhor - determina qual a melhor jogada dentro da lista de jogadas possiveis chamando recursivamente o método minimax	
	
%%% se apenas existir uma jogada possível na lista de jogadas possiveis...

melhor(P,T,M,[Q1],Q,U):-
	mover(T,Q1,M,T2),
	inverte_marcador(M,M2),!,
	minimax(P,T2,M2,_Q,U),
	Q = Q1, !,
	mostra_valor(P,Q,U),!.
		
%%% se houver mais de uma jogada na lista de jogadas possiveis...

melhor(P,T,M,[Q1|Tail],Q,U):-
	mover(T,Q1,M,T2),
	inverte_marcador(M,M2),!,
	minimax(P,T2,M2,_Q,U1),
	melhor(P,T,M,Tail,Q2,U2),
	mostra_valor(P,Q1,U1),
	melhor2(P,M,Q1,U1,Q2,U2,Q,U).
	
	
%%% melhor2 - retorna a melhor jogada de duas baseado no valor da sua utilidade	

%%% se as duas jogadas tiverem o mesmo valor de utilidade escolhe uma aleatória

melhor2(P,M,Q1,U1,Q2,U2,Q,U):-
	maximizar(M),
	U1 > U2,
	Q = Q1,
	U = U1, !.

melhor2(P,M,Q1,U1,Q2,U2,Q,U):-
	minimizar(M),
	U1 < U2,
	Q = Q1,
	U = U1, !.
	
melhor2(P,M,Q1,U1,Q2,U2,Q,U):-
	U1 == U2,
	aleatorio_int_1n(10,R),
	melhor2a(P,R,M,Q1,U1,Q2,U2,Q,U), !.
	
melhor2(P,M,Q1,U1,Q2,U2,Q,U):-
	Q = Q2,
	U = U2, !.


%%% melhor2a - escolhe aleatoriamente um de dois quadrados com o mesmo valor de utilidade

melhor2a(P,R,M,Q1,U1,Q2,U2,Q,U):-
	R < 6,
	Q = Q1,
	U = U1, !.
	
melhor2a(P,R,M,Q1,U1,Q2,U2,Q,U):-
	Q = Q2,
	U = U2, !.	
	

/*****************************************
			MOSTRADOR
*****************************************/	
	
mostra_jogadores:-
	nl,
	jogador(1,V1),
	write('Jogador 1 e '),
	write(V1),
	nl,
	jogador(2,V2),
	write('Jogador 2 e '),
	write(V2),!.
	
mostra_vencedor(T,X):-
	vitoria(T,x),
	write('X ganha o jogo'), !,
	X = 1.
	
mostra_vencedor(T,X):-
	vitoria(T,o),
	write('O ganha o jogo'), !,
	X = 2.

mostra_vencedor(T,X):-
	write('O jogo terminou empatado'),
	X = 0.

mostra_tabuleiro(T):-
	nl,nl,
	mostra_quadrado(T,1),
	write('|'),
	mostra_quadrado(T,2),
	write('|'),
	mostra_quadrado(T,3),
	nl,
	write('-----------'),
	nl,
	mostra_quadrado(T,4),
	write('|'),
	mostra_quadrado(T,5),
	write('|'),
	mostra_quadrado(T,6),
	nl,
	write('-----------'),
	nl,
	mostra_quadrado(T,7),
	write('|'),
	mostra_quadrado(T,8),
	write('|'),
	mostra_quadrado(T,9), !.
	
mostra_tabuleiro:-
	tabuleiro(T),
	mostra_tabuleiro(T), !.
	
mostra_quadrado(T,Q):-
	quadrado(T,Q,M),
	write(' '),
	mostra_quadrado2(Q,M),
	write(' '), !.
	
mostra_quadrado2(Q,V):-
	marcador_vazio(V),
	write(Q), !.
	
mostra_quadrado2(Q,M):-
	write(M), !.
	
mostra_valor(P,Q,U):-
	P == 1,
	nl,
	write('Quadrado '),
	write(Q),
	write(', utilidade: '),
	write(U), !.
	
mostra_valor(P,Q,U):-
	true.
	
	
/******************************************
			NUMEROS ALEATORIOS
******************************************/	
	
aleatorio_int_1n(N, V):-
	V is random(N) + 1, !.

/*******************************************
			AUXILIARES
*******************************************/	
	
%%% insere_item - dado a lista L substitui o item V na posicao N 
	
insere_item(L, N, V, L2) :-
	insere_item2(L, N, V, 1, L2).

insere_item2( [], N, V, A, L2) :- 
	N == -1, 
	L2 = [].

insere_item2( [_|T1], N, V, A, [V|T2] ) :- 
	A = N,
	A1 is N + 1,
	insere_item2( T1, -1, V, A1, T2 ).

insere_item2( [H|T1], N, V, A, [H|T2] ) :- 
	A1 is A + 1,
	insere_item2( T1, N, V, A1, T2 ).
	