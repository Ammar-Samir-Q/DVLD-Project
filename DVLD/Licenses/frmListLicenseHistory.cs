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
    public partial class frmListLicenseHistory : Form
    {
        private int _PersonID = -1;
        public frmListLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            
        }
        public frmListLicenseHistory()
        {
            InitializeComponent();
            ctrlPersonCardWithFilter1.EnableFilter=true;
        }

        private void _LoadData()
        {
            if(_PersonID>0)
            {
                ctrlPersonCardWithFilter1.EnableFilter =false;
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlLicenseHistoryInfo1.LoadInfoByPersonID(_PersonID);
            }
            else
            {
                ctrlPersonCardWithFilter1.EnableFilter = true;
                ctrlPersonCardWithFilter1.FilterFocus();
            }

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID=obj;
            if (_PersonID <= 0)
            {
                ctrlLicenseHistoryInfo1.Clear();
            }
            else
                ctrlLicenseHistoryInfo1.LoadInfoByPersonID(_PersonID);

        }

        private void frmListLicenseHistory_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
    }
}
