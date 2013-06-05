using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    Common m_Common = new Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblUserName.Text = Session.Contents[m_Common.getSessionUser()].ToString();
    }
}
