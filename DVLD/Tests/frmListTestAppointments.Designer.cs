namespace DVLD
{
    partial class frmTestAppointemnt
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.dgvTestAppointments = new System.Windows.Forms.DataGridView();
            this.cmTestAppointments = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editTestAppointemntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblAppointmentsTitle = new System.Windows.Forms.Label();
            this.lblTitleRecords = new System.Windows.Forms.Label();
            this.ctrlLocalDrivingApplicationInfo1 = new DVLD.ctrlLocalDrivingApplicationInfo();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestAppointments)).BeginInit();
            this.cmTestAppointments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(398, 87);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(85, 39);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Test";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.Location = new System.Drawing.Point(103, 761);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(24, 20);
            this.lblRecords.TabIndex = 1;
            this.lblRecords.Text = "---";
            // 
            // dgvTestAppointments
            // 
            this.dgvTestAppointments.AllowUserToAddRows = false;
            this.dgvTestAppointments.AllowUserToDeleteRows = false;
            this.dgvTestAppointments.AllowUserToOrderColumns = true;
            this.dgvTestAppointments.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvTestAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestAppointments.ContextMenuStrip = this.cmTestAppointments;
            this.dgvTestAppointments.Location = new System.Drawing.Point(11, 513);
            this.dgvTestAppointments.MultiSelect = false;
            this.dgvTestAppointments.Name = "dgvTestAppointments";
            this.dgvTestAppointments.ReadOnly = true;
            this.dgvTestAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTestAppointments.Size = new System.Drawing.Size(855, 244);
            this.dgvTestAppointments.TabIndex = 2;
            // 
            // cmTestAppointments
            // 
            this.cmTestAppointments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editTestAppointemntToolStripMenuItem,
            this.takeTestToolStripMenuItem});
            this.cmTestAppointments.Name = "contextMenuStrip1";
            this.cmTestAppointments.Size = new System.Drawing.Size(123, 48);
            this.cmTestAppointments.Opening += new System.ComponentModel.CancelEventHandler(this.cmTestAppointments_Opening);
            // 
            // editTestAppointemntToolStripMenuItem
            // 
            this.editTestAppointemntToolStripMenuItem.Image = global::DVLD.Properties.Resources.Icno_Edit;
            this.editTestAppointemntToolStripMenuItem.Name = "editTestAppointemntToolStripMenuItem";
            this.editTestAppointemntToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.editTestAppointemntToolStripMenuItem.Text = "Edit";
            this.editTestAppointemntToolStripMenuItem.Click += new System.EventHandler(this.editTestAppointemntToolStripMenuItem_Click);
            // 
            // takeTestToolStripMenuItem
            // 
            this.takeTestToolStripMenuItem.Image = global::DVLD.Properties.Resources.Icon_Applications;
            this.takeTestToolStripMenuItem.Name = "takeTestToolStripMenuItem";
            this.takeTestToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.takeTestToolStripMenuItem.Text = "Take Test";
            this.takeTestToolStripMenuItem.Click += new System.EventHandler(this.takeTestToolStripMenuItem_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::DVLD.Properties.Resources.icon_Add;
            this.btnAdd.Location = new System.Drawing.Point(788, 452);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(78, 55);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblAppointmentsTitle
            // 
            this.lblAppointmentsTitle.AutoSize = true;
            this.lblAppointmentsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointmentsTitle.Location = new System.Drawing.Point(12, 487);
            this.lblAppointmentsTitle.Name = "lblAppointmentsTitle";
            this.lblAppointmentsTitle.Size = new System.Drawing.Size(108, 20);
            this.lblAppointmentsTitle.TabIndex = 6;
            this.lblAppointmentsTitle.Text = "Appointments";
            // 
            // lblTitleRecords
            // 
            this.lblTitleRecords.AutoSize = true;
            this.lblTitleRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleRecords.Location = new System.Drawing.Point(11, 761);
            this.lblTitleRecords.Name = "lblTitleRecords";
            this.lblTitleRecords.Size = new System.Drawing.Size(86, 20);
            this.lblTitleRecords.TabIndex = 7;
            this.lblTitleRecords.Text = "#Records :";
            // 
            // ctrlLocalDrivingApplicationInfo1
            // 
            this.ctrlLocalDrivingApplicationInfo1.Location = new System.Drawing.Point(11, 129);
            this.ctrlLocalDrivingApplicationInfo1.Name = "ctrlLocalDrivingApplicationInfo1";
            this.ctrlLocalDrivingApplicationInfo1.Size = new System.Drawing.Size(855, 333);
            this.ctrlLocalDrivingApplicationInfo1.TabIndex = 5;
            // 
            // pbImage
            // 
            this.pbImage.Image = global::DVLD.Properties.Resources.png_Test;
            this.pbImage.Location = new System.Drawing.Point(359, -4);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(181, 88);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 8;
            this.pbImage.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.icon_AppID3;
            this.pictureBox2.Location = new System.Drawing.Point(126, 487);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // frmTestAppointemnt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 795);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.lblTitleRecords);
            this.Controls.Add(this.lblAppointmentsTitle);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvTestAppointments);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.ctrlLocalDrivingApplicationInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmTestAppointemnt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test";
            this.Load += new System.EventHandler(this.frmTestAppointemnt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestAppointments)).EndInit();
            this.cmTestAppointments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.DataGridView dgvTestAppointments;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ContextMenuStrip cmTestAppointments;
        private ctrlLocalDrivingApplicationInfo ctrlLocalDrivingApplicationInfo1;
        private System.Windows.Forms.ToolStripMenuItem editTestAppointemntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeTestToolStripMenuItem;
        private System.Windows.Forms.Label lblAppointmentsTitle;
        private System.Windows.Forms.Label lblTitleRecords;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}