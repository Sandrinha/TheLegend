%Definicao das estruturas para testes


%user_profile(Id,Name,Tags,Birth,Sex,Humor,Email,Nickname).

user_profile(1,joao,[musica, porto],15-09-85,male,_,_,_).
user_profile(2,joana,[musica, lisboa, dança, Csharp],1-02-85,female,_,_,_).
user_profile(3,manuel,['C#', televisão, porto],10-05-84,male,_,_,_).
user_profile(4,luis,[música, televisão, coimbra],25-09-80,male,_,_,_).
user_profile(5,andreia,[teatro, música, porto],25-12-83,female,_,_,_).
user_profile(6,josé,['C#', televisão, leiria],05-01-87,male,_,_,_).
user_profile(7,antónio,[Csharp, tv, braga],9-06-90,male,_,_,_).
user_profile(8,liliana,[dança, música, tv, leiria],23-11-88,female,_,_,_).

%user_conn(Id,ConnList).
%ConnList=[(IdA,TagA,Strength1),(IdB,TagB,Strength2),...]

user_conn(1,[(2,amigo,3),(4,amigo,3),(6,irmao,1),(7,primo,2)]).
user_conn(2,[(1,amigo,3),(3,irmao,1),(5,amigo,3),(8,amigo,3)]).
user_conn(3,[(2,irmao,1),(4,primo,2),(6,amigo,3),(7,amigo,3)]).
user_conn(4,[(1,amigo,3),(3,primo,2),(6,amigo,1),(8,amigo,3)]).
user_conn(5,[(2,amigo,3),(6,amigo,3),(7,amigo,3),(8,amigo,3)]).
user_conn(6,[(1,irmao,1),(3,amigo,3),(4,amigo,1),(5,amigo,3),(7,amigo,3),(8,amigo,3)]).
user_conn(7,[(1,primo,2),(3,amigo,3),(5,amigo,3),(6,amigo,3),(8,amigo,3)]).
user_conn(8,[(2,amigo,3),(4,amigo,3),(5,amigo,3),(6,amigo,3),(7,amigo,3)]).

%semantic_eq([tag1,tag2,...,tagn]).
semantic_eq(['c#','csharp']).
semantic_eq(['tv','televisão']).

%%teste

uniao([],L,L).
uniao([X|L1],L2,LU):-member(X,L2),!,uniao(L1,L2,LU).
uniao([X|L1],L2,[X|LU]):-uniao(L1,L2,LU).

%intersecçao de dois conjuntos
inter([],_,[]).
inter([X|L1],L2,[X|LI]):-member(X,L2),!,inter(L1,L2,LI).
inter([_|L1],L2,LI):- inter(L1,L2,LI).


%apaga um elem da lista
apaga(_,[],[]).
apaga(X,[X|L],M):-!,apaga(X,L,M).
apaga(X,[Y|L],[Y|M]):-apaga(X,L,M).


%contar nº de elementos em lista

%lista vazia
soma_lista([],0).
soma_lista([_|T],Total):-soma_lista(T,Soma),
				Total is 1+Soma.

%Passa de lista para um set
set([],[]).
set([H|T],[H|Out]) :-
    not(member(H,T)),
    set(T,Out).
set([H|T],Out) :-
    member(H,T),
    set(T,Out).

%Exclusao
diferença(L1,L2,LR):-	uniao(L1,L2,LU),
				inter(L1,L2,LI),
				diferença2(LU,LI,LR).
diferença2([],_,[]).	%só vamos guardar os elementos que não estão na lista dos comuns
diferença2([H|T],LI,LR):-member(H,LI),!,diferença2(T,LI,LR).
diferença2([H|T],LI,[H|LR]):-diferença2(T,LI,LR).

%Menor
min([X],X).
min([Y|R],X):- min(R,X), X < Y, !.
min([Y|_],Y).




%rede de amigos até 3ª grau

rede(User,Lamigos):-user_conn(User,L),
			findall(Amigo,member((Amigo,_,_),L),Lamigos1),
			findall(Y, (member(X,Lamigos1),user_conn(X,L2),member((Y,_,_),L2)),Lamigos2),
			uniao(Lamigos1,Lamigos2,Lamigost),
			findall(Z,(member(XY,Lamigost),user_conn(XY,L3),member((Z,_,_),L3)),Lamigost2),
			uniao(Lamigost,Lamigost2,Lamigost3),
			apaga(User,Lamigost3,Lamigos3),
			set(Lamigos3, Lamigos).

			


