using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreinamentoDLL
{
    public partial class FormNFCom : Form
    {
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnConsultaStatusNFCom;

        public FormNFCom()
        {
            InitializeComponent();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 120);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "NFCom";

            //
            // BtnConsultaStatusNFCom
            //
            this.BtnConsultaStatusNFCom = new System.Windows.Forms.Button();
            this.groupBox1.Controls.Add(this.BtnConsultaStatusNFCom);

            this.BtnConsultaStatusNFCom.Location = new System.Drawing.Point(6, 19);
            this.BtnConsultaStatusNFCom.Name = "BtnConsultaStatusNFCom";
            this.BtnConsultaStatusNFCom.Size = new System.Drawing.Size(197, 23);
            this.BtnConsultaStatusNFCom.TabIndex = 0;
            this.BtnConsultaStatusNFCom.Text = "Consulta Status";
            this.BtnConsultaStatusNFCom.UseVisualStyleBackColor = true;
            this.BtnConsultaStatusNFCom.Click += new System.EventHandler(this.BtnConsultaStatusNFCom);
            // 
            // FormTestes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1323, 565);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormTestes";
            this.Text = "Treinamento Uni.NFCom";
            this.Load += new System.EventHandler(this.FormNFCom_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void FormNFCom_Load(object sender, EventArgs e)
        {

        }
    }
}
