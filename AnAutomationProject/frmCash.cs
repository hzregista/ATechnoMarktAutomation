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
using DevExpress.Charts;
namespace TheTechnoMarketAutomation
{
    public partial class frmCash : Form
    {
        public frmCash()
        {
            InitializeComponent();
        }
        dbConn con = new dbConn();
        void getRetailCustomersData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute RetailTracking",con.conn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        } 
        void getCorporateCustomersData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute CorporateTracking", con.conn());
            da.Fill(dt);
            dataGridView3.DataSource = dt;
        }
        void totalincome()
        {
            SqlCommand cmd = new SqlCommand("Select Sum(TOTALPRICE) From tbBillDetail",con.conn());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblTotalIncome.Text = dr[0].ToString();
            }
            con.conn().Close();
        }
        void totalcost()
        {
            SqlCommand cmd = new SqlCommand("Select Sum(COUNT*BUYINGPRICE) From tbProducts", con.conn());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblCost.Text = dr[0].ToString();
            }
            con.conn().Close();
        }
        void totalstockvalue()
        {
            SqlCommand cmd = new SqlCommand("Select Sum(COUNT*SALEPRICE) From tbProducts", con.conn());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblTotalStockValue.Text = dr[0].ToString();
            }
            con.conn().Close();
        }
        void lastinvoices()
        {
            SqlCommand cmd = new SqlCommand("SELECT (ELECTRICBILL + WATERBILL + GASBILL + INTERNETBILL + EXTRAS) From tbOutgoings Order By ID asc",con.conn());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblPayments.Text = dr[0].ToString();
            }
            con.conn().Close();
        }
        void totalsalary()
        {
            SqlCommand cmd = new SqlCommand("SELECT sum(SALARY) From tbEmployees", con.conn());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblTotalSales.Text = dr[0].ToString();
            }
            con.conn().Close();
        }
        void numberofreatilcustomers()
        {
            SqlCommand cmd = new SqlCommand("SELECT Count (*) From tbRetailCustomers", con.conn());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblRCustomers.Text = dr[0].ToString();
            }
            con.conn().Close();
        }
        void numberofcorporatecustomers()
        {
            SqlCommand cmd = new SqlCommand("SELECT Count (*) From tbCorporateCustomers", con.conn());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblCCustomers.Text = dr[0].ToString();
            }
            con.conn().Close();
        }
        void numberofemployees()
        {
            SqlCommand cmd = new SqlCommand("SELECT Count (*) From tbEmployees", con.conn());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblEmployees.Text = dr[0].ToString();
            }
            con.conn().Close();
        }
        void chartbills()
        {
            SqlCommand cmd = new SqlCommand("SELECT top 12 MONTH, (ELECTRICBILL+WATERBILL+GASBILL+INTERNETBILL) From tbOutgoings order by ID Desc",con.conn());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Bills"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            con.conn().Close();
        }
        void chartextras()
        {
            SqlCommand cmd = new SqlCommand("SELECT top 12 MONTH, EXTRAS From tbOutgoings order by ID Desc", con.conn());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl2.Series["Extras"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            con.conn().Close();
        }
        void chartsalaries()
        {
            SqlCommand cmd = new SqlCommand("SELECT top 12 MONTH, SALARIES From tbOutgoings order by ID Desc", con.conn());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl3.Series["Salaries"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            con.conn().Close();
        }
        private void frmCash_Load(object sender, EventArgs e)
        {
            getRetailCustomersData();
            getCorporateCustomersData();
            totalincome();
            lastinvoices();
            totalsalary();
            numberofcorporatecustomers();
            numberofreatilcustomers();
            totalcost();
            totalstockvalue();
            numberofemployees();
            chartbills();
            chartsalaries();
            chartextras();
        }
        
    }
}
