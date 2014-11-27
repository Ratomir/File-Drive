using EnterpriseDT.Net.Ftp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Aplikacija
{
    public partial class frmUnosFoldera : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public frmUnosFoldera()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Konstruktor koji incijalizuje komponente forme i referencira ftp element iz forme main.
        /// </summary>
        /// <param name="ftpServer">ftp parametar iz forme main</param>
        public frmUnosFoldera(FTPConnection ftpServer)
        {
            InitializeComponent();
            ftpConnection1 = ftpServer;
        }

        private void frmUnosFoldera_Load(object sender, EventArgs e)
        {
            if(ftpConnection1.IsConnected == false)
            {
                ftpConnection1.Connect();
            }
        }

        private void btnKreirajFolder_Click(object sender, EventArgs e)
        {
            if(!provjeraGreske())
            {
                Properties.Settings.Default.nazivFoldera = txtImeFoldera.Text;
                try
                {
                    ftpConnection1.CreateDirectory(Properties.Settings.Default.nazivFoldera[0] + "\\" + Properties.Settings.Default.nazivFoldera);
                    MessageBox.Show("Uspješno ste kreirali Vaš folder na serveru pod nazivom \"" + Properties.Settings.Default.nazivFoldera + "\".", "Informacije", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Properties.Settings.Default.folderPostoji = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtImeFoldera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnKreirajFolder_Click(sender, (EventArgs)e);
                txtImeFoldera.Text = "";
            }
        }

        private void frmUnosFoldera_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Postoji mogućnost da korisnik odmah klikne dugme Close, zato je potrebno još jednom preispitati validnost foldera.
            //Nije potrebno da kupimo vrijednost koju funkcija ProvjeraGreske vraca.
            if (Properties.Settings.Default.folderPostoji == false)
            {
                provjeraGreske();
                ftpConnection1.Close();
            }

            Properties.Settings.Default.Save();
        }

        private bool provjeraGreske()
        {
            if (txtImeFoldera.TextLength == 0)
            {
                MessageBox.Show("Niste unjeli ime foldera.", "Onemogućeno kreiranje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else if ((txtImeFoldera.Text[0] >= 21 && txtImeFoldera.Text[0] <= 64) || (txtImeFoldera.Text[0] >= 91 && txtImeFoldera.Text[0] <= 96) || (txtImeFoldera.Text[0] >= 123))
            {
                MessageBox.Show("Prvi karakter mora da bude neko slovo engleskog alfabeta.", "Onemogućeno kreiranje", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return true;
            }
            else if (ftpConnection1.DirectoryExists(txtImeFoldera.Text[0] + "\\" + txtImeFoldera.Text))
            {
                MessageBox.Show("Folder na putanju " + txtImeFoldera.Text[0] + "\\" + txtImeFoldera.Text + ", postoji. Molimo Vas da unesete neko drugo ime.", "Onemogućeno kreiranje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            return false;
        }

        
    }
}
