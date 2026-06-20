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
    public partial class frmListDetainedLicenses : Form
    {
        private DataTable _dtDetaindLicenses;

        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void _LoadDataGridView()
        {
           _dtDetaindLicenses=clsDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource= _dtDetaindLicenses;

            if(dgvDetainedLicenses.Rows.Count>0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 100;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 100;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 150;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 100;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 100;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 150;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.NO";
                dgvDetainedLicenses.Columns[6].Width = 80;

                dgvDetainedLicenses.Columns[7].HeaderText = "FullName";
                dgvDetainedLicenses.Columns[7].Width = 230;

                dgvDetainedLicenses.Columns[8].HeaderText = "Release App ID";
                dgvDetainedLicenses.Columns[8].Width = 130;
            }
            

        }

        private void _LoadComboBoxs()
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("Detain ID");
            cbFilter.Items.Add("Is Released");
            cbFilter.Items.Add("National No");
            cbFilter.Items.Add("Full Name");
            cbFilter.Items.Add("Release Application ID");
            cbFilter.SelectedIndex = 0;

            cbIsReleased.Items.Add("All");
            cbIsReleased.Items.Add("Yes");
            cbIsReleased.Items.Add("No");
            cbIsReleased.SelectedIndex = 0;
        }

        private void _ApplayFilters()
        {
            string Filter=cbFilter.Text.Replace(" ","");
            
            if(Filter==""||txtFilter.Text=="")
            {
                _dtDetaindLicenses.DefaultView.RowFilter = "";
                return;
            }

           

            if (cbFilter.SelectedIndex==3||cbFilter.SelectedIndex == 4)
            {
                _dtDetaindLicenses.DefaultView.RowFilter=String.Format("{0} Like '{1}%'",Filter,txtFilter.Text);  
            }
            else
            {
                _dtDetaindLicenses.DefaultView.RowFilter = String.Format("{0} = {1}", Filter, txtFilter.Text);  
            }

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilter.SelectedIndex==2)
            {
                cbIsReleased.Visible=true;
                txtFilter.Visible=false;
                return;
            }

            if(cbFilter.SelectedIndex==0)
            {
                txtFilter.Clear();
                txtFilter.Visible=false;
                return;
            }

            txtFilter.Clear();
                txtFilter.Visible = true;

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if(cbFilter.SelectedIndex!=0)
            _ApplayFilters();
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedIndex== 3 || cbFilter.SelectedIndex == 4)
                return;
           
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            
        }
        private void _CountRecords()
        {
            lblRecordsvalue.Text=dgvDetainedLicenses.Rows.Count.ToString();
        }
        private void _Refreashdgv()
        {
            _dtDetaindLicenses=clsDetainedLicense.GetAllDetainedLicenses();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _LoadComboBoxs();
            _LoadDataGridView();
            _CountRecords();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetainLicense();
            frm.ShowDialog();
            _Refreashdgv();
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            Form frm=new frmReleaseLicense();
            frm.ShowDialog();
            _Refreashdgv();
        }

        private void showPerosnDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmPersondetails(clsLicense.FindLicense((int)dgvDetainedLicenses.SelectedRows[0].Cells[1].Value).DriverInfo.PersonID);
            frm.ShowDialog();   
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmLicenseInfo((int)dgvDetainedLicenses.SelectedRows[0].Cells[1].Value);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListLicenseHistory(clsLicense.FindLicense((int)dgvDetainedLicenses.SelectedRows[0].Cells[1].Value).DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)dgvDetainedLicenses.SelectedRows[0].Cells[3].Value;
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseLicense((int)dgvDetainedLicenses.SelectedRows[0].Cells[1].Value);
            frm.ShowDialog();
            _Refreashdgv();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cbFilter.SelectedIndex == 2)
            {
                if (cbIsReleased.SelectedIndex == 0)
                    return;

                _dtDetaindLicenses.DefaultView.RowFilter = String.Format("{0} = {1}", "IsReleased", cbIsReleased.SelectedIndex == 1 ? "1" : "0");
            }
        }
    }
}
