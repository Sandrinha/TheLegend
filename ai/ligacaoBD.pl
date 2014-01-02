/****************
Criação do datasource:
Control Panel > Administrative Tools > Data Sources (ODBC) > Adicionar DNS "TheLegend"
****************/

:- use_module(library(odbc)).


init_bd:-  odbc_connect('TheLegend', _,
                     [ user(sa),
                       password('Qwerty2013'),
                       alias(thelegend),
                       open(once)
                     ]).

teste :-
        odbc_query(thelegend,
                   'SELECT * FROM User').