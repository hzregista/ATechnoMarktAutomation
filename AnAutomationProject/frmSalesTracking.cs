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
    public partial class frmSalesTracking : Form
    {
        public frmSalesTracking()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        private void frmSalesTracking_Load(object sender, EventArgs e)
        {
            getData();
        }
        void getData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec CorporateTracking", con.conn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Exec RetailTracking", con.conn());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }
    }
}
