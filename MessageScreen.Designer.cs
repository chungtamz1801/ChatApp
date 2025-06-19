namespace ChatApp
{
    partial class MessageScreen
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Friend_Port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Friend_IP = new System.Windows.Forms.TextBox();
            this.message_lst = new System.Windows.Forms.ListBox();
            this.message_tbx = new System.Windows.Forms.TextBox();
            this.send_btn = new System.Windows.Forms.Button();
            this.key_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Information_Port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Information_IP = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.Friend_Port);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.Friend_IP);
            this.groupBox2.Location = new System.Drawing.Point(364, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(266, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Friend";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Port";
            // 
            // Friend_Port
            // 
            this.Friend_Port.Location = new System.Drawing.Point(72, 58);
            this.Friend_Port.Name = "Friend_Port";
            this.Friend_Port.Size = new System.Drawing.Size(162, 22);
            this.Friend_Port.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "IP";
            // 
            // Friend_IP
            // 
            this.Friend_IP.Location = new System.Drawing.Point(72, 30);
            this.Friend_IP.Name = "Friend_IP";
            this.Friend_IP.Size = new System.Drawing.Size(162, 22);
            this.Friend_IP.TabIndex = 0;
            // 
            // message_lst
            // 
            this.message_lst.FormattingEnabled = true;
            this.message_lst.ItemHeight = 16;
            this.message_lst.Location = new System.Drawing.Point(85, 211);
            this.message_lst.Name = "message_lst";
            this.message_lst.ScrollAlwaysVisible = true;
            this.message_lst.Size = new System.Drawing.Size(785, 196);
            this.message_lst.TabIndex = 6;
            // 
            // message_tbx
            // 
            this.message_tbx.Location = new System.Drawing.Point(85, 441);
            this.message_tbx.Name = "message_tbx";
            this.message_tbx.Size = new System.Drawing.Size(472, 22);
            this.message_tbx.TabIndex = 4;
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(574, 441);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(75, 23);
            this.send_btn.TabIndex = 7;
            this.send_btn.Text = "Send";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // key_btn
            // 
            this.key_btn.Location = new System.Drawing.Point(675, 111);
            this.key_btn.Name = "key_btn";
            this.key_btn.Size = new System.Drawing.Size(97, 34);
            this.key_btn.TabIndex = 8;
            this.key_btn.Text = "Key";
            this.key_btn.UseVisualStyleBackColor = true;
            this.key_btn.Click += new System.EventHandler(this.key_btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Information_Port);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Information_IP);
            this.groupBox1.Location = new System.Drawing.Point(85, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Port";
            // 
            // Information_Port
            // 
            this.Information_Port.Enabled = false;
            this.Information_Port.Location = new System.Drawing.Point(72, 58);
            this.Information_Port.Name = "Information_Port";
            this.Information_Port.Size = new System.Drawing.Size(162, 22);
            this.Information_Port.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "IP";
            // 
            // Information_IP
            // 
            this.Information_IP.Enabled = false;
            this.Information_IP.Location = new System.Drawing.Point(72, 30);
            this.Information_IP.Name = "Information_IP";
            this.Information_IP.Size = new System.Drawing.Size(162, 22);
            this.Information_IP.TabIndex = 0;
            // 
            // MessageScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 504);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.key_btn);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.message_tbx);
            this.Controls.Add(this.message_lst);
            this.Controls.Add(this.groupBox2);
            this.Name = "MessageScreen";
            this.Text = "MessageScreen";
            this.Load += new System.EventHandler(this.MessageScreen_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Friend_Port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Friend_IP;
        private System.Windows.Forms.ListBox message_lst;
        private System.Windows.Forms.TextBox message_tbx;
        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.Button key_btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Information_Port;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Information_IP;
    }
}