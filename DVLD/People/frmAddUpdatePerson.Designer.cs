namespace DVLD
{
    partial class frmAddEditPerson
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
            this.btnSave = new System.Windows.Forms.Button();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.cbCountry = new System.Windows.Forms.ComboBox();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.lblTitlePersonID = new System.Windows.Forms.Label();
            this.lblPersonID = new System.Windows.Forms.Label();
            this.lblTitleEmail = new System.Windows.Forms.Label();
            this.lblTitleGendor = new System.Windows.Forms.Label();
            this.lblTitleNationalNo = new System.Windows.Forms.Label();
            this.lblTitleName = new System.Windows.Forms.Label();
            this.gbAddPerson = new System.Windows.Forms.GroupBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkRemoveImage = new System.Windows.Forms.LinkLabel();
            this.linkSetImage = new System.Windows.Forms.LinkLabel();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtNationaNO = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSecondName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtThirdName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitleCountry = new System.Windows.Forms.Label();
            this.lblTitlePhone = new System.Windows.Forms.Label();
            this.lblTitleDateOfBirth = new System.Windows.Forms.Label();
            this.lblTitleAddress = new System.Windows.Forms.Label();
            this.epAddEditPerson = new System.Windows.Forms.ErrorProvider(this.components);
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.gbAddPerson.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epAddEditPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(472, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(248, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add - Person";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::DVLD.Properties.Resources.icon_Save;
            this.btnSave.Location = new System.Drawing.Point(999, 386);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(158, 58);
            this.btnSave.TabIndex = 13;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(201, 58);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(150, 29);
            this.txtFirstName.TabIndex = 0;
            this.txtFirstName.Validating += new System.ComponentModel.CancelEventHandler(this.txtFirstName_Validating);
            // 
            // cbCountry
            // 
            this.cbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cbCountry.FormattingEnabled = true;
            this.cbCountry.Location = new System.Drawing.Point(574, 244);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Size = new System.Drawing.Size(199, 32);
            this.cbCountry.TabIndex = 11;
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.dtpDateOfBirth.Location = new System.Drawing.Point(596, 119);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(325, 29);
            this.dtpDateOfBirth.TabIndex = 9;
            // 
            // lblTitlePersonID
            // 
            this.lblTitlePersonID.AutoSize = true;
            this.lblTitlePersonID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitlePersonID.Location = new System.Drawing.Point(76, 68);
            this.lblTitlePersonID.Name = "lblTitlePersonID";
            this.lblTitlePersonID.Size = new System.Drawing.Size(118, 25);
            this.lblTitlePersonID.TabIndex = 5;
            this.lblTitlePersonID.Text = "Person ID :";
            // 
            // lblPersonID
            // 
            this.lblPersonID.AutoSize = true;
            this.lblPersonID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersonID.Location = new System.Drawing.Point(200, 68);
            this.lblPersonID.Name = "lblPersonID";
            this.lblPersonID.Size = new System.Drawing.Size(47, 25);
            this.lblPersonID.TabIndex = 6;
            this.lblPersonID.Text = "N/A";
            // 
            // lblTitleEmail
            // 
            this.lblTitleEmail.AutoSize = true;
            this.lblTitleEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleEmail.Location = new System.Drawing.Point(70, 246);
            this.lblTitleEmail.Name = "lblTitleEmail";
            this.lblTitleEmail.Size = new System.Drawing.Size(77, 25);
            this.lblTitleEmail.TabIndex = 7;
            this.lblTitleEmail.Text = "Email :";
            // 
            // lblTitleGendor
            // 
            this.lblTitleGendor.AutoSize = true;
            this.lblTitleGendor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleGendor.Location = new System.Drawing.Point(62, 180);
            this.lblTitleGendor.Name = "lblTitleGendor";
            this.lblTitleGendor.Size = new System.Drawing.Size(95, 25);
            this.lblTitleGendor.TabIndex = 8;
            this.lblTitleGendor.Text = "Gendor :";
            // 
            // lblTitleNationalNo
            // 
            this.lblTitleNationalNo.AutoSize = true;
            this.lblTitleNationalNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleNationalNo.Location = new System.Drawing.Point(59, 115);
            this.lblTitleNationalNo.Name = "lblTitleNationalNo";
            this.lblTitleNationalNo.Size = new System.Drawing.Size(140, 25);
            this.lblTitleNationalNo.TabIndex = 9;
            this.lblTitleNationalNo.Text = "National NO :";
            // 
            // lblTitleName
            // 
            this.lblTitleName.AutoSize = true;
            this.lblTitleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleName.Location = new System.Drawing.Point(59, 52);
            this.lblTitleName.Name = "lblTitleName";
            this.lblTitleName.Size = new System.Drawing.Size(80, 25);
            this.lblTitleName.TabIndex = 10;
            this.lblTitleName.Text = "Name :";
            // 
            // gbAddPerson
            // 
            this.gbAddPerson.Controls.Add(this.pictureBox8);
            this.gbAddPerson.Controls.Add(this.pictureBox7);
            this.gbAddPerson.Controls.Add(this.pictureBox6);
            this.gbAddPerson.Controls.Add(this.pictureBox5);
            this.gbAddPerson.Controls.Add(this.pictureBox4);
            this.gbAddPerson.Controls.Add(this.pictureBox3);
            this.gbAddPerson.Controls.Add(this.pictureBox2);
            this.gbAddPerson.Controls.Add(this.pictureBox1);
            this.gbAddPerson.Controls.Add(this.linkRemoveImage);
            this.gbAddPerson.Controls.Add(this.linkSetImage);
            this.gbAddPerson.Controls.Add(this.btnSave);
            this.gbAddPerson.Controls.Add(this.pbImage);
            this.gbAddPerson.Controls.Add(this.rbFemale);
            this.gbAddPerson.Controls.Add(this.rbMale);
            this.gbAddPerson.Controls.Add(this.btnClose);
            this.gbAddPerson.Controls.Add(this.txtPhone);
            this.gbAddPerson.Controls.Add(this.txtAddress);
            this.gbAddPerson.Controls.Add(this.txtEmail);
            this.gbAddPerson.Controls.Add(this.cbCountry);
            this.gbAddPerson.Controls.Add(this.dtpDateOfBirth);
            this.gbAddPerson.Controls.Add(this.txtNationaNO);
            this.gbAddPerson.Controls.Add(this.label4);
            this.gbAddPerson.Controls.Add(this.txtSecondName);
            this.gbAddPerson.Controls.Add(this.label5);
            this.gbAddPerson.Controls.Add(this.label3);
            this.gbAddPerson.Controls.Add(this.txtLastName);
            this.gbAddPerson.Controls.Add(this.txtThirdName);
            this.gbAddPerson.Controls.Add(this.label1);
            this.gbAddPerson.Controls.Add(this.lblTitleCountry);
            this.gbAddPerson.Controls.Add(this.lblTitlePhone);
            this.gbAddPerson.Controls.Add(this.lblTitleDateOfBirth);
            this.gbAddPerson.Controls.Add(this.lblTitleAddress);
            this.gbAddPerson.Controls.Add(this.lblTitleEmail);
            this.gbAddPerson.Controls.Add(this.lblTitleGendor);
            this.gbAddPerson.Controls.Add(this.lblTitleNationalNo);
            this.gbAddPerson.Controls.Add(this.lblTitleName);
            this.gbAddPerson.Controls.Add(this.txtFirstName);
            this.gbAddPerson.Location = new System.Drawing.Point(17, 93);
            this.gbAddPerson.Name = "gbAddPerson";
            this.gbAddPerson.Size = new System.Drawing.Size(1167, 450);
            this.gbAddPerson.TabIndex = 11;
            this.gbAddPerson.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DVLD.Properties.Resources.icon_Country;
            this.pictureBox8.Location = new System.Drawing.Point(389, 236);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(31, 28);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 32;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::DVLD.Properties.Resources.icon_PhoneCall;
            this.pictureBox7.Location = new System.Drawing.Point(389, 177);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(31, 28);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 31;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::DVLD.Properties.Resources.icon_BirthDate;
            this.pictureBox6.Location = new System.Drawing.Point(389, 116);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(31, 28);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 30;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DVLD.Properties.Resources.icon_Address;
            this.pictureBox5.Location = new System.Drawing.Point(22, 323);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(31, 28);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 29;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLD.Properties.Resources.icon_Email;
            this.pictureBox4.Location = new System.Drawing.Point(22, 248);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(31, 28);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 28;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DVLD.Properties.Resources.icon_Gender;
            this.pictureBox3.Location = new System.Drawing.Point(22, 181);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(31, 28);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 27;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.icon_National;
            this.pictureBox2.Location = new System.Drawing.Point(22, 112);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 28);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 26;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.icon_Name;
            this.pictureBox1.Location = new System.Drawing.Point(22, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // linkRemoveImage
            // 
            this.linkRemoveImage.AutoSize = true;
            this.linkRemoveImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkRemoveImage.Location = new System.Drawing.Point(935, 272);
            this.linkRemoveImage.Name = "linkRemoveImage";
            this.linkRemoveImage.Size = new System.Drawing.Size(91, 25);
            this.linkRemoveImage.TabIndex = 25;
            this.linkRemoveImage.TabStop = true;
            this.linkRemoveImage.Text = "Remove";
            this.linkRemoveImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRemoveImage_LinkClicked);
            // 
            // linkSetImage
            // 
            this.linkSetImage.AutoSize = true;
            this.linkSetImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkSetImage.Location = new System.Drawing.Point(1049, 272);
            this.linkSetImage.Name = "linkSetImage";
            this.linkSetImage.Size = new System.Drawing.Size(108, 25);
            this.linkSetImage.TabIndex = 12;
            this.linkSetImage.TabStop = true;
            this.linkSetImage.Text = "Set Image";
            this.linkSetImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSetImage_LinkClicked);
            // 
            // pbImage
            // 
            this.pbImage.Image = global::DVLD.Properties.Resources.png_Male;
            this.pbImage.InitialImage = global::DVLD.Properties.Resources.png_Male;
            this.pbImage.Location = new System.Drawing.Point(945, 58);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(212, 200);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 23;
            this.pbImage.TabStop = false;
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFemale.Location = new System.Drawing.Point(279, 181);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(92, 28);
            this.rbFemale.TabIndex = 6;
            this.rbFemale.TabStop = true;
            this.rbFemale.Text = "Female";
            this.rbFemale.UseVisualStyleBackColor = true;
            this.rbFemale.CheckedChanged += new System.EventHandler(this.rbFemale_CheckedChanged);
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMale.Location = new System.Drawing.Point(204, 181);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(69, 28);
            this.rbMale.TabIndex = 5;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "Male";
            this.rbMale.UseVisualStyleBackColor = true;
            this.rbMale.CheckedChanged += new System.EventHandler(this.rbMale_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(850, 386);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(143, 58);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtPhone.Location = new System.Drawing.Point(574, 180);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(199, 29);
            this.txtPhone.TabIndex = 10;
            this.txtPhone.Validating += new System.ComponentModel.CancelEventHandler(this.txtPhone_Validating);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(201, 332);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(628, 112);
            this.txtAddress.TabIndex = 8;
            this.txtAddress.Validating += new System.ComponentModel.CancelEventHandler(this.txtAddress_Validating);
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtEmail.Location = new System.Drawing.Point(221, 244);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(150, 29);
            this.txtEmail.TabIndex = 7;
            this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmail_Validating);
            // 
            // txtNationaNO
            // 
            this.txtNationaNO.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtNationaNO.Location = new System.Drawing.Point(201, 115);
            this.txtNationaNO.Name = "txtNationaNO";
            this.txtNationaNO.Size = new System.Drawing.Size(150, 29);
            this.txtNationaNO.TabIndex = 4;
            this.txtNationaNO.Validating += new System.ComponentModel.CancelEventHandler(this.txtNationaNO_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(430, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "Second";
            // 
            // txtSecondName
            // 
            this.txtSecondName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtSecondName.Location = new System.Drawing.Point(389, 58);
            this.txtSecondName.Name = "txtSecondName";
            this.txtSecondName.Size = new System.Drawing.Size(150, 29);
            this.txtSecondName.TabIndex = 1;
            this.txtSecondName.Validating += new System.ComponentModel.CancelEventHandler(this.txtSecondName_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(616, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Third";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(800, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Last";
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtLastName.Location = new System.Drawing.Point(746, 58);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(150, 29);
            this.txtLastName.TabIndex = 3;
            this.txtLastName.Validating += new System.ComponentModel.CancelEventHandler(this.txtLastName_Validating);
            // 
            // txtThirdName
            // 
            this.txtThirdName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtThirdName.Location = new System.Drawing.Point(568, 58);
            this.txtThirdName.Name = "txtThirdName";
            this.txtThirdName.Size = new System.Drawing.Size(150, 29);
            this.txtThirdName.TabIndex = 2;
            this.txtThirdName.Validating += new System.ComponentModel.CancelEventHandler(this.txtThirdName_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(251, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "First";
            // 
            // lblTitleCountry
            // 
            this.lblTitleCountry.AutoSize = true;
            this.lblTitleCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleCountry.Location = new System.Drawing.Point(430, 239);
            this.lblTitleCountry.Name = "lblTitleCountry";
            this.lblTitleCountry.Size = new System.Drawing.Size(99, 25);
            this.lblTitleCountry.TabIndex = 14;
            this.lblTitleCountry.Text = "Country :";
            // 
            // lblTitlePhone
            // 
            this.lblTitlePhone.AutoSize = true;
            this.lblTitlePhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitlePhone.Location = new System.Drawing.Point(430, 180);
            this.lblTitlePhone.Name = "lblTitlePhone";
            this.lblTitlePhone.Size = new System.Drawing.Size(86, 25);
            this.lblTitlePhone.TabIndex = 13;
            this.lblTitlePhone.Text = "Phone :";
            // 
            // lblTitleDateOfBirth
            // 
            this.lblTitleDateOfBirth.AutoSize = true;
            this.lblTitleDateOfBirth.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleDateOfBirth.Location = new System.Drawing.Point(429, 119);
            this.lblTitleDateOfBirth.Name = "lblTitleDateOfBirth";
            this.lblTitleDateOfBirth.Size = new System.Drawing.Size(147, 25);
            this.lblTitleDateOfBirth.TabIndex = 12;
            this.lblTitleDateOfBirth.Text = "Date Of Birth :";
            // 
            // lblTitleAddress
            // 
            this.lblTitleAddress.AutoSize = true;
            this.lblTitleAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleAddress.Location = new System.Drawing.Point(66, 326);
            this.lblTitleAddress.Name = "lblTitleAddress";
            this.lblTitleAddress.Size = new System.Drawing.Size(103, 25);
            this.lblTitleAddress.TabIndex = 11;
            this.lblTitleAddress.Text = "Address :";
            // 
            // epAddEditPerson
            // 
            this.epAddEditPerson.ContainerControl = this;
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::DVLD.Properties.Resources.icon_ID;
            this.pictureBox9.Location = new System.Drawing.Point(39, 65);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(31, 28);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox9.TabIndex = 33;
            this.pictureBox9.TabStop = false;
            // 
            // frmAddEditPerson
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1193, 549);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.lblPersonID);
            this.Controls.Add(this.lblTitlePersonID);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.gbAddPerson);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmAddEditPerson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add-Person";
            this.Load += new System.EventHandler(this.frmAddEditPerson_Load);
            this.gbAddPerson.ResumeLayout(false);
            this.gbAddPerson.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epAddEditPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.ComboBox cbCountry;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Label lblTitlePersonID;
        private System.Windows.Forms.Label lblPersonID;
        private System.Windows.Forms.Label lblTitleEmail;
        private System.Windows.Forms.Label lblTitleGendor;
        private System.Windows.Forms.Label lblTitleNationalNo;
        private System.Windows.Forms.Label lblTitleName;
        private System.Windows.Forms.GroupBox gbAddPerson;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSecondName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtThirdName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitleCountry;
        private System.Windows.Forms.Label lblTitlePhone;
        private System.Windows.Forms.Label lblTitleDateOfBirth;
        private System.Windows.Forms.Label lblTitleAddress;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtNationaNO;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel linkRemoveImage;
        private System.Windows.Forms.LinkLabel linkSetImage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.ErrorProvider epAddEditPerson;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox9;
    }
}