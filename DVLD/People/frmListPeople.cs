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
    public partial class frmManagePeople : Form
    {

        private DataTable _dtAllPeople = DVLD_BLL.clsPeople.GetAllPeople();
        private DataTable _dtPeople = new DataTable();

        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void _LoadDataGridView()
        {
       
            _dtPeople = _dtAllPeople;
            if(_dtPeople.Rows.Count>0)
            {
                dgvPeople.DataSource= _dtPeople;
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 90;

                dgvPeople.Columns[1].HeaderText = "National No";
                dgvPeople.Columns[1].Width = 90;

                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[3].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 120;

                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gendor";
                dgvPeople.Columns[6].Width = 80;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 120;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;

                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;

                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 193;

                _CountRecords();
            }



        }

        private void _CountRecords()
        {
            lblRecordsvalue.Text = dgvPeople.RowCount.ToString();
        }

        private void _RefreashDataGridView()
        {
            _dtPeople=DVLD_BLL.clsPeople.GetAllPeople();
            dgvPeople.DataSource = _dtPeople;
            _CountRecords();
        }

        private void _LoadFilters()
        {
            cbFilter.Items.Clear();
            cbFilter.Items.Add("None");
            foreach (DataColumn Col in _dtPeople.Columns)
            {
                cbFilter.Items.Add(Col.ColumnName);
            }
            cbFilter.SelectedIndex = 0;
        }

        private void _ApplyFilters()
        {
            string Filter = "";
            Filter = cbFilter.Text.Replace(" ", "");

            if (txtFilter.Text.Trim() == "" || cbFilter.Text == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                _CountRecords();
                return;
            }
            else
            {
                if(Filter=="PersonID")
                {
                    _dtPeople.DefaultView.RowFilter = String.Format("[{0}] ={1}", Filter, txtFilter.Text.Trim());
                }
                else
                {
                    _dtPeople.DefaultView.RowFilter = String.Format("[{0}] LIKE '{1}%'", Filter, txtFilter.Text.Trim());
                }
            }

        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _LoadDataGridView();
            _LoadFilters();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPerson();
            frm.ShowDialog();
            _RefreashDataGridView();
        }

        private void indoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmPersondetails(Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value));
            frm.ShowDialog();
            _RefreashDataGridView();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPerson((int)(dgvPeople.SelectedRows[0].Cells[0].Value));
            frm.ShowDialog();
            _RefreashDataGridView();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPerson();
            frm.ShowDialog();
            _RefreashDataGridView();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Person With ID " + (dgvPeople.SelectedRows[0].Cells["PersonID"].Value).ToString() + " Will Be Deleted Are You Sure?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (DVLD_BLL.clsPeople.DeletePerson(Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value)))
                {
                    MessageBox.Show("Person Deleted SuccessFully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error : Faild To Delete This Perosn", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _RefreashDataGridView();
            }
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon...","Not Emplmented Yet",MessageBoxButtons.OK,MessageBoxIcon.Stop);
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon...", "Not Emplmented Yet", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }




        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0)
            {
                txtFilter.Clear();
                txtFilter.Enabled = false;
                txtFilter.Visible = false;
            }
            else
            {
                txtFilter.Enabled = true;
                txtFilter.Visible = true;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
                  _ApplyFilters();
               // _RefreashDataGridView();
            
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.Text=="PersonID")
            {
                e.Handled=!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar);
            }
        }

        private void showLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmListLicenseHistory((int)dgvPeople.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }

