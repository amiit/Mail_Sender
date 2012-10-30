using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Net.Mail;

namespace QiHe.MiscTools.EmailSender
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string attachmentFile = fileBrower1.FilePath;
            if (attachmentFile != String.Empty && !File.Exists(attachmentFile))
            {
                MessageBox.Show(attachmentFile + " not found.");
                return;
            }

            buttonSend.Enabled = false;
            bool result = false;
            if (attachmentFile != String.Empty)
            {
                MailMessage mail = new System.Net.Mail.MailMessage(
                    textBoxFrom.Text,
                    textBoxTo.Text);
                mail.Subject = textBoxSubject.Text;
                mail.Body = textBoxBody.Text;
                mail.Attachments.Add(new Attachment(attachmentFile));
                result = QiHe.CodeLib.Net.EmailSender.Send(mail);
            }
            else
            {
                result = QiHe.CodeLib.Net.EmailSender.Send(
                    textBoxFrom.Text,
                    textBoxTo.Text,
                    textBoxSubject.Text,
                    textBoxBody.Text);
            }
            if (result)
            {
                MessageBox.Show("The email is sent.");
            }
            else
            {
                MessageBox.Show("Send email failed.");
            }
            buttonSend.Enabled = true;
        }
    }
}