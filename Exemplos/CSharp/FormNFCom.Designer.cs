namespace TreinamentoDLL
{
    partial class FormNFCom
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
            this.BtnConsultaStatusNFCom = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnConsultaStatusNFCom
            // 
            this.BtnConsultaStatusNFCom.Location = new System.Drawing.Point(57, 19);
            this.BtnConsultaStatusNFCom.Name = "BtnConsultaStatusNFCom";
            this.BtnConsultaStatusNFCom.Size = new System.Drawing.Size(225, 47);
            this.BtnConsultaStatusNFCom.TabIndex = 0;
            this.BtnConsultaStatusNFCom.Text = "Consulta Status";
            this.BtnConsultaStatusNFCom.UseVisualStyleBackColor = true;
            this.BtnConsultaStatusNFCom.Click += new System.EventHandler(this.BtnConsultaStatusNFCom_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnConsultaStatusNFCom);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 264);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "NFCom";
            // 
            // FormNFCom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 288);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormNFCom";
            this.Text = "Treinamento da Uni.NFCom";
            this.Load += new System.EventHandler(this.FormNFCom_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        
        #endregion

        private System.Windows.Forms.Button BtnConsultaStatusNFCom;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}