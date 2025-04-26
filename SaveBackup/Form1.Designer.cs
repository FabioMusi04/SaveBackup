namespace SaveBackup
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            BtnCaricaSalvataggio = new Button();
            BtnSalvaSalvataggio = new Button();
            linkLblSalvataggioEsterno = new LinkLabel();
            linkLblCaricamentoEsterno = new LinkLabel();
            CmbSalvataggi = new ComboBox();
            LblCmb = new Label();
            toolTip = new ToolTip(components);
            SuspendLayout();
            // 
            // BtnCaricaSalvataggio
            // 
            BtnCaricaSalvataggio.Location = new Point(240, 100);
            BtnCaricaSalvataggio.Name = "BtnCaricaSalvataggio";
            BtnCaricaSalvataggio.Size = new Size(200, 40);
            BtnCaricaSalvataggio.TabIndex = 3;
            BtnCaricaSalvataggio.Text = "📂 Ripristina Salvataggio";
            BtnCaricaSalvataggio.UseVisualStyleBackColor = true;
            BtnCaricaSalvataggio.Click += BtnCaricaSalvataggio_Click;
            // 
            // BtnSalvaSalvataggio
            // 
            BtnSalvaSalvataggio.Location = new Point(30, 100);
            BtnSalvaSalvataggio.Name = "BtnSalvaSalvataggio";
            BtnSalvaSalvataggio.Size = new Size(200, 40);
            BtnSalvaSalvataggio.TabIndex = 2;
            BtnSalvaSalvataggio.Text = "💾 Salva Backup";
            BtnSalvaSalvataggio.UseVisualStyleBackColor = true;
            BtnSalvaSalvataggio.Click += BtnSalvaSalvataggio_Click;
            // 
            // linkLblSalvataggioEsterno
            // 
            linkLblSalvataggioEsterno.AutoSize = true;
            linkLblSalvataggioEsterno.Location = new Point(30, 160);
            linkLblSalvataggioEsterno.Name = "linkLblSalvataggioEsterno";
            linkLblSalvataggioEsterno.Size = new Size(12, 15);
            linkLblSalvataggioEsterno.TabIndex = 4;
            linkLblSalvataggioEsterno.TabStop = true;
            linkLblSalvataggioEsterno.Text = "-";
            linkLblSalvataggioEsterno.LinkClicked += LinkLblSalvataggioEsterno_LinkClicked;
            // 
            // linkLblCaricamentoEsterno
            // 
            linkLblCaricamentoEsterno.AutoSize = true;
            linkLblCaricamentoEsterno.Location = new Point(30, 185);
            linkLblCaricamentoEsterno.Name = "linkLblCaricamentoEsterno";
            linkLblCaricamentoEsterno.Size = new Size(12, 15);
            linkLblCaricamentoEsterno.TabIndex = 5;
            linkLblCaricamentoEsterno.TabStop = true;
            linkLblCaricamentoEsterno.Text = "-";
            linkLblCaricamentoEsterno.LinkClicked += LinkLblCaricamentoEsterno_LinkClicked;
            // 
            // CmbSalvataggi
            // 
            CmbSalvataggi.DropDownStyle = ComboBoxStyle.DropDownList;
            CmbSalvataggi.FormattingEnabled = true;
            CmbSalvataggi.Location = new Point(30, 55);
            CmbSalvataggi.Name = "CmbSalvataggi";
            CmbSalvataggi.Size = new Size(300, 23);
            CmbSalvataggi.TabIndex = 1;
            CmbSalvataggi.SelectedIndexChanged += CmbSalvataggi_SelectedIndexChanged;
            // 
            // LblCmb
            // 
            LblCmb.AutoSize = true;
            LblCmb.Location = new Point(30, 30);
            LblCmb.Name = "LblCmb";
            LblCmb.Size = new Size(191, 15);
            LblCmb.TabIndex = 0;
            LblCmb.Text = "Seleziona il salvataggio da caricare:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(500, 250);
            Controls.Add(LblCmb);
            Controls.Add(CmbSalvataggi);
            Controls.Add(BtnSalvaSalvataggio);
            Controls.Add(BtnCaricaSalvataggio);
            Controls.Add(linkLblSalvataggioEsterno);
            Controls.Add(linkLblCaricamentoEsterno);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Backup Salvataggi";
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private Button BtnCaricaSalvataggio;
        private Button BtnSalvaSalvataggio;
        private LinkLabel linkLblSalvataggioEsterno;
        private LinkLabel linkLblCaricamentoEsterno;
        private ComboBox CmbSalvataggi;
        private Label LblCmb;
        private ToolTip toolTip;
    }
}
