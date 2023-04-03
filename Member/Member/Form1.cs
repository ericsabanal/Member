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

            if (string.IsNullOrEmpty(txtid.Text) || string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Value is Required", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
                con.Open();

                SQLiteCommand cmd = new SQLiteCommand("insert into User_Table(ID,Name,Age) VALUES (@ID,@Name,@Age)", con);
                cmd.Parameters.AddWithValue("@ID", int.Parse(txtid.Text));
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                txtid.Text = "";
                txtName.Text = "";
                txtAge.Text = "";
                MessageBox.Show(" Successfully Inserted");
            }
           
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
            //    dataGridView1.Rows.Clear();
            //    dataGridView1.Refresh();

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
            string query = string.Format("SELECT * FROM User_Table where Name like '%{0}%'",name);
            return DataAccess.ExecuteQuery(query);
        }
       

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Form1 form = new Form1();
           dataGridView1.DataSource= form.GetName(txtSearch.Text.Trim());
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iExit = MessageBox.Show("Confirm if you want to exit", "DatagridView", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (iExit == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            iExit = MessageBox.Show("Confirm if you want to Exit","DataGridView",MessageBoxButtons.YesNo,MessageBoxIcon.Information);

            if (iExit == DialogResult.Yes) 
            {
                Application.Exit();
            }
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("select * from User_Table", con);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void refreshToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("select * from User_Table", con);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {
            //this accepts only numbers and hyphen
            if (System.Text.RegularExpressions.Regex.IsMatch(txtid.Text,"[^0-9-]")) 
            {
                MessageBox.Show("Please Enter only a Numbers");
                txtid.Text = txtid.Text.Remove(txtid.Text.Length -1);
            }
            
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtAge_TextChanged_1(object sender, EventArgs e)
        {
            //this accepts only numbers and hyphen
            if (System.Text.RegularExpressions.Regex.IsMatch(txtAge.Text, "[^0-9]"))
            {
                MessageBox.Show("Please Enter only a Numbers");
                txtAge.Text = txtid.Text.Remove(txtAge.Text.Length - 1);
            }
        }

        //https://www.youtube.com/watch?v=ocD7wuF8PTg&ab_channel=InterviewPoint
        //https://stackoverflow.com/questions/16050749/how-restrict-textbox-in-c-sharp-to-only-receive-numbers-and-dot-or-comma
        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar) && !Char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;   
            }
        }
    }
}
