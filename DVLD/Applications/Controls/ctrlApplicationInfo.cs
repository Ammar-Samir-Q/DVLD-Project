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
    public partial class ctrlApplicationInfo : UserControl
    {
       
        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        private int _ApplicationID = -1;
        private DVLD_BLL.clsApplication _Application;
        
        public int ApplicationID { get { return _ApplicationID; } }
        public DVLD_BLL.clsApplication ApplicationInfo { get { return _Application; } }

        public void LoadApplication(int ApplicationID)
        {
            this._ApplicationID=ApplicationID;
            _LoadInfo();
        }

        private void _LoadInfo()
        {
            if(_ApplicationID>0)
            {
                _Application = DVLD_BLL.clsApplication.FindBaseApplication(_ApplicationID);
                if(_Application!=null)
                {
                    lblApplicationID.Text = _Application.ApplicationID.ToString();
                    lblFullName.Text=_Application.PersonInfo.FullName();
                    lblAppDate.Text=_Application.ApplicationDate.ToShortDateString();
                    lblAppType.Text = _Application.ApplicationTypeInfo.ApplicationTypeTitle.ToString();
                    lblAppStatus.Text = (int)_Application.ApplicationStatus==1?"New": (int)_Application.ApplicationStatus == 2 ?"Cancelled":"Completed";
                    lblLastStatus.Text=_Application.LastStatusDate.ToString();
                    lblPaidFees.Text = _Application.PaidFees.ToString();
                    lblCreatedByUser.Text = _Application.UserInfo.UserName;
                    linklblPersonInfo.Visible=true;

                    return;
                }
                else
                {
                    linklblPersonInfo.Visible = false;
                    MessageBox.Show("Faild To Find The Application With ApplicationID ="+_ApplicationID+" ...", "Error");
                    return;
                }

            }
            else
            {
                MessageBox.Show("Cant Load Application Correctly", "Error");
                return ;
            }

        }

        private void linklblPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm =new frmPersondetails(_Application.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
