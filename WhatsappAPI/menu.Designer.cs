namespace WhatsappAPI
{
    partial class menu
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.whatsappAPIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSendData = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whatsappAPIToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(957, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // whatsappAPIToolStripMenuItem
            // 
            this.whatsappAPIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptin,
            this.mnuSendData});
            this.whatsappAPIToolStripMenuItem.Name = "whatsappAPIToolStripMenuItem";
            this.whatsappAPIToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.whatsappAPIToolStripMenuItem.Text = "Whatsapp API";
            // 
            // mnuOptin
            // 
            this.mnuOptin.Name = "mnuOptin";
            this.mnuOptin.Size = new System.Drawing.Size(209, 22);
            this.mnuOptin.Text = "Generate Opt-in QR Code";
            this.mnuOptin.Click += new System.EventHandler(this.mnuOptin_Click);
            // 
            // mnuSendData
            // 
            this.mnuSendData.Name = "mnuSendData";
            this.mnuSendData.Size = new System.Drawing.Size(209, 22);
            this.mnuSendData.Text = "Send Data";
            this.mnuSendData.Click += new System.EventHandler(this.mnuSendData_Click);
            // 
            // menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 512);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "menu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem whatsappAPIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOptin;
        private System.Windows.Forms.ToolStripMenuItem mnuSendData;
    }
}



