/****************
Criacao do datasource:
Control Panel > Administrative Tools > Data Sources (ODBC) > Adicionar DNS "TheLegend"
****************/


:- use_module(library(odbc)).


/****************
uniao/3:
faz a uniao entre as listas do primeiro e segundo argumento e guarda-a na lista do terceiro argumento
****************/

uniao([],L,L).
uniao([X|L1],L2,LU):-member(X,L2),!,uniao(L1,L2,LU).
uniao([X|L1],L2,[X|LU]):-uniao(L1,L2,LU).


/****************
set/2
transforma a lista do 1� argumento num set e guarda-o na lista do 2� argumento
****************/

set([],[]).
set([H|T],[H|Out]) :-
    not(member(H,T)),
    set(T,Out).
set([H|T],Out) :-
    member(H,T),
    set(T,Out).

/****************
apaga/3
apaga o elemento do 1� argumento da lista do 2� argumento e guarda a nova lista no 3� argumento
****************/

apaga(_,[],[]).
apaga(X,[X|L],M):-!,apaga(X,L,M).
apaga(X,[Y|L],[Y|M]):-apaga(X,L,M).


/****************
conta_lista/2
conta o numero de elementos da lista do 1� argumento e guarda-o no 2� argumento
****************/

conta_lista([],0).
conta_lista([_|T],Total):-conta_lista(T,Soma),
				Total is 1+Soma.


/******************
Cria a ligacao a bd com o alias 'thelegend'
*******************/
init_bd:-  odbc_connect('TheLegend', _,
                     [ user(sa),
                       password('Qwerty2013'),
                       alias(thelegend),
                       open(once)
                     ]).


/******************
carrega_users/1:
carrega todos os utilizadores para a Lista do 1� argumento;
Users = [(1, jose, 'M'), (3, Maria, 'F'), (UserId, UserName, Sex)]
*******************/

carrega_users(Users) :- 
        findall((UserID,User,Sex),
                odbc_query(thelegend,
                           'select UserId, UserName, Sex from dbo.UserProfile',
                           row(UserID,User,Sex)),
                Users).


/******************
lidusers/1:
cria lista com os ids de todos os utilizadores e guarda-a ena lista do 1� argumento
******************/

lidusers(Lusers):-
	carrega_users(L),
	findall(User,member((User,_,_),L),Lusers).


/******************
Carrega_conn/1:
carrega todas as ligacoes entre os utilizadores para a Lista do 1� argumento
Conn = [(1, 5, 6, 3), (2, 4, 5, 2), (RelationShipId, UserId1, UserId2, TagRelationId)]
*******************/

carrega_conn(Conn) :- 
        findall((Rel,User1,User2,TagR), 
		odbc_query(thelegend,
                'select RelationShipId,UserId1,UserId2,TagRelationId from dbo.RelationShips', 
		row(Rel,User1,User2,TagR)),
		Conn).


/******************
listar_amigos/2:
cria uma lista com todos os amigos do 1� argumento e guarda-a na lista do 2� argumento
******************/

listar_amigos(X,Lamigos):-carrega_conn(L),
		findall(User,(member((_,X,User,_),L);member((_,User,X,_),L)),Lamigos).

/******************
listar_amigos2/2:
cria uma lista com todos os amigos at� 2� grau do 1� argumento e guarda-a na lista do 3� argumento
******************/

listar_amigos2(Z,Lamigos):-
		listar_amigos(Z,L1),
		carrega_conn(L),
		findall(Amigo,(member(X,L1),(member((_,X,Amigo,_),L);member((_,Amigo,X,_),L))),L2),
		uniao(L1,L2,L3),
		set(L3,L4),
		apaga(Z,L4,Lamigos).

/******************
listar_amigos3/2:
cria uma lista com todos os amigos at� 3� grau do 1� argumento e guarda-a na lista do 3� argumento
******************/

listar_amigos3(Z,Lamigos):-
		listar_amigos2(Z,L1),
		carrega_conn(L),
		findall(Amigo,(member(X,L1),(member((_,X,Amigo,_),L);member((_,Amigo,X,_),L))),L2),
		uniao(L1,L2,L3),
		set(L3,L4),
		apaga(Z,L4,Lamigos).
		

/******************
conta_amigos3/2:
onta todos os amigos at� 3� grau do 1� argumento e guarda-o no 2� argumento
******************/

conta_amigos3(Z,Namigos):-
		listar_amigos3(Z,L),
		conta_lista(L,Namigos).
