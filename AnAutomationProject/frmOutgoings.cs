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
    public partial class frmOutgoings : Form
    {
        public frmOutgoings()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        int id = 0;
        void getData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbOutgoings", con.conn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void frmOutgoings_Load(object sender, EventArgs e)
        {
            getData();   
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO tbOutgoings (ELECTRICBILL,WATERBILL,GASBILL,INTERNETBILL,SALARIES,EXTRAS,MONTH,YEAR) VALUES (@v1,@v2,@v3,@v4,@v5,@v6,@v7,@v8)", con.conn());
            cmd.Parameters.AddWithValue("@v1", txtElectric.Text);
            cmd.Parameters.AddWithValue("@v2", txtWater.Text);
            cmd.Parameters.AddWithValue("@v3", txtGas.Text);
            cmd.Parameters.AddWithValue("@v4", txtInternet.Text);
            cmd.Parameters.AddWithValue("@v5", txtSalaries.Text);
            cmd.Parameters.AddWithValue("@v6", txtExtras.Text);
            cmd.Parameters.AddWithValue("@v7", cmbMonth.Text);
            cmd.Parameters.AddWithValue("@v8", cmbYear.Text);
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
                SqlCommand cmd = new SqlCommand("UPDATE tbOutgoings SET ELECTRICBILL=@v1,WATERBILL=@v2,GASBILL=@v3,INTERNETBILL=@v4,SALARIES=@v5,EXTRAS=@v6, MONTH=@v7, YEAR=@v8 WHERE ID=@id", con.conn());
                cmd.Parameters.AddWithValue("@v1", txtElectric.Text);
                cmd.Parameters.AddWithValue("@v2", txtWater.Text);
                cmd.Parameters.AddWithValue("@v3", txtGas.Text);
                cmd.Parameters.AddWithValue("@v4", txtInternet.Text);
                cmd.Parameters.AddWithValue("@v5", txtSalaries.Text);
                cmd.Parameters.AddWithValue("@v6", txtExtras.Text);
                cmd.Parameters.AddWithValue("@v7", cmbMonth.Text);
                cmd.Parameters.AddWithValue("@v8", cmbYear.Text);
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
                SqlCommand cmd = new SqlCommand("DELETE FROM tbOutgoings WHERE ID = @id", con.conn());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
                id = 0;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtElectric.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtWater.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtGas.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtInternet.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtSalaries.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtExtras.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            cmbMonth.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            cmbYear.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "")
            {
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            }
            else
            {
                id = 0;
            }
        }
    }
}
