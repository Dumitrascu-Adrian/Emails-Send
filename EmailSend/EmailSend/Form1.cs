using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
namespace EmailSend
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
        }
        string[] lines;
        void ReadFile()
        {
            string input = "";

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "C# Corner Open File Dialog";
            fileDialog.InitialDirectory = @"D:\";
            fileDialog.Filter = "All files (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                input = fileDialog.FileName;

            }         
            lines = System.IO.File.ReadAllLines(input);
            foreach (string line in lines)
            {
                listBox1.Items.Add(line);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ReadFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = lines.Length;
            progressBar1.Step = 1;
            try
            {
                string username = "du4@gmail.com"; //gmailul tau
                string password = "dru";  //parola contului

                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                 SmtpServer.Credentials = new System.Net.NetworkCredential(username, password);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(username);
                mail.Subject = Subject.Text;
                mail.Body = Message.Text;

                foreach (string line in lines)
                {                 
                    mail.To.Add(line);
                    progressBar1.PerformStep();
                 }
                SmtpServer.Send(mail);
                progressBar1.Visible = false;
                MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

     
    }
}
