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
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

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
        // https://www.google.com/search?q=windows+form+how+to+check+in+database+if+user+already+exist+in+sqlitedatabase+C%23+windows+form&oq=windows+form+how+to+check+in+database+if+user+already+exist+in+sqlitedatabase+C%23+windows+form&aqs=chrome..69i57j69i60.1208j0j7&sourceid=chrome&ie=UTF-8#fpstate=ive&vld=cid:8e253690,vid:AAlWMGMl5Y0
       
        private void Form1_Load(object sender, EventArgs e)
        {
           SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
                con.Open();

                SQLiteCommand cmd = new SQLiteCommand("select * from User_Table", con);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                this.BackColor = Color.DarkGreen;
                // Set the CellBorderStyle property to None
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;

                // Set the Anchor property to Top, Left, Right, Bottom
                dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

                // Get the font of the TextBox control
                Font textBoxFont = txtFirstname.Font;

                // Set the font of the DataGridView control
                dataGridView1.DefaultCellStyle.Font = textBoxFont;

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                txtworkerid.Text = row.Cells[0].Value.ToString();
                txtFirstname.Text = row.Cells[1].Value.ToString();
                txtmiddlename.Text = row.Cells[2].Value.ToString();
                txtAge.Text = row.Cells[3].Value.ToString();
                txtsuffix.Text = row.Cells[4].Value.ToString();
                dateTimePicker1.Text = row.Cells[5].Value.ToString();

                txtworkerid.ForeColor = Color.White;
                txtworkerid.BackColor = Color.Black;

                txtFirstname.ForeColor = Color.White;
                txtFirstname.BackColor = Color.Black;

                txtmiddlename.ForeColor = Color.White;
                txtmiddlename.BackColor = Color.Black;

                txtAge.ForeColor = Color.White;
                txtAge.BackColor = Color.Black;

                txtsuffix.ForeColor = Color.White;
                txtsuffix.BackColor = Color.Black;

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
            string query = string.Format("SELECT * FROM User_Table where FirstName like '%{0}%'",name);
            return DataAccess.ExecuteQuery(query);
        }
       

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Form1 form = new Form1();
           dataGridView1.DataSource= form.GetName(txtSearch.Text.Trim());

            txtSearch.BackColor = Color.Black;
            txtSearch.ForeColor = Color.White;
        }

        //private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    iExit = MessageBox.Show("Confirm if you want to exit", "DatagridView", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

        //    if (iExit == DialogResult.Yes)
        //    {
        //        Application.Exit();
        //    }
        //}



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

       

        private void txtid_TextChanged(object sender, EventArgs e)
        {
            //this accepts only numbers and hyphen
            if (System.Text.RegularExpressions.Regex.IsMatch(txtworkerid.Text, "[^0-9-]"))
            {
                MessageBox.Show("Please Fill the Right ID Number");
                txtworkerid.Text = txtworkerid.Text.Remove(txtworkerid.Text.Length - 1);
            }

        }


        //https://www.youtube.com/watch?v=ocD7wuF8PTg&ab_channel=InterviewPoint
        //https://stackoverflow.com/questions/16050749/how-restrict-textbox-in-c-sharp-to-only-receive-numbers-and-dot-or-comma
        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar) && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;   
            }
        }

        private void txtAge_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //Delete na key ang gagamitin sa pagbura
            //this only accept numbers 
            e.Handled = !Char.IsNumber(e.KeyChar);

        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtworkerid.Text = "";
            txtFirstname.Text = "";
            txtmiddlename.Clear();
            txtAge.Text = "";
        }

       
        private void insertToolStripMenuItem_Click(object sender, EventArgs e)    
        {
             if (string.IsNullOrEmpty(txtworkerid.Text))
            {
                MessageBox.Show("WorkersID is Required", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtFirstname.Text))
            {
                MessageBox.Show("FirstName is Required", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtmiddlename.Text))
            {
                MessageBox.Show("MiddleName is Required", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtAge.Text))
            {
                MessageBox.Show("Please Fill Up Your Age", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else
            {
            SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT WorkersID from User_Table where WorkersID ='" + txtworkerid.Text + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("WorkersID Already Exist");
                }
                else
                {

                    con.Open();
                    string query = "INSERT INTO User_Table (WorkersID, FirstName, middlename, Age, suffix, birthday) VALUES (@WorkersID, @FirstName, @middlename, @Age, @suffix, @birthday)";

                    SQLiteCommand cmd = new SQLiteCommand(query, con);
                    cmd.Parameters.AddWithValue("@WorkersID", int.Parse(txtworkerid.Text));
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstname.Text);
                    cmd.Parameters.AddWithValue("@middlename", txtmiddlename.Text);
                    cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                    cmd.Parameters.AddWithValue("@suffix", txtsuffix.Text);
                    cmd.Parameters.AddWithValue("@birthday", dateTimePicker1.Value);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    txtworkerid.Text = "";
                    txtFirstname.Text = "";
                    txtmiddlename.Text = "";
                    txtAge.Text = "";
                    txtsuffix.Clear();
                    dateTimePicker1.Value = DateTime.Now.AddYears(-18);
                    MessageBox.Show("Successfully Inserted");
                }
            }
        }



        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtworkerid.Text) || string.IsNullOrEmpty(txtFirstname.Text) || string.IsNullOrEmpty(txtAge.Text))
            {
                MessageBox.Show("Value is Required", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtmiddlename.Text)) 
            {
                MessageBox.Show("MiddleName is Required", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
          
            else
            {
                SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
                con.Open();

                SQLiteCommand cmd = new SQLiteCommand("UPDATE User_Table SET FirstName=@FirstName,middlename=@middlename,Age=@Age,suffix=@suffix WHERE WorkersID=@WorkersID", con);
                cmd.Parameters.AddWithValue("@WorkersID", int.Parse(txtworkerid.Text));
                cmd.Parameters.AddWithValue("@FirstName", txtFirstname.Text);
                cmd.Parameters.AddWithValue("@middlename", txtmiddlename.Text);
                cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                cmd.Parameters.AddWithValue("@suffix", txtsuffix.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                txtworkerid.Text = "";
                txtFirstname.Text = "";
                txtmiddlename.Clear();
                txtAge.Text = "";
                txtsuffix.Clear();
                MessageBox.Show("Successfully Updated");


            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtworkerid.Text))
            {
                MessageBox.Show("Please enter a WorkersID to delete.");
                return;
            }

            // Display a confirmation dialog to the user
            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Delete the record from the database
                string query = @"Data Source=C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db";
                using (SQLiteConnection con = new SQLiteConnection(query))
                {
                    con.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM User_Table WHERE WorkersID=@WorkersID", con))
                    {
                        cmd.Parameters.AddWithValue("@WorkersID", int.Parse(txtworkerid.Text));
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }

                // Clear the textboxes
                txtworkerid.Text = "";
                txtFirstname.Text = "";
                txtmiddlename.Clear();
                txtAge.Text = "";
                txtsuffix.Clear();

                MessageBox.Show("Deleted successfully.");
            }
        }

        private void txtmiddlename_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar) && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtsuffix_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar) && !Char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDateTime = dateTimePicker1.Value;
            return;
        }

        private void workersInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workers workers = new Workers();
            workers.Show();
        }

    }
}
