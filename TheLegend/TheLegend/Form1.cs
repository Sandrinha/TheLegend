using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheLegend
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Users WHERE user=@user AND senha=@senha";
            Conexao conexao = new Conexao();
            conexao.conetar();

            OleDbCommand cmd = new OleDbCommand(query, conexao.cn);

            OleDbDataReader dr = cmd.ExecuteReader();

            //metodo para procurar na Bd o registo em causa e retorna o mesmo
            if(dr.Read())
            {
                Login.login(dr["user"].ToString(), dr["senha"].ToString(), Convert.ToInt32(dr["nivel"]));
                button1.Enabled = true;
            }
        }
    }
}
