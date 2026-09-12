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
namespace DVLD
{
    public partial class ctrlPersonInfo : UserControl
    {
        private clsPeople _Person;

        private int _PersonID = -1;

        
        public int PersonID { 
            get { return _PersonID; }
           
        }
        
        public clsPeople PersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonInfo()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {
            _Person=clsPeople.FindPerson(PersonID);
            if (_Person!=null)
            {
                _LoadPersonInfo();
                _PersonID = PersonID;
            }
            else
            {
                MessageBox.Show("No Person With PersonID [" + PersonID.ToString()+ "] Has Found", "Not Found");
                _PersonID = -1;
                _ResetPerosnInfo();
                return;
            }
        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = DVLD_BLL.clsPeople.FindPerson(NationalNo);
            if (_Person != null)
            {
                _PersonID=_Person.PersonID;
                _LoadPersonInfo();
            }
            else
            {
                MessageBox.Show("No Person With NationalNo ["+NationalNo+"] Has Found", "Not Found");
                _ResetPerosnInfo();
                return;
            }
           

        }

        


        private void _LoadPersonInfo()
        {
                 linkEditPerson.Enabled = true;
                lblPersonID.Text = _Person.PersonID.ToString();
                lblName.Text = _Person.FullName();
                lblNationalNo.Text = _Person.NationalNo.ToString();
                lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
                lblPhone.Text = _Person.Phone.ToString();
                lblEmail.Text = string.IsNullOrEmpty(_Person.Email)?"No Email":_Person.Email;
            //Handel Address Length To Solve Problem (Address Interbutes With Image)
                if(_Person.Address.Length>=22)
                {
                lblAddress.Visible = false;
                txtAddress.Visible = true;
                txtAddress.Text = _Person.Address.Trim();
                }
                else
                    {
                lblAddress.Visible = true;
                txtAddress.Visible = false;
                lblAddress.Text=_Person.Address.Trim();
                     }
                      
                lblBirthOfDate.Text = _Person.DateOfBirth.ToShortDateString();
                lblCountry.Text = _Person.CountryInfo.CountryName;
                if (!string.IsNullOrEmpty(_Person.ImagePath))
                {
                if (File.Exists(_Person.ImagePath))
                    pbPersonImage.ImageLocation = _Person.ImagePath.ToString();
                
                }
                else
                {
                    if (_Person.Gendor == 0)
                        pbPersonImage.Image = Properties.Resources.png_Male;
                    else
                        pbPersonImage.Image= Properties.Resources.png_Female;
                }
            }

        private void _ResetPerosnInfo()
        {
            linkEditPerson.Enabled=false;
            lblPersonID.Text = "???";
            lblName.Text = "???";
            lblNationalNo.Text = "???";
            lblGendor.Text = "???";
            lblPhone.Text = "???";
            lblEmail.Text = "???";
            lblAddress.Text = "???";
            lblBirthOfDate.Text = "???";
            lblCountry.Text = "???";
            pbPersonImage.Image = Properties.Resources.png_Male;
            
            
        }


        private void linkEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmAddEditPerson(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
