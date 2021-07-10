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
    public partial class frmInvoiceDetail : Form
    {
        public frmInvoiceDetail()
        {
            InitializeComponent();
        }
        private void frmInvoiceDetail_Load(object sender, EventArgs e)
        {
            btnNewRecord.Enabled = true;
            getData();
            label1.Text = "Selected Invoice ID: " + billid.ToString();
            label6.Text = "No Choice Right Now!";
        }
        dbConn con = new dbConn();
        int id = 0;
        int billid;
        public void setbillid(int billid)
        {
            this.billid = billid;
        }
        void getData()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbBillDetail WHERE BILLID='"+billid.ToString()+"'",con.conn());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProduct.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtCount.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtUnitPrice.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            lblTotalPrice.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "")
            {
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                label6.Text = "Selected Invoice Detail ID: " + id.ToString();
            }
            else
            {
                id = 0;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("Please Select a Record");
            }
            else
            {
                double count = Convert.ToDouble(txtCount.Text);
                double unitprice = Convert.ToDouble(txtUnitPrice.Text);
                double totalprice = count * unitprice;
                SqlCommand cmd = new SqlCommand("UPDATE tbBillDetail SET PRODUCT=@v1,COUNT=@v2,UNITPRICE=@v3,TOTALPRICE=@v4 WHERE ID=@id", con.conn());
                cmd.Parameters.AddWithValue("@v1", txtProduct.Text);
                cmd.Parameters.AddWithValue("@v2", txtCount.Text);
                cmd.Parameters.AddWithValue("@v3", txtUnitPrice.Text);
                cmd.Parameters.AddWithValue("@v4", totalprice);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("Please Select a Record");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbBillDetail WHERE ID = @id", con.conn());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
                billid = 0;
                label6.Text = "No Choice Right Now!";
            }
        }

        private void btnNewRecord_Click(object sender, EventArgs e)
        {
                double count = Convert.ToDouble(txtCount.Text);
                double unitprice = Convert.ToDouble(txtUnitPrice.Text);
                double totalprice = unitprice * count;
                lblTotalPrice.Text = totalprice.ToString();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbBillDetail (BILLID,PRODUCT,COUNT,UNITPRICE,TOTALPRICE) VALUES (@id,@v1,@v2,@v3,@v4)", con.conn());
                cmd.Parameters.AddWithValue("@id", billid);
                cmd.Parameters.AddWithValue("@v1", txtProduct.Text);
                cmd.Parameters.AddWithValue("@v2", txtCount.Text);
                cmd.Parameters.AddWithValue("@v3", txtUnitPrice.Text);
                cmd.Parameters.AddWithValue("@v4", lblTotalPrice.Text);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
        }
    }
}
