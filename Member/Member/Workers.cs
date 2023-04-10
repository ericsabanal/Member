using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SQLite;

namespace Member
{
    public partial class Workers : Form
    {
        private object dataGridView1;

        public Workers()
        {
            InitializeComponent();
        }

        private void Workers_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dgvWorkers.SelectedRows) 
            {
                Form1 form1 = new Form1();
                form1.txtworkerid.Text = row.Cells[0].Value.ToString();
            }
        }

        private void workersToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SQLiteConnection con = new SQLiteConnection(@"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db");
            con.Open();

            SQLiteCommand cmd = new SQLiteCommand("select * from User_Table", con);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvWorkers.DataSource = dt;

        }

        private void form1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.ShowDialog();
        }
    }
}
