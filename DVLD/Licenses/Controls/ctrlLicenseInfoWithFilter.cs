using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BLL;
namespace DVLD
{
    public partial class ctrlLicenseInfoWithFilter : UserControl
    {
        public ctrlLicenseInfoWithFilter()
        {
            InitializeComponent();
        }
        //Event 
        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> Handler = OnLicenseSelected;
            if (Handler != null)
                Handler(LicenseID);
        }
        //----------------------------------------------------------
        private int _LicenseID = -1;
        
        public int LicenseID { get { return ctrlLicenseInfo1.LicenseID; } }

        public clsLicense LicenseInfo { get { return ctrlLicenseInfo1.LicenseInfo; } }

        public bool FilterEnabled(bool Enabled)
        {
            txtFilters.Enabled = Enabled;
            btnFind.Enabled = Enabled;
            lblLicense.Enabled = Enabled;
            return Enabled;
        }

        public void SetIsDetained(bool IsDetained)
        {
            ctrlLicenseInfo1.SetIsDetained(IsDetained);
        }

        public void FilterTextIDFoucs()
        {
            txtFilters.Focus();
        }
      
        private void _Find()
        {
            if(txtFilters.Text==null)
            {
                txtFilters.Focus();
                MessageBox.Show("Please Enter License ID First","Enter LicenseID",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
             _LicenseID= int.Parse(txtFilters.Text);
            if (_LicenseID > 0)
            {
                    ctrlLicenseInfo1.LoadLicenseInfo(_LicenseID);
                if(ctrlLicenseInfo1.LicenseInfo==null)
                {
                    _LicenseID = -1;
                }

                if (OnLicenseSelected != null)
                    LicenseSelected(_LicenseID);         
            }
            else
                MessageBox.Show("License With ID [ " + _LicenseID + " ] Not Found", "Not Found",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        public void LoadLicense(int LicenseID)
        {
            txtFilters.Text = LicenseID.ToString();
            ctrlLicenseInfo1.LoadLicenseInfo(LicenseID);
            _LicenseID=LicenseID;   
            if (OnLicenseSelected != null&&txtFilters.Enabled==true)
                LicenseSelected(LicenseID);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            _Find();
        }

        private void txtFilters_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled=!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar);

            if(e.KeyChar ==(char)13)
            {
                btnFind.PerformClick();
            }
        }

       
    }
}
