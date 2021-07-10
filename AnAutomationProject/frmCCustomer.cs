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
    public partial class frmCCustomer : Form
    {
        public frmCCustomer()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        int id = 0;
        private void frmCCustomer_Load(object sender, EventArgs e)
        {
            getData();
        }
        void getData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbCorporateCustomers", con.conn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtAuthorized.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtGsm.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtAddress.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "")
            {
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            }
            else
            {
                id = 0;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO tbCorporateCustomers (NAME,AUTHORIZED,GSM,EMAIL,ADDRESS) VALUES (@v1,@v2,@v3,@v4,@v5)", con.conn());
            cmd.Parameters.AddWithValue("@v1", txtName.Text);
            cmd.Parameters.AddWithValue("@v2", txtAuthorized.Text);
            cmd.Parameters.AddWithValue("@v3", txtGsm.Text);
            cmd.Parameters.AddWithValue("@v4", txtEmail.Text);
            cmd.Parameters.AddWithValue("@v5", txtAddress.Text);
            cmd.ExecuteNonQuery();
            con.conn().Close();
            getData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("Please Select a Record");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbCorporateCustomers SET NAME=@v1,AUTHORIZED=@v2,GSM=@v3,EMAIL=@v4,ADDRESS=@v5 WHERE ID=@id", con.conn());
                cmd.Parameters.AddWithValue("@v1", txtName.Text);
                cmd.Parameters.AddWithValue("@v2", txtAuthorized.Text);
                cmd.Parameters.AddWithValue("@v3", txtGsm.Text);
                cmd.Parameters.AddWithValue("@v4", txtEmail.Text);
                cmd.Parameters.AddWithValue("@v5", txtAddress.Text);
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
                SqlCommand cmd = new SqlCommand("DELETE FROM tbCorporateCustomers WHERE ID = @id", con.conn());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
                id = 0;
            }
        }
    }
}
