using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main : System.Web.UI.Page
{
    Common m_Common = new Common();
    SqlConnection conn = new SqlConnection("Data Source=BLAZE-PC\\SQLEXPRESS;Initial Catalog=smarthousedb;Integrated Security=True");

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataReader rdr = null;
        SqlCommand cmd = new SqlCommand("houseDetailesProc", conn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@houseid", int.Parse(Session[m_Common.getSessionUser()].ToString())));
        rdr = cmd.ExecuteReader();

        if (rdr.Read())
        {
            Session.Add(m_Common.getSessionHouseIP(), rdr.GetString(2));
        }
        else 
        {
            Server.Transfer("Defualt.aspx");
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool devstat = false;

        // Run the stored procdure for getting the specific device data and present it on the items
        
        SqlDataReader rdr = null;
        SqlCommand cmd = new SqlCommand("deviceDetailesProc", conn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@deviceid", int.Parse(DropDownList1.SelectedItem.Value)));
        rdr = cmd.ExecuteReader();

        if (rdr.Read())
        {
            devstat = rdr.GetBoolean(2);
        }


        //devstat = returned val

        if (devstat)
        {
            RblStatus.SelectedIndex = 0;
        }
        else 
        {
            RblStatus.SelectedIndex = 1;
        }
    }
    protected void RblStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool devstat = false;

        if (RblStatus.SelectedIndex == 1)
            devstat = true;
        // Run the stored procdure for getting the specific device data and present it on the items
        SqlDataReader rdr = null;
        SqlCommand cmd1 = new SqlCommand("deviceDetailesProc", conn);
        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
        cmd1.Parameters.Add(new SqlParameter("@deviceid", int.Parse(DropDownList1.SelectedItem.Value)));
        rdr = cmd1.ExecuteReader();

        if (rdr.Read())
        {
            SqlCommand cmd2 = new SqlCommand("updateDeviceProc", conn);
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            cmd2.Parameters.Add(new SqlParameter("@deviceid",rdr.GetInt32(0)));
            cmd2.Parameters.Add(new SqlParameter("@devname",rdr.GetString(1)));
            cmd2.Parameters.Add(new SqlParameter("@status",devstat));
            cmd2.Parameters.Add(new SqlParameter("@param1",rdr.GetString(3)));
            cmd2.Parameters.Add(new SqlParameter("@param2", rdr.GetString(4)));
            cmd2.Parameters.Add(new SqlParameter("@param3", rdr.GetString(5)));
            cmd2.Parameters.Add(new SqlParameter("@param4", rdr.GetString(6)));
            cmd2.Parameters.Add(new SqlParameter("@param5", rdr.GetString(7)));
            cmd2.ExecuteNonQuery();
        }

        deviceUpdated(rdr.GetInt32(0), rdr.GetString(1), devstat, rdr.GetString(3), rdr.GetString(4), rdr.GetString(5), rdr.GetString(6), rdr.GetString(7));
    }

    private void deviceUpdated(int devid, string name, bool devstat, string param1, string param2, string param3,
        string param4, string param5)
    {
        try
        {
            // Create a TcpClient. 
            // Note, for this client to work you need to have a TcpServer  
            // connected to the same address as specified by the server, port 
            // combination.
            Int32 port = m_Common.getHousePort();
            String server = Session[m_Common.getSessionHouseIP()].ToString();
            TcpClient client = new TcpClient(server, port);

            string strBreak = m_Common.getBreak();

            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(devid.ToString() + strBreak + name + strBreak + devstat.ToString() +
                strBreak + param1 + strBreak + param2 + strBreak + param3 + strBreak + param4 + strBreak + param5);

            // Get a client stream for reading and writing. 
            //  Stream stream = client.GetStream();

            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);

            // Receive the TcpServer.response. 

            // Buffer to store the response bytes.
            data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            responseData.CompareTo("OK");

            // Close everything.
            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
    }
}