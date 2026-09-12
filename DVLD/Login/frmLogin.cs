using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.MemoryMappedFiles;
using System.IO;
using DVLD_BLL;
using Microsoft.Win32;
namespace DVLD

{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private clsUser _User;
        // To Save Login Info


        private  void _Login()
        {
            _User = clsUser.FindUserByUserNameAndPassword(txtUserName.Text, txtPassword.Text);

            if (_User!=null)
            {
                    if (_User.IsActive)
                    {
                    if (cbRememberMe.Checked)
                    {
                         clsGlobalSettings.SaveLoginInfoToReg(txtUserName.Text, txtPassword.Text);
                    }
                    else
                    clsGlobalSettings.SaveLoginInfoToReg("","");
                    clsGlobalSettings.UserID=_User.UserID;
                    clsGlobalSettings.CurrentUserInfo = _User;
                    Form frm = new frmMainMenu(this);
                    this.Hide();
                    frm.ShowDialog();
                    }
                    else
                        MessageBox.Show("This Account Is Not Active Contact With Admin To Active This Account", "Account Not Active",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                
                
            }
            else
            {
              
                txtUserName.Focus();
                MessageBox.Show("Invalid UserName Or Password", "Wrong Credential",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void _Load()
        {
            string UserName = "", Password = "";
            if(clsGlobalSettings.ReadLoginIInfoFromReg(ref UserName,ref Password))
            {
                if(UserName!="")
                {
                    btnLogin.Focus();
                    txtUserName.Text = UserName;
                    txtPassword.Text = Password;   
                    cbRememberMe.Checked = true;
                }
            }
            else
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
                cbRememberMe.Checked = false;
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            _Login();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
