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
    public partial class frmMainPage : Form
    {
        public frmMainPage()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        void getStock()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select Type, Sum(Count) as 'Amount' From tbProducts Group By TYPE Having Sum(Count) <= 10 order by Sum(Count)", con.conn());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        void getCorEvent()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec RecentCorporateSales", con.conn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void getRetEvent()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec RecentRetailSales", con.conn());
            da.Fill(dt);
            dataGridView4.DataSource = dt;
        }
        void getNotes()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbNotes", con.conn());
            da.Fill(dt);
            dataGridView3.DataSource = dt;
        }
        private void frmMainPage_Load(object sender, EventArgs e)
        {
            getStock();
            getCorEvent();
            getRetEvent();
            getNotes();            
        }
       
    }
}
