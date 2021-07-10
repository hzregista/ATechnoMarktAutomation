using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheTechnoMarketAutomation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void formshow(Form form)
        {
            panel1.Controls.Clear();
            form.MdiParent = this;
            form.FormBorderStyle = FormBorderStyle.None;
            panel1.Controls.Add(form);
            form.Show();
        }
        void mainpage()
        {
            frmMainPage fmp = new frmMainPage();
            formshow(fmp);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            frmProducts fpr = new frmProducts();
            formshow(fpr);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.tcmb.gov.tr/wps/wcm/connect/tr/tcmb+tr/main+page+site+area/bugun");
            mainpage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmRCustomers frc = new frmRCustomers();
            formshow(frc);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmCCustomer fcc = new frmCCustomer();
            formshow(fcc);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmEmployees fem = new frmEmployees();
            formshow(fem);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            frmYellowPages fyp = new frmYellowPages();
            formshow(fyp);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmOutgoings fog = new frmOutgoings();
            formshow(fog);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmBanks fba = new frmBanks();
            formshow(fba);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            frmInvoices fin = new frmInvoices();
            formshow(fin);
        }

        private void btnSalesTracking_Click(object sender, EventArgs e)
        {
            frmSalesTracking fst = new frmSalesTracking();
            formshow(fst);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmStocks fso = new frmStocks();
            formshow(fso);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            mainpage();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            frmNotes fnt = new frmNotes();
            formshow(fnt);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            frmNewInvoice fni = new frmNewInvoice();
            formshow(fni);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            frmCash fc = new frmCash();
            formshow(fc);
        }
    }
}
