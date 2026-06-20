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
    public partial class ctrlUserInfo : UserControl
    {
        public ctrlUserInfo()
        {
            InitializeComponent();
        }
        private int _UserID = -1;
        private DVLD_BLL.clsUser _User;

        public int UserID
            {
            get { return _UserID; }
              }
        public DVLD_BLL.clsUser User
        { get { return _User; } }


        public void LoadUserByUserID(int UserID)
        {
            _UserID= UserID;
            _User=DVLD_BLL.clsUser.FindUserByUserID(UserID);
            if(!_Load())
            {
                _Reset();
                MessageBox.Show("User With User ID " + UserID.ToString() + " Not Found", "Error");
                return;
            }
        }

        public void LoadUserByPersonID(int PersonID)
        {
            _User = DVLD_BLL.clsUser.FindUserByPersonID(PersonID);
            if (!_Load())
            {
                _Reset();
                MessageBox.Show("User With Person ID " + UserID.ToString() + " Not Found", "Error");
                return;
            }
        }

        private bool _Load()
        {
        if(_User != null)
            {
                ctrlPersonInfo1.LoadPersonInfo(_User.PersonID);
                lblUserID.Text = _User.UserID.ToString();
                lblUserName.Text = _User.UserName.ToString();
                lblIsActive.Text = _User.IsActive==true?"Active":"UnActive";
                return true;
            }
            return false;
        }

        private void _Reset()
        {
            
            lblIsActive.Text = "---";
            lblUserID.Text = "---";
            lblUserName.Text = "---";
        }

    }
}
