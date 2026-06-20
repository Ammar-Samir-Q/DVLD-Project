using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.IO;
using DVLD_BLL;

namespace DVLD
{
    public partial class ctrlLicenseInfo : UserControl
    {
        private int _LicenseID = -1;
        private clsLicense _License;

        public ctrlLicenseInfo()
        {
            InitializeComponent();
        }

        public int LicenseID { get { return _LicenseID; } }
        public clsLicense LicenseInfo { get { return _License; } } 

        public void LoadLicenseInfo(int LicenseID)
        {
            _LicenseID=LicenseID;
            if (_LicenseID > 0)
                _LoadData();
        }

        private void _LoadPersonImage()
        {
            if (string.IsNullOrEmpty(_License.ApplicationInfo.PersonInfo.ImagePath))
            {
                pbImage.Image = _License.ApplicationInfo.PersonInfo.Gendor == 0 ? Properties.Resources.png_Male : Properties.Resources.png_Female;
            }
            else
                if (File.Exists(_License.ApplicationInfo.PersonInfo.ImagePath))
                pbImage.Load(_License.ApplicationInfo.PersonInfo.ImagePath);
            else
                MessageBox.Show("Could Not Found The Person Image..."+ _License.ApplicationInfo.PersonInfo.ImagePath, "Error");



        }


        public void SetIsDetained(bool IsDetained)
        {
            lblIsDetained.Text = IsDetained ? "Yes" : "No";
        }
        private void _LoadData()
        {
            _License = clsLicense.FindLicense(_LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Could Not Found The License Info ...", "Error");
                _LicenseID = -1;
                return;
            }


                lblLicenseID.Text =_License.LicenseID.ToString();
                lblClassName.Text = _License.LicesneClassInfo.ClassName;
                lblName.Text = _License.DriverInfo.PersonInfo.FullName();
                lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
                lblGendor.Text = _License.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
                lblIssueDate.Text = _License.IssueDate.ToShortDateString();
                lblIssueReason.Text = _License.GetIssueReasonText();
                lblNotes.Text = _License.Notes.ToString()==""?"No Notes":_License.Notes.ToString();
                lblActive.Text = _License.IsActive ==true ? "Yes" : "No";
                lblDateOfBirth.Text = _License.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
                lblExpirationDate.Text=_License.ExpirationDate.ToShortDateString();
                lblDriverID.Text = _License.DriverID.ToString();
                //Handle This Later
                lblIsDetained.Text = _License.IsDetained()?"Yes":"No";


            _LoadPersonImage();



        }


    }
}
