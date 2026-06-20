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
    public partial class frmTestAppointemnt : Form
    {
        private int _LocalDrivingApplicationID = -1;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private DataTable _dtTestAppointments;
        public frmTestAppointemnt(int LocalDrivingApplication,int TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingApplicationID=LocalDrivingApplication;
            _TestTypeID=(clsTestType.enTestType)TestTypeID;

        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch(_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblTitle.Text = "Vision Test";
                    this.Text = "Schdule "+lblTitle.Text;
                    pbImage.Image = Properties.Resources.png_VisionTest;
                    return;
                case clsTestType.enTestType.WrittenTest:
                    lblTitle.Text = "Written Test";
                    this.Text = "Schdule " + lblTitle.Text;
                    pbImage.Image = Properties.Resources.png_WrittenTest;
                    return;
                case clsTestType.enTestType.StreetTest:
                    pbImage.Image = Properties.Resources.png_StreetTest;
                    lblTitle.Text = "Street Test";
                    this.Text = "Schdule " + lblTitle.Text;
                    return;
            }
        }

        private void _LoadForm()
        {
            _LoadTestTypeImageAndTitle();

            ctrlLocalDrivingApplicationInfo1.LoadLocalDrivingApplicationByID(_LocalDrivingApplicationID);
            _LoadDataGridView();
            _CountRecords();
        }

        private void _CountRecords()
        {
            lblRecords.Text=dgvTestAppointments.Rows.Count.ToString();
        }

        private void _LoadDataGridView()
        {
            _dtTestAppointments = clsTestAppointemnt.GetAllTestAppointmentsByLocalLicenseID(_LocalDrivingApplicationID,(int)_TestTypeID);
            dgvTestAppointments.DataSource = _dtTestAppointments;
            if(dgvTestAppointments.Rows.Count > 0)
            {
                dgvTestAppointments.Columns[0].Width =100;
                dgvTestAppointments.Columns[1].Width = 300;
                dgvTestAppointments.Columns[2].Width = 200;
                dgvTestAppointments.Columns[3].Width = 250;
            }

        }

        private void _RefreashDataGridView()
        {
            _LoadDataGridView();
           // _dtTestAppointments =clsTestAppointemnts.GetAllTestAppointmentsByLocalLicenseID(_LocalDrivingApplicationID,(int)_TestTypeID);
        }

        
        private void frmTestAppointemnt_Load(object sender, EventArgs e)
        {
            _LoadForm();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            clsLocalDrivingApplication LocalDrivingApplication = clsLocalDrivingApplication.FindLocalDrivingApplication(_LocalDrivingApplicationID);
            if(LocalDrivingApplication != null)
            {
                if(LocalDrivingApplication.IsThereAnActiveSchedualTest((int)_TestTypeID))
                {
                    MessageBox.Show("Person Already Have an Active Test Appointment", "Already Exists",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return;
                }
                if (LocalDrivingApplication.DosePassTestType((int)_TestTypeID))
                {
                    MessageBox.Show("Person Already Passed This Test", "Already Passed",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    Form frm = new frmAddEditTestAppointment(_LocalDrivingApplicationID, (int)_TestTypeID);
                    frm.ShowDialog();
                    _RefreashDataGridView();

                }

            }      
        }

        private void editTestAppointemntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditTestAppointment(_LocalDrivingApplicationID,(int)_TestTypeID, (int)dgvTestAppointments.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
            _RefreashDataGridView();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmTakeTest((int)dgvTestAppointments.SelectedRows[0].Cells[0].Value,_TestTypeID);
            frm.ShowDialog();
            _RefreashDataGridView();
            ctrlLocalDrivingApplicationInfo1.LoadLocalDrivingApplicationByID(_LocalDrivingApplicationID);
        }

        private void cmTestAppointments_Opening(object sender, CancelEventArgs e)
        {
            if(clsTestAppointemnt.Find((int)dgvTestAppointments.SelectedRows[0].Cells[0].Value).IsLocked==true)
            {
                editTestAppointemntToolStripMenuItem.Enabled = false;
                takeTestToolStripMenuItem.Enabled=false;
            }
            else
            {
                editTestAppointemntToolStripMenuItem.Enabled = true;
                takeTestToolStripMenuItem.Enabled = true;
            }
        }
    }
}
