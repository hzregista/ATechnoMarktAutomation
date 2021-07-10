using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace TheTechnoMarketAutomation
{
    public partial class frmMail : Form
    {
        public frmMail()
        {
            InitializeComponent();
        }
        private void frmMail_Load(object sender, EventArgs e)
        {
        }
        public void getmail(string mail)
        {
            label1.Text = mail;
            this.Hide(); 
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("MailAddress", "Password"); // It should get e-mail address and password parameters here.
            client.Port = 587;
            client.Host = "gmail.com";
            client.EnableSsl = true;
            msg.To.Add(label1.Text);
            msg.From = new MailAddress("MailAddress"); //E-mail address...
            msg.Subject = txtSubject.Text;
            msg.Body = txtMessage.Text;
            client.Send(msg);
        }
    }
}
