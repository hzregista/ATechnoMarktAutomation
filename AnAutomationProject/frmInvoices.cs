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
    public partial class frmInvoices : Form
    {
        public frmInvoices()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        int billid = 0;
        public void getData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbBillingData", con.conn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void frmInvoices_Load(object sender, EventArgs e)
        {
            getData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtCustomer.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtBiller.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtReceiver.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtDate.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtTime.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "")
            {
                billid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            }
            else
            {
                billid = 0;
            }
        }

        private void frmInvoices_Click(object sender, EventArgs e)
        {
            billid = 0;
            txtNo.Text = "";
            txtCustomer.Text = "";
            txtBiller.Text = "";
            txtReceiver.Text = "";
            txtDate.Text = "";
            txtTime.Text = "";
        }

        private void btnInfAdd_Click(object sender, EventArgs e)
        {
        }

        private void btnInUpdate_Click(object sender, EventArgs e)
        {
            if (billid == 0)
            {
                MessageBox.Show("Please Select a Record");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbBillingData SET NO=@v1,CUSTOMER=@v2,BILLER=@v3,RECEIVER=@v4,DATE=@v5, TIME=@v6 WHERE ID=@id", con.conn());
                cmd.Parameters.AddWithValue("@v1", txtNo.Text);
                cmd.Parameters.AddWithValue("@v2", txtCustomer.Text);
                cmd.Parameters.AddWithValue("@v3", txtBiller.Text);
                cmd.Parameters.AddWithValue("@v4", txtReceiver.Text);
                cmd.Parameters.AddWithValue("@v5", txtDate.Text);
                cmd.Parameters.AddWithValue("@v6", txtTime.Text);
                cmd.Parameters.AddWithValue("@id", billid);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
            }
        }

        private void btnInDelete_Click(object sender, EventArgs e)
        {
            if (billid == 0)
            {
                MessageBox.Show("Please Select a Record");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbBillingData WHERE ID = @id", con.conn());
                cmd.Parameters.AddWithValue("@id", billid);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
                billid = 0;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        { 
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmInvoiceDetail fid = new frmInvoiceDetail();
            fid.Hide();
            if (billid != 0)
            {
                fid.setbillid(billid);
                fid.Show();
            }
        }
    }
}
