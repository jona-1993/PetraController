namespace Petra_Controller
{
    partial class PetraC
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.IPTB = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.PortTF = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.CapteursLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GrappinCB = new System.Windows.Forms.CheckBox();
            this.TapisBrasCB = new System.Windows.Forms.CheckBox();
            this.VentouseCB = new System.Windows.Forms.CheckBox();
            this.VentouseBrasCB = new System.Windows.Forms.CheckBox();
            this.Convoyeur2CB = new System.Windows.Forms.CheckBox();
            this.Convoyeur1CB = new System.Windows.Forms.CheckBox();
            this.L1CB = new System.Windows.Forms.CheckBox();
            this.L2CB = new System.Windows.Forms.CheckBox();
            this.TCB = new System.Windows.Forms.CheckBox();
            this.SCB = new System.Windows.Forms.CheckBox();
            this.CSCB = new System.Windows.Forms.CheckBox();
            this.APCB = new System.Windows.Forms.CheckBox();
            this.PPCB = new System.Windows.Forms.CheckBox();
            this.DECB = new System.Windows.Forms.CheckBox();
            this.RailTrackBar = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.ControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RailTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // IPTB
            // 
            this.IPTB.Location = new System.Drawing.Point(177, 188);
            this.IPTB.Name = "IPTB";
            this.IPTB.Size = new System.Drawing.Size(136, 20);
            this.IPTB.TabIndex = 0;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(203, 273);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Connexion";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // PortTF
            // 
            this.PortTF.Location = new System.Drawing.Point(177, 229);
            this.PortTF.Name = "PortTF";
            this.PortTF.Size = new System.Drawing.Size(100, 20);
            this.PortTF.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(138, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.OrangeRed;
            this.label2.Location = new System.Drawing.Point(138, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.OrangeRed;
            this.label3.Location = new System.Drawing.Point(120, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 27);
            this.label3.TabIndex = 5;
            this.label3.Text = "CONNEXION AU PETRA";
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(202, 499);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 6;
            this.CloseButton.Text = "Fermer";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ControlPanel
            // 
            this.ControlPanel.BackColor = System.Drawing.Color.Silver;
            this.ControlPanel.Controls.Add(this.label5);
            this.ControlPanel.Controls.Add(this.RailTrackBar);
            this.ControlPanel.Controls.Add(this.DECB);
            this.ControlPanel.Controls.Add(this.PPCB);
            this.ControlPanel.Controls.Add(this.APCB);
            this.ControlPanel.Controls.Add(this.CSCB);
            this.ControlPanel.Controls.Add(this.SCB);
            this.ControlPanel.Controls.Add(this.TCB);
            this.ControlPanel.Controls.Add(this.L2CB);
            this.ControlPanel.Controls.Add(this.L1CB);
            this.ControlPanel.Controls.Add(this.CapteursLabel);
            this.ControlPanel.Controls.Add(this.label4);
            this.ControlPanel.Controls.Add(this.GrappinCB);
            this.ControlPanel.Controls.Add(this.TapisBrasCB);
            this.ControlPanel.Controls.Add(this.VentouseCB);
            this.ControlPanel.Controls.Add(this.VentouseBrasCB);
            this.ControlPanel.Controls.Add(this.Convoyeur2CB);
            this.ControlPanel.Controls.Add(this.Convoyeur1CB);
            this.ControlPanel.Location = new System.Drawing.Point(78, 67);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(325, 381);
            this.ControlPanel.TabIndex = 7;
            this.ControlPanel.Visible = false;
            // 
            // CapteursLabel
            // 
            this.CapteursLabel.AutoSize = true;
            this.CapteursLabel.Location = new System.Drawing.Point(193, 29);
            this.CapteursLabel.Name = "CapteursLabel";
            this.CapteursLabel.Size = new System.Drawing.Size(49, 13);
            this.CapteursLabel.TabIndex = 15;
            this.CapteursLabel.Text = "Capteurs";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Actuateurs";
            // 
            // GrappinCB
            // 
            this.GrappinCB.AutoSize = true;
            this.GrappinCB.Location = new System.Drawing.Point(47, 263);
            this.GrappinCB.Name = "GrappinCB";
            this.GrappinCB.Size = new System.Drawing.Size(63, 17);
            this.GrappinCB.TabIndex = 5;
            this.GrappinCB.Text = "Grappin";
            this.GrappinCB.UseVisualStyleBackColor = true;
            this.GrappinCB.CheckedChanged += new System.EventHandler(this.GrappinCB_CheckedChanged);
            // 
            // TapisBrasCB
            // 
            this.TapisBrasCB.AutoSize = true;
            this.TapisBrasCB.Location = new System.Drawing.Point(47, 222);
            this.TapisBrasCB.Name = "TapisBrasCB";
            this.TapisBrasCB.Size = new System.Drawing.Size(82, 17);
            this.TapisBrasCB.TabIndex = 4;
            this.TapisBrasCB.Text = "Bras (Tapis)";
            this.TapisBrasCB.UseVisualStyleBackColor = true;
            this.TapisBrasCB.CheckedChanged += new System.EventHandler(this.TapisBrasCB_CheckedChanged);
            // 
            // VentouseCB
            // 
            this.VentouseCB.AutoSize = true;
            this.VentouseCB.Location = new System.Drawing.Point(47, 183);
            this.VentouseCB.Name = "VentouseCB";
            this.VentouseCB.Size = new System.Drawing.Size(71, 17);
            this.VentouseCB.TabIndex = 3;
            this.VentouseCB.Text = "Ventouse";
            this.VentouseCB.UseVisualStyleBackColor = true;
            this.VentouseCB.CheckedChanged += new System.EventHandler(this.VentouseCB_CheckedChanged);
            // 
            // VentouseBrasCB
            // 
            this.VentouseBrasCB.AutoSize = true;
            this.VentouseBrasCB.Location = new System.Drawing.Point(47, 141);
            this.VentouseBrasCB.Name = "VentouseBrasCB";
            this.VentouseBrasCB.Size = new System.Drawing.Size(101, 17);
            this.VentouseBrasCB.TabIndex = 2;
            this.VentouseBrasCB.Text = "Ventouse (Bras)";
            this.VentouseBrasCB.UseVisualStyleBackColor = true;
            this.VentouseBrasCB.CheckedChanged += new System.EventHandler(this.VentouseBrasCB_CheckedChanged);
            // 
            // Convoyeur2CB
            // 
            this.Convoyeur2CB.AutoSize = true;
            this.Convoyeur2CB.Location = new System.Drawing.Point(47, 102);
            this.Convoyeur2CB.Name = "Convoyeur2CB";
            this.Convoyeur2CB.Size = new System.Drawing.Size(86, 17);
            this.Convoyeur2CB.TabIndex = 1;
            this.Convoyeur2CB.Text = "Convoyeur 2";
            this.Convoyeur2CB.UseVisualStyleBackColor = true;
            this.Convoyeur2CB.CheckedChanged += new System.EventHandler(this.Convoyeur2CB_CheckedChanged);
            // 
            // Convoyeur1CB
            // 
            this.Convoyeur1CB.AutoSize = true;
            this.Convoyeur1CB.Location = new System.Drawing.Point(47, 65);
            this.Convoyeur1CB.Name = "Convoyeur1CB";
            this.Convoyeur1CB.Size = new System.Drawing.Size(86, 17);
            this.Convoyeur1CB.TabIndex = 0;
            this.Convoyeur1CB.Text = "Convoyeur 1";
            this.Convoyeur1CB.UseVisualStyleBackColor = true;
            this.Convoyeur1CB.CheckedChanged += new System.EventHandler(this.Convoyeur1CB_CheckedChanged);
            // 
            // L1CB
            // 
            this.L1CB.AutoSize = true;
            this.L1CB.Enabled = false;
            this.L1CB.Location = new System.Drawing.Point(196, 65);
            this.L1CB.Name = "L1CB";
            this.L1CB.Size = new System.Drawing.Size(38, 17);
            this.L1CB.TabIndex = 16;
            this.L1CB.Text = "L1";
            this.L1CB.UseVisualStyleBackColor = true;
            // 
            // L2CB
            // 
            this.L2CB.AutoSize = true;
            this.L2CB.Enabled = false;
            this.L2CB.Location = new System.Drawing.Point(196, 88);
            this.L2CB.Name = "L2CB";
            this.L2CB.Size = new System.Drawing.Size(38, 17);
            this.L2CB.TabIndex = 17;
            this.L2CB.Text = "L2";
            this.L2CB.UseVisualStyleBackColor = true;
            // 
            // TCB
            // 
            this.TCB.AutoSize = true;
            this.TCB.Enabled = false;
            this.TCB.Location = new System.Drawing.Point(196, 111);
            this.TCB.Name = "TCB";
            this.TCB.Size = new System.Drawing.Size(33, 17);
            this.TCB.TabIndex = 18;
            this.TCB.Text = "T";
            this.TCB.UseVisualStyleBackColor = true;
            // 
            // SCB
            // 
            this.SCB.AutoSize = true;
            this.SCB.Enabled = false;
            this.SCB.Location = new System.Drawing.Point(196, 134);
            this.SCB.Name = "SCB";
            this.SCB.Size = new System.Drawing.Size(33, 17);
            this.SCB.TabIndex = 19;
            this.SCB.Text = "S";
            this.SCB.UseVisualStyleBackColor = true;
            // 
            // CSCB
            // 
            this.CSCB.AutoSize = true;
            this.CSCB.Enabled = false;
            this.CSCB.Location = new System.Drawing.Point(196, 157);
            this.CSCB.Name = "CSCB";
            this.CSCB.Size = new System.Drawing.Size(40, 17);
            this.CSCB.TabIndex = 20;
            this.CSCB.Text = "CS";
            this.CSCB.UseVisualStyleBackColor = true;
            // 
            // APCB
            // 
            this.APCB.AutoSize = true;
            this.APCB.Enabled = false;
            this.APCB.Location = new System.Drawing.Point(196, 180);
            this.APCB.Name = "APCB";
            this.APCB.Size = new System.Drawing.Size(40, 17);
            this.APCB.TabIndex = 21;
            this.APCB.Text = "AP";
            this.APCB.UseVisualStyleBackColor = true;
            // 
            // PPCB
            // 
            this.PPCB.AutoSize = true;
            this.PPCB.Enabled = false;
            this.PPCB.Location = new System.Drawing.Point(196, 203);
            this.PPCB.Name = "PPCB";
            this.PPCB.Size = new System.Drawing.Size(40, 17);
            this.PPCB.TabIndex = 22;
            this.PPCB.Text = "PP";
            this.PPCB.UseVisualStyleBackColor = true;
            // 
            // DECB
            // 
            this.DECB.AutoSize = true;
            this.DECB.Enabled = false;
            this.DECB.Location = new System.Drawing.Point(196, 227);
            this.DECB.Name = "DECB";
            this.DECB.Size = new System.Drawing.Size(41, 17);
            this.DECB.TabIndex = 23;
            this.DECB.Text = "DE";
            this.DECB.UseVisualStyleBackColor = true;
            // 
            // RailTrackBar
            // 
            this.RailTrackBar.Location = new System.Drawing.Point(44, 320);
            this.RailTrackBar.Maximum = 3;
            this.RailTrackBar.Name = "RailTrackBar";
            this.RailTrackBar.Size = new System.Drawing.Size(104, 45);
            this.RailTrackBar.TabIndex = 24;
            this.RailTrackBar.Scroll += new System.EventHandler(this.RailTrackBar_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 301);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Transport de la pièce:";
            // 
            // PetraC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Petra_Controller.Properties.Resources.petraBG;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(478, 570);
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PortTF);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.IPTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PetraC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Petra Controller";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PetraC_FormClosing);
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RailTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IPTB;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox PortTF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.CheckBox GrappinCB;
        private System.Windows.Forms.CheckBox TapisBrasCB;
        private System.Windows.Forms.CheckBox VentouseCB;
        private System.Windows.Forms.CheckBox VentouseBrasCB;
        private System.Windows.Forms.CheckBox Convoyeur2CB;
        private System.Windows.Forms.CheckBox Convoyeur1CB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label CapteursLabel;
        private System.Windows.Forms.CheckBox DECB;
        private System.Windows.Forms.CheckBox PPCB;
        private System.Windows.Forms.CheckBox APCB;
        private System.Windows.Forms.CheckBox CSCB;
        private System.Windows.Forms.CheckBox SCB;
        private System.Windows.Forms.CheckBox TCB;
        private System.Windows.Forms.CheckBox L2CB;
        private System.Windows.Forms.CheckBox L1CB;
        private System.Windows.Forms.TrackBar RailTrackBar;
        private System.Windows.Forms.Label label5;
    }
}

