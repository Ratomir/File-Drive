using EnterpriseDT.Net.Ftp;
using SQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Aplikacija
{
    class Funkcije
    {
        /// <summary>
        /// Ova funkcija vraća index kada počinje ime nekog fajla u adresi temp.
        /// </summary>
        /// <param name="temp">Adresa koji se testira.</param>
        /// <returns>pointer na početak imena fajla</returns>
        public static int pointerString(string temp)
        {
            char ch;
            int pointer;

            ch = temp[temp.Length - 1];
            pointer = temp.Length - 1;

            while (ch != '\\')
            {
                ch = temp[pointer--];
            }

            return pointer;
        }

        /// <summary>
        /// Ova funkcija pretražuje čekirani pretraživač sa željeni tekstom.
        /// </summary>
        /// <param name="pretrazivac"> Referenca na WebBrowser.</param>
        /// <param name="stringZaPretrazivanje"> String po kojem će se pretražiti čekirani pretraživač.</param>
        /// <param name="A">3 bitni broj, kodovan od 000 do 101</param>
        public static void webPretrazivacOtvoriStranicu(WebBrowser pretrazivac, String stringZaPretrazivanje, byte A)
        {
            if (A == 000)
            {
                pretrazivac.Navigate("https://www.google.ba/#q=" + stringZaPretrazivanje);
            }
            else if (A == 001)
            {
                pretrazivac.Navigate("http://search.yahoo.com/search;_ylt=A0oG7huR2ZNSaEUAXURXNyoA?ei=UTF-8&fr=yfp-t-900&p=" + stringZaPretrazivanje);
            }
            else if (A == 010)
            {
                pretrazivac.Navigate("http://en.wikipedia.org/wiki/" + stringZaPretrazivanje);
            }
            else if (A == 011)
            {
                pretrazivac.Navigate("http://www.amazon.com/s/ref=nb_sb_noss_1?url=search-alias%3Daps&field-keywords=" + stringZaPretrazivanje);
            }
            else if (A == 100)
            {
                pretrazivac.Navigate("http://www.ebay.com/sch/i.html?_odkw=" + stringZaPretrazivanje + "&_osacat=0&_from=R40&_trksid=p2045573.m570.l1313.TR4.TRC1.A0&_nkw=" + stringZaPretrazivanje + "&_sacat=0");
            }
            else
            {
                pretrazivac.Navigate("http://www.youtube.com/results?search_query=" + stringZaPretrazivanje);
            }

        }

        /// <summary>
        /// Ova funcija šalje podatke na ftp server.
        /// </summary>
        /// <param name="server">Referenca na objekat FTPConnection.</param>
        /// <param name="lstAdresa">Lista adresa fajlova koji treba da se pošalju.</param>
        public static bool slanjeFajlovaNaServer(FTPConnection server, List<String> lstAdresa)
        {
            try
            {
                for (int i = 0; i < lstAdresa.Count; i++)
                {
                    server.UploadFile(lstAdresa[i], lstAdresa[i].Substring(pointerString(lstAdresa[i]) + 2));
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Greška");
                return false;
            }
        }

        /// <summary>
        /// Ova funkcija šalje jedan fajl na ftp server.
        /// </summary>
        /// <param name="server">Referenca na objekat FTPConnection.</param>
        /// <param name="putanjaFajla">Localna putanja do fajla.</param>
        public static bool slanjeFajlaNaServer(FTPConnection server, string putanjaFajla)
        {
            try
            {
                server.UploadFile(putanjaFajla, putanjaFajla.Substring(pointerString(putanjaFajla) + 2));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Greška");
                return false;
            }
        }

        /// <summary>
        /// Ova funcija šalje podatke i bazu na ftp server.
        /// </summary>
        /// <param name="server">Referenca na objekat FTPConnection.</param>
        /// <param name="lstAdresa">Lista adresa fajlova koji treba da se pošalju.</param>
        public static bool slanjeFajlova_i_BazeNaServer(FTPConnection server, List<String> lstAdresa)
        {
            try
            {
                if (slanjeFajlovaNaServer(server, lstAdresa))
                {
                    if (slanjeFajlaNaServer(server, Application.StartupPath + @"\Baza.s3db"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Greška");
                return false;
            }
        }

        /// <summary>
        /// Ova funkcija otvara folder pretraživać i izabranu adresu skenira.
        /// </summary>
        /// <param name="folderPretrazivac"> Folder pretraživač preko kojeg ćemo izabrati željenu adresu. </param>
        public static void izaberiAdresuZaSkeniranje(FolderBrowserDialog folderPretrazivac)
        {
            if (folderPretrazivac.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.adresaZaPretrazivanje = folderPretrazivac.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Ova funkcija otvara save file dialog i upisuje tekst u fajl sa izabranom ekstenziom.
        /// </summary>
        /// <param name="tekstZaUpis"> Tekst za upis u fajl.</param>
        public static void snimiFajl(string tekstZaUpis)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.ShowHelp = true;
            saveFileDialog.Title = "Snimi listu";
            saveFileDialog.Filter = "txt fajl (*.txt)|*.txt|Word dokument 2003 (*.doc)|*.doc|Word dokumnet 2010 (*.docx)|*.docx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter protokZaUpis = new StreamWriter(saveFileDialog.FileName);

                protokZaUpis.WriteLine(tekstZaUpis);
                protokZaUpis.Close();
            }
        }

        /// <summary>
        /// Ova funkcija prihvata podatke samo iz Explorer-a.
        /// </summary>
        /// <param name="e">DragEventArgs - parametar</param>
        public static void dragEnter(DragEventArgs e)
        {
            // Check if the Dataformat of the data can be accepted
            // (we only accept file drops from Explorer, etc.)
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it

            //e.Effect = DragDropEffects.All;
        }

        public static bool provjeriKonekciju()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
