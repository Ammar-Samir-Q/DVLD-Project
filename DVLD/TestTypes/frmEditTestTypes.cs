using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.TestTypes
{
    public partial class frmEditTestTypes : Form
    {
        private int _TestTypeID = -1;
        private DVLD_BLL.clsTestType _Test;

        public frmEditTestTypes(int TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void _Load()
        {
            if(_TestTypeID>0)
            {
                _Test=DVLD_BLL.clsTestType.Find(_TestTypeID);
                if(_Test!=null)
                {
                    lblID.Text = _Test.ID.ToString();
                    txtTitle.Text = _Test.Title;
                    txtDescription.Text= _Test.Description; 
                    txtFees.Text = _Test.Fees.ToString();
                }
                else
                {
                    MessageBox.Show("Error :Test type Not Found", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }
        }

        private void _Save()
        {
            _Test.Title = txtTitle.Text.Trim();
            _Test.Description= txtDescription.Text.Trim();
            _Test.Fees=float.Parse(txtFees.Text.Trim());
            
            if(_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error : Faild To Save This Data","Faild",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }


        }

        private void frmEditTestTypes_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                _Save();
            }
            else
                MessageBox.Show("Some Fields Are Not Valid");
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;
                ep.SetError(txtTitle, "Please Enter Title");

            }
            else
            {
                e.Cancel=false;
                ep.SetError(txtTitle, null);
            }
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                e.Cancel = true;
                ep.SetError(txtDescription, "This Filed Can't Be Null");

            }
            else
            {
                e.Cancel = false;
                ep.SetError(txtDescription, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel = true;
                ep.SetError(txtFees, "Please Enter Test Fees");

            }
            else
            {
                e.Cancel = false;
                ep.SetError(txtFees, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
