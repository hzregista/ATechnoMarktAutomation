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
    public partial class frmProductDetails : Form
    {
        public frmProductDetails()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        string ProductName;
        public void setProductName(string productName)
        {
            this.ProductName = productName;
        }
        private void frmProductDetails_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select*From tbProducts Where TYPE='"+ProductName+"'",con.conn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
