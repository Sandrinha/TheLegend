/****************
Criação do datasource:
Control Panel > Administrative Tools > Data Sources (ODBC) > Adicionar DNS "TheLegend"
****************/

:- use_module(library(odbc)).


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
carrega todos os utilizadores para a Lista 'Users' 
tipo: Users = [row(1, jose, 'M'), row(3, Maria, 'F'), row (UserId, UserName, Sex)]
*******************/

carrega_users(Users) :- 
        findall(X, 
		odbc_query(thelegend,
                'select UserId, UserName, Sex from dbo.UserProfile', 
		X),
		Users).

/******************
carrega todas as ligacoes entre os utilizadores para a Lista 'Conn' 
tipo: Con = [row(1, 5, 6, 3), row(2, 4, 5, 2), row (RelationShipId, UserId1, UserId2, TagRelationId)]

*******************/

carrega_conn(Conn) :- 
        findall(X, 
		odbc_query(thelegend,
                'select * from dbo.RelationShips', 
		X),
		Conn).


