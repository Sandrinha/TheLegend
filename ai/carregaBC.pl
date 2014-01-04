/****************
Criacao do datasource:
Control Panel > Administrative Tools > Data Sources (ODBC) > Adicionar DNS "TheLegend"
****************/


:- use_module(library(odbc)).


/****************
inter/3:
faz a interseccao entre as listas do primeiro e segundo argumento e guarda-a na lista do terceiro argumento
****************/

inter([],_,[]).
inter([X|L1],L2,[X|LI]):-member(X,L2),!,inter(L1,L2,LI).
inter([_|L1],L2,LI):- inter(L1,L2,LI).


/****************
uniao/3:
faz a uniao entre as listas do primeiro e segundo argumento e guarda-a na lista do terceiro argumento
****************/

uniao([],L,L).
uniao([X|L1],L2,LU):-member(X,L2),!,uniao(L1,L2,LU).
uniao([X|L1],L2,[X|LU]):-uniao(L1,L2,LU).


/****************
set/2
transforma a lista do 1º argumento num set e guarda-o na lista do 2º argumento
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
apaga o elemento do 1º argumento da lista do 2º argumento e guarda a nova lista no 3º argumento
****************/

apaga(_,[],[]).
apaga(X,[X|L],M):-!,apaga(X,L,M).
apaga(X,[Y|L],[Y|M]):-apaga(X,L,M).

/****************
apagalista/3
apaga os elementos da lista do 2º argumento da lista do 1º argumento originando a lista do 3º argumento
****************/

apagalista([],_,[]).
apagalista([H|T], L, LR):- member(H, L), apagalista(T, L, LR), !.
apagalista([H|T], L, [H|TLR]):- apagalista(T, L, TLR).

/****************
conta_lista/2
conta o numero de elementos da lista do 1º argumento e guarda-o no 2º argumento
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
carrega todos os utilizadores para a Lista do 1º argumento;
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
cria lista com os ids de todos os utilizadores e guarda-a ena lista do 1º argumento
******************/

lidusers(Lusers):-
	carrega_users(L),
	findall(User,member((User,_,_),L),Lusers).


/******************
Carrega_conn/1:
carrega todas as ligacoes entre os utilizadores para a Lista do 1º argumento
Conn = [(1, 5, 6, 3), (2, 4, 5, 2), (RelationShipId, UserId1, UserId2, TagRelationId)]
*******************/

carrega_conn(Conn) :- 
        findall((Rel,User1,User2,TagR), 
		odbc_query(thelegend,
                'select RelationShipId,UserId1,UserId2,TagRelationId from dbo.RelationShips', 
		row(Rel,User1,User2,TagR)),
		Conn).


/******************
Carrega_tagsUser/1:
carrega todas as tags de todos os utilizadores para a Lista do 1º argumento
agsUsers = [(1, 3), (2, 3), (2, 5), (Tag_TagID, UserProfile_UserId)]

******************/

carrega_tagsUsers(TagsUsers) :- 
        findall((TagId,UserId), 
		odbc_query(thelegend,
                'select * from dbo.TagUserProfiles', 
		row(TagId,UserId)),
		TagsUsers).


/******************
listar_amigos/2:
cria uma lista com todos os amigos do 1º argumento e guarda-a na lista do 2º argumento
******************/

listar_amigos(X,Lamigos):-carrega_conn(L),
		findall(User,(member((_,X,User,_),L);member((_,User,X,_),L)),Lamigos).

/******************
listar_amigos2/2:
cria uma lista com todos os amigos até 2º grau do 1º argumento e guarda-a na lista do 3º argumento
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
cria uma lista com todos os amigos até 3º grau do 1º argumento e guarda-a na lista do 3º argumento
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
onta todos os amigos até 3º grau do 1º argumento e guarda-o no 2º argumento
******************/

conta_amigos3(Z,Namigos):-
		listar_amigos3(Z,L),
		conta_lista(L,Namigos).

/******************
listaTagsUser/2
guarda todas os ids das tags do utilizador do 1º argumento e guarda-as na lista do 2º argumento
******************/

listaTagsUser(User,LTags):-
			carrega_tagsUsers(LTU),
			findall(T,member((T,User),LTU),LTags).


/******************
amigosTagComum/3
guarda os amigos do 1º argumento com N tags em comum na lista do 3º argumento sendo N o segundo argumento
******************/

amigosTagComum(User,N,LF):-
			listaTagsUser(User,LTagsUser),
			listar_amigos(User,Lamigos),
			verifica_comum(User,N,LTagsUser,Lamigos,LF).
			

/******************
verifica_comum/5
verifica todas as tags dos elementos da lista do 4º argumento e guarda na lista do 5º argumento
os amigos que tem N tags em comum da lista do 3º argumento sendo N o elemento do 2º argumento
******************/

verifica_comum(_,_,_,[],[]).
verifica_comum(_,N,LTagsUser,[H|T],[H|TF]):-
			listaTagsUser(H,LTags),
			inter(LTagsUser,LTags,Res),
			conta_lista(Res,N1),
			N1>=N,!,
			verifica_comum(_,N,LTagsUser,T,TF).
verifica_comum(_,N,LTagsUser,[_|T],LF):-
			verifica_comum(_,N,LTagsUser,T,LF).


/*******************
sugere_amigos/2
sugere ao elemento do 1º argumento uma lista de potenciais amigos tendo por base as tags e conexoes 
partilhadas (até 3º nivel) guardando essa lista no 2º argumento
*******************/

sugere_amigos(User,LSug):-
			listar_amigos(User,Lamigos),
			listar_amigos3(User,Lamigos3),
			apagalista(Lamigos3,Lamigos,LPAmigos),
			listaTagsUser(User,LTags),
			sugere_amigos(User,LTags,LPAmigos,LSug).


/*******************
sugere_amigos/4
guarda todos os elementos da lista do 3º argumento que partilhem pelo menos 1 tag com a lista de tags 
do 2º argumento originando uma lista de potenciais amigos guardada no 4º argumento
*******************/

sugere_amigos(_,_,[],[]).

sugere_amigos(_,LTagsUser,[H|T],[H|LF]):-
			listaTagsUser(H,LTags),
			inter(LTagsUser,LTags,Res),
			conta_lista(Res,N),
			N>=1,!,
			sugere_amigos(_,LTagsUser,T,LF).

sugere_amigos(_,LTagsUser,[_|T],LF):-
			sugere_amigos(_,LTagsUser,T,LF).

