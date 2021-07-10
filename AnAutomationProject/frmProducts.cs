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
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        int id=0;
        void getData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbProducts", con.conn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void frmProducts_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO tbProducts (TYPE,BRAND,MODEL,YEAR,COUNT,BUYINGPRICE,SALEPRICE) VALUES (@v1,@v2,@v3,@v4,@v5,@v6,@v7)", con.conn());
            cmd.Parameters.AddWithValue("@v1", txtType.Text);
            cmd.Parameters.AddWithValue("@v2", txtBrand.Text);
            cmd.Parameters.AddWithValue("@v3", txtModel.Text);
            cmd.Parameters.AddWithValue("@v4", txtYear.Text);
            cmd.Parameters.AddWithValue("@v5", int.Parse(txtCount.Text));
            cmd.Parameters.AddWithValue("@v6", decimal.Parse(txtCost.Text));
            cmd.Parameters.AddWithValue("@v7", decimal.Parse(txtPrice.Text));
            cmd.ExecuteNonQuery();
            con.conn().Close();
            getData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
            txtType.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtBrand.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtModel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtYear.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtCount.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtCost.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtPrice.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "")
            {
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            }else
            {
                id = 0;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("Please Select a Record");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbProducts SET TYPE=@v1,BRAND=@v2,MODEL=@v3,YEAR=@v4,COUNT=@v5,BUYINGPRICE=@v6,SALEPRICE=@v7 WHERE ID=@id", con.conn());
                cmd.Parameters.AddWithValue("@v1", txtType.Text);
                cmd.Parameters.AddWithValue("@v2", txtBrand.Text);
                cmd.Parameters.AddWithValue("@v3", txtModel.Text);
                cmd.Parameters.AddWithValue("@v4", txtYear.Text);
                cmd.Parameters.AddWithValue("@v5", int.Parse(txtCount.Text));
                cmd.Parameters.AddWithValue("@v6", decimal.Parse(txtCost.Text));
                cmd.Parameters.AddWithValue("@v7", decimal.Parse(txtPrice.Text));
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
                SqlCommand cmd = new SqlCommand("DELETE FROM tbProducts WHERE ID = @id", con.conn());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
                id = 0;
            }
        }
    }
}
