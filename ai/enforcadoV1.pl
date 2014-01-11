/******************************
Enforcado - TheLegend
José Vilela
11-01-2014
******************************/

palavras([sopa, informatica, francesinha, porto, thelegend]).

%%% inicio - inicio do Jogo

inicio:-
	escolhePalavra(Palavra),!,
	write('Benvindo ao enforcado'),
	nl,
	name(Palavra,LPalavra),
	criaVazios(LPalavra,LVazia),
	jogar(LPalavra,LVazia,0).

	
%%% escolhePalavra/1 - coloca no 1º argumento uma palavra escolhida aleatóriamente
	
escolhePalavra(Palavra):-
	palavras(L),
	length(L,N),
	aleatorio_int_1n(N,X),
	escolhePalavraN(L,X,Palavra).
	
	
%%% escolhePalavraN/3 - escolhe o Nesimo elemento( N é o 2º argumento) 
%%% da lista do 1º argumento e coloca-a no 3º argumento
	
escolhePalavraN([H|T],1,H).
	
escolhePalavraN([H|T],N,P):-
		N1 is N - 1,
		escolhePalavraN(T,N1,P1),
		P = P1.


%%% criaVazios/2 - 

criaVazios(LCodigoPalavra,LCodigoVazio):-
	maplist(palavraVazio,LCodigoPalavra,LCodigoVazio).
	
palavraVazio(P,V):-
	P == 0'_ -> V = P ; V = 0'*.
	
%%% jogar/3

jogar(LPalavra, LVazia,	Contador):-
	name(PalavraVazia,LVazia),
	write(PalavraVazia),
	nl,
	write('Qual o seu Palpite (escreve uma letra seguida de .)?'),
	nl,
	read(Palpite),!,
	name(Palpite,[CPalpite]),
	verificaPalpite(LPalavra,LVazia,CPalpite,Contador).


%%% verificaPalpite/4 - 

verificaPalpite(LPalavra,LVazia,CPalpite,Contador):-	
	member(CPalpite,LPalavra), !,
	write('Correcto!'),nl,
	substituir(LPalavra,LVazia,CPalpite,NovoLVazia),
	vitoria(LPalavra,NovoLVazia,Contador).
	
verificaPalpite(LPalavra,LVazia,_,Contador):-
	( Contador == 5
	-> format('Fim do Jogo. Nao conseguiu adivinhar a palavra (~s)~n',[LPalavra])
	; write('Palpite Errado'), nl,
	Contador1 is Contador + 1,
	write('Ja errou '), write(Contador1), write(' de 5'),nl,
	jogar(LPalavra,LVazia,Contador1) ).
	
	
%%% vitoria/3 - verifica se o jogador já adivinhou a Palavra

vitoria(LPalavra, LVazia, Contador):-
	name(Palavra,LPalavra),
	name(PalavraVazia,LVazia),
	PalavraVazia = Palavra, !,
	write('Parabéns! Ganhou o jogo.').
	
vitoria(LPalavra, LVazia, Contador):-
	jogar(LPalavra, LVazia, Contador).
	
	
%%% substituir/4 - 

substituir(LPalavra,LVazia,CPalpite,NovoLVazia):-
	maplist(colocaPalpite(CPalpite),LPalavra,LVazia,NovoLVazia).

colocaPalpite(Palpite, Palavra, Vazia, Mostrar):-
	Palpite == Palavra -> Mostrar = Palavra;
	Mostrar = Vazia.
	
/******************************************
			NUMEROS ALEATORIOS
******************************************/	
	
aleatorio_int_1n(N, V):-
	V is random(N) + 1, !.
	