namespace Aplikacija
{
    partial class frmPodesavanjeFoldera
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPodesavanjeFoldera));
            this.btnPromjeniIme = new System.Windows.Forms.Button();
            this.btnKreirajFolder = new System.Windows.Forms.Button();
            this.cmbMjeraMemorije = new System.Windows.Forms.ComboBox();
            this.lblMemorija = new System.Windows.Forms.Label();
            this.lnkPutanja = new System.Windows.Forms.LinkLabel();
            this.lnkImeFoldera = new System.Windows.Forms.LinkLabel();
            this.dgvTabela = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.server = new EnterpriseDT.Net.Ftp.FTPConnection(this.components);
            this.tabMeni = new System.Windows.Forms.TabControl();
            this.tabTabelarniPrikaz = new System.Windows.Forms.TabPage();
            this.tabPojedinacniFajlovi = new System.Windows.Forms.TabPage();
            this.lblPojedinacniFajloviDatumVrijeme = new System.Windows.Forms.Label();
            this.lblPojedinacniFajloviLokacija = new System.Windows.Forms.Label();
            this.lblPojedinacniFajloviImeFajla = new System.Windows.Forms.Label();
            this.lblPojedinacniFajloviId = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new System.Data.DataSet();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.downloadToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.btnPobrisiFolder = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabela)).BeginInit();
            this.tabMeni.SuspendLayout();
            this.tabTabelarniPrikaz.SuspendLayout();
            this.tabPojedinacniFajlovi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).BeginInit();
            this.bindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPromjeniIme
            // 
            this.btnPromjeniIme.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPromjeniIme.Enabled = false;
            this.btnPromjeniIme.FlatAppearance.BorderSize = 0;
            this.btnPromjeniIme.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPromjeniIme.Location = new System.Drawing.Point(22, 90);
            this.btnPromjeniIme.Name = "btnPromjeniIme";
            this.btnPromjeniIme.Size = new System.Drawing.Size(75, 30);
            this.btnPromjeniIme.TabIndex = 21;
            this.btnPromjeniIme.Text = "Promjeni ime";
            this.btnPromjeniIme.UseVisualStyleBackColor = false;
            this.btnPromjeniIme.Click += new System.EventHandler(this.btnPromjeniIme_Click);
            // 
            // btnKreirajFolder
            // 
            this.btnKreirajFolder.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnKreirajFolder.Enabled = false;
            this.btnKreirajFolder.FlatAppearance.BorderSize = 0;
            this.btnKreirajFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnKreirajFolder.Location = new System.Drawing.Point(22, 20);
            this.btnKreirajFolder.Name = "btnKreirajFolder";
            this.btnKreirajFolder.Size = new System.Drawing.Size(75, 30);
            this.btnKreirajFolder.TabIndex = 20;
            this.btnKreirajFolder.Text = "Kreiraj folder";
            this.btnKreirajFolder.UseVisualStyleBackColor = false;
            this.btnKreirajFolder.Click += new System.EventHandler(this.btnKreirajFolder_Click);
            // 
            // cmbMjeraMemorije
            // 
            this.cmbMjeraMemorije.FormattingEnabled = true;
            this.cmbMjeraMemorije.Items.AddRange(new object[] {
            "KB",
            "MB"});
            this.cmbMjeraMemorije.Location = new System.Drawing.Point(207, 63);
            this.cmbMjeraMemorije.Name = "cmbMjeraMemorije";
            this.cmbMjeraMemorije.Size = new System.Drawing.Size(52, 21);
            this.cmbMjeraMemorije.TabIndex = 19;
            this.cmbMjeraMemorije.SelectedIndexChanged += new System.EventHandler(this.cmbMjeraMemorije_SelectedIndexChanged);
            // 
            // lblMemorija
            // 
            this.lblMemorija.AutoSize = true;
            this.lblMemorija.Location = new System.Drawing.Point(148, 66);
            this.lblMemorija.Name = "lblMemorija";
            this.lblMemorija.Size = new System.Drawing.Size(13, 13);
            this.lblMemorija.TabIndex = 18;
            this.lblMemorija.Text = "0";
            // 
            // lnkPutanja
            // 
            this.lnkPutanja.AutoSize = true;
            this.lnkPutanja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkPutanja.Location = new System.Drawing.Point(148, 36);
            this.lnkPutanja.Name = "lnkPutanja";
            this.lnkPutanja.Size = new System.Drawing.Size(59, 13);
            this.lnkPutanja.TabIndex = 17;
            this.lnkPutanja.TabStop = true;
            this.lnkPutanja.Text = "Nepoznato";
            this.lnkPutanja.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPutanja_LinkClicked);
            // 
            // lnkImeFoldera
            // 
            this.lnkImeFoldera.AutoSize = true;
            this.lnkImeFoldera.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkImeFoldera.Location = new System.Drawing.Point(148, 7);
            this.lnkImeFoldera.Name = "lnkImeFoldera";
            this.lnkImeFoldera.Size = new System.Drawing.Size(59, 13);
            this.lnkImeFoldera.TabIndex = 16;
            this.lnkImeFoldera.TabStop = true;
            this.lnkImeFoldera.Text = "Nepoznato";
            this.lnkImeFoldera.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkImeFoldera_LinkClicked);
            // 
            // dgvTabela
            // 
            this.dgvTabela.AllowUserToAddRows = false;
            this.dgvTabela.AllowUserToDeleteRows = false;
            this.dgvTabela.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTabela.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvTabela.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTabela.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTabela.Location = new System.Drawing.Point(6, 6);
            this.dgvTabela.Name = "dgvTabela";
            this.dgvTabela.ReadOnly = true;
            this.dgvTabela.Size = new System.Drawing.Size(494, 195);
            this.dgvTabela.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(9, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Fajlovi:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(9, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Zauzetno memorije:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(9, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Putanja:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Ime foldera:";
            // 
            // server
            // 
            this.server.ParentControl = this;
            this.server.Password = "ratomir";
            this.server.ServerAddress = "127.0.0.1";
            this.server.TransferNotifyInterval = ((long)(4096));
            this.server.UserName = "Ratomir";
            // 
            // tabMeni
            // 
            this.tabMeni.Controls.Add(this.tabTabelarniPrikaz);
            this.tabMeni.Controls.Add(this.tabPojedinacniFajlovi);
            this.tabMeni.Location = new System.Drawing.Point(12, 120);
            this.tabMeni.Name = "tabMeni";
            this.tabMeni.SelectedIndex = 0;
            this.tabMeni.Size = new System.Drawing.Size(514, 233);
            this.tabMeni.TabIndex = 22;
            // 
            // tabTabelarniPrikaz
            // 
            this.tabTabelarniPrikaz.Controls.Add(this.dgvTabela);
            this.tabTabelarniPrikaz.Location = new System.Drawing.Point(4, 22);
            this.tabTabelarniPrikaz.Name = "tabTabelarniPrikaz";
            this.tabTabelarniPrikaz.Padding = new System.Windows.Forms.Padding(3);
            this.tabTabelarniPrikaz.Size = new System.Drawing.Size(506, 207);
            this.tabTabelarniPrikaz.TabIndex = 0;
            this.tabTabelarniPrikaz.Text = "Tabelarni prikaz";
            this.tabTabelarniPrikaz.UseVisualStyleBackColor = true;
            // 
            // tabPojedinacniFajlovi
            // 
            this.tabPojedinacniFajlovi.Controls.Add(this.lblPojedinacniFajloviDatumVrijeme);
            this.tabPojedinacniFajlovi.Controls.Add(this.lblPojedinacniFajloviLokacija);
            this.tabPojedinacniFajlovi.Controls.Add(this.lblPojedinacniFajloviImeFajla);
            this.tabPojedinacniFajlovi.Controls.Add(this.lblPojedinacniFajloviId);
            this.tabPojedinacniFajlovi.Controls.Add(this.label8);
            this.tabPojedinacniFajlovi.Controls.Add(this.label7);
            this.tabPojedinacniFajlovi.Controls.Add(this.label6);
            this.tabPojedinacniFajlovi.Controls.Add(this.label5);
            this.tabPojedinacniFajlovi.Controls.Add(this.bindingNavigator);
            this.tabPojedinacniFajlovi.Location = new System.Drawing.Point(4, 22);
            this.tabPojedinacniFajlovi.Name = "tabPojedinacniFajlovi";
            this.tabPojedinacniFajlovi.Padding = new System.Windows.Forms.Padding(3);
            this.tabPojedinacniFajlovi.Size = new System.Drawing.Size(506, 207);
            this.tabPojedinacniFajlovi.TabIndex = 1;
            this.tabPojedinacniFajlovi.Text = "Pojedinačni fajlovi";
            this.tabPojedinacniFajlovi.UseVisualStyleBackColor = true;
            // 
            // lblPojedinacniFajloviDatumVrijeme
            // 
            this.lblPojedinacniFajloviDatumVrijeme.AutoSize = true;
            this.lblPojedinacniFajloviDatumVrijeme.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPojedinacniFajloviDatumVrijeme.Location = new System.Drawing.Point(112, 145);
            this.lblPojedinacniFajloviDatumVrijeme.Name = "lblPojedinacniFajloviDatumVrijeme";
            this.lblPojedinacniFajloviDatumVrijeme.Size = new System.Drawing.Size(53, 16);
            this.lblPojedinacniFajloviDatumVrijeme.TabIndex = 8;
            this.lblPojedinacniFajloviDatumVrijeme.Text = "*********";
            // 
            // lblPojedinacniFajloviLokacija
            // 
            this.lblPojedinacniFajloviLokacija.AutoSize = true;
            this.lblPojedinacniFajloviLokacija.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPojedinacniFajloviLokacija.Location = new System.Drawing.Point(112, 109);
            this.lblPojedinacniFajloviLokacija.Name = "lblPojedinacniFajloviLokacija";
            this.lblPojedinacniFajloviLokacija.Size = new System.Drawing.Size(53, 16);
            this.lblPojedinacniFajloviLokacija.TabIndex = 7;
            this.lblPojedinacniFajloviLokacija.Text = "*********";
            // 
            // lblPojedinacniFajloviImeFajla
            // 
            this.lblPojedinacniFajloviImeFajla.AutoSize = true;
            this.lblPojedinacniFajloviImeFajla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPojedinacniFajloviImeFajla.Location = new System.Drawing.Point(112, 73);
            this.lblPojedinacniFajloviImeFajla.Name = "lblPojedinacniFajloviImeFajla";
            this.lblPojedinacniFajloviImeFajla.Size = new System.Drawing.Size(53, 16);
            this.lblPojedinacniFajloviImeFajla.TabIndex = 6;
            this.lblPojedinacniFajloviImeFajla.Text = "*********";
            // 
            // lblPojedinacniFajloviId
            // 
            this.lblPojedinacniFajloviId.AutoSize = true;
            this.lblPojedinacniFajloviId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPojedinacniFajloviId.Location = new System.Drawing.Point(112, 37);
            this.lblPojedinacniFajloviId.Name = "lblPojedinacniFajloviId";
            this.lblPojedinacniFajloviId.Size = new System.Drawing.Size(53, 16);
            this.lblPojedinacniFajloviId.TabIndex = 5;
            this.lblPojedinacniFajloviId.Text = "*********";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(6, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "Datum i vrijeme:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(6, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Lokacija:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(6, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Ime fajla:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(6, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Id:";
            // 
            // bindingNavigator
            // 
            this.bindingNavigator.AddNewItem = null;
            this.bindingNavigator.BindingSource = this.bindingSource;
            this.bindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator.DeleteItem = null;
            this.bindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.deleteToolStripButton,
            this.downloadToolStripButton,
            this.printToolStripButton,
            this.toolStripSeparator,
            this.helpToolStripButton});
            this.bindingNavigator.Location = new System.Drawing.Point(3, 3);
            this.bindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator.Name = "bindingNavigator";
            this.bindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator.Size = new System.Drawing.Size(500, 25);
            this.bindingNavigator.TabIndex = 0;
            this.bindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = this.dataSet;
            this.bindingSource.Position = 0;
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "NewDataSet";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "1";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteToolStripButton.Enabled = false;
            this.deleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripButton.Image")));
            this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.deleteToolStripButton.Text = "Delete";
            this.deleteToolStripButton.ToolTipText = "Izbriši selektovani fajl iz foldera";
            this.deleteToolStripButton.Click += new System.EventHandler(this.deleteToolStripButton_Click);
            // 
            // downloadToolStripButton
            // 
            this.downloadToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.downloadToolStripButton.Enabled = false;
            this.downloadToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("downloadToolStripButton.Image")));
            this.downloadToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.downloadToolStripButton.Name = "downloadToolStripButton";
            this.downloadToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.downloadToolStripButton.Text = "&Save";
            this.downloadToolStripButton.ToolTipText = "Skini izabrani fajl";
            this.downloadToolStripButton.Click += new System.EventHandler(this.downloadToolStripButton_Click);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Enabled = false;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printToolStripButton.Text = "&Print";
            this.printToolStripButton.ToolTipText = "Odštampaj podatke za izabrani fajl";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
            this.helpToolStripButton.ToolTipText = "Pomoć";
            // 
            // btnPobrisiFolder
            // 
            this.btnPobrisiFolder.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPobrisiFolder.Enabled = false;
            this.btnPobrisiFolder.FlatAppearance.BorderSize = 0;
            this.btnPobrisiFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPobrisiFolder.Location = new System.Drawing.Point(22, 54);
            this.btnPobrisiFolder.Name = "btnPobrisiFolder";
            this.btnPobrisiFolder.Size = new System.Drawing.Size(75, 30);
            this.btnPobrisiFolder.TabIndex = 23;
            this.btnPobrisiFolder.Text = "Pobriši folder";
            this.btnPobrisiFolder.UseVisualStyleBackColor = false;
            this.btnPobrisiFolder.Click += new System.EventHandler(this.btnPobrisiFolder_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPobrisiFolder);
            this.groupBox1.Controls.Add(this.btnKreirajFolder);
            this.groupBox1.Controls.Add(this.btnPromjeniIme);
            this.groupBox1.Location = new System.Drawing.Point(340, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(119, 127);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upravljanje folderom";
            // 
            // frmPodesavanjeFoldera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 355);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabMeni);
            this.Controls.Add(this.cmbMjeraMemorije);
            this.Controls.Add(this.lblMemorija);
            this.Controls.Add(this.lnkPutanja);
            this.Controls.Add(this.lnkImeFoldera);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPodesavanjeFoldera";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Podesavanje foldera";
            this.Load += new System.EventHandler(this.frmPodesavanjeFoldera_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabela)).EndInit();
            this.tabMeni.ResumeLayout(false);
            this.tabTabelarniPrikaz.ResumeLayout(false);
            this.tabPojedinacniFajlovi.ResumeLayout(false);
            this.tabPojedinacniFajlovi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).EndInit();
            this.bindingNavigator.ResumeLayout(false);
            this.bindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPromjeniIme;
        private System.Windows.Forms.Button btnKreirajFolder;
        private System.Windows.Forms.ComboBox cmbMjeraMemorije;
        private System.Windows.Forms.Label lblMemorija;
        private System.Windows.Forms.LinkLabel lnkPutanja;
        private System.Windows.Forms.LinkLabel lnkImeFoldera;
        private System.Windows.Forms.DataGridView dgvTabela;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private EnterpriseDT.Net.Ftp.FTPConnection server;
        private System.Windows.Forms.TabControl tabMeni;
        private System.Windows.Forms.TabPage tabTabelarniPrikaz;
        private System.Windows.Forms.TabPage tabPojedinacniFajlovi;
        private System.Windows.Forms.Label lblPojedinacniFajloviDatumVrijeme;
        private System.Windows.Forms.Label lblPojedinacniFajloviLokacija;
        private System.Windows.Forms.Label lblPojedinacniFajloviImeFajla;
        private System.Windows.Forms.Label lblPojedinacniFajloviId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.BindingNavigator bindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton downloadToolStripButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Data.DataSet dataSet;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPobrisiFolder;
    }
}