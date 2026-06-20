namespace DVLD
{
    partial class frmLocalDrivingApplicationInfo
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.ctrlLocalDrivingApplicationInfo1 = new DVLD.ctrlLocalDrivingApplicationInfo();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(223, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(394, 33);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Local Driving Application-Info";
            // 
            // ctrlLocalDrivingApplicationInfo1
            // 
            this.ctrlLocalDrivingApplicationInfo1.Location = new System.Drawing.Point(12, 45);
            this.ctrlLocalDrivingApplicationInfo1.Name = "ctrlLocalDrivingApplicationInfo1";
            this.ctrlLocalDrivingApplicationInfo1.Size = new System.Drawing.Size(863, 329);
            this.ctrlLocalDrivingApplicationInfo1.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(686, 367);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(171, 45);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmLocalDrivingApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 418);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlLocalDrivingApplicationInfo1);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmLocalDrivingApplicationInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Local Driving Application Info";
            this.Load += new System.EventHandler(this.frmLocalDrivingApplicationInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private ctrlLocalDrivingApplicationInfo ctrlLocalDrivingApplicationInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}