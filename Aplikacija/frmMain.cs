using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SQL;
using System.Diagnostics;
using Finisar.SQLite;
using System.IO;
using System.Drawing.Printing;
using WMPLib;
using System.Threading;

namespace Aplikacija
{
    public partial class frmMain : Form
    {
        SQLClass sql = new SQLClass();
        Stopwatch stoperica = new Stopwatch();

        List<String> lstAdresa = new List<string>();

        //### FUNKCIJE ZA FORMU

        #region GLAVNI KONSTURKTOR

        /// <summary>
        /// Konstruktor forme Main.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region UČITAVANJE FORME

        /// <summary>
        /// Funkcija se izvršava dok se učitava forma.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.folderPostoji = false;

            fileSystemWatcher1.EnableRaisingEvents = false;

            this.BackColor = Properties.Settings.Default.BojaPozadine;
            toolStrip1.BackColor = Properties.Settings.Default.BojaStatusMenija;
            menuStrip1.BackColor = Properties.Settings.Default.menuStripBoja;
            tabMeniSkeniranje.BackColor = Properties.Settings.Default.TabSkeniranje;
            tabMeniListaSkeniranja.BackColor = Properties.Settings.Default.TabListaSkeniranja;
            tabMeniOtvoriListu.BackColor = Properties.Settings.Default.TabOtvoriListu;
            tabMeniMuzickiPlayer.BackColor = Properties.Settings.Default.TabMuzickiPlayer;
            tabMeniPdfCitac.BackColor = Properties.Settings.Default.TabPdfCitac;
            tabMeniWebPretrazivac.BackColor = Properties.Settings.Default.TabWebPretrazivac;
            tabMeniInformacije.BackColor = Properties.Settings.Default.TabInformacije;

            this.Text += " - Dobrodošli " + Environment.MachineName;

            if (Directory.Exists(Properties.Settings.Default.adresaZaPretrazivanje) == true)
            {
                fileSystemWatcher1.Path = Properties.Settings.Default.adresaZaPretrazivanje;
                txtSkeniranjePutanja.Text = Properties.Settings.Default.adresaZaPretrazivanje;
            }

            if (Funkcije.provjeriKonekciju())
            {
                otvoriGoogleToolStripMenuItem_Click(sender, e);

                if (Properties.Settings.Default.folderPostoji)
                {
                    bwSkidanje.RunWorkerAsync();
                }
            }
            else
            {
                tslPoruka.Text = "Internet konekcija onemogućena!";
                tslPoruka.ForeColor = Color.FromArgb(255,0,0);
            }
        }

        #endregion

        //#### BANER FAKULTETA

        #region FAKULTET

        /// <summary>
        /// Funckija na lijevi dvosturki klik miša otvara stranicu etf fakulteta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pctFakultet_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Process.Start("http://www.etf.unssa.rs.ba/" as string);
            }
        }

        #endregion

        //#### DOGADJAJI ZA RAD SA OPCIJAMA IZ MENIJA

        #region MENI FAJL

        /// <summary>
        /// Funkcija spušta file meni.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fajlToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            fajlToolStripMenuItem.ShowDropDown();
        }

        private void noviToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileSystemWatcher1.EnableRaisingEvents = false;

            if (txtListaSkeniranjaLista.Text.Length > 0)
            {
                txtListaSkeniranjaLista.Text = "";

                toolStripLabelBrojKreiranja.Text = Convert.ToString(0);
                toolStripLabelBrojBrisanja.Text = Convert.ToString(0);
                toolStripLabelBrojPromjena.Text = Convert.ToString(0);
                toolStripLabelBrojPreimenovanja.Text = Convert.ToString(0);

                meniBrojacKreiranja.ToolTipText = Convert.ToString(0);
                meniBrojacBrisanja.ToolTipText = Convert.ToString(0);
                meniBrojacPromjena.ToolTipText = Convert.ToString(0);
                meniBrojacPreimenovanja.ToolTipText = Convert.ToString(0);

                MessageBox.Show("Brojači su vraćeni na nulu. Da bi pokrenuli novu simulaciju pritisnite 'Start'.", "Informacije", MessageBoxButtons.OK);
            }

            btnSkeniranjeStart.Enabled = true;

            lblStanjePracenja.Text = "OFF";

            tabMeni.SelectedIndex = 0;

            //Naredbe za malu aplikaciju
            meniStanjePracenja.Text = "Isključeno";
            meniStanjePracenja.ToolTipText = "Isključeno";
            meniStanjePracenja.Enabled = true;

            timer1.Stop();
            stoperica.Stop();
            stoperica.Reset();
        }

        #region OTVORI

        private void otvoriListuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            otvoriSkeniranuListu();
        }

        private void otvoriAdresuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileSystemWatcher1.EnableRaisingEvents == true)
            {
                var chose = MessageBox.Show("Trenutno ne možete izabrati adresu pošto sistem trenutno prati već izabranu lokaciju.\nIdite na novu fajl, zatim izaberite novu putanju. New->Open->Otvori adresu", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.None);

                switch (chose)
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Retry:
                        otvoriAdresuToolStripMenuItem_Click(sender, e);
                        break;
                    default:
                        break;
                }
            }
            else
                btnSkeniranjePutanja_Click(sender, e);
        }

        #endregion

        private void snimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter Fajl = new StreamWriter(txtSkeniranjePutanja.Text + "\\Lista.txt");

            Fajl.WriteLine(txtListaSkeniranjaLista.Text);
            Fajl.Close();
        }

        private void snimiKaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Funkcije.snimiFajl(txtListaSkeniranjaLista.Text);
        }

        private string documentContents;
        private string stringToPrint;

        #region Print

        private void printSnimljenuListuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtOtvoriListuLista.Text.Length == 0)
            {
                tabMeni.SelectedTab = tabMeniOtvoriListu;
                var odgovor = MessageBox.Show("Niste učitali nijednu skeniranu listu. Da li želite da učitate novu listu i isprintate je?", "Informacije", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                switch (odgovor)
                {
                    case DialogResult.No:
                        return;

                    case DialogResult.Yes:
                        otvoriSkeniranuListu();
                        break;
                    default:
                        break;
                }
            }

            /*
             * Potrebno je ponovo potvrditi šta treba da se štampa ako u međuvremenu
             * između učitavanja i printanja dođe do izmjene učitanog fajla.
             * */
            printDocument1.DocumentName = lblOtvoriListuImeListe.Text + " " + DateTime.Now.ToString();
            documentContents = txtOtvoriListuLista.Text;
            stringToPrint = documentContents;

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printTrenutnuListuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtListaSkeniranjaLista.Text.Length == 0)
            {
                tabMeni.SelectedTab = tabMeniListaSkeniranja;
                MessageBox.Show("Trenutna lista skeniranja je prazna.", "Informacije", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                printDocument1.DocumentName = "Trenutna Lista " + DateTime.Now.ToString();
                documentContents = txtListaSkeniranjaLista.Text;
                stringToPrint = documentContents;

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
        }

        #endregion

        #region Print Pregled

        private void snimljenuListuToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (txtOtvoriListuLista.Text.Length == 0)
            {
                tabMeni.SelectedTab = tabMeniOtvoriListu;
                var odgovor = MessageBox.Show("Niste učitali nijednu skeniranu listu. Da li želite da učitate novu listu za pregled?", "Informacije", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                switch (odgovor)
                {
                    case DialogResult.No:
                        return;

                    case DialogResult.Yes:
                        otvoriListuToolStripMenuItem_Click(sender, e);

                        break;
                    default:
                        break;
                }
            }

            /*
             * Potrebno je ponovo potvrditi šta treba da se štampa ako u međuvremenu
             * između učitavanja i printanja dođe do izmjene učitanog fajla.
             * */
            printDocument1.DocumentName = lblOtvoriListuImeListe.Text + " " + DateTime.Now.ToString();
            documentContents = txtOtvoriListuLista.Text;
            stringToPrint = documentContents;

            try
            {
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trenutnaListaZaSkeniranjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtListaSkeniranjaLista.Text.Length == 0)
            {
                tabMeni.SelectedTab = tabMeniListaSkeniranja;
                MessageBox.Show("Trenutna lista skeniranja je prazna.", "Informacije", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                printDocument1.DocumentName = "Trenutna Lista " + DateTime.Now.ToString();
                documentContents = txtListaSkeniranjaLista.Text;
                stringToPrint = documentContents;

                try
                {
                    printPreviewDialog1.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;

            // Sets the value of charactersOnPage to the number of characters  
            // of stringToPrint that will fit within the bounds of the page.
            e.Graphics.MeasureString(stringToPrint, this.Font,
                e.MarginBounds.Size, StringFormat.GenericTypographic,
                out charactersOnPage, out linesPerPage);

            // Draws the string within the bounds of the page.
            e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
            e.MarginBounds, StringFormat.GenericTypographic);

            // Remove the portion of the string that has been printed.
            stringToPrint = stringToPrint.Substring(charactersOnPage);

            // Check to see if more pages are to be printed.
            e.HasMorePages = (stringToPrint.Length > 0);

            // If there are no more pages, reset the string to be printed. 
            if (!e.HasMorePages)
                stringToPrint = documentContents;
        }

        private void izlazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rezultat = MessageBox.Show("Da li želite da snimite fajl prije napuštanja aplikacije?", "Snimanje ili Izlaz", MessageBoxButtons.YesNoCancel);

            switch (rezultat)
            {
                case DialogResult.Cancel:
                    break;
                case DialogResult.No:
                    Environment.Exit(1);
                    break;
                case DialogResult.Yes:
                    Funkcije.snimiFajl(txtListaSkeniranjaLista.Text);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region MENI EDIT

        private void editToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            editToolStripMenuItem.ShowDropDown();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Control alat = ActiveControl;

                if (alat is TextBox)
                {
                    TextBox txtBox = (TextBox)alat;
                    txtBox.Undo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška");
            }
        }

        private void isjeciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Control alat = ActiveControl;

                if (alat is TextBox)
                {
                    TextBox txtBox = (TextBox)alat;
                    txtBox.Cut();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška");
            }
        }

        private void kopirajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Control alat = ActiveControl;

                if (alat is TextBox)
                {
                    TextBox txtBox = (TextBox)alat;
                    txtBox.Copy();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void zalijepiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Control alat = ActiveControl;

                if (alat is TextBox)
                {
                    TextBox txtBox = (TextBox)alat;
                    txtBox.Paste();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void selektujSveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Control alat = ActiveControl;

                if (alat is TextBox)
                {
                    TextBox txtBox = (TextBox)alat;
                    txtBox.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region MENI ALATI

        private void alatiToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            alatiToolStripMenuItem.ShowDropDown();
        }

        private void promjeniBojuPozadineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = colorDialog1.Color;
                Properties.Settings.Default.BojaPozadine = colorDialog1.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void promjeniBojuMenijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                menuStrip1.BackColor = colorDialog1.Color;
                Properties.Settings.Default.menuStripBoja = colorDialog1.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void promjeniBojuStatusMenijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                toolStrip1.BackColor = colorDialog1.Color;
                Properties.Settings.Default.BojaStatusMenija = colorDialog1.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void cmdPromjeniBojuZaTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmdPromjeniBojuZaTab.Text)
            {
                case "Skeniranje":
                    {
                        if (colorDialog1.ShowDialog() == DialogResult.OK)
                        {
                            tabMeniSkeniranje.BackColor = colorDialog1.Color;
                            tabMeni.SelectedTab = tabMeniSkeniranje;

                            Properties.Settings.Default.TabSkeniranje = colorDialog1.Color;
                            Properties.Settings.Default.Save();
                        }
                        break;
                    }
                case "Lista skeniranja":
                    {
                        if (colorDialog1.ShowDialog() == DialogResult.OK)
                        {
                            tabMeniListaSkeniranja.BackColor = colorDialog1.Color;
                            tabMeni.SelectedTab = tabMeniListaSkeniranja;

                            Properties.Settings.Default.TabListaSkeniranja = colorDialog1.Color;
                            Properties.Settings.Default.Save();
                        }
                        break;
                    }
                case "Otvori listu":
                    {
                        if (colorDialog1.ShowDialog() == DialogResult.OK)
                        {
                            tabMeniOtvoriListu.BackColor = colorDialog1.Color;
                            tabMeni.SelectedTab = tabMeniOtvoriListu;

                            Properties.Settings.Default.TabOtvoriListu = colorDialog1.Color;
                            Properties.Settings.Default.Save();
                        }
                        break;
                    }
                case "Muzički player":
                    {
                        if (colorDialog1.ShowDialog() == DialogResult.OK)
                        {
                            tabMeniMuzickiPlayer.BackColor = colorDialog1.Color;
                            tabMeni.SelectedTab = tabMeniMuzickiPlayer;

                            Properties.Settings.Default.TabMuzickiPlayer = colorDialog1.Color;
                            Properties.Settings.Default.Save();
                        }
                        break;
                    }
                case "Adobe Pdf čitač":
                    {
                        if (colorDialog1.ShowDialog() == DialogResult.OK)
                        {
                            tabMeniPdfCitac.BackColor = colorDialog1.Color;
                            tabMeni.SelectedTab = tabMeniPdfCitac;

                            Properties.Settings.Default.TabPdfCitac = colorDialog1.Color;
                            Properties.Settings.Default.Save();
                        }
                        break;
                    }
                case "Web pretraživač":
                    {
                        if (colorDialog1.ShowDialog() == DialogResult.OK)
                        {
                            tabMeniWebPretrazivac.BackColor = colorDialog1.Color;
                            tabMeni.SelectedTab = tabMeniWebPretrazivac;

                            Properties.Settings.Default.TabWebPretrazivac = colorDialog1.Color;
                            Properties.Settings.Default.Save();
                        }
                        break;
                    }
                case "Inforamcije":
                    {
                        if (colorDialog1.ShowDialog() == DialogResult.OK)
                        {
                            tabMeniInformacije.BackColor = colorDialog1.Color;
                            tabMeni.SelectedTab = tabMeniInformacije;

                            Properties.Settings.Default.TabInformacije = colorDialog1.Color;
                            Properties.Settings.Default.Save();
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void promjeniBojuSvihTabovaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                tabMeniSkeniranje.BackColor = colorDialog1.Color;
                tabMeniListaSkeniranja.BackColor = colorDialog1.Color;
                tabMeniOtvoriListu.BackColor = colorDialog1.Color;
                tabMeniMuzickiPlayer.BackColor = colorDialog1.Color;
                tabMeniPdfCitac.BackColor = colorDialog1.Color;
                tabMeniWebPretrazivac.BackColor = colorDialog1.Color;
                tabMeniInformacije.BackColor = colorDialog1.Color;

                Properties.Settings.Default.TabSkeniranje = colorDialog1.Color;
                Properties.Settings.Default.TabListaSkeniranja = colorDialog1.Color;
                Properties.Settings.Default.TabOtvoriListu = colorDialog1.Color;
                Properties.Settings.Default.TabMuzickiPlayer = colorDialog1.Color;
                Properties.Settings.Default.TabPdfCitac = colorDialog1.Color;
                Properties.Settings.Default.TabWebPretrazivac = colorDialog1.Color;
                Properties.Settings.Default.TabInformacije = colorDialog1.Color;
            }
        }

        private void folderNaServeruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPodesavanjeFoldera PodesavanjeFoldera = new frmPodesavanjeFoldera(ftpConnection1);
            PodesavanjeFoldera.Show();
        }

        #endregion

        #region MENI POMOĆ

        private void pomocToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            pomocToolStripMenuItem.ShowDropDown();
        }

        private void helpProzorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Help.ShowHelp(this, @"file://" + Application.StartupPath + @"\Build chm documentation\Pomoc.chm");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Greška pri otvaranju", MessageBoxButtons.OK);
            }
        }

        private void pdfFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var odgovor = MessageBox.Show("Da li želite da otovrite pomoć u našem pdf čitaču ili nekom drugom?", "Info", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (odgovor)
            {
                case DialogResult.Cancel:
                    break;

                case DialogResult.No:
                    {
                        try
                        {
                            Process.Start(Application.StartupPath + "\\Build pdf documentation\\Pomoc.pdf");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK);
                        }

                        break;
                    }

                case DialogResult.Yes:
                    {
                        try
                        {
                            this.Cursor = Cursors.WaitCursor;
                            Thread.Sleep(3575);
                            axAcroPDF1.LoadFile(Application.StartupPath + "\\Build pdf documentation\\Pomoc.pdf");
                            this.Cursor = Cursors.Default;
                            lblAdobePdfCitacImeFajla.Text = "Pomoc.pdf";
                            tabMeni.SelectedTab = tabMeniPdfCitac;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK);
                        }
                        finally
                        {
                            this.Cursor = Cursors.Default;
                        }
                        break;
                    }

                default:
                    break;
            }
        }

        private void wordFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + @"\Build word documentation\Pomoc.docx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void htmlFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + @"\\Build html documentation\Dobrodosli.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void oAplikacijiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabMeni.SelectedTab = tabMeniInformacije;
        }

        #endregion

        #region MENI WEB-PRETRAŽIVAČ

        private void webPretrazivacToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            webPretrazivacToolStripMenuItem.ShowDropDown();
        }

        private void otvoriGoogleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("www.google.com");
            radWebPretrazivacGoogle.Checked = true;
        }

        private void otvoriYahooToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("www.yahoo.com");
            radWebPretrazivacYahoo.Checked = true;
        }

        private void otvoriWikipediuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("www.Wikipedia.org");
            radWebPretrazivacWikipedia.Checked = true;
        }

        private void otvoriAmazonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("www.amazon.com");
            radWebPretrazivacAmazon.Checked = true;
        }

        private void otvoriEBayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("www.ebay.com");
            radWebPretrazivacEBay.Checked = true;
        }

        private void otvoriYoutubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("www.youtube.com");
            radWebPretrazivacYouTube.Checked = true;
        }

        private void otvoriFacebookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("www.facebook.com");
        }

        #endregion

        //#### TABOVI

        #region TAB SKENIRANJE

        private void txtSkeniranjePutanja_TextChanged(object sender, EventArgs e)
        {
            lblListaSkeniranjaPutanja.Text = txtSkeniranjePutanja.Text;

            txtMeniOpcijeAdresa.Text = txtSkeniranjePutanja.Text;
        }

        private void btnSkeniranjePutanja_Click(object sender, EventArgs e)
        {
            Funkcije.izaberiAdresuZaSkeniranje(folderBrowserDialog1);
            txtSkeniranjePutanja.Text = Properties.Settings.Default.adresaZaPretrazivanje;
        }

        private void txtSkeniranjeFilter_TextChanged(object sender, EventArgs e)
        {
            fileSystemWatcher1.Filter = "*" + txtSkeniranjeFilter.Text + "*";
        }

        private void btnSkeniranjeStart_Click(object sender, EventArgs e)
        {
            pripremaZaStart_I_StartovanjePracenja();
        }

        private void btnSkeniranjeSnimiListu_Click(object sender, EventArgs e)
        {
            if (txtListaSkeniranjaLista.Text.Length >= 1)
                Funkcije.snimiFajl(txtListaSkeniranjaLista.Text);
        }

        private void chkSkeniranjeTipPracenjaKreiranje_Click(object sender, EventArgs e)
        {
            chkSkeniranjeTipPracenjaKreiranje.Checked = !chkSkeniranjeTipPracenjaKreiranje.Checked;
            if (chkSkeniranjeTipPracenjaKreiranje.Checked)
            {
                chkSkeniranjeTipPracenjaKreiranje.Checked = false;

                meniOpcijePracenjaKreiranje.Image = Properties.Resources.uncheck;
                meniOpcijePracenjaKreiranje.ToolTipText = "Isključeno";
            }
            else
            {
                chkSkeniranjeTipPracenjaKreiranje.Checked = true;

                meniOpcijePracenjaKreiranje.Image = Properties.Resources.check;
                meniOpcijePracenjaKreiranje.ToolTipText = "Uključeno";
            }
        }

        private void chkSkeniranjeTipPracenjaBrisanje_Click(object sender, EventArgs e)
        {
            chkSkeniranjeTipPracenjaBrisanje.Checked = !chkSkeniranjeTipPracenjaBrisanje.Checked;
            if (chkSkeniranjeTipPracenjaBrisanje.Checked)
            {
                chkSkeniranjeTipPracenjaBrisanje.Checked = false;

                meniOpcijePracenjaBrisanje.Image = Properties.Resources.uncheck;
                meniOpcijePracenjaBrisanje.ToolTipText = "Isključeno";
            }
            else
            {
                chkSkeniranjeTipPracenjaBrisanje.Checked = true;

                meniOpcijePracenjaBrisanje.Image = Properties.Resources.check;
                meniOpcijePracenjaBrisanje.ToolTipText = "Uključeno";
            }
        }

        private void chkSkeniranjeTipPracenjaPromjena_Click(object sender, EventArgs e)
        {
            chkSkeniranjeTipPracenjaPromjena.Checked = !chkSkeniranjeTipPracenjaPromjena.Checked;
            if (chkSkeniranjeTipPracenjaPromjena.Checked)
            {
                chkSkeniranjeTipPracenjaPromjena.Checked = false;

                meniOpcijePracenjaPromjena.Image = Properties.Resources.uncheck;
                meniOpcijePracenjaPromjena.ToolTipText = "Isključeno";
            }
            else
            {
                chkSkeniranjeTipPracenjaPromjena.Checked = true;

                //meniOpcijePracenjaPromjena.Image = Properties.Resources.check;
                meniOpcijePracenjaPromjena.ToolTipText = "Uključeno";
            }
        }

        private void chkSkeniranjeTipPracenjaPreimenovanje_Click(object sender, EventArgs e)
        {
            chkSkeniranjeTipPracenjaPreimenovanje.Checked = !chkSkeniranjeTipPracenjaPreimenovanje.Checked;
            if (chkSkeniranjeTipPracenjaPreimenovanje.Checked)
            {
                chkSkeniranjeTipPracenjaPreimenovanje.Checked = false;

                meniOpcijePracenjaPreimenovanje.Image = Properties.Resources.uncheck;
                meniOpcijePracenjaPreimenovanje.ToolTipText = "Isključeno";
            }
            else
            {
                chkSkeniranjeTipPracenjaPreimenovanje.Checked = true;

                meniOpcijePracenjaPreimenovanje.Image = Properties.Resources.check;
                meniOpcijePracenjaPreimenovanje.ToolTipText = "Uključeno";
            }
        }

        #endregion

        #region TAB LISTA SKENIRANJA

        #endregion

        #region TAB OTVORI LISTU

        private void btnOtvoriListuPutanja_Click(object sender, EventArgs e)
        {
            otvoriSkeniranuListu();
        }

        private void btnOtvoriListuPrintanje_Click(object sender, EventArgs e)
        {
            printDocument1.DocumentName = lblOtvoriListuImeListe.Text + " " + DateTime.Now.ToString();
            documentContents = txtOtvoriListuLista.Text;
            stringToPrint = documentContents;

            try
            {
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void txtOtvoriListuLista_DragEnter(object sender, DragEventArgs e)
        {
            Funkcije.dragEnter(e);
        }

        private void txtOtvoriListuLista_DragDrop(object sender, DragEventArgs e)
        {
            string[] ucitanePutanje = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            StreamReader Fajl = new StreamReader(ucitanePutanje[0]);

            txtOtvoriListuLista.Text = Fajl.ReadToEnd();

            lblOtvoriListuImeListe.Text = ucitanePutanje[0].Substring(Funkcije.pointerString(ucitanePutanje[0]) + 2);

            Fajl.Close();
        }

        #endregion

        #region TAB MUZIČKI PLAYER

        string[] imenaPjesama;
        List<String> putanje = new List<string>();
        bool ucitavanje = true;
        int indexTrenutnePjesme = 0;

        private void btnMuzickiPlayerPutanja_Click(object sender, EventArgs e)
        {
            openFileDialog1.Reset();

            openFileDialog1.Title = "Izaberite pjesmu";
            openFileDialog1.InitialDirectory = Environment.SpecialFolder.MyMusic.ToString();
            openFileDialog1.Filter = "mp3 |*.mp3|mp4 |*mp4|wave |*wav|midi |*midi";
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                imenaPjesama = openFileDialog1.SafeFileNames; //uzimamo niz imena stringova sa SafeFileNames a sa SafeFileName samo ime jednog fajla

                for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                {
                    putanje.Add(openFileDialog1.FileNames[i]); // uzimamo niz putanja pjesama
                }

                for (int i = 0; i < imenaPjesama.Length; i++)
                {
                    lstMuzickiPlayerListaPjesama.Items.Add(imenaPjesama[i]);
                }

                if (ucitavanje)
                {
                    WindowsMediaPlayer.URL = openFileDialog1.FileName;
                    indexTrenutnePjesme = 0;
                    ucitavanje = false;
                }

                lstMuzickiPlayerListaPjesama.SelectedIndex = lstMuzickiPlayerListaPjesama.Items.Count - imenaPjesama.Length;

                this.Cursor = Cursors.Default;
            }
        }

        private void btnMuzickiPlayerPobrisiPjesmu_Click(object sender, EventArgs e)
        {
            if (lstMuzickiPlayerListaPjesama.SelectedIndex >= 0)
            {
                for (int i = lstMuzickiPlayerListaPjesama.SelectedIndex; i < putanje.Count - 1; i++)
                {
                    putanje[i] = putanje[i + 1];
                }

                putanje.RemoveAt(putanje.Count - 1);

                int index = lstMuzickiPlayerListaPjesama.SelectedIndex;

                lstMuzickiPlayerListaPjesama.Items.RemoveAt(index);


                if (index == 0 && lstMuzickiPlayerListaPjesama.Items.Count >= 1)
                {
                    lstMuzickiPlayerListaPjesama.SelectedIndex = 0;
                }

                else if (index == (lstMuzickiPlayerListaPjesama.Items.Count))
                {
                    lstMuzickiPlayerListaPjesama.SelectedIndex = lstMuzickiPlayerListaPjesama.Items.Count - 1;
                }

                else
                {
                    lstMuzickiPlayerListaPjesama.SelectedIndex = index;
                }

                WindowsMediaPlayer.Ctlcontrols.stop();
                WindowsMediaPlayer.Refresh();
            }
        }

        private void btnMuzickiPlayerUnazad_Click(object sender, EventArgs e)
        {
            if (lstMuzickiPlayerListaPjesama.SelectedIndex > 0)
                lstMuzickiPlayerListaPjesama.SelectedIndex--;
        }

        private void btnMuzickiPlayerStop_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer.Ctlcontrols.stop();

            if (lstMuzickiPlayerListaPjesama.SelectedIndex > 0)
                lstMuzickiPlayerListaPjesama.SelectedIndex = 0;
        }

        private void btnMuzickiPlayerPlay_Click(object sender, EventArgs e)
        {
            if (pauza)
            {
                WindowsMediaPlayer.Ctlcontrols.play();
                pauza = false;
            }

            else
                if (lstMuzickiPlayerListaPjesama.SelectedIndex >= 0)
                {
                    WindowsMediaPlayer.URL = putanje[lstMuzickiPlayerListaPjesama.SelectedIndex];
                    indexTrenutnePjesme = lstMuzickiPlayerListaPjesama.SelectedIndex;
                }
        }

        bool pauza = false;
        private void btnMuzickiPlayerPauza_Click(object sender, EventArgs e)
        {
            if (pauza == false)
            {
                WindowsMediaPlayer.Ctlcontrols.pause();
                pauza = true;
            }
        }

        private void btnMuzickiPlayerUnaprijed_Click(object sender, EventArgs e)
        {
            if (lstMuzickiPlayerListaPjesama.SelectedIndex < lstMuzickiPlayerListaPjesama.Items.Count - 1)
                lstMuzickiPlayerListaPjesama.SelectedIndex++;
        }

        private void lstMuzickiPlayerListaPjesama_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WindowsMediaPlayer.URL = putanje[lstMuzickiPlayerListaPjesama.SelectedIndex];
                indexTrenutnePjesme = lstMuzickiPlayerListaPjesama.SelectedIndex;
            }
        }

        private void lstMuzickiPlayerListaPjesama_DragEnter(object sender, DragEventArgs e)
        {
            Funkcije.dragEnter(e);
        }

        private void lstMuzickiPlayerListaPjesama_DragDrop(object sender, DragEventArgs e)
        {
            bool uslovPomjeranjaIndexa = false;
            string[] ucitanePutanje = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            for (int i = 0; i < ucitanePutanje.Length; i++)
            {
                if ((Path.GetExtension(ucitanePutanje[i]) == ".mp3") || (Path.GetExtension(ucitanePutanje[i]) == ".mp4") || (Path.GetExtension(ucitanePutanje[i]) == ".wav") || (Path.GetExtension(ucitanePutanje[i]) == ".midi"))
                {
                    putanje.Add(ucitanePutanje[i]); // uzimamo niz putanja pjesama
                    lstMuzickiPlayerListaPjesama.Items.Add(ucitanePutanje[i].Substring(Funkcije.pointerString(ucitanePutanje[i]) + 2));

                    uslovPomjeranjaIndexa = true;
                }
            }

            if (ucitavanje)
            {
                WindowsMediaPlayer.URL = putanje[0];
                indexTrenutnePjesme = 0;
                ucitavanje = false;

                lstMuzickiPlayerListaPjesama.SelectedIndex = 0;
            }

            if (uslovPomjeranjaIndexa)
            {
                lstMuzickiPlayerListaPjesama.SelectedIndex = lstMuzickiPlayerListaPjesama.Items.Count - ucitanePutanje.Length;
            }
        }

        private void WindowsMediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            switch (WindowsMediaPlayer.playState)
            {
                case WMPLib.WMPPlayState.wmppsStopped:
                    {
                        if (indexTrenutnePjesme == 0)
                        {
                            lstMuzickiPlayerListaPjesama.SelectedIndex = ++indexTrenutnePjesme;
                            WindowsMediaPlayer.Refresh();
                            WindowsMediaPlayer.URL = putanje[indexTrenutnePjesme];
                            WindowsMediaPlayer.Ctlcontrols.play();
                        }
                        break;
                    }

                default:
                    break;
            }
        }

        #endregion

        #region TAB ADOBE PDF ČITAČ

        private void btnAdobePdfCitac_Click(object sender, EventArgs e)
        {
            openFileDialog1.Reset();

            openFileDialog1.Title = "Izaberite pdf fajl";
            openFileDialog1.Filter = "Pdf |*.pdf*";
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "Pdf fajl";
            openFileDialog1.ShowHelp = true;

            openFileDialog1.InitialDirectory = Properties.Settings.Default.adresaPdfFajl;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.adresaPdfFajl = openFileDialog1.FileName.Substring(0, Funkcije.pointerString(openFileDialog1.FileName) + 1);
                Properties.Settings.Default.Save();

                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    Thread.Sleep(3575);
                    axAcroPDF1.LoadFile(openFileDialog1.FileName);
                    this.Cursor = Cursors.Default;
                    lblAdobePdfCitacImeFajla.Text = openFileDialog1.SafeFileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK);
                }
            }
        }

        #endregion

        #region WEB PRETRAŽIVAČ

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            txtWebPretrazivacAdresa.Text = webBrowser1.Url.ToString();
        }

        private void btnWebPretrazivac_Click(object sender, EventArgs e)
        {
            /*
             * Pretražujemo samo za tekst koji je dužine veće ili jednakoj 1 karakter.
             */
            if (txtWebPretrazivacTekst.TextLength > 0)
            {
                if (radWebPretrazivacGoogle.Checked)
                {
                    Funkcije.webPretrazivacOtvoriStranicu(webBrowser1, txtWebPretrazivacTekst.Text, 000);
                }
                else if (radWebPretrazivacYahoo.Checked)
                {
                    Funkcije.webPretrazivacOtvoriStranicu(webBrowser1, txtWebPretrazivacTekst.Text, 001);
                }
                else if (radWebPretrazivacWikipedia.Checked)
                {
                    Funkcije.webPretrazivacOtvoriStranicu(webBrowser1, txtWebPretrazivacTekst.Text, 010);
                }
                else if (radWebPretrazivacAmazon.Checked)
                {
                    Funkcije.webPretrazivacOtvoriStranicu(webBrowser1, txtWebPretrazivacTekst.Text, 011);
                }
                else if (radWebPretrazivacEBay.Checked)
                {
                    Funkcije.webPretrazivacOtvoriStranicu(webBrowser1, txtWebPretrazivacTekst.Text, 100);
                }
                else
                {
                    Funkcije.webPretrazivacOtvoriStranicu(webBrowser1, txtWebPretrazivacTekst.Text, 101);
                }
            }
        }

        private void txtWebPretrazivac_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue == 13) && (txtWebPretrazivacTekst.TextLength > 0))
            {
                if ((txtWebPretrazivacTekst.Text[0] == '\r') && (txtWebPretrazivacTekst.Text[1] == '\n'))
                {
                    txtWebPretrazivacTekst.Text = txtWebPretrazivacTekst.Text.Substring(2);
                }

                /*
                 * Ovaj if služi u slučaju da korisnik dva puta za redom pritisne enter.
                 * Na drugom prolazu, će se na prazan tekst dodati \r i \n. Kada ih skinem u gornjem if
                 * dalji dio koda puca. Zato ima smisla pretraživati samo ako je tekst u boxu veći od 0
                 * */
                if (txtWebPretrazivacTekst.TextLength > 0)
                {
                    String pom = txtWebPretrazivacTekst.Text;

                    if (radWebPretrazivacGoogle.Checked)
                    {
                        Funkcije.webPretrazivacOtvoriStranicu(webBrowser1, txtWebPretrazivacTekst.Text, 000);
                    }
                    else if (radWebPretrazivacYahoo.Checked)
                    {
                        Funkcije.webPretrazivacOtvoriStranicu(webBrowser1, txtWebPretrazivacTekst.Text, 001);
                    }
                    else if (radWebPretrazivacWikipedia.Checked)
                    {
                        Funkcije.webPretrazivacOtvoriStranicu(webBrowser1, txtWebPretrazivacTekst.Text, 010);
                    }
                    else if (radWebPretrazivacAmazon.Checked)
                    {
                        Funkcije.webPretrazivacOtvoriStranicu(webBrowser1, txtWebPretrazivacTekst.Text, 011);
                    }
                    else if (radWebPretrazivacEBay.Checked)
                    {
                        Funkcije.webPretrazivacOtvoriStranicu(webBrowser1, txtWebPretrazivacTekst.Text, 100);
                    }
                    else
                    {
                        Funkcije.webPretrazivacOtvoriStranicu(webBrowser1, txtWebPretrazivacTekst.Text, 101);
                    }

                    txtWebPretrazivacTekst.ResetText();
                    txtWebPretrazivacTekst.Text = pom;
                    txtWebPretrazivacTekst.SelectionLength = 0;
                }
            }
        }

        private void btnWebPretrazivacUnazad_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void btnWebPretrazivacReload_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void btnWebPretrazivacUnaprijed_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        #endregion

        //#### DONJI TOOL STRIP

        #region TOOL STRIP

        private void Ime_Click(object sender, EventArgs e)
        {
            Process.Start("http://about.me/ratomir.vukadin/" as string);
        }

        #endregion

        //DOGADJAJI ZA RAD SA MALOM APLIKACIOM

        #region OPCIJE APLIKACIJE

        private void meniOpcijeAplikacijeUnesiAdresu_Click(object sender, EventArgs e)
        {
            Funkcije.izaberiAdresuZaSkeniranje(folderBrowserDialog1);
            txtSkeniranjePutanja.Text = Properties.Settings.Default.adresaZaPretrazivanje;
        }

        private void txtMeniOpcijeAdresa_TextChanged(object sender, EventArgs e)
        {
            lblListaSkeniranjaPutanja.Text = txtSkeniranjePutanja.Text;
            txtSkeniranjePutanja.Text = txtMeniOpcijeAdresa.Text;
        }

        #endregion
 
        #region OPCIJE PRAĆENJA

        private void meniOpcijePracenjaPreimenovanje_Klik(object sender, EventArgs e)
        {

        }

        private void meniOpcijePracenjaPromjena_Klik(object sender, EventArgs e)
        {

        }

        private void meniOpcijePracenjaKreiranje_Klik(object sender, EventArgs e)
        {

        }

        private void meniOpcijePracenjaBrisanje_Klik(object sender, EventArgs e)
        {

        }

        #endregion

        #region POKRENI PRAĆENJE

        private void meniStanjePracenja_Click(object sender, EventArgs e)
        {
            pripremaZaStart_I_StartovanjePracenja();
        }

        #endregion

        //#### FUNKCIJE ZA RAD SA 'fileSystemWatcher'-om

        #region PROMJENA

        int brojProlazaka = 1;
        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            if (chkSkeniranjeTipPracenjaPromjena.Checked)
            {
                if (brojProlazaka == 1)
                {
                    txtListaSkeniranjaLista.Text += DateTime.Now + " " + "Fajl promjenjen > " + txtSkeniranjePutanja.Text + "\r\n";
                    brojProlazaka++;

                    upisAdrese(e.FullPath);
                }

                else if (brojProlazaka == 2)
                {
                    txtListaSkeniranjaLista.Text += DateTime.Now + " " + "Fajl promjenjen > " + e.Name + "\r\n";
                    brojProlazaka = 1;
                }

                int broj = Convert.ToInt16(toolStripLabelBrojPromjena.Text);
                broj++;

                toolStripLabelBrojPromjena.Text = broj.ToString();

                meniBrojacPromjena.ToolTipText = toolStripLabelBrojPromjena.Text;
            }
        }

        #endregion

        //#region KREIRANJE

        //private void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
        //{
        //    if (chkSkeniranjeTipPracenjaKreiranje.Checked)
        //    {
        //        txtListaSkeniranjaLista.Text += DateTime.Now + " " + "Fajl kreiran > " + e.Name + "\r\n";

        //        int broj = Convert.ToInt16(toolStripLabelBrojKreiranja.Text);
        //        broj++;

        //        Upis_Adrese(e.FullPath);

        //        toolStripLabelBrojKreiranja.Text = broj.ToString();

        //        meniBrojacKreiranja.ToolTipText = toolStripLabelBrojKreiranja.Text;
        //    }
        //}

        //#endregion

        #region BRISANJE

        private void fileSystemWatcher1_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            if (chkSkeniranjeTipPracenjaBrisanje.Checked)
            {
                txtListaSkeniranjaLista.Text += DateTime.Now + " " + "Fajl pobrisan > " + e.Name + "\r\n"; ;

                int broj = Convert.ToInt16(toolStripLabelBrojBrisanja.Text);
                broj++;

                //lstAdresa.Add(e.FullPath);

                toolStripLabelBrojBrisanja.Text = broj.ToString();

                meniBrojacBrisanja.ToolTipText = toolStripLabelBrojBrisanja.Text;
            }
        }

        #endregion

        #region PREOMJENA IMENA

        private void fileSystemWatcher1_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            if (chkSkeniranjeTipPracenjaPreimenovanje.Checked)
            {
                txtListaSkeniranjaLista.Text += DateTime.Now + " " + "Fajl preimenovan iz > " + e.OldName + " u " + e.Name + "\r\n";

                int broj = Convert.ToInt16(toolStripLabelBrojPreimenovanja.Text);
                broj++;

                upisAdrese(e.FullPath);

                toolStripLabelBrojPreimenovanja.Text = broj.ToString();

                meniBrojacPreimenovanja.ToolTipText = toolStripLabelBrojPreimenovanja.Text;
            }
        }

        #endregion

        private void upisAdrese(string adresaZaUpis)
        {
            if (!(lstAdresa.Exists(adresa => adresa == adresaZaUpis)))
            {
                lstAdresa.Add(adresaZaUpis);
            }
        }

        //#### TIMER-I

        #region TIMER 1

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (lblStanjePracenja.Text == "ON")
            {
                stoperica.Stop(); 
                timer2.Stop();
                timer1.Stop();

                if (Convert.ToInt16(toolStripLabelBrojKreiranja.Text) > 0 ||
                    Convert.ToInt16(toolStripLabelBrojBrisanja.Text) > 0 ||
                    Convert.ToInt16(toolStripLabelBrojPreimenovanja.Text) > 0 ||
                    Convert.ToInt16(toolStripLabelBrojPromjena.Text) > 0)
                {
                    switch (Properties.Settings.Default.folderPostoji)
                    {
                        #region TRUE
                        case true:
                            {
                                sql.otovriKonekciju();
                                for (int i = 0; i < lstAdresa.Count; i++)
                                {
                                    sql.unesiPodatke(lstAdresa[i].Substring(Funkcije.pointerString(lstAdresa[i].ToString()) + 2), lstAdresa[i].ToString());
                                }
                                sql.zatvoriKonekciju();
                                
                                if (Funkcije.slanjeFajlova_i_BazeNaServer(ftpConnection1, lstAdresa))
                                {
                                    tslPoruka.Text = "Uspješno su poslati fajlovi i baza!";
                                }
                                else
                                {
                                    tslPoruka.Text = "Neuspješnp slanje!";
                                }

                                break;
                            }
                        #endregion

                        #region FALSE
                        case false:
                            {
                                var odgovor = MessageBox.Show("Još niste kreirali folder na serveru, nažalost, program ne može da pošalje Vaše podatke na server.\nDa li želite sada da kreirate folder i pošaljete željene podatke?!", "Inforamcije", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                switch (odgovor)
                                {
                                    #region ODGOVOR NE

                                    case DialogResult.No:
                                        break;

                                    #endregion

                                    #region ODGOVOR DA
                                    case DialogResult.Yes:
                                        {
                                            frmUnosFoldera unosFoldera = new frmUnosFoldera(ftpConnection1);

                                            /*
                                             * Razlog zbog čega ne koristimo poziv forme
                                             *          unosFoldera.Show();
                                             * je taj što trebamo sačekati unos korisnika. Da nam se ne bi forma odmah zatvorila
                                             * koristimo drugi način.
                                             * 
                                             * */
                                            if (unosFoldera.ShowDialog(this) == DialogResult.OK)
                                            {
                                            }

                                            if (Properties.Settings.Default.folderPostoji)
                                            {
                                                //Upis u bazu

                                                sql.otovriKonekciju();
                                                for (int i = 0; i < lstAdresa.Count; i++)
                                                {
                                                    sql.unesiPodatke(lstAdresa[i].Substring(Funkcije.pointerString(lstAdresa[i].ToString()) + 2), lstAdresa[i].ToString());
                                                }
                                                sql.zatvoriKonekciju();

                                                ftpConnection1.ChangeWorkingDirectory(Properties.Settings.Default.nazivFoldera[0] + "\\" + Properties.Settings.Default.nazivFoldera);
                                               
                                                //Slanje podataka na server
                                                if (Funkcije.slanjeFajlova_i_BazeNaServer(ftpConnection1, lstAdresa))
                                                {
                                                    tslPoruka.Text = "Uspješno su poslati fajlovi i baza!";
                                                }
                                                else
                                                {
                                                    tslPoruka.Text = "Neuspješnp slanje!";
                                                }
                                            }
                                            break;
                                        }

                                    #endregion

                                    #region DEFAULT

                                    default:
                                        break;

                                    #endregion
                                }

                                break;
                            }

                        #endregion

                        #region DEFAULT
                        default:
                            break;

                        #endregion
                    }
                }

                iskljuciPracenje();
            }

            //Ovde se vrši uključivanje kada program radi u intervalima.
            #region UKLJUČIVANJE

            else
            {
                lblStanjePracenja.Text = "ON";

                grpSkeniranjeKomandiSetStart.Text = "Status praćenja - ON";

                fileSystemWatcher1.Path = txtSkeniranjePutanja.Text;
                fileSystemWatcher1.EnableRaisingEvents = true;

                //Naredbe za malu aplikaciju
                meniStanjePracenja.Text = "Uključeno";
                meniStanjePracenja.ToolTipText = "Uključeno";
                meniStanjePracenja.Enabled = false;

                timer1.Stop();
                stoperica.Reset();
                timer1.Start();
                stoperica.Start();
            }

            #endregion
        }

        #endregion

        private void iskljuciPracenje()
        {
            lblStanjePracenja.Text = "OFF";

            grpSkeniranjeKomandiSetStart.Text = "Status praćenja - OFF";

            fileSystemWatcher1.EnableRaisingEvents = false;

            if (txtListaSkeniranjaLista.Text.Length > 0)
            {
                txtListaSkeniranjaLista.Text = "";

                toolStripLabelBrojKreiranja.Text = Convert.ToString(0);
                toolStripLabelBrojBrisanja.Text = Convert.ToString(0);
                toolStripLabelBrojPromjena.Text = Convert.ToString(0);
                toolStripLabelBrojPreimenovanja.Text = Convert.ToString(0);

                lstAdresa.Clear();
            }

            //Naredbe za malu aplikaciju
            meniStanjePracenja.Text = "Isključeno";
            meniStanjePracenja.ToolTipText = "Isključeno";
            meniStanjePracenja.Enabled = true;

            //timer1.Stop();
            stoperica.Reset();
            timer1.Start();
            stoperica.Start();
            timer2.Start();
        }       

        #region TIMER 2

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblStatusPracenjaTimer.Text = "" + stoperica.Elapsed.Minutes + ":" + stoperica.Elapsed.Seconds;
            txtMeniVrijeme.Text = "" + stoperica.Elapsed.Minutes + ":" + stoperica.Elapsed.Seconds;
        }

        #endregion

        //#########################################

        /// <summary>
        /// Ova funkcija vrši prvojere čekiranja i adrese prije startovanja praćenja.
        /// Ako su svi uslovi ispunjeni za praćenje funkcija poziva funkciju StartovanjePracenja i 
        /// započinje se skeniranje lokacije koja je izabrana.
        /// </summary>
        public void pripremaZaStart_I_StartovanjePracenja()
        {
            if (chkSkeniranjeTipPracenjaKreiranje.Checked || chkSkeniranjeTipPracenjaBrisanje.Checked || chkSkeniranjeTipPracenjaPromjena.Checked || chkSkeniranjeTipPracenjaPreimenovanje.Checked)
            {
                if (fileSystemWatcher1.Path == "")
                {
                    var izbor = MessageBox.Show("Niste izabrali putanju za pracenje.\nDa li želite da izaberete putanju? ", "Onemogućen start", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    switch (izbor)
                    {
                        case DialogResult.No:
                            break;
                        case DialogResult.Yes:
                            {
                                Funkcije.izaberiAdresuZaSkeniranje(folderBrowserDialog1);
                                txtSkeniranjePutanja.Text = Properties.Settings.Default.adresaZaPretrazivanje;

                                startovanjePracenja();

                                break;
                            }
                        default:
                            break;
                    }
                }

                else
                {
                    if (Directory.Exists(txtSkeniranjePutanja.Text) == false)
                    {
                        var izbor = MessageBox.Show("Niste izabrali tačnu putanju za pracenje.\nDa li želite da izaberete automackim putem putanju? ", "Onemogućen start", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        switch (izbor)
                        {
                            case DialogResult.No:
                                break;

                            case DialogResult.Yes:
                                Funkcije.izaberiAdresuZaSkeniranje(folderBrowserDialog1);
                                txtSkeniranjePutanja.Text = Properties.Settings.Default.adresaZaPretrazivanje;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        startovanjePracenja();

                        return;
                    }
                }
            }

            else
            {
                MessageBox.Show("Niste čekirali ni jednu od opcija, u meniju 'Tipovi praćenja'. ", "Onemogućen start", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Ova funkcija pokreće praćenje izabrane lokacije, startuje štoperice i obavještava korisnika da je praćenje započeto.
        /// </summary>
        public void startovanjePracenja()
        {
            btnSkeniranjeStart.Enabled = false;
            grpSkeniranjeKomandiSetStart.Text = "Status praćenja - ON";

            lblStanjePracenja.Text = "ON";

            /*Ovom naredbom prikazujem tab koji zelim.*/
            tabMeni.SelectedTab = tabMeniListaSkeniranja;

            //Naredbe za malu aplikaciju
            meniStanjePracenja.Text = "Uključeno";
            meniStanjePracenja.ToolTipText = "Uključeno";
            meniStanjePracenja.Enabled = false;
            meniStanjePracenja.Image = Properties.Resources.power_on;


            fileSystemWatcher1.Path = txtSkeniranjePutanja.Text;
            fileSystemWatcher1.EnableRaisingEvents = true;
            timer1.Start();
            stoperica.Start();
            timer2.Start();
        }

        /// <summary>
        /// Ova funkcija otvara fajl dialog, čita izabrani fajl i njegov sadržaj smješta u txtOtvoriListuLista.
        /// </summary>
        public void otvoriSkeniranuListu()
        {
            tabMeni.SelectedTab = tabMeniOtvoriListu;

            openFileDialog1.Reset();

            openFileDialog1.Title = "Izaberi jednu listu";
            openFileDialog1.Filter = "List files |*.txt";
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowHelp = true;

            openFileDialog1.InitialDirectory = Properties.Settings.Default.adresaListe;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lblOtvoriListuImeListe.Text = openFileDialog1.SafeFileName;

                StreamReader Fajl = new StreamReader(openFileDialog1.FileName);

                txtOtvoriListuLista.Text = Fajl.ReadToEnd();

                printDocument1.DocumentName = openFileDialog1.SafeFileName;
                documentContents = txtOtvoriListuLista.Text;
                stringToPrint = documentContents;

                Fajl.Close();

                Properties.Settings.Default.adresaListe = openFileDialog1.FileName.Substring(0, Funkcije.pointerString(openFileDialog1.FileName) + 1);
                Properties.Settings.Default.Save();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            fileSystemWatcher1.EnableRaisingEvents = false;
        }

        private void bwSkidanje_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ftpConnection1.Connect();
                ftpConnection1.ChangeWorkingDirectory(Properties.Settings.Default.nazivFoldera[0] + "\\" + Properties.Settings.Default.nazivFoldera);

                ftpConnection1.DownloadFile(Application.StartupPath, "Baza.s3db");

                sql.otovriKonekciju();
                sql.procitajBazu();
                String imeFajla, lokacijaZaSmjestanje;
                while (sql.Citac.Read())
                {
                    lokacijaZaSmjestanje = sql.Citac["Lokacija"].ToString();
                    lokacijaZaSmjestanje = lokacijaZaSmjestanje.Remove(Funkcije.pointerString(lokacijaZaSmjestanje) + 1);

                    if (!(Directory.Exists(lokacijaZaSmjestanje)))
                    {
                        System.IO.Directory.CreateDirectory(lokacijaZaSmjestanje);
                    }

                    imeFajla = sql.Citac["ImeFajla"].ToString();
                    ftpConnection1.DownloadFile(lokacijaZaSmjestanje, imeFajla);
                }

                sql.zatvoriCitac();
                sql.zatvoriKonekciju();
            }
            catch (Exception)
            {
                tslPoruka.Text = "Greška pri skidanju fajlova!";
                //tslPoruka.ForeColor = Color.FromArgb(255, 0, 0);
            }
            
        }

        private void bwSkidanje_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslPoruka.Text = "Uspješno su preuzeti fajlovi";
        }

        private void bwSlanje_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //for (int i = 0; i < lstAdresa.Count; i++)
                //{
                //    //string adresa = lstAdresa[i];
                //    //string naziv = lstAdresa[i].Substring(pointerString(lstAdresa[i]) + 2);

                //    server.UploadFile(lstAdresa[i], lstAdresa[i].Substring(pointerString(lstAdresa[i]) + 2));
                //}

                foreach (string fajl in lstAdresa)
                {
                    string naziv = fajl;
                    naziv = naziv.Substring(Funkcije.pointerString(naziv) + 2);
                    ftpConnection1.UploadFile(fajl, naziv);
                }
                    
                Funkcije.slanjeFajlaNaServer(ftpConnection1, Application.StartupPath + @"\Baza.s3db");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Greška");
            }
            
        }

        private void bwSlanje_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslPoruka.Text = "Fajlovi su uspješno poslati.";
        }

        private void tsmiSkeniranje_Click(object sender, EventArgs e)
        {
            if (tsmiSkeniranje.CheckState == CheckState.Checked)
            {
                tabMeni.TabPages.Remove(tabMeniSkeniranje);
                tsmiSkeniranje.CheckState = CheckState.Unchecked;
            }
            else
            {
                tsmiSkeniranje.CheckState = CheckState.Checked;

                if (tabMeni.TabPages.Contains(tabMeniListaSkeniranja))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniListaSkeniranja), tabMeniSkeniranje);
                }
                else if (tabMeni.TabPages.Contains(tabMeniOtvoriListu))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniOtvoriListu), tabMeniSkeniranje);
                }
                else if (tabMeni.TabPages.Contains(tabMeniMuzickiPlayer))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniMuzickiPlayer), tabMeniSkeniranje);
                }
                else if (tabMeni.TabPages.Contains(tabMeniPdfCitac))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniPdfCitac), tabMeniSkeniranje);
                }
                else if (tabMeni.TabPages.Contains(tabMeniWebPretrazivac))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniWebPretrazivac), tabMeniSkeniranje);
                }
                else if (tabMeni.TabPages.Contains(tabMeniInformacije))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniInformacije), tabMeniSkeniranje);
                }
                else
                {
                    tabMeni.TabPages.Insert(0, tabMeniSkeniranje);
                }
            }
        }

        private void tsmiListaSkeniranja_Click(object sender, EventArgs e)
        {
            if (tsmiListaSkeniranja.CheckState == CheckState.Checked)
            {
                tabMeni.HideTab(tabMeniListaSkeniranja);
                tsmiListaSkeniranja.CheckState = CheckState.Unchecked;
            }
            else
            {
                tsmiListaSkeniranja.CheckState = CheckState.Checked;

                if (tabMeni.TabPages.Contains(tabMeniOtvoriListu))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniOtvoriListu), tabMeniListaSkeniranja);
                }
                else if (tabMeni.TabPages.Contains(tabMeniMuzickiPlayer))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniMuzickiPlayer), tabMeniListaSkeniranja);
                }
                else if (tabMeni.TabPages.Contains(tabMeniPdfCitac))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniPdfCitac), tabMeniListaSkeniranja);
                }
                else if (tabMeni.TabPages.Contains(tabMeniWebPretrazivac))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniWebPretrazivac), tabMeniListaSkeniranja);
                }
                else if (tabMeni.TabPages.Contains(tabMeniInformacije))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniInformacije), tabMeniListaSkeniranja);
                }
                else
                {
                    tabMeni.TabPages.Insert(0, tabMeniListaSkeniranja);
                }
            }
        }

        private void tsmiOtvoriListu_Click(object sender, EventArgs e)
        {
            if (tsmiOtvoriListu.CheckState == CheckState.Checked)
            {
                tabMeni.HideTab(tabMeniOtvoriListu);
                tsmiOtvoriListu.CheckState = CheckState.Unchecked;
            }
            else
            {
                tsmiOtvoriListu.CheckState = CheckState.Checked;

                if (tabMeni.TabPages.Contains(tabMeniMuzickiPlayer))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniMuzickiPlayer), tabMeniOtvoriListu);
                }
                else if (tabMeni.TabPages.Contains(tabMeniPdfCitac))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniPdfCitac), tabMeniOtvoriListu);
                }
                else if (tabMeni.TabPages.Contains(tabMeniWebPretrazivac))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniWebPretrazivac), tabMeniOtvoriListu);
                }
                else if (tabMeni.TabPages.Contains(tabMeniInformacije))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniInformacije), tabMeniOtvoriListu);
                }
                else
                {
                    tabMeni.TabPages.Insert(0, tabMeniOtvoriListu);
                }
            }
        }

        private void tsmiMuzickiPlayer_Click(object sender, EventArgs e)
        {
            if (tsmiMuzickiPlayer.CheckState == CheckState.Checked)
            {
                tabMeni.HideTab(tabMeniMuzickiPlayer);
                tsmiMuzickiPlayer.CheckState = CheckState.Unchecked;
            }
            else
            {
                tsmiMuzickiPlayer.CheckState = CheckState.Checked;

                if (tabMeni.TabPages.Contains(tabMeniPdfCitac))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniPdfCitac), tabMeniMuzickiPlayer);
                }
                else if (tabMeni.TabPages.Contains(tabMeniWebPretrazivac))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniWebPretrazivac), tabMeniMuzickiPlayer);
                }
                else if (tabMeni.TabPages.Contains(tabMeniInformacije))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniInformacije), tabMeniMuzickiPlayer);
                }
                else
                {
                    tabMeni.TabPages.Insert(0, tabMeniMuzickiPlayer);
                }
            }
        }

        private void tsmiPdfCitac_Click(object sender, EventArgs e)
        {
            if (tsmiPdfCitac.CheckState == CheckState.Checked)
            {
                tabMeni.HideTab(tabMeniPdfCitac);
                tsmiPdfCitac.CheckState = CheckState.Unchecked;
            }
            else
            {
                tsmiPdfCitac.CheckState = CheckState.Checked;

                if (tabMeni.TabPages.Contains(tabMeniWebPretrazivac))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniWebPretrazivac), tabMeniPdfCitac);
                }
                else if (tabMeni.TabPages.Contains(tabMeniInformacije))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniInformacije), tabMeniPdfCitac);
                }
                else
                {
                    tabMeni.TabPages.Insert(0, tabMeniPdfCitac);
                }
            }
        }

        private void tsmiWebPretrazivac_Click(object sender, EventArgs e)
        {
            if (tsmiWebPretrazivac.CheckState == CheckState.Checked)
            {
                tabMeni.HideTab(tabMeniWebPretrazivac);
                tsmiWebPretrazivac.CheckState = CheckState.Unchecked;
            }
            else
            {
                tsmiWebPretrazivac.CheckState = CheckState.Checked;

                if (tabMeni.TabPages.Contains(tabMeniInformacije))
                {
                    tabMeni.TabPages.Insert(tabMeni.TabPages.IndexOf(tabMeniInformacije), tabMeniWebPretrazivac);
                }
                else
                {
                    tabMeni.TabPages.Insert(0, tabMeniWebPretrazivac);
                }
            }
        }

        private void tsmiInforamcije_Click(object sender, EventArgs e)
        {
            if (tsmiInforamcije.CheckState == CheckState.Checked)
            {
                tabMeni.HideTab(tabMeniInformacije);
                tsmiInforamcije.CheckState = CheckState.Unchecked;
            }
            else
            {
                tsmiInforamcije.CheckState = CheckState.Checked;
                tabMeni.TabPages.Insert((tabMeni.TabCount == 0) ? 0 : tabMeni.TabCount, tabMeniInformacije);
            }
        }
        
        private void tabMeni_TabClosing(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabMeniSkeniranje)
            {
                tsmiSkeniranje.CheckState = CheckState.Unchecked;
            }
            else if (e.TabPage == tabMeniListaSkeniranja)
            {
                tsmiListaSkeniranja.CheckState = CheckState.Unchecked;
            }
            else if (e.TabPage == tabMeniOtvoriListu)
            {
                tsmiOtvoriListu.CheckState = CheckState.Unchecked;
            }
            else if (e.TabPage == tabMeniMuzickiPlayer)
            {
                tsmiMuzickiPlayer.CheckState = CheckState.Unchecked;
            }
            else if (e.TabPage == tabMeniPdfCitac)
            {
                tsmiPdfCitac.CheckState = CheckState.Unchecked;
            }
            else if (e.TabPage == tabMeniWebPretrazivac)
            {
                tsmiWebPretrazivac.CheckState = CheckState.Unchecked;
            }
            else
            {
                tsmiInforamcije.CheckState = CheckState.Unchecked;
            }
        }
    }
}
