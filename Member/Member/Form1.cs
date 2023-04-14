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
using System.Xml.Linq;

namespace Member
{
    public partial class Form1 : Form
    {

        private EmployeeDAO dao;
        public Form1()
        {
            InitializeComponent();
            dao = new EmployeeDAO(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
        }


        public void loadData()
        {
            dataGridView1.Refresh();
            SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
            con.Open();

            SQLiteCommand cmd = new SQLiteCommand("select * from User_Table", con);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        //https://www.youtube.com/watch?v=Vfr6dS8DjOY&ab_channel=PinoyFreeCoder
        // https://www.google.com/search?q=windows+form+how+to+check+in+database+if+user+already+exist+in+sqlitedatabase+C%23+windows+form&oq=windows+form+how+to+check+in+database+if+user+already+exist+in+sqlitedatabase+C%23+windows+form&aqs=chrome..69i57j69i60.1208j0j7&sourceid=chrome&ie=UTF-8#fpstate=ive&vld=cid:8e253690,vid:AAlWMGMl5Y0

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
            con.Open();

            SQLiteCommand cmd = new SQLiteCommand("select * from User_Table", con);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

            this.BackColor = Color.DarkGreen;
            // Set the CellBorderStyle property to None
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;

            // Set the Anchor property to Top, Left, Right, Bottom
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            // Get the font of the TextBox control
            Font textBoxFont = txtFirstname.Font;

            // Set the font of the DataGridView control
            dataGridView1.DefaultCellStyle.Font = textBoxFont;


            //https://www.google.com/search?tbm=vid&sxsrf=APwXEde6YOWKfQphKJ9CPpGDL5Snd_b_AA:1681258289548&q=how+to+change+color+of+column+header+of+datagridview+in+c%23&sa=X&ved=2ahUKEwjv9f6Yh6P-AhWzxTgGHX65A-UQ8ccDegQICxAH&biw=1352&bih=776&dpr=1#fpstate=ive&vld=cid:8d652060,vid:bJoy-JFMGYA
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;

            //https://www.google.com/search?q=how+to+change+the+column+header+in+datagridview+in+csharp%3F&oq=how+to+change+the+column+header+in+datagridview+in+csharp%3F&aqs=chrome..69i57j0i22i30l7j0i390i650.24632j0j7&sourceid=chrome&ie=UTF-8#fpstate=ive&vld=cid:f7245692,vid:5S0JDRMdjfU
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "FIRST NAME";

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                txtworkerid.Text = row.Cells[0].Value.ToString();
                txtFirstname.Text = row.Cells[1].Value.ToString();
                txtmiddlename.Text = row.Cells[2].Value.ToString();
                txtlastname.Text = row.Cells[3].Value.ToString();
                txtAge.Text = row.Cells[4].Value.ToString();
                txtsuffix.Text = row.Cells[5].Value.ToString();
                dtp.Text = row.Cells[6].Value.ToString();
                lstCivilStat.Text = row.Cells[7].Value.ToString();
                txtdesignation.Text = row.Cells[8].Value.ToString();


                txtworkerid.ForeColor = Color.White;
                txtworkerid.BackColor = Color.Black;

                txtFirstname.ForeColor = Color.White;
                txtFirstname.BackColor = Color.Black;

                txtmiddlename.ForeColor = Color.White;
                txtmiddlename.BackColor = Color.Black;

                txtlastname.ForeColor = Color.White;
                txtlastname.BackColor = Color.Black;

                txtAge.ForeColor = Color.White;
                txtAge.BackColor = Color.Black;

                txtsuffix.ForeColor = Color.White;
                txtsuffix.BackColor = Color.Black;

                lstCivilStat.ForeColor = Color.White;
                lstCivilStat.BackColor = Color.Black;

                txtdesignation.ForeColor = Color.White;
                txtdesignation.BackColor = Color.Black;

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



        public DataTable GetName(string name)
        {
            string query = string.Format("SELECT * FROM User_Table where FirstName like '%{0}%'", name);
            return DataAccess.ExecuteQuery(query);
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            dataGridView1.DataSource = form.GetName(txtSearch.Text.Trim());
            txtSearch.BackColor = Color.Black;
            txtSearch.ForeColor = Color.White;
        }


        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadData();
        }



        private void txtid_TextChanged(object sender, EventArgs e)
        {
            //this accepts only numbers and hyphen
            if (System.Text.RegularExpressions.Regex.IsMatch(txtworkerid.Text, "[^0-9-]"))
            {
                MessageBox.Show("Please Fill the Right ID Number");
                txtworkerid.Text = txtworkerid.Text.Remove(txtworkerid.Text.Length - 1);
                return;
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
            txtsuffix.Clear();
            lstCivilStat.SelectedIndex = 0;
            txtdesignation.Clear();
            dtp.Value = DateTime.Now;
            txtlastname.Clear();
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
                    using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM User_Table WHERE workers_id=@workers_id", con))
                    {
                        cmd.Parameters.AddWithValue("@workers id", int.Parse(txtworkerid.Text));
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    loadData();
                }

                // Clear the textboxes
                txtworkerid.Text = "";
                txtFirstname.Text = "";
                txtmiddlename.Clear();
                txtlastname.Clear();
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


        private void refreshToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            //this accepts only numbers and hyphen
            if (System.Text.RegularExpressions.Regex.IsMatch(txtworkerid.Text, "[^0-9-]"))
            {
                MessageBox.Show("Please Fill the Right Age");
                txtworkerid.Text = txtworkerid.Text.Remove(txtworkerid.Text.Length - 1);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            txtSearch.Clear();
        }

        private void txtdesignation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar) && !Char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }


        private void addEmployeeButton_Click(object sender, EventArgs e)
        {
         
            try
            {
               
                Employee newEmployee = new Employee();
                newEmployee.WorkersID = int.Parse(txtworkerid.Text);
                newEmployee.FirstName = txtFirstname.Text;
                newEmployee.MiddleName = txtmiddlename.Text;
                newEmployee.LastName = txtlastname.Text;
                newEmployee.Birthday = dtp.Value;
            
                dao.Create(newEmployee);
           
            }

            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }





        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                Employee employee = new Employee()
                {
                    FirstName = txtFirstname.Text,
                    MiddleName=txtmiddlename.Text,
                    LastName=txtlastname.Text,  
                    Birthday = dtp.Value
                };
                dao.Update(employee);
                MessageBox.Show("Employee Successfully Updated");
            }
            catch (InvalidOperationException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}


