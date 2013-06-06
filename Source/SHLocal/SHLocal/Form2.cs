using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHLocal
{
    public partial class Form2 : Form
    {
        Form1 m_form;
        public Form2(Form1 f)
        {
            InitializeComponent();
            m_form = f;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection("Data Source=BLAZE-PC\\SQLEXPRESS;Initial Catalog=smarthousedb;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[loginProc]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@houseid",int.Parse(tbHouse.Text)));
            cmd.Parameters.Add(new SqlParameter("@user",tbUser.Text));
            cmd.Parameters.Add(new SqlParameter("@password",tbPass.Text));
            
            rdr = cmd.ExecuteReader();

            if (!rdr.Read())
            {
                lblErr.Visible = true;
            }
            else
            {
                //return the id back to the father form
                m_form.setHouseID(int.Parse(tbHouse.Text));
                m_form.setUser(tbUser.Text);
                m_form.setConnected(true);
                conn.Close();
                m_form.TryToConnect();
                this.Close();
            }

            conn.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
