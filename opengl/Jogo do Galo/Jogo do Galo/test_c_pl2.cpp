// test_c_pl2.cpp : Exemplo de como ligar o c++ ao prolog
// este exemplo chama o predicado listar_amigos3/2 com o 1º argumento = 4 (user id), retorna no 2º argumento a lista dos ids
// dos seus amigos até 3º nivel. o exemplo percorre a lista retornada e mostra um a um os ids dos amigos.

#include "stdafx.h"


int _tmain(int argc, _TCHAR* argv[])
{
	return 0;
}

#include "SWI-cpp.h" 
#include <iostream>  
using namespace std;
int main(){
    char* argv[] = {"libswipl.dll", "-s", "carregaBC.pl", NULL};
	PlEngine e(3,argv);	

 PlTermv av(2);
 av[0] =  PlCompound("4");
 

 PlQuery q("listar_amigos3",av);

 
 //while (q.next_solution())  neste caso só queremos a 1ª solucao
 q.next_solution();
	 PlTail l(av[1]);
	 PlTerm e1;
	 while (l.next (e1))
	 {
		 cout << (char*)e1 << endl;
	 }

 
 cin.get();
 return 1; 
} 

