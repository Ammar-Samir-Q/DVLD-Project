namespace DVLD
{
    partial class frmLogin
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
            this.lblTitleUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.cbRememberMe = new System.Windows.Forms.CheckBox();
            this.lblTitlePassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblTitleLogin = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblSmallTitle = new System.Windows.Forms.Label();
            this.lblVersionNumber = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitleUserName
            // 
            this.lblTitleUserName.AutoSize = true;
            this.lblTitleUserName.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTitleUserName.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleUserName.ForeColor = System.Drawing.Color.Black;
            this.lblTitleUserName.Location = new System.Drawing.Point(48, 210);
            this.lblTitleUserName.Name = "lblTitleUserName";
            this.lblTitleUserName.Size = new System.Drawing.Size(140, 29);
            this.lblTitleUserName.TabIndex = 0;
            this.lblTitleUserName.Text = "UserName :";
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(194, 213);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(309, 33);
            this.txtUserName.TabIndex = 0;
            // 
            // cbRememberMe
            // 
            this.cbRememberMe.AutoSize = true;
            this.cbRememberMe.BackColor = System.Drawing.Color.Gainsboro;
            this.cbRememberMe.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRememberMe.ForeColor = System.Drawing.Color.Black;
            this.cbRememberMe.Location = new System.Drawing.Point(194, 296);
            this.cbRememberMe.Name = "cbRememberMe";
            this.cbRememberMe.Size = new System.Drawing.Size(152, 27);
            this.cbRememberMe.TabIndex = 2;
            this.cbRememberMe.Text = "Remember Me";
            this.cbRememberMe.UseVisualStyleBackColor = false;
            // 
            // lblTitlePassword
            // 
            this.lblTitlePassword.AutoSize = true;
            this.lblTitlePassword.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTitlePassword.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitlePassword.ForeColor = System.Drawing.Color.Black;
            this.lblTitlePassword.Location = new System.Drawing.Point(58, 257);
            this.lblTitlePassword.Name = "lblTitlePassword";
            this.lblTitlePassword.Size = new System.Drawing.Size(130, 29);
            this.lblTitlePassword.TabIndex = 4;
            this.lblTitlePassword.Text = "Password :";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(194, 257);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(309, 33);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblTitleLogin
            // 
            this.lblTitleLogin.AutoSize = true;
            this.lblTitleLogin.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTitleLogin.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleLogin.ForeColor = System.Drawing.Color.Black;
            this.lblTitleLogin.Location = new System.Drawing.Point(119, 151);
            this.lblTitleLogin.Name = "lblTitleLogin";
            this.lblTitleLogin.Size = new System.Drawing.Size(346, 35);
            this.lblTitleLogin.TabIndex = 6;
            this.lblTitleLogin.Text = "Login To Your Account";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTitle.Location = new System.Drawing.Point(18, 205);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(348, 58);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "DVLD System";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.cbRememberMe);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.lblTitleLogin);
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Controls.Add(this.lblTitlePassword);
            this.panel1.Controls.Add(this.lblTitleUserName);
            this.panel1.Location = new System.Drawing.Point(372, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(563, 536);
            this.panel1.TabIndex = 8;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLD.Properties.Resources.logo_username;
            this.pictureBox4.Location = new System.Drawing.Point(11, 213);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(31, 29);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 14;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.logo_lock;
            this.pictureBox2.Location = new System.Drawing.Point(11, 257);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 29);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.button1.Image = global::DVLD.Properties.Resources.Icon_Close;
            this.button1.Location = new System.Drawing.Point(502, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 43);
            this.button1.TabIndex = 4;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Gainsboro;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Image = global::DVLD.Properties.Resources.Icon_Login;
            this.btnLogin.Location = new System.Drawing.Point(403, 296);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(62, 57);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblSmallTitle
            // 
            this.lblSmallTitle.AutoSize = true;
            this.lblSmallTitle.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSmallTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblSmallTitle.Location = new System.Drawing.Point(12, 263);
            this.lblSmallTitle.Name = "lblSmallTitle";
            this.lblSmallTitle.Size = new System.Drawing.Size(350, 23);
            this.lblSmallTitle.TabIndex = 9;
            this.lblSmallTitle.Text = "Driving License Managment System";
            // 
            // lblVersionNumber
            // 
            this.lblVersionNumber.AutoSize = true;
            this.lblVersionNumber.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionNumber.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblVersionNumber.Location = new System.Drawing.Point(12, 497);
            this.lblVersionNumber.Name = "lblVersionNumber";
            this.lblVersionNumber.Size = new System.Drawing.Size(53, 23);
            this.lblVersionNumber.TabIndex = 10;
            this.lblVersionNumber.Text = "V1.1";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DVLD.Properties.Resources.png_WhiteLicense;
            this.pictureBox3.Location = new System.Drawing.Point(78, 147);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(188, 74);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(935, 529);
            this.Controls.Add(this.lblVersionNumber);
            this.Controls.Add(this.lblSmallTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLogin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitleUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.CheckBox cbRememberMe;
        private System.Windows.Forms.Label lblTitlePassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblTitleLogin;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblSmallTitle;
        private System.Windows.Forms.Label lblVersionNumber;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}