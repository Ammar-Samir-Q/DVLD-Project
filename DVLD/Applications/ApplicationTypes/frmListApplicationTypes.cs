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
    public partial class frmListApplicationTypes : Form
    {
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }
        private DataTable _dtAllTypes; 

        private void _CountRecords()
        {
            if(dgvApplicationTypes.Rows.Count > 0)
            lblRecords.Text =dgvApplicationTypes.Rows.Count.ToString();
        }

        private void _LoadDataGridView()
        {
            _dtAllTypes = DVLD_BLL.clsApplicationType.GetAllApplicationTypes();
            if( _dtAllTypes == null )
            {
                MessageBox.Show("Data Not Found..", "Error");
                return;
            }
            dgvApplicationTypes.DataSource = _dtAllTypes;
            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 120;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 535;

                dgvApplicationTypes.Columns[2].HeaderText = "Fee";
                dgvApplicationTypes.Columns[2].Width = 200;
            }
        }

        private void _RefreashData()
        {
            _dtAllTypes=DVLD_BLL.clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource= _dtAllTypes;
            _CountRecords();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _LoadDataGridView();
            _CountRecords();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmEditApplicationType((int)dgvApplicationTypes.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
            _RefreashData();
        }
    }
}
