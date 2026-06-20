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
    public partial class frmPersondetails : Form
    {
        public frmPersondetails(int PersonID)
        {
            InitializeComponent();
            ctrlPersonInfo1.LoadPersonInfo(PersonID);

        }

        public frmPersondetails(string NationalNo)
        {
            InitializeComponent();
            ctrlPersonInfo1.LoadPersonInfo(NationalNo);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
