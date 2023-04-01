using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Member
{
    public partial class Form1 : Form
    {

        DialogResult iExit;
        public Form1()
        {
            InitializeComponent();
        }


        //https://www.youtube.com/watch?v=Vfr6dS8DjOY&ab_channel=PinoyFreeCoder
        private void button1_Click(object sender, EventArgs e)
        {
           
            SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
            con.Open(); 

            SQLiteCommand cmd = new SQLiteCommand("insert into User_Table(ID,Name,Age) VALUES (@ID,@Name,@Age)",con);
            cmd.Parameters.AddWithValue("@ID",int.Parse(txtid.Text));
            cmd.Parameters.AddWithValue("@Name",txtName.Text);
            cmd.Parameters.AddWithValue("@Age",txtAge.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            txtid.Text = "";
            txtName.Text = "";
            txtAge.Text = "";
            MessageBox.Show(" Successfully Inserted");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
            con.Open();

            SQLiteCommand cmd = new SQLiteCommand("UPDATE User_Table SET Name=@Name, Age=@Age WHERE ID=@ID", con);
            cmd.Parameters.AddWithValue("@ID", int.Parse(txtid.Text));
            cmd.Parameters.AddWithValue("@Name", txtName.Text);
            cmd.Parameters.AddWithValue("@Age", txtAge.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            txtid.Text = "";
            txtName.Text = "";
            txtAge.Text = "";
            MessageBox.Show(" Successfully Updated");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
            con.Open();

            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM User_Table WHERE ID=@ID", con);
            cmd.Parameters.AddWithValue("@ID", int.Parse(txtid.Text));
            cmd.ExecuteNonQuery();
            con.Close();

            txtid.Text = "";
            txtName.Text = "";
            txtAge.Text = "";
            MessageBox.Show(" Successfully Deleted");
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
            con.Open();

            SQLiteCommand cmd = new SQLiteCommand("select * from User_Table", con);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                txtid.Text =   row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString();
                txtAge.Text =  row.Cells[2].Value.ToString();

            }

        }

        private void btnRead_Click(object sender, EventArgs e)
        {

            SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
            con.Open();

            SQLiteCommand cmd = new SQLiteCommand("select * from User_Table", con);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        //https://www.youtube.com/watch?v=6YMRiQ87EVo&ab_channel=DJOamen
        private void btnExit_Click(object sender, EventArgs e)
        {
            iExit = MessageBox.Show("Confirm if you want to exit","DatagridView",MessageBoxButtons.YesNo,MessageBoxIcon.Information);

            if (iExit == DialogResult.Yes) 
            {
            Application.Exit();
            }
        }

        public DataTable GetName(string name) 
        {
            string query = string.Format("select * from User_Table where Name ='{0}'", name);
            return DataAccess.ExecuteQuery(query);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetName(txtName.Text.Trim());
          
        }
    }
}
