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
    public partial class frmNotes : Form
    {
        public frmNotes()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        int id = 0;
        void getData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbNotes", con.conn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void frmNotes_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDate.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtTitle.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtNote.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtWriter.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
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
            SqlCommand cmd = new SqlCommand("INSERT INTO tbNotes (DATE,TITLE,NOTE,WRITER) VALUES (@v1,@v2,@v3,@v4)", con.conn());
            cmd.Parameters.AddWithValue("@v1", txtDate.Text);
            cmd.Parameters.AddWithValue("@v2", txtTitle.Text);
            cmd.Parameters.AddWithValue("@v3", txtNote.Text);
            cmd.Parameters.AddWithValue("@v4", txtWriter.Text);
            cmd.ExecuteNonQuery();
            con.conn().Close();
            getData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("Please Select a Record");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbNotes WHERE ID = @id", con.conn());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
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
                SqlCommand cmd = new SqlCommand("UPDATE tbNotes SET DATE=@v1,TITLE=@v2,NOTE=@v3,WRITER=@v4 WHERE ID=@id", con.conn());
                cmd.Parameters.AddWithValue("@v1", txtDate.Text);
                cmd.Parameters.AddWithValue("@v2", txtTitle.Text);
                cmd.Parameters.AddWithValue("@v3", txtNote.Text);
                cmd.Parameters.AddWithValue("@v4", txtWriter.Text);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.conn().Close();
                getData();
            }
        }
    }
}
