using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TheTechnoMarketAutomation
{
    public partial class frmYellowPages : Form
    {
        public frmYellowPages()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        String email;
        void getData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbRetailCustomers", con.conn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            DataTable dtt = new DataTable();
            SqlDataAdapter daa = new SqlDataAdapter("SELECT * FROM tbCorporateCustomers", con.conn());
            daa.Fill(dtt);
            dataGridView2.DataSource = dtt;

        }
        private void frmYellowPages_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmMail frmail = new frmMail();
            email = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            if (email != null)
            {
                frmail.getmail(email);
            }
            frmail.Show();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmMail frmail = new frmMail();
            email = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            if (email != null)
            {
                frmail.getmail(email); 
            }

            frmail.Show();
        }
    }
}
