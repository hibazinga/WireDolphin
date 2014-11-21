namespace Sniffer
{
    partial class Filter
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
            this.ARP = new System.Windows.Forms.CheckBox();
            this.IPV4 = new System.Windows.Forms.CheckBox();
            this.IPV6 = new System.Windows.Forms.CheckBox();
            this.ICMP = new System.Windows.Forms.CheckBox();
            this.IGMP = new System.Windows.Forms.CheckBox();
            this.TCP = new System.Windows.Forms.CheckBox();
            this.UDP = new System.Windows.Forms.CheckBox();
            this.APPLY = new System.Windows.Forms.Button();
            this.CLEAR = new System.Windows.Forms.Button();
            this.Expression = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MACAdrS = new System.Windows.Forms.TextBox();
            this.IPAdrS = new System.Windows.Forms.TextBox();
            this.PORTNumS = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.MACAdrD = new System.Windows.Forms.TextBox();
            this.IPAdrD = new System.Windows.Forms.TextBox();
            this.PORTNumD = new System.Windows.Forms.TextBox();
            this.OK = new System.Windows.Forms.Button();
            this.CLOSE = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ARP
            // 
            this.ARP.AutoSize = true;
            this.ARP.Location = new System.Drawing.Point(117, 47);
            this.ARP.Name = "ARP";
            this.ARP.Size = new System.Drawing.Size(46, 18);
            this.ARP.TabIndex = 0;
            this.ARP.Text = "ARP";
            this.ARP.UseVisualStyleBackColor = true;
            this.ARP.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // IPV4
            // 
            this.IPV4.AutoSize = true;
            this.IPV4.Location = new System.Drawing.Point(117, 86);
            this.IPV4.Name = "IPV4";
            this.IPV4.Size = new System.Drawing.Size(49, 18);
            this.IPV4.TabIndex = 1;
            this.IPV4.Text = "IPV4";
            this.IPV4.UseVisualStyleBackColor = true;
            // 
            // IPV6
            // 
            this.IPV6.AutoSize = true;
            this.IPV6.Location = new System.Drawing.Point(169, 86);
            this.IPV6.Name = "IPV6";
            this.IPV6.Size = new System.Drawing.Size(49, 18);
            this.IPV6.TabIndex = 2;
            this.IPV6.Text = "IPV6";
            this.IPV6.UseVisualStyleBackColor = true;
            // 
            // ICMP
            // 
            this.ICMP.AutoSize = true;
            this.ICMP.Location = new System.Drawing.Point(117, 112);
            this.ICMP.Name = "ICMP";
            this.ICMP.Size = new System.Drawing.Size(52, 18);
            this.ICMP.TabIndex = 3;
            this.ICMP.Text = "ICMP";
            this.ICMP.UseVisualStyleBackColor = true;
            // 
            // IGMP
            // 
            this.IGMP.AutoSize = true;
            this.IGMP.Location = new System.Drawing.Point(169, 112);
            this.IGMP.Name = "IGMP";
            this.IGMP.Size = new System.Drawing.Size(54, 18);
            this.IGMP.TabIndex = 4;
            this.IGMP.Text = "IGMP";
            this.IGMP.UseVisualStyleBackColor = true;
            // 
            // TCP
            // 
            this.TCP.AutoSize = true;
            this.TCP.Location = new System.Drawing.Point(117, 156);
            this.TCP.Name = "TCP";
            this.TCP.Size = new System.Drawing.Size(44, 18);
            this.TCP.TabIndex = 5;
            this.TCP.Text = "TCP";
            this.TCP.UseVisualStyleBackColor = true;
            // 
            // UDP
            // 
            this.UDP.AutoSize = true;
            this.UDP.Location = new System.Drawing.Point(169, 156);
            this.UDP.Name = "UDP";
            this.UDP.Size = new System.Drawing.Size(48, 18);
            this.UDP.TabIndex = 6;
            this.UDP.Text = "UDP";
            this.UDP.UseVisualStyleBackColor = true;
            // 
            // APPLY
            // 
            this.APPLY.Location = new System.Drawing.Point(117, 251);
            this.APPLY.Name = "APPLY";
            this.APPLY.Size = new System.Drawing.Size(75, 27);
            this.APPLY.TabIndex = 7;
            this.APPLY.Text = "APPLY";
            this.APPLY.UseVisualStyleBackColor = true;
            this.APPLY.Click += new System.EventHandler(this.button1_Click);
            // 
            // CLEAR
            // 
            this.CLEAR.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CLEAR.Location = new System.Drawing.Point(340, 251);
            this.CLEAR.Name = "CLEAR";
            this.CLEAR.Size = new System.Drawing.Size(75, 27);
            this.CLEAR.TabIndex = 8;
            this.CLEAR.Text = "CLEAR";
            this.CLEAR.UseVisualStyleBackColor = true;
            this.CLEAR.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Expression
            // 
            this.Expression.Location = new System.Drawing.Point(117, 201);
            this.Expression.Name = "Expression";
            this.Expression.Size = new System.Drawing.Size(437, 22);
            this.Expression.TabIndex = 9;
            this.Expression.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 14);
            this.label1.TabIndex = 10;
            this.label1.Text = "Link Layer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 14);
            this.label2.TabIndex = 11;
            this.label2.Text = "Internet Layer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 14);
            this.label3.TabIndex = 12;
            this.label3.Text = "Transport Layer";
            // 
            // MACAdrS
            // 
            this.MACAdrS.Location = new System.Drawing.Point(380, 57);
            this.MACAdrS.Name = "MACAdrS";
            this.MACAdrS.Size = new System.Drawing.Size(100, 22);
            this.MACAdrS.TabIndex = 13;
            // 
            // IPAdrS
            // 
            this.IPAdrS.Location = new System.Drawing.Point(380, 106);
            this.IPAdrS.Name = "IPAdrS";
            this.IPAdrS.Size = new System.Drawing.Size(100, 22);
            this.IPAdrS.TabIndex = 14;
            // 
            // PORTNumS
            // 
            this.PORTNumS.Location = new System.Drawing.Point(380, 146);
            this.PORTNumS.Name = "PORTNumS";
            this.PORTNumS.Size = new System.Drawing.Size(100, 22);
            this.PORTNumS.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(298, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 16;
            this.label4.Text = "MAC Address";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(298, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 14);
            this.label5.TabIndex = 17;
            this.label5.Text = "IP Address";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(298, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 14);
            this.label6.TabIndex = 18;
            this.label6.Text = "Port Number";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 204);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 14);
            this.label7.TabIndex = 19;
            this.label7.Text = "Expression";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(399, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 14);
            this.label8.TabIndex = 20;
            this.label8.Text = "Source";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(528, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 14);
            this.label9.TabIndex = 21;
            this.label9.Text = "Destination";
            // 
            // MACAdrD
            // 
            this.MACAdrD.Location = new System.Drawing.Point(516, 57);
            this.MACAdrD.Name = "MACAdrD";
            this.MACAdrD.Size = new System.Drawing.Size(100, 22);
            this.MACAdrD.TabIndex = 22;
            this.MACAdrD.TextChanged += new System.EventHandler(this.MACAdrD_TextChanged);
            // 
            // IPAdrD
            // 
            this.IPAdrD.Location = new System.Drawing.Point(516, 106);
            this.IPAdrD.Name = "IPAdrD";
            this.IPAdrD.Size = new System.Drawing.Size(100, 22);
            this.IPAdrD.TabIndex = 23;
            // 
            // PORTNumD
            // 
            this.PORTNumD.Location = new System.Drawing.Point(516, 146);
            this.PORTNumD.Name = "PORTNumD";
            this.PORTNumD.Size = new System.Drawing.Size(100, 22);
            this.PORTNumD.TabIndex = 24;
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(232, 251);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 27);
            this.OK.TabIndex = 25;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // CLOSE
            // 
            this.CLOSE.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CLOSE.Location = new System.Drawing.Point(456, 251);
            this.CLOSE.Name = "CLOSE";
            this.CLOSE.Size = new System.Drawing.Size(75, 27);
            this.CLOSE.TabIndex = 26;
            this.CLOSE.Text = "CLOSE";
            this.CLOSE.UseVisualStyleBackColor = true;
            this.CLOSE.Click += new System.EventHandler(this.CLOSE_Click);
            // 
            // Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CLOSE;
            this.ClientSize = new System.Drawing.Size(641, 366);
            this.Controls.Add(this.CLOSE);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.PORTNumD);
            this.Controls.Add(this.IPAdrD);
            this.Controls.Add(this.MACAdrD);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PORTNumS);
            this.Controls.Add(this.IPAdrS);
            this.Controls.Add(this.MACAdrS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Expression);
            this.Controls.Add(this.CLEAR);
            this.Controls.Add(this.APPLY);
            this.Controls.Add(this.UDP);
            this.Controls.Add(this.TCP);
            this.Controls.Add(this.IGMP);
            this.Controls.Add(this.ICMP);
            this.Controls.Add(this.IPV6);
            this.Controls.Add(this.IPV4);
            this.Controls.Add(this.ARP);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Filter";
            this.Text = "FilterForm";
            this.Load += new System.EventHandler(this.Filter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ARP;
        private System.Windows.Forms.CheckBox IPV4;
        private System.Windows.Forms.CheckBox IPV6;
        private System.Windows.Forms.CheckBox ICMP;
        private System.Windows.Forms.CheckBox IGMP;
        private System.Windows.Forms.CheckBox TCP;
        private System.Windows.Forms.CheckBox UDP;
        private System.Windows.Forms.Button APPLY;
        private System.Windows.Forms.Button CLEAR;
        private System.Windows.Forms.TextBox Expression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox MACAdrS;
        private System.Windows.Forms.TextBox IPAdrS;
        private System.Windows.Forms.TextBox PORTNumS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox MACAdrD;
        private System.Windows.Forms.TextBox IPAdrD;
        private System.Windows.Forms.TextBox PORTNumD;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button CLOSE;
    }
}