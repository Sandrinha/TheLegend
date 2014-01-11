// test_c_pl.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"


int _tmain(int argc, _TCHAR* argv[])
{
	return 0;
}

#include "SWI-cpp.h" 
#include <iostream>  
using namespace std;
int main(){
	// char* argv[] = {"libswipl.dll", "-s", "D:\\teste.pl", NULL};
    char* argv[] = {"libswipl.dll", "-s", "teste.pl", NULL};
	PlEngine e(3,argv);

 PlTermv av(2);
 //av[0] =  PlCompound("jaco");
 av[1] =  PlCompound("jose");
 

 PlQuery q("ancestral", av);

 while (q.next_solution())  {
	 cout << (char*)av[0] << endl;
 }
 cin.get();
 return 1; 
} 

