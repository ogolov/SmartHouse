using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    Common m_Common = new Common();
    SqlConnection conn = new SqlConnection("Data Source=BLAZE-PC\\SQLEXPRESS;Initial Catalog=smarthousedb;Integrated Security=True");

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {

  
    }
    protected void BtnLog_Click(object sender, EventArgs e)
    {
        // get all the data and valid it using sotred procdure
        SqlDataReader rdr = null;
        SqlCommand cmd = new SqlCommand("loginProc", conn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@houseid", tbHouse.Text));
        cmd.Parameters.Add(new SqlParameter("@user", tbUser.Text));
        cmd.Parameters.Add(new SqlParameter("@password", tbPass.Text));
        rdr = cmd.ExecuteReader();

        if (rdr.Read())
        {
            // add the house id and user to the session param
            Session.Add(m_Common.getSessionHouse(), tbHouse.Text);
            Session.Add(m_Common.getSessionUser(), tbUser.Text);

            Server.Transfer("Main.aspx");
        }
        else
        {
            // display error msg
            lblErr.Visible = true;
        }
        
    }
}