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
    public partial class frmNewInvoice : Form
    {
        public frmNewInvoice()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        private void frmNewInvoice_Load(object sender, EventArgs e)
        {
            getemployee();
            getproduct();
            txtName.Enabled = false;
            txtSurname.Enabled = false;
            txtGsm.Enabled = false;
            txtEmail.Enabled = false;
            txtAddress.Enabled = false;
            txtFName.Enabled = false;
            txtFAuthorized.Enabled = false;
        }
        int stock;
        int productID = 0;
        int employeeID = 0;
        int customerID = 0;
        string customerName = "";
        string productName = "";
        int billID = 0;
        void getproduct()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID, TYPE, BRAND, MODEL, YEAR, COUNT, SALEPRICE From tbProducts", con.conn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void getemployee()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID, NAME +' '+SURNAME AS FULLNAME From tbEmployees", con.conn());
            da.Fill(dt);
            cmbSeller.ValueMember = "ID";
            cmbSeller.DisplayMember = "FULLNAME";
            cmbSeller.DataSource = dt;
        }
        void getcorporate()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbCorporateCustomers", con.conn());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        void getretail()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbRetailCustomers", con.conn());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        void createinvoicedetail()
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO tbBillDetail (BILLID,PRODUCT,COUNT,UNITPRICE,TOTALPRICE) VALUES (@v1,@v2,@v3,@v4,@v5)", con.conn());
            cmd.Parameters.AddWithValue("@v1", billID);
            cmd.Parameters.AddWithValue("@v2", productName);
            cmd.Parameters.AddWithValue("@v3", txtCount.Text);
            cmd.Parameters.AddWithValue("@v4", txtUnitPrice.Text);
            cmd.Parameters.AddWithValue("@v5", lblTotalPrice.Text);
            cmd.ExecuteNonQuery();
            con.conn().Close();
        }
        void createinvoice()
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO tbBillingData (NO,CUSTOMER,BILLER,RECEIVER,DATE,TIME) VALUES (@v1,@v2,@v3,@v4,@v5,@v6)", con.conn());
            cmd.Parameters.AddWithValue("@v1", txtNo.Text);
            cmd.Parameters.AddWithValue("@v2", customerName);
            cmd.Parameters.AddWithValue("@v3", txtBiller.Text);
            cmd.Parameters.AddWithValue("@v4", txtReceiver.Text);
            cmd.Parameters.AddWithValue("@v5", txtDate.Text);
            cmd.Parameters.AddWithValue("@v6", txtTime.Text);
            cmd.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("Select top 1 * From tbBillingData Order By ID desc",con.conn());
            SqlDataReader read = cmd2.ExecuteReader();
            while (read.Read())
            {
                billID =  Convert.ToInt32(read["ID"]);
            }
            con.conn().Close();
        }
        void stockdecrease()
        {
            SqlCommand cmd = new SqlCommand("Update tbProducts Set COUNT = COUNT-@v1 Where ID=@id",con.conn());
            cmd.Parameters.AddWithValue("@v1", txtCount.Text);
            cmd.Parameters.AddWithValue("@id", productID);
            cmd.ExecuteNonQuery();
            con.conn().Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (productID == 0 || employeeID == 0 || customerID == 0 || txtCount.Text == "" || txtUnitPrice.Text == "" || stock < Convert.ToInt32(txtCount.Text))
            {
                MessageBox.Show("Error! You've Entered Incorrectly or Incompletely");
            }
            else if (chckCorporate.Checked == true)
            {
                createinvoice();
                double count = Convert.ToDouble(txtCount.Text);
                double price = Convert.ToDouble(txtUnitPrice.Text);
                double total = count * price;
                lblTotalPrice.Text = total.ToString();
                createinvoicedetail();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbCorporateSalesTracking (PRODUCTID,AMOUNT,EMPLOYEEID,CUSTOMERID,PRICE,TOTAL,BILLID,DATE) Values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", con.conn());
                cmd.Parameters.AddWithValue("@p1", productID);
                cmd.Parameters.AddWithValue("@p2", txtCount.Text);
                cmd.Parameters.AddWithValue("@p3", employeeID);
                cmd.Parameters.AddWithValue("@p4", customerID);
                cmd.Parameters.AddWithValue("@p5", txtUnitPrice.Text);
                cmd.Parameters.AddWithValue("@p6", lblTotalPrice.Text);
                cmd.Parameters.AddWithValue("@p7", billID);
                cmd.Parameters.AddWithValue("@p8", txtDate.Text);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getcorporate();
            }
            else if (chckRetail.Checked == true)
            {
                createinvoice();
                double count = Convert.ToDouble(txtCount.Text);
                double price = Convert.ToDouble(txtUnitPrice.Text);
                double total = count * price;
                lblTotalPrice.Text = total.ToString();
                createinvoicedetail();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbRetailSalesTracking (PRODUCTID,AMOUNT,EMPLOYEEID,CUSTOMERID,PRICE,TOTAL,BILLID,DATE) Values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", con.conn());
                cmd.Parameters.AddWithValue("@p1", productID);
                cmd.Parameters.AddWithValue("@p2", txtCount.Text);
                cmd.Parameters.AddWithValue("@p3", employeeID);
                cmd.Parameters.AddWithValue("@p4", customerID);
                cmd.Parameters.AddWithValue("@p5", txtUnitPrice.Text);
                cmd.Parameters.AddWithValue("@p6", lblTotalPrice.Text);
                cmd.Parameters.AddWithValue("@p7", billID);
                cmd.Parameters.AddWithValue("@p8", txtDate.Text);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getretail();
            }      
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (chckCorporate.Checked == true)
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbCorporateCustomers (NAME,AUTHORIZED,GSM,EMAIL,ADDRESS) VALUES (@v1,@v2,@v3,@v4,@v5)", con.conn());
                cmd.Parameters.AddWithValue("@v1", txtFName.Text);
                cmd.Parameters.AddWithValue("@v2", txtFAuthorized.Text);
                cmd.Parameters.AddWithValue("@v3", txtGsm.Text);
                cmd.Parameters.AddWithValue("@v4", txtEmail.Text);
                cmd.Parameters.AddWithValue("@v5", txtAddress.Text);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getcorporate();
            }
            else if (chckRetail.Checked == true)
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbRetailCustomers (NAME,SURNAME,GSM,EMAIL,ADDRESS) VALUES (@v1,@v2,@v3,@v4,@v5)", con.conn());
                cmd.Parameters.AddWithValue("@v1", txtName.Text);
                cmd.Parameters.AddWithValue("@v2", txtSurname.Text);
                cmd.Parameters.AddWithValue("@v3", txtGsm.Text);
                cmd.Parameters.AddWithValue("@v4", txtEmail.Text);
                cmd.Parameters.AddWithValue("@v5", txtAddress.Text);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getretail();
            }
            else
            {
                MessageBox.Show("Please Select a Type of Customer");
            }
        }
        private void chckRetail_CheckedChanged(object sender, EventArgs e)
        {
            if (chckCorporate.Checked == true)
            {
                chckCorporate.Checked = false;
            }
            if (chckRetail.Checked == true)
            {
                txtName.Enabled = true;
                txtSurname.Enabled = true;
                txtGsm.Enabled = true;
                txtEmail.Enabled = true;
                txtAddress.Enabled = true;
                getretail();
            } 
            if (chckRetail.Checked == false)
            {
                txtName.Enabled = false;
                txtSurname.Enabled = false;
            }

        }
        private void chckCorporate_CheckedChanged(object sender, EventArgs e)
        {
            if (chckRetail.Checked == true)
            {
                chckRetail.Checked = false;
            }
            if (chckCorporate.Checked == true)
            {
                txtFAuthorized.Enabled = true;
                txtFName.Enabled = true;
                txtGsm.Enabled = true;
                txtEmail.Enabled = true;
                txtAddress.Enabled = true;
                getcorporate();
            }
            if (chckCorporate.Checked == false)
            {
                txtFName.Enabled = false;
                txtFAuthorized.Enabled = false;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           productID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
           txtUnitPrice.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
           lblProductID.Text = productID.ToString();
           SqlCommand cmd = new SqlCommand("Select MODEL From tbProducts WHERE ID=@id", con.conn());
           cmd.Parameters.AddWithValue("@id", productID);
           SqlDataReader read = cmd.ExecuteReader();
           while (read.Read())
           {
               productName = read["MODEL"].ToString();
           }
           con.conn().Close();
           stock = Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value);
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            customerID = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
            lblCustomerID.Text = customerID.ToString();
            if (chckCorporate.Checked == true)
            {
                SqlCommand cmd = new SqlCommand("Select NAME From tbCorporateCustomers WHERE ID=@id", con.conn());
                cmd.Parameters.AddWithValue("@id",customerID);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    customerName = read["NAME"].ToString();
                }
                con.conn().Close();
            }
            else if (chckRetail.Checked == true)
            {
                SqlCommand cmd = new SqlCommand("Select NAME From tbRetailCustomers WHERE ID=@id", con.conn());
                cmd.Parameters.AddWithValue("@id", customerID);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    customerName = read["NAME"].ToString();
                }
                con.conn().Close();   
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
        }
        private void cmbSeller_SelectedIndexChanged(object sender, EventArgs e)
        {
            employeeID = Convert.ToInt32(cmbSeller.SelectedValue);
            lblEmployeeID.Text = employeeID.ToString();
        }
    }
}
