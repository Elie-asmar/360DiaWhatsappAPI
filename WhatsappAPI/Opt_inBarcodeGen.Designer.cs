namespace WhatsappAPI
{
    partial class Opt_inBarcodeGen
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtMsg = new System.Windows.Forms.RichTextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtnbr = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Whatsapp Text";
            // 
            // txtMsg
            // 
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMsg.Location = new System.Drawing.Point(123, 6);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtMsg.Size = new System.Drawing.Size(505, 96);
            this.txtMsg.TabIndex = 1;
            this.txtMsg.Text = "أوافق على إرسال نتائج الفحوصات الطبية والمخبرية والأشعة من خلال تطبيق وتساب ";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(510, 134);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(118, 54);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generate Barcode";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(123, 224);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(168, 150);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Whatsapp Number";
            // 
            // txtnbr
            // 
            this.txtnbr.Location = new System.Drawing.Point(123, 108);
            this.txtnbr.Name = "txtnbr";
            this.txtnbr.Size = new System.Drawing.Size(505, 20);
            this.txtnbr.TabIndex = 5;
            this.txtnbr.Text = "96181315142";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(123, 380);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(168, 54);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save To Disk";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Opt_inBarcodeGen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 539);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtnbr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.label1);
            this.Name = "Opt_inBarcodeGen";
            this.Text = "Opt-in Barcode Generator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Opt_inBarcodeGen_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtMsg;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtnbr;
        private System.Windows.Forms.Button btnSave;
    }
}