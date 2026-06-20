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
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }
        private int _InternationaLicenseID = -1;
        private DVLD_BLL.clsInternationaLicense _InternationalLicenses;

        public int InternationalLicenseID { get { return _InternationaLicenseID; } }
        public DVLD_BLL.clsInternationaLicense InternationaLicenseInfo { get { return _InternationalLicenses;} }

        private void _LoadData()
        {
            _InternationalLicenses = DVLD_BLL.clsInternationaLicense.FindInternationalLicense(_InternationaLicenseID);
            if (_InternationalLicenses != null)
            {
                lblName.Text = _InternationalLicenses.PersonInfo.FullName();
                lblInternationalLicenseID.Text = _InternationalLicenses.InternationalLicenseID.ToString();
                lblLicenseID.Text = _InternationalLicenses.IssueUsingLicenseID.ToString();
                lblNationalNo.Text = _InternationalLicenses.PersonInfo.NationalNo;
                lblGendor.Text = _InternationalLicenses.PersonInfo.Gendor == 1 ? "Male" : "Female";
                lblIssueDate.Text = _InternationalLicenses.IssueDate.ToShortDateString();
                lblApplicationID.Text = _InternationalLicenses.ApplicationID.ToString();
                lblIsActive.Text = _InternationalLicenses.IsActive == true ? "Yes" : "No";
                lblDateOfBirth.Text = _InternationalLicenses.PersonInfo.DateOfBirth.ToShortDateString();
                lblDriverID.Text = _InternationalLicenses.DriverID.ToString();
                lblExpirationDate.Text = _InternationalLicenses.ExpirationDate.ToShortDateString();

                if (!string.IsNullOrEmpty(_InternationalLicenses.PersonInfo.ImagePath))
                {
                    pbPersonImage.ImageLocation = _InternationalLicenses.PersonInfo.ImagePath;
                }
                else
                {
                    if (_InternationalLicenses.PersonInfo.Gendor == 0)
                        pbPersonImage.Image = Properties.Resources.png_Male;
                    else
                        pbPersonImage.Image = Properties.Resources.png_Female;


                }

            }
            else
                MessageBox.Show("Faild To Load International Licecnse","Error");
        }

        public void LoadInternationalLicense(int InternationalLicenseID)
        {
            _InternationaLicenseID=InternationalLicenseID;
            if(InternationalLicenseID>0)
            {
                _LoadData();
            }
        }


    }
}
