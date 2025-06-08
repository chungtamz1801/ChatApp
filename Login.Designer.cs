namespace ChatApp
{
    partial class Login
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
            this.label2 = new System.Windows.Forms.Label();
            this.port_tbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ip_tbx = new System.Windows.Forms.TextBox();
            this.login_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(278, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Port";
            // 
            // port_tbx
            // 
            this.port_tbx.Location = new System.Drawing.Point(334, 198);
            this.port_tbx.Name = "port_tbx";
            this.port_tbx.Size = new System.Drawing.Size(162, 22);
            this.port_tbx.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(278, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP";
            // 
            // ip_tbx
            // 
            this.ip_tbx.Location = new System.Drawing.Point(334, 170);
            this.ip_tbx.Name = "ip_tbx";
            this.ip_tbx.Size = new System.Drawing.Size(162, 22);
            this.ip_tbx.TabIndex = 4;
            // 
            // login_btn
            // 
            this.login_btn.Location = new System.Drawing.Point(334, 255);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(97, 34);
            this.login_btn.TabIndex = 8;
            this.login_btn.Text = "Login";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.port_tbx);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ip_tbx);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox port_tbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ip_tbx;
        private System.Windows.Forms.Button login_btn;
    }
}