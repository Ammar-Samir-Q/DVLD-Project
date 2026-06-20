using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.MemoryMappedFiles;
using DVLD_BLL;

namespace DVLD
{
    public partial class frmAddEditPerson : Form
    {
        enum enMode { eAdd, eEdit };
        private int _PersonID =-1;
        private DVLD_BLL.clsPeople _Person;
        private enMode _Mode;
       
        public delegate void DataBackEventHandler(object obj, int PersonID);
        public event DataBackEventHandler DataBack;


        public frmAddEditPerson()
        {
            InitializeComponent();
            _Mode=enMode.eAdd;
         }
        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();
                _PersonID = PersonID;
                _Mode = enMode.eEdit;
        }

        private void SetDate()
        {
            dtpDateOfBirth.MaxDate=DateTime.Now.AddYears(-18);
            dtpDateOfBirth.MinDate=DateTime.Now.AddYears(-100);
        }
        private void LoadCounrtyInComboBox()
        {
            cbCountry.DataSource = DVLD_BLL.clsCountry.GetAllCountries();
            if (cbCountry.Items.Count <= 0)
                return;

            cbCountry.DisplayMember = "CountryName";
            cbCountry.ValueMember = "CountryID";
            //Yemen
            cbCountry.SelectedIndex = 189;

        }
        private void _SetDefaultImages()
        {

            if (rbMale.Checked)
            {
                pbImage.Image = Properties.Resources.png_Male;
            }
            if (rbFemale.Checked)
            {
                pbImage.Image = Properties.Resources.png_Female;
            }
        }
        
        private void _LoadImage()
        {
            if (_Person==null)
            {
                _SetDefaultImages();
            }
            else
            {
                if (string.IsNullOrEmpty(_Person.ImagePath))
                    _SetDefaultImages();
                else
                     pbImage.ImageLocation = _Person.ImagePath;
                   
            }
          
            _SetRemovelinkVisabilty();
        }
        private void _SetRemovelinkVisabilty()
        {
            linkRemoveImage.Visible=!string.IsNullOrEmpty(pbImage.ImageLocation)?true:_Person==null?false:string.IsNullOrEmpty(_Person.ImagePath)?false:true;
        }

        private void _ResetDefaultValues()
        {
            if(_Mode == enMode.eAdd)
            {
                lblTitle.Text = "Add-Person";
                this.Text = "Add Person";
                _Person=new DVLD_BLL.clsPeople();
            }
            LoadCounrtyInComboBox();
            SetDate();
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationaNO.Text = "";
            txtPhone.Text = "";
            rbMale.Checked = true;
            txtEmail.Text = "";
            txtAddress.Text = "";
            dtpDateOfBirth.Value=dtpDateOfBirth.MaxDate;
        }
        private void _CopyImagePathToProject()
        {
            if(!string.IsNullOrEmpty(_Person.ImagePath))
            {
                string ProjectPath = Application.StartupPath;
                string ImagesFolder = Path.Combine(ProjectPath, "DVLD_PersonImages");
                if (!Directory.Exists(ImagesFolder))
                {
                    Directory.CreateDirectory(ImagesFolder);
                }
                string FileName=Guid.NewGuid().ToString()+Path.GetExtension(_Person.ImagePath);
                string Destnation = Path.Combine(ImagesFolder, FileName);
                File.Copy(_Person.ImagePath, Destnation, true);
                
            }
            else
            {
                _Person.ImagePath = null;
            }
        }

        private void _LoadData()
        {
            _Person = DVLD_BLL.clsPeople.FindPerson(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("Perosn With PerosonID : " + _PersonID.ToString() +
                    " Is Not Found! ", "Not Found", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) ;
                this.Close();
                return;
            }
            lblTitle.Text = "Update-Person";
            this.Text = "Update-Perosn";

            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            //Handel Null
            if (_Person.ThirdName != null)
                txtThirdName.Text = _Person.ThirdName;

            txtLastName.Text = _Person.LastName;
            txtNationaNO.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;
            txtPhone.Text = _Person.Phone;

            //Handel Null
            if (_Person.Email != null)
                txtEmail.Text = _Person.Email;

            cbCountry.SelectedValue = (_Person.NationalityCountryID);
            txtAddress.Text = _Person.Address;
            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                pbImage.ImageLocation = _Person.ImagePath;
            }
            _LoadImage();
            _SetRemovelinkVisabilty();

        }

        private void _Load()
        {
            _ResetDefaultValues();
            if (_Mode == enMode.eEdit)
                _LoadData();
            
        }

        

        private void _Save()
        {
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            //Handel Null
            if (!string.IsNullOrEmpty(txtThirdName.Text))
                _Person.ThirdName = txtThirdName.Text.Trim();
            else
                _Person.ThirdName = "";

            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo=txtNationaNO.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            if(rbMale.Checked==true)
                _Person.Gendor = 0;
            else
                _Person.Gendor = 1;

            _Person.Phone=txtPhone.Text.Trim();

            //Handel Null
            if (!string.IsNullOrEmpty(txtEmail.Text))
            _Person.Email=txtEmail.Text.Trim();
            else
                _Person.Email = "";


            _Person.NationalityCountryID=(Convert.ToInt32(cbCountry.SelectedValue));
            _Person.Address = txtAddress.Text.Trim();
            if (!_HandelImage())
                return;

            if(_Person.Save())
            {
                _Mode = enMode.eEdit;
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
            else
            {
                MessageBox.Show("Error : Faild To Save Data","Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                this.Close();
                return;
            }
            //Delegate Return PerosnID
            DataBack?.Invoke(this, _Person.PersonID);


            lblTitle.Text = "Update-Person";
            this.Text=lblTitle.Text;
            lblPersonID.Text =_Person.PersonID.ToString();
            _PersonID=_Person.PersonID;
        }

        //Handel Image ------------------
     private bool _HandelImage()
        {
            if (_Person.ImagePath == pbImage.ImageLocation)
                return true;

            if(_Person.ImagePath!=pbImage.ImageLocation)
            {
                if (!string.IsNullOrEmpty(_Person.ImagePath) )
                {
                    try
                    {
                        //Delete Old Image
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        //Log it Later
                        return false;
                    }
                }
            }
            if(pbImage.ImageLocation!=null)
            {
                string SourcePath=pbImage.ImageLocation.ToString();
                if(clsUtil.CopyImageToFileLocation( ref SourcePath))
                {
                    
                    
                    pbImage.ImageLocation = SourcePath;
                    _Person.ImagePath=SourcePath;
                    return true;
                }
                else
                {
                    MessageBox.Show("Error While Copying The Image", "Image-Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error); ;
                    return false;
                }
            }

            return true;
        }
        
      
        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _Load();
        }


        private void linkSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd= new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Title = "Choose A Picture";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //_Person.ImagePath=ofd.FileName;
                pbImage.ImageLocation=ofd.FileName;
            }
            _SetRemovelinkVisabilty();
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            _LoadImage();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //_CopyImagePathToProject();
            if (this.ValidateChildren())
                _Save();
            else
                MessageBox.Show("Some Filds Are Invalid", "Validation");
                return;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            _LoadImage();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.ImageLocation = null;
            _Person.ImagePath = null;
            _LoadImage();
            linkRemoveImage.Visible = false;
        }

        //Validation
        private bool _GenralEmptyValidateMethod(TextBox TextBox, string Messeage)
        {
            if (string.IsNullOrEmpty(TextBox.Text))
            {

                epAddEditPerson.SetError(TextBox, Messeage);
                return true;
            }
            else
            {
                epAddEditPerson.SetError(TextBox, null);
                return false;
            }

        }

        //Special Handling
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text == "")
                return;
            if(!DVLD_BLL.clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel=true;
                epAddEditPerson.SetError(txtEmail, "Invalid Email Format");
            }
            else
            {
                epAddEditPerson.SetError(txtEmail, null);
            }
        }

        //Special Handling
        private void txtNationaNO_Validating(object sender, CancelEventArgs e)
        {

            e.Cancel = _GenralEmptyValidateMethod(txtNationaNO, "This Filed Cant Be Null");

            if (DVLD_BLL.clsValidation.IsNationalNoExists(txtNationaNO.Text.Trim()) && _Person.NationalNo != txtNationaNO.Text)
            {
                e.Cancel = true;
                epAddEditPerson.SetError(txtNationaNO, "National No [" + txtNationaNO.Text + "] Is Already Exists");
            }
            else
            {
                e.Cancel = false;
                epAddEditPerson.SetError(txtNationaNO, null);
            }
            }


        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = _GenralEmptyValidateMethod(txtFirstName, "This Filed Cant Be Null");
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = _GenralEmptyValidateMethod(txtSecondName, "This Filed Cant Be Null");
        }

        private void txtThirdName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = _GenralEmptyValidateMethod(txtThirdName, "This Filed Cant Be Null");
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = _GenralEmptyValidateMethod(txtLastName, "This Filed Cant Be Null");
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = _GenralEmptyValidateMethod(txtPhone, "This Filed Cant Be Null");
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = _GenralEmptyValidateMethod(txtAddress, "This Filed Cant Be Null");
        }
    }
    }

