using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLegend
{
    class Conexao
    {
        public OleDbConnection cn = new OleDbConnection();

        public void conetar()
        {
            cn.ConnectionString = "@Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Resources\banco.m­db";
            cn.Open();
        }

        public void desconetar()
        {
            cn.Close();
        }

    }
}
