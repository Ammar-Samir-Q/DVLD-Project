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
    public partial class frmListTestTypes : Form
    {
        private DataTable _AllTestType;

        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void _Load()
        {
            _AllTestType=DVLD_BLL.clsTestType.GetAllTestTypes();
            if( _AllTestType != null )
            {
                dgvTestTypes.DataSource = _AllTestType;
                if(dgvTestTypes.Rows.Count > 0)
                {
                    dgvTestTypes.Columns[0].HeaderText = "ID";
                    dgvTestTypes.Columns[0].Width = 50;

                    dgvTestTypes.Columns[1].HeaderText = "Title";
                    dgvTestTypes.Columns[1].Width = 200;

                    dgvTestTypes.Columns[2].HeaderText = "Description";
                    dgvTestTypes.Columns[2].Width = 350;

                    dgvTestTypes.Columns[3].HeaderText = "Fees";
                    dgvTestTypes.Columns[3].Width = 135;

                }
            }

        }
        private void _RefreashData()
        {
            _AllTestType = DVLD_BLL.clsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = _AllTestType;
        }

        private void _CountRecords()
        {
            lblRecords.Text = dgvTestTypes.Rows.Count.ToString();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _Load();
            _CountRecords();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmEditTestTypes((int)dgvTestTypes.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
            _RefreashData();
        }

      
    }
}
