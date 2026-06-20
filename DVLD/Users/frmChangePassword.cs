using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        private DVLD_BLL.clsUser _User;
        private int _UserID = -1;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void _ResetDefaultValues()
        {
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtCurrentPassword.Focus();
        }

        private void _Load()
        {
            _ResetDefaultValues();
            _User = DVLD_BLL.clsUser.FindUserByUserID(_UserID);
            if (_User != null)
            {
                ctrlUserInfo1.LoadUserByUserID(_UserID);
            }
            else
            {
                MessageBox.Show("Error : User Not Found", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private void _Save()
        {
            _User.Password = txtNewPassword.Text.Trim();
            if (_User.Save())
            {
                MessageBox.Show("Password Changed SuccsussFully", "Pasword Updated",MessageBoxButtons.OK,MessageBoxIcon.Information);
                _ResetDefaultValues();
            }
            else
            {
                MessageBox.Show("Error : Faild To Change The Password", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
                _Save();
        }

        private void brnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Validating For Empty And Special Cases
        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text))
            {
                e.Cancel = true;
                epPassword.SetError(txtCurrentPassword, "Please Enter Your Current Password");
                return;
            }
            else
            {
                e.Cancel = false;
                epPassword.SetError(txtCurrentPassword, null);
            }

            if (DVLD_BLL.clsHash.HashData(txtCurrentPassword.Text.Trim()) != _User.Password)
            {
                epPassword.SetError(txtCurrentPassword, "Wrong Password");
                e.Cancel = true;
            }
            else
            {
                epPassword.SetError(txtCurrentPassword, null);
                e.Cancel = false;
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtNewPassword.Text))
            {
                e.Cancel= true;
                epPassword.SetError(txtNewPassword, "Please Enter The New Password");
            }
            else
            {
                e.Cancel = false;
                epPassword.SetError(txtNewPassword,null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                e.Cancel= true;
                epPassword.SetError(txtConfirmPassword, "Please Confirm Your Password");
                return;
            }
            else
            {
                e.Cancel = false;
                epPassword.SetError (txtConfirmPassword, null);
            }
            if (txtConfirmPassword.Text!=txtNewPassword.Text)
            {
                e.Cancel = true;
                epPassword.SetError(txtConfirmPassword, "Passwords Dosen't Match");
            }
            else
            {
                e.Cancel = false;
                epPassword.SetError(txtConfirmPassword, null);
            }
        }
    }
}
