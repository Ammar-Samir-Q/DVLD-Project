using DVLD_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmAddEditUser : Form
    {
        enum enMode { eAdd,eUpdate}
       private int _UserID = -1;
       private DVLD_BLL.clsUser _User;
        private enMode _Mode;

        //Constructors 
        public frmAddEditUser()
        {
            InitializeComponent();
            _Mode = enMode.eAdd;
        }
        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = enMode.eUpdate;
        }

        //Main Methods
        private void _LoadData()
        {
            _User = DVLD_BLL.clsUser.FindUserByUserID(_UserID);
            if (_User == null)
            {
                MessageBox.Show("User Not Found", "Error");
                this.Close();
                return;
            }
            lblTitle.Text = "Update User";
            this.Text = "Update User";
            ctrlPersonCardWithFilter1.EnableFilter = false;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _UserID.ToString();
            txtUserName.Text = _User.UserName;
            cbIsActive.Checked = _User.IsActive;

        }

        private void _Save()
        {
            
            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password =txtPassword.Text;
           _User.IsActive = cbIsActive.Checked;

            if (_User.Save())
            {
                _Mode = enMode.eUpdate;
                MessageBox.Show("User Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _UserID = _User.UserID;
                _LoadData();
            }
            else
            {
                MessageBox.Show("Faild To Save User", "Error While Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void _NextButton()
        {
            if (_Mode == enMode.eAdd)
            {
                if (ctrlPersonCardWithFilter1.PersonID > 0)
                {
                    if (!DVLD_BLL.clsUser.IsPersonHaveUser(ctrlPersonCardWithFilter1.PersonID))
                    {
                        ctrlPersonCardWithFilter1.EnableFilter=false;
                        btnNext.Visible = false;
                        btnSave.Enabled = true;
                        tc.TabPages.Add(tpUserInfo);
                        tc.SelectedTab = tpUserInfo;
                    }
                    else
                    {
                        MessageBox.Show("Person ID :" + ctrlPersonCardWithFilter1.PersonID.ToString() + " Is Already have Account", "Person",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Person First", "Select Person",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    tc.TabPages.Remove(tpUserInfo);
                }
            }
            else
            {
                tc.SelectedIndex = 1;
                btnNext.Visible = false; 
            }
        }

        private void _ResetValues()
        {
            _SetTabControl();
            if (_Mode == enMode.eAdd)
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add User";
                _User = new DVLD_BLL.clsUser();
                _UserID = -1;
                return;
            }
            

        }
        private void _SetTabControl()
        {
            if(_Mode == enMode.eAdd)
            {
                tc.TabPages.Remove(tpUserInfo);
                btnSave.Enabled = false;
                ctrlPersonCardWithFilter1.EnableFilter = true;
            }
            else
            {
                
                ctrlPersonCardWithFilter1.EnableFilter = false;
                btnSave.Enabled = true;
            }
        }
        
        //----------

        private void btnNext_Click(object sender, EventArgs e)
        {
            _NextButton();
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetValues();
            if(_Mode== enMode.eUpdate)
            _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                _Save();
            }
            else
                MessageBox.Show("some filds are not valid","Validation Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //Validation

        private bool _GenralEmptyValidateMethod(TextBox TextBox, string Messeage)
        {
            if (string.IsNullOrEmpty(TextBox.Text))
            {

                epUsers.SetError(TextBox, Messeage);
                return true;
            }
            else
            {
                epUsers.SetError(TextBox, null);
                return false;
            }

        }


        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (DVLD_BLL.clsUser.IsUserExists(txtUserName.Text.Trim()))
                {
    
              epUsers.SetError(txtUserName, "UserName Already Exists");

                }
            else
                epUsers.SetError(txtUserName, null);
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel= _GenralEmptyValidateMethod(txtUserName, "This Filed Cant Be Null");
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = _GenralEmptyValidateMethod(txtPassword, "This Filed Cant Be Null");
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = _GenralEmptyValidateMethod(txtConfirmPassword, "This Filed Cant Be Null");
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                epUsers.SetError(txtConfirmPassword, "The Password Isnt Correct");
            }
            else
            {
                e.Cancel = false;
                epUsers.SetError(txtConfirmPassword, null);
            }

        }
    }
}
