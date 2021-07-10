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
    public partial class frmStocks : Form
    {
        public frmStocks()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        String productName;
        void getData()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select TYPE, Sum(COUNT) As 'AMOUNT' From tbProducts group by TYPE", con.conn());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void frmStocks_Load(object sender, EventArgs e)
        {
            getData();
            SqlCommand cmd = new SqlCommand("Select TYPE, Sum(COUNT) As 'AMOUNT' From tbProducts group by TYPE",con.conn());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]),int.Parse(dr[1].ToString()));
            }
            con.conn().Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmProductDetails fpd = new frmProductDetails();
            fpd.Hide();
            productName = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (productName != null)
            {
                fpd.setProductName(productName);
                fpd.Show();
            }
        }
    }
}
