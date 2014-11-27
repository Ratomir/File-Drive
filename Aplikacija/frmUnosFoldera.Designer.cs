namespace Aplikacija
{
    /// <summary>
    /// Forma Unos Foldera.
    /// </summary>
    partial class frmUnosFoldera
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnosFoldera));
            this.btnKreirajFolder = new System.Windows.Forms.Button();
            this.txtImeFoldera = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ftpConnection1 = new EnterpriseDT.Net.Ftp.FTPConnection(this.components);
            this.SuspendLayout();
            // 
            // btnKreirajFolder
            // 
            this.btnKreirajFolder.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKreirajFolder.FlatAppearance.BorderSize = 2;
            this.btnKreirajFolder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnKreirajFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKreirajFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnKreirajFolder.Location = new System.Drawing.Point(90, 62);
            this.btnKreirajFolder.Name = "btnKreirajFolder";
            this.btnKreirajFolder.Size = new System.Drawing.Size(106, 29);
            this.btnKreirajFolder.TabIndex = 5;
            this.btnKreirajFolder.Text = "Kreiraj folder";
            this.btnKreirajFolder.UseVisualStyleBackColor = true;
            this.btnKreirajFolder.Click += new System.EventHandler(this.btnKreirajFolder_Click);
            // 
            // txtImeFoldera
            // 
            this.txtImeFoldera.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtImeFoldera.Location = new System.Drawing.Point(90, 12);
            this.txtImeFoldera.Multiline = true;
            this.txtImeFoldera.Name = "txtImeFoldera";
            this.txtImeFoldera.Size = new System.Drawing.Size(192, 33);
            this.txtImeFoldera.TabIndex = 4;
            this.txtImeFoldera.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.txtImeFoldera, "Za prvi karater dozvoljena su samo slova engleskog alfabeta.");
            this.txtImeFoldera.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImeFoldera_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(2, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ime foldera:";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTip1.ToolTipTitle = "Info";
            // 
            // ftpConnection1
            // 
            this.ftpConnection1.ParentControl = this;
            this.ftpConnection1.Password = "ratomir";
            this.ftpConnection1.ServerAddress = "127.0.0.1";
            this.ftpConnection1.TransferNotifyInterval = ((long)(4096));
            this.ftpConnection1.UserName = "Ratomir";
            // 
            // frmUnosFoldera
            // 
            this.AcceptButton = this.btnKreirajFolder;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 96);
            this.Controls.Add(this.btnKreirajFolder);
            this.Controls.Add(this.txtImeFoldera);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUnosFoldera";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unos foldera";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUnosFoldera_FormClosing);
            this.Load += new System.EventHandler(this.frmUnosFoldera_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKreirajFolder;
        private System.Windows.Forms.TextBox txtImeFoldera;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private EnterpriseDT.Net.Ftp.FTPConnection ftpConnection1;
    }
}