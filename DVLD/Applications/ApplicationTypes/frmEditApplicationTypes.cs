using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.ApplicationTypes
{
    public partial class frmEditApplicationType : Form
    {
        
        private int _ApplicationTypeID = -1;
        private DVLD_BLL.clsApplicationType _ApplicationType;
        
        public frmEditApplicationType(int AppTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = AppTypeID;
        }

        private void _Load()
        {
            if(_ApplicationTypeID>0)
            {
               _ApplicationType=DVLD_BLL.clsApplicationType.Find(_ApplicationTypeID);
                if( _ApplicationType!=null )
                {
                    lblID.Text = _ApplicationType.ApplicationTypeID.ToString();
                    txtTitle.Text = _ApplicationType.ApplicationTypeTitle.ToString();
                    txtFees.Text = _ApplicationType.ApplicationTypeFee.ToString();
                }
                else
                {
                    MessageBox.Show("Error : Application Type Not Found","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                this.Close();
                return;
            }


        }

        private void _Save()
        {
            _ApplicationType.ApplicationTypeID = _ApplicationTypeID;
            _ApplicationType.ApplicationTypeTitle=txtTitle.Text.Trim();
            _ApplicationType.ApplicationTypeFee = float.Parse(txtFees.Text.Trim());

            if (_ApplicationType.Save())
            {
                MessageBox.Show("Data Saved Succussfully", "Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error : Faild To Save Data","Faild",MessageBoxButtons.OK,MessageBoxIcon.Error);


        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled=!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar);
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel=true;
                ep.SetError(txtFees, "This Field Cant Be Null");
            }
            else
            {
                e.Cancel = false;
                ep.SetError(txtFees, null);
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;
                ep.SetError(txtTitle, "This Field Cant Be Null");
            }
            else
            {
                e.Cancel = false;
                ep.SetError(txtTitle, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some Fileds Are Not Valid","Validation Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            _Save();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
