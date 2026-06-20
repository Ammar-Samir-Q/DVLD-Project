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
    public partial class frmAddEditTestAppointment : Form
    {
        private int _LocalDrivingApplicationID = -1;
        private int _TestTypeID = -1;
        private int _TestAppointmentID = -1;


        public frmAddEditTestAppointment(int LocalDrivingApplicationID,int TestTypeID,int TestAppointment=-1)
        {
         InitializeComponent();
          _LocalDrivingApplicationID=LocalDrivingApplicationID;
          _TestTypeID=TestTypeID;
          _TestAppointmentID=TestAppointment;
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddEditTestAppointment_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestType = (ctrlScheduleTest.enTestType)_TestTypeID;
            ctrlScheduleTest1.LoadInfo(_LocalDrivingApplicationID,_TestAppointmentID);
        }
    }
}
