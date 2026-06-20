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
    public partial class ctrlLicenseHistoryInfo : UserControl
    {       
        private int _DriverID = -1;
        private clsDriver _Driver;

        private DataTable _dtDriverLocalLicenseHistory;
        private DataTable _dtDriverInternationalLicenseHistory;

        public ctrlLicenseHistoryInfo()
        {
            InitializeComponent();
        }

        public int DriverID { get { return _DriverID; } }
        public clsDriver DriverInfo { get { return _Driver; } }

        public void LoadInfo(int DriverID)
        {
            _DriverID=DriverID;
            if(_DriverID>0)
            {
                _LoadData();
            }

        }
        public void LoadInfoByPersonID(int PersonID)
        {
            _Driver=clsDriver.FindDriverByPersonID(PersonID);
            if(_Driver!=null)
            {
                _DriverID = _Driver.DriverID;
                _LoadLocalLicensesDataGrid();
                _LoadInternationalDataGrid();
                _CountRecords();
            }
            else
            {
                MessageBox.Show("There Is No Driver Linked With PersonID : " + PersonID , "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

        }

        public void Clear()
        {
            if (_dtDriverInternationalLicenseHistory != null && _dtDriverLocalLicenseHistory != null)
            {
                _dtDriverInternationalLicenseHistory.Clear();
                _dtDriverLocalLicenseHistory.Clear();
            }
        }

        private void _CountRecords()
        {
            lblRecords.Text =dgvLicenses.Rows.Count.ToString();
            lblRecordsInter.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void _LoadInternationalDataGrid()
        {
            _dtDriverInternationalLicenseHistory= clsInternationaLicense.GetAllInternationalLicensesForPersonID(_Driver.PersonInfo.PersonID);
            dgvInternationalLicenses.DataSource = _dtDriverInternationalLicenseHistory;
            if(dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Inter License ID";
                dgvInternationalLicenses.Columns[0].Width = 150;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 120;

                dgvInternationalLicenses.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[2].Width = 100;

                dgvInternationalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[3].Width = 200;

                dgvInternationalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[4].Width = 200;

                dgvInternationalLicenses.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[5].Width = 100;
            }
        }
        private void _LoadLocalLicensesDataGrid()
        {
            _dtDriverLocalLicenseHistory= clsLicense.GetAllLicensesForPersonID(_Driver.PersonInfo.PersonID);
            dgvLicenses.DataSource=_dtDriverLocalLicenseHistory;
            if (dgvLicenses.Rows.Count > 0)
            {
                dgvLicenses.Columns[0].HeaderText = "Lic ID";
                dgvLicenses.Columns[0].Width =90;

                dgvLicenses.Columns[1].HeaderText = "App Id";
                dgvLicenses.Columns[1].Width = 85;

                dgvLicenses.Columns[2].HeaderText = "Class Name";
                dgvLicenses.Columns[2].Width = 200;

                dgvLicenses.Columns[3].HeaderText = "Issue Date";
                dgvLicenses.Columns[3].Width = 200;

                dgvLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvLicenses.Columns[4].Width = 200;

                dgvLicenses.Columns[5].HeaderText = "Is Active";
                dgvLicenses.Columns[5].Width = 100;
            }

        }
        private void _LoadData()
        {
            _Driver=DVLD_BLL.clsDriver.FindDriver(_DriverID);
            if(_Driver!=null )
            {
                _LoadLocalLicensesDataGrid();
                _LoadInternationalDataGrid();
                _CountRecords();
            }
            else
            {
                MessageBox.Show("There Is No Driver With DriverID : " + _DriverID + " ...", "Error");
                return;
            }
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm =new frmLicenseInfo((int)dgvLicenses.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmInternationalLicenseInfo((int)dgvInternationalLicenses.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
        }

       
    }
}
