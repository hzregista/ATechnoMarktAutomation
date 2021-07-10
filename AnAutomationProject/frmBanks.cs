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
    public partial class frmBanks : Form
    {
        public frmBanks()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        int firmid = 0;
        int bankid = 0;
        void getData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute BankInformation", con.conn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void getfirm()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID, NAME From tbCorporateCustomers",con.conn());
            da.Fill(dt);
            cmbFirm.ValueMember = "ID";
            cmbFirm.DisplayMember = "NAME";
            cmbFirm.DataSource = dt;
        }
        private void frmBanks_Load(object sender, EventArgs e)
        {
            getData();
            getfirm();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            firmid = Convert.ToInt32(cmbFirm.SelectedValue);
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtBranch.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtAccountNo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtAuthorized.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtDate.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            cmbFirm.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "")
            {
                bankid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            }
            else
            {
                bankid = 0;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (firmid == 0) { MessageBox.Show("Please Select a Firm"); }
            else
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbBanks (BANKNAME, BRANCH, ACCOUNTNO, AUTHORIZED, DATE, FIRMID) VALUES (@v1,@v2,@v3,@v4,@v5,@v6)", con.conn());
                cmd.Parameters.AddWithValue("@v1", txtName.Text);
                cmd.Parameters.AddWithValue("@v2", txtBranch.Text);
                cmd.Parameters.AddWithValue("@v3", txtAccountNo.Text);
                cmd.Parameters.AddWithValue("@v4", txtAuthorized.Text);
                cmd.Parameters.AddWithValue("@v5", txtDate.Text);
                cmd.Parameters.AddWithValue("@v6", firmid);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
                
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (bankid == 0)
            {
                MessageBox.Show("Please Select a Record");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbBanks SET BANKNAME=@v1,BRANCH=@v2,ACCOUNTNO=@v3,AUTHORIZED=@v4,DATE=@v5,FIRMID=@firmid WHERE ID=@bankid", con.conn());
                cmd.Parameters.AddWithValue("@v1", txtName.Text);
                cmd.Parameters.AddWithValue("@v2", txtBranch.Text);
                cmd.Parameters.AddWithValue("@v3", txtAccountNo.Text);
                cmd.Parameters.AddWithValue("@v4", txtAuthorized.Text);
                cmd.Parameters.AddWithValue("@v5", txtDate.Text);
                cmd.Parameters.AddWithValue("@firmid", firmid);
                cmd.Parameters.AddWithValue("@bankid", bankid);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (bankid == 0)
            {
                MessageBox.Show("Please Select a Record");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbBanks WHERE ID = @id", con.conn());
                cmd.Parameters.AddWithValue("@id", bankid);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
                bankid = 0;
            }
        }
    }
}
