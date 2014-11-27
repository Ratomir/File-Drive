using System;
using System.Collections.Generic;
using System.Text;

using Finisar.SQLite;
using System.Data;
using System.Windows.Forms;

namespace SQL
{
    class SQLClass
    {
        private String konekcioniString = "Data Source=" + Application.StartupPath + @"\Baza.s3db;Version=3;New=False;Compress=True;";

        /// <summary>
        /// Properties za atribut (string)konekcioniString.
        /// </summary>
        public String KonekcioniString
        {
            get { return konekcioniString; }
        }

        private SQLiteConnection konekcija;

        /// <summary>
        /// Properties za atribut konekcija.
        /// </summary>
        public SQLiteConnection Konekcija
        {
            get { return konekcija; }
            set { konekcija = value; }
        }

        private SQLiteCommand komanda;

        /// <summary>
        /// Properties za atribut komanda.
        /// </summary>
        public SQLiteCommand Komanda
        {
            get { return komanda; }
            set { komanda = value; }
        }

        private SQLiteDataReader citac;

        /// <summary>
        /// Properties za atribut citac.
        /// </summary>
        public SQLiteDataReader Citac
        {
            get { return citac; }
            set { citac = value; }
        }

        private SQLiteDataAdapter adapter;

        /// <summary>
        /// Properties za atribut adapter.
        /// </summary>
        public SQLiteDataAdapter Adapter
        {
            get { return adapter; }
            set { adapter = value; }
        }

        /// <summary>
        /// Funckija postavlja konekcini string za SQLiteConnection.
        /// </summary>
        public void postaviKonekcioniString()
        {
            Konekcija = new SQLiteConnection(KonekcioniString);
        }

        /// <summary>
        /// Funckija učitava podatke iz baze i prosljeđuje podatke u tabelu.
        /// </summary>
        /// <param name="dataGrid"> dataGrid (Tabela) na kojoj se prikazuju podatci. </param>
        /// <param name="bindingSource"> Izvoru podataka se dodjeljuje tabela koja se učitava.</param>
        public void ucitavanjePodataka(DataGridView dataGrid, BindingSource bindingSource)
        {
            DataTable dataTable = new DataTable();
            Adapter = new SQLiteDataAdapter();
            
            Komanda = new SQLiteCommand("SELECT * FROM tabela", Konekcija);
            Adapter.SelectCommand = Komanda;
            Adapter.Fill(dataTable);

            dataGrid.DataSource = dataTable;
            bindingSource.DataSource = dataTable;
        }

        /// <summary>
        /// Funkcija unosi podatke u bazu.
        /// </summary>
        public void unesiPodatke(string imeFajla, string lokacijaFajla)
        {
            using (Komanda = new SQLiteCommand("INSERT INTO Tabela (Id, ImeFajla, Lokacija, DatumVrijeme) VALUES ((SELECT max(Id) FROM Tabela)+1, '" + imeFajla.ToString() + "', '" + lokacijaFajla.ToString() + "', '" + DateTime.Now.ToString() + "')", Konekcija))
            {
                Komanda.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Funkcija briše podataka po Id-ju.
        /// </summary>
        /// <param name="Id"> Vrijednost Id po kojoj se pronađe događaj za brisanje.</param>
        public void pobrisiPodatak(string Id)
        {
            using (Komanda = new SQLiteCommand("DELETE FROM Tabela Where Id = " + Id, Konekcija))
            {
                Komanda.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Funkcija otvara konekciju na bazu.
        /// </summary>
        public void otovriKonekciju()
        {
            postaviKonekcioniString();
            Konekcija.Open();
        }

        /// <summary>
        /// Funkcija zatvara konekciju na bazu.
        /// </summary>
        public void zatvoriKonekciju()
        {
            Konekcija.Close();
            Konekcija.Dispose();
        }

        /// <summary>
        /// Funkcija koja zatvara konekicju na citac, odnosno SQLiteDataReader.
        /// </summary>
        public void zatvoriCitac()
        {
            Citac.Close();
            Citac.Dispose();
        }

        /// <summary>
        /// Funckija otvara bazu i pročita je do kraja.
        /// </summary>
        public void procitajBazu()
        {
            using (Komanda = new SQLiteCommand("SELECT * FROM Tabela", Konekcija))
            {
                Citac = Komanda.ExecuteReader();
            }
        }
    }
}
