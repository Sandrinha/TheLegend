using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLegend
{
   static class Login
    {
       static string user;
       static string senha;
       static int nivel;
       public static void login(string user1, string senha1, int nivel1)
       {
           user = user1;
           senha = senha1;
           nivel = nivel1;
       }

       public static void logout()
       {
           user = null;
           senha = null;
           nivel = 0;
       }

       public static String getUser()
       {
           return "User: " + user + "\n Senha: " + senha + "\n Nivel: " + nivel; 
       }


    }
}
