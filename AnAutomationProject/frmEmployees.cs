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
    public partial class frmEmployees : Form
    {
        public frmEmployees()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        int id = 0;
        void getData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbEmployees", con.conn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void frmEmployees_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSurname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtGsm.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtAddress.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtJob.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtSalary.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
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
            SqlCommand cmd = new SqlCommand("INSERT INTO tbEmployees (NAME,SURNAME,GSM,EMAIL,ADDRESS,JOB,SALARY) VALUES (@v1,@v2,@v3,@v4,@v5,@v6,@v7)", con.conn());
            cmd.Parameters.AddWithValue("@v1", txtName.Text);
            cmd.Parameters.AddWithValue("@v2", txtSurname.Text);
            cmd.Parameters.AddWithValue("@v3", txtGsm.Text);
            cmd.Parameters.AddWithValue("@v4", txtEmail.Text);
            cmd.Parameters.AddWithValue("@v5", txtAddress.Text);
            cmd.Parameters.AddWithValue("@v6", txtJob.Text);
            cmd.Parameters.AddWithValue("@v7", txtSalary.Text);
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
                SqlCommand cmd = new SqlCommand("UPDATE tbEmployees SET NAME=@v1,SURNAME=@v2,GSM=@v3,EMAIL=@v4,ADDRESS=@v5,JOB=@v6,SALARY=@v7 WHERE ID=@id", con.conn());
                cmd.Parameters.AddWithValue("@v1", txtName.Text);
                cmd.Parameters.AddWithValue("@v2", txtSurname.Text);
                cmd.Parameters.AddWithValue("@v3", txtGsm.Text);
                cmd.Parameters.AddWithValue("@v4", txtEmail.Text);
                cmd.Parameters.AddWithValue("@v5", txtAddress.Text);
                cmd.Parameters.AddWithValue("@v6", txtJob.Text);
                cmd.Parameters.AddWithValue("@v7", txtSalary.Text);
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
                SqlCommand cmd = new SqlCommand("DELETE FROM tbEmployees WHERE ID = @id", con.conn());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
                id = 0;
            }
        }
    }
}
