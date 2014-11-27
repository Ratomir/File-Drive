using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EnterpriseDT.Net.Ftp;
using System.Diagnostics;
using SQL;
using Finisar.SQLite;
using System.IO;

namespace Aplikacija
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmPodesavanjeFoldera : Form
    {
        long zauzetoMemorijeKB = 0;

        /// <summary>
        /// 
        /// </summary>
        public frmPodesavanjeFoldera()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftpConnection"></param>
        public frmPodesavanjeFoldera(FTPConnection ftpConnection)
        {
            InitializeComponent();

            server = ftpConnection;
        }

        private void frmPodesavanjeFoldera_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.folderPostoji)
            {
                btnPromjeniIme.Enabled   = true;
                btnPobrisiFolder.Enabled = true;
                lnkImeFoldera.Text = Properties.Settings.Default.nazivFoldera;
                lnkPutanja.Text = Properties.Settings.Default.nazivFoldera[0] + "\\" + Properties.Settings.Default.nazivFoldera;
            }

            if (Properties.Settings.Default.folderPostoji == true && server.Exists("\\" + Properties.Settings.Default.nazivFoldera[0] + "\\" + Properties.Settings.Default.nazivFoldera + "\\Baza.s3db"))
            {
                ucitavanjeForme();

                deleteToolStripButton.Enabled = true;
                downloadToolStripButton.Enabled = true;
                printToolStripButton.Enabled = true;
            }
            else
            {
                btnKreirajFolder.Enabled = true;
            }
        }

        private void lnkImeFoldera_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!(lnkImeFoldera.Text == "Nepoznato"))
            {
                Process.Start(("ftp:\\127.0.0.1\\" + Properties.Settings.Default.nazivFoldera[0] + "\\" + Properties.Settings.Default.nazivFoldera) as string);
            }
        }

        private void lnkPutanja_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!(lnkPutanja.Text == "Nepoznato"))
            {
                Process.Start(("ftp:\\127.0.0.1\\" + Properties.Settings.Default.nazivFoldera[0] + "\\" + Properties.Settings.Default.nazivFoldera) as string);
            }
        }

        private void cmbMjeraMemorije_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbMjeraMemorije.Text)
            {
                case "KB":
                    {
                        lblMemorija.Text = zauzetoMemorijeKB.ToString();
                        break;
                    }
                case "MB":
                    {
                        lblMemorija.Text = Math.Round(zauzetoMemorijeKB/1024f, 2).ToString();
                        break;
                    }
                default:
                    break;
            }
        }

        private void btnPromjeniIme_Click(object sender, EventArgs e)
        {
            frmUnosFoldera formaUnosFoldera = new frmUnosFoldera(server);
            string prethodnoIme = Properties.Settings.Default.nazivFoldera;
            if (formaUnosFoldera.ShowDialog() == DialogResult.OK)
            {
            }

            if (prethodnoIme != Properties.Settings.Default.nazivFoldera)
            {
                try
                {
                    if (server.RenameFile("\\D\\Danijela", "\\B\\Branimir"))
                    {
                        MessageBox.Show("Uspješno ste preimenovali folder u " + Properties.Settings.Default.nazivFoldera + " i premjestili sve fajlove.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Greška"); 
                }
            }
        }

        private void btnKreirajFolder_Click(object sender, EventArgs e)
        {
            frmUnosFoldera formaUnosFoldera = new frmUnosFoldera();
            if (formaUnosFoldera.ShowDialog() == DialogResult.OK)
            {
            }

            if (Properties.Settings.Default.folderPostoji)
            {
                btnKreirajFolder.Enabled = false;
                btnPromjeniIme.Enabled = true;
            }
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            var odgovor = MessageBox.Show("Da li ste sigurni da želite da izbrišete \"" + lblPojedinacniFajloviImeFajla.Text + "\"?", "Informacije", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            switch (odgovor)
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    {
                        try
                        {
                            string imeFoldera = Properties.Settings.Default.nazivFoldera[0] + "\\" + Properties.Settings.Default.nazivFoldera + "\\";
                            if (server.DeleteFile("\\" + imeFoldera + lblPojedinacniFajloviImeFajla.Text))
                            {
                                MessageBox.Show("Uspješno ste izvršili brisanje fajla " + lblPojedinacniFajloviImeFajla.Text + ".", "Informacije", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                SQLClass baza = new SQLClass();
                                baza.otovriKonekciju();
                                baza.pobrisiPodatak(lblPojedinacniFajloviId.Text);
                                baza.zatvoriKonekciju();

                                lblPojedinacniFajloviId.DataBindings.Clear();
                                lblPojedinacniFajloviImeFajla.DataBindings.Clear();
                                lblPojedinacniFajloviLokacija.DataBindings.Clear();
                                lblPojedinacniFajloviDatumVrijeme.DataBindings.Clear();

                                int brojDogadjaja = bindingNavigator.BindingSource.Count;

                                if (--brojDogadjaja == 0)
                                {
                                    lblPojedinacniFajloviId.Text = "*********";
                                    lblPojedinacniFajloviImeFajla.Text = "*********";
                                    lblPojedinacniFajloviLokacija.Text = "*********";
                                    lblPojedinacniFajloviDatumVrijeme.Text = "*********";

                                    dgvTabela.DataSource = null;
                                    dgvTabela.Refresh();

                                    bindingNavigator.DataBindings.Clear();
                                    bindingNavigator.Refresh();

                                    deleteToolStripButton.Enabled = false;
                                    downloadToolStripButton.Enabled = false;
                                    printToolStripButton.Enabled = false;

                                    server.DeleteFile("\\" + Properties.Settings.Default.nazivFoldera[0] + "\\" + Properties.Settings.Default.nazivFoldera + "\\Baza.s3db");
                                    zauzetoMemorijeKB = 0;
                                    lblMemorija.Text = "0";
                                }
                                else
                                {
                                    Funkcije.slanjeFajlaNaServer(server, Application.StartupPath + @"\Baza.s3db");
                                    ucitavanjeForme();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Desila se greška pri brisanju.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                        break;
                    }
                default:
                    break;
            }
        }

        private void downloadToolStripButton_Click(object sender, EventArgs e)
        {
            string lokacijaZaSmjestanje = lblPojedinacniFajloviLokacija.Text.Remove(Funkcije.pointerString(lblPojedinacniFajloviLokacija.Text) + 1);
            if (!(Directory.Exists(lokacijaZaSmjestanje)))
            {
                System.IO.Directory.CreateDirectory(lokacijaZaSmjestanje);
            }

            server.DownloadFile(lokacijaZaSmjestanje, lblPojedinacniFajloviImeFajla.Text);
        }

        private void ucitavanjeForme()
        {
            zauzetoMemorijeKB = 0;

            SQLClass baza = new SQLClass();
            FTPFile[] fajl;

            /*
             *Pozicioniramo se na lokaciju iznad klijentovog foldera.
             * */
            server.ChangeWorkingDirectory("\\" + Properties.Settings.Default.nazivFoldera[0]);
            fajl = server.GetFileInfos(Properties.Settings.Default.nazivFoldera);
            /*
             * Pozicioniramo se na klijentov folder.
             * */
            server.ChangeWorkingDirectory(Properties.Settings.Default.nazivFoldera);

            for (int i = 0; i < fajl.Length; i++)
            {
                zauzetoMemorijeKB += fajl[i].Size;
            }

            lblMemorija.Text = zauzetoMemorijeKB.ToString();

            cmbMjeraMemorije.Text = "MB";

            server.DownloadFile(Application.StartupPath, "Baza.s3db");

            baza.otovriKonekciju();
            baza.ucitavanjePodataka(dgvTabela, bindingSource);
            baza.zatvoriKonekciju();

            bindingNavigator.BindingSource = bindingSource;

            lblPojedinacniFajloviId.DataBindings.Add(new Binding("Text", bindingSource, "Id"));
            lblPojedinacniFajloviImeFajla.DataBindings.Add(new Binding("Text", bindingSource, "ImeFajla"));
            lblPojedinacniFajloviLokacija.DataBindings.Add(new Binding("Text", bindingSource, "Lokacija"));
            lblPojedinacniFajloviDatumVrijeme.DataBindings.Add(new Binding("Text", bindingSource, "DatumVrijeme"));
        }

        private void btnPobrisiFolder_Click(object sender, EventArgs e)
        {

        }
    }
}
