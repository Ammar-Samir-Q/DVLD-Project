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
    public partial class frmListDrivers : Form
    {
        public frmListDrivers()
        {
            InitializeComponent();
        }
       private DataTable _dtDrivers;
        private void _CountRecords()
        {
            lblRecords.Text=dgvDrivers.Rows.Count.ToString();
        }

        private void _LoadDataGridView()
        {
            _dtDrivers= DVLD_BLL.clsDriver.GetAllDrivers();
            dgvDrivers.DataSource = _dtDrivers;
            if(dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width =120 ;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 150;

                dgvDrivers.Columns[2].HeaderText = "National No";
                dgvDrivers.Columns[2].Width = 150;

                dgvDrivers.Columns[3].HeaderText = "FullName";
                dgvDrivers.Columns[3].Width = 400;

                dgvDrivers.Columns[4].HeaderText = "Created Date";
                dgvDrivers.Columns[4].Width = 300;

                dgvDrivers.Columns[5].HeaderText = "Number Of Active Licenses";
                dgvDrivers.Columns[5].Width = 150;
            }



        }

        private void _LoadFilters()
        {
            cbFilters.Items.Clear();
            cbFilters.Items.Add("None");
            cbFilters.Items.Add("Driver ID");
            cbFilters.Items.Add("Person ID");
            cbFilters.Items.Add("NationalNo");
            cbFilters.Items.Add("FullName");
            cbFilters.Items.Add("Number Of Active Licenses");
            cbFilters.SelectedIndex = 0;
        }

        private void _ApplyFilters()
        {
            string filterName =cbFilters.Text.Replace(" ","");
            if(cbFilters.SelectedIndex==0 || txtFilter.Text=="")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                return;
            }
            if(cbFilters.SelectedIndex==3||cbFilters.SelectedIndex==4)
            {
                _dtDrivers.DefaultView.RowFilter = String.Format("{0} Like '{1}%'", filterName, txtFilter.Text); ;
            }
            else
            {
                _dtDrivers.DefaultView.RowFilter = String.Format("{0} = {1}", filterName, txtFilter.Text);
            }



        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _LoadDataGridView();
            _CountRecords();
            _LoadFilters();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            _ApplyFilters();
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear();
            if(cbFilters.SelectedIndex==0)
            {
                txtFilter.Visible = false;
                _dtDrivers.DefaultView.RowFilter = "";
            }
            else
                txtFilter.Visible = true;
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilters.SelectedIndex==1 ||cbFilters.SelectedIndex==2||cbFilters.SelectedIndex==5)
            {
                e.Handled=!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar);
            }
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmPersondetails((int)dgvDrivers.SelectedRows[0].Cells[1].Value);
            frm.ShowDialog();
        }


        private void showLicenseHistoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmListLicenseHistory((int)dgvDrivers.SelectedRows[0].Cells[1].Value);
            frm.ShowDialog();
        }
    }
}
