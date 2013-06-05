using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHLocal
{
    public partial class Form1 : Form
    {
        public bool isActive;
        private int m_houseID;
        private string m_user = null;
        private bool m_connected = false;
        private int m_selectedRow = -1;
        private SqlConnection conn = new SqlConnection("Data Source=BLAZE-PC\\SQLEXPRESS;Initial Catalog=smarthousedb;Integrated Security=True");

        public void setHouseID(int id)
        {
            m_houseID = id;
        }

        public void setUser(string user)
        {
            m_user = user;
        }
        
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Start Running
            StartProgressBar();
            tsslP.Text = "Initalize...";

            //initalize

            // Start the listener thread
            Thread oThread = new Thread(new ThreadStart(listener));
            oThread.Start();

            //Creates Connection to the local DB

            if (m_houseID != 0 && m_user != null && !m_connected)
            {
                this.isActive = false;
                // try to connect
                Form2 f = new Form2(this);
                f.Activate();
                f.Show();

                while (!this.isActive)
                { }

            }

            if (m_connected)
            {
                tsslTD.Text = "Login Time: " + DateTime.Now.ToString("HH:mm:ss tt");
                tsslP.Text = "Ready";
            }
            else
            {
                tsslTD.Text = "Failed to Login connection is lost: " + DateTime.Now.ToString("HH:mm:ss tt");
                tsslP.Text = "Error";
            }

            StopProgressBar();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            // Show about box
            AboutBox1 ab1 = new AboutBox1();
            ab1.Activate();
            ab1.Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            // Run new help via web page
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo pi = new System.Diagnostics.ProcessStartInfo();
            pi.FileName = "http://www.google.com";
            p.StartInfo = pi;
            p.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartProgressBar();

            if (m_selectedRow != -1) // checks if a device has been selected
            {

                // Get the device prop
                int nDevid = int.Parse(label5.Text);
                bool bStatus = radioButton1.Checked;
                string sDevName = textBox1.Text;

                // use the update func
                SqlDataReader rdr = null;
                SqlCommand cmd1 = new SqlCommand("deviceDetailesProc", conn);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@deviceid", m_selectedRow.ToString()));
                rdr = cmd1.ExecuteReader();

                if (rdr.Read())
                {
                    SqlCommand cmd2 = new SqlCommand("updateDeviceProc", conn);
                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd2.Parameters.Add(new SqlParameter("@deviceid", rdr.GetValue(0)));
                    cmd2.Parameters.Add(new SqlParameter("@devname", sDevName));
                    cmd2.Parameters.Add(new SqlParameter("@status", bStatus));
                    cmd2.Parameters.Add(new SqlParameter("@param1", rdr.GetValue(3)));
                    cmd2.Parameters.Add(new SqlParameter("@param2", rdr.GetValue(4)));
                    cmd2.Parameters.Add(new SqlParameter("@param3", rdr.GetValue(5)));
                    cmd2.Parameters.Add(new SqlParameter("@param4", rdr.GetValue(6)));
                    cmd2.Parameters.Add(new SqlParameter("@param5", rdr.GetValue(7)));
                    cmd2.ExecuteNonQuery();
                }
            }

            StopProgressBar();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.isActive = false;
            // Open connection 
            Form2 login = new Form2(this);
            login.Activate();
            login.Show();

            while (!this.isActive)
            { }
        }

        private void StartProgressBar()
        {
            // run progress bar 
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripProgressBar1.MarqueeAnimationSpeed = 30;
        }

        private void StopProgressBar()
        {
            // stop progress bar 
            toolStripProgressBar1.Style = ProgressBarStyle.Continuous;
            toolStripProgressBar1.MarqueeAnimationSpeed = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            m_selectedRow = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("deviceDetailesProc", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@deviceid", m_selectedRow.ToString()));
            rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                label5.Text = rdr.GetValue(0).ToString();
                textBox1.Text = rdr.GetValue(1).ToString();
                if (bool.Parse(rdr.GetValue(2).ToString()))
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                }
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            radioButton1.Checked = false;
        }

        private void listener()
        {
 
        }
    }
}
