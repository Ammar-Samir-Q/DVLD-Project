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
    public partial class frmListUsers : Form
    {

        static private DataTable _dtAllUsers;


        public frmListUsers()
        {
            InitializeComponent();
        }

        private void _LoadDataGridView()
        {
            _dtAllUsers=DVLD_BLL.clsUser.GetAllUsers();
            if (_dtAllUsers != null)
            {
                dgvUsers.DataSource = _dtAllUsers;
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 200;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 200;

                dgvUsers.Columns[2].HeaderText = "FullName";
                dgvUsers.Columns[2].Width = 400;

                dgvUsers.Columns[3].HeaderText = "UserName";
                dgvUsers.Columns[3].Width = 300;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 150;

            }

        }

        private void _RefreashDataGridView()
        {
            _dtAllUsers = DVLD_BLL.clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            _CountRecords();
        }

        private void _LoadCB()
        {
            cbFilter.Items.Add("None");
            foreach (DataColumn Col in _dtAllUsers.Columns)
            {
                cbFilter.Items.Add(Col.ToString());
            }
            cbFilter.SelectedIndex = 0;
        }

        private void _CountRecords()
        {
            lblRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void _ApplyFilters()
        {

            if (cbFilter.SelectedIndex != 0 && !string.IsNullOrEmpty(txtFilter.Text))
            {
                string Filter = cbFilter.Text.Replace(" ", "");
                if (cbFilter.Text == "PersonID" || cbFilter.Text == "UserID")
                    _dtAllUsers.DefaultView.RowFilter = String.Format("{0}={1}", Filter, txtFilter.Text);
                else
                    _dtAllUsers.DefaultView.RowFilter = String.Format("{0} LIKE '{1}%'", Filter, txtFilter.Text);

            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter = "";
            }

        }


        private void frmUsers_Load(object sender, EventArgs e)
        {
            _LoadDataGridView();
            _LoadCB();
            _CountRecords();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbIsActive.SelectedIndex = 0;
            txtFilter.Clear();
            if (cbFilter.SelectedIndex == 0 || cbFilter.SelectedItem.ToString() == "IsActive")
            {
                txtFilter.Visible = false;
            }
            else
                txtFilter.Visible = true;
            if (cbFilter.SelectedItem.ToString() == "IsActive")
            {
                cbIsActive.Visible = true;
            }
            else
                cbIsActive.Visible = false;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            _ApplyFilters();
            _CountRecords();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "IsActive")
            {
                switch (cbIsActive.SelectedIndex)
                {
                    case 0:
                        _dtAllUsers.DefaultView.RowFilter = "";
                        break;
                    case 1:
                        _dtAllUsers.DefaultView.RowFilter = "IsActive =1";
                        break;
                    case 2:
                        _dtAllUsers.DefaultView.RowFilter = "IsActive =0";
                        break;
                }
                _CountRecords();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUserInfo((int)dgvUsers.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "PersonID" || cbFilter.Text == "UserID")
            {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser();
            frm.ShowDialog();
            _RefreashDataGridView();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser((int)dgvUsers.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
            _RefreashDataGridView();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser();
            frm.ShowDialog();
            _RefreashDataGridView();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.SelectedRows[0].Cells[0].Value;
            if (MessageBox.Show("User With User ID : " + UserID.ToString() + " Will Be Deleted Are You Sure ?", "Delete", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (DVLD_BLL.clsUser.DeleteUser(UserID))
                {
                    MessageBox.Show("User With User ID : " + UserID.ToString() + " Is Deleted Successfully", "Deleted",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Error : Faild To Delete This User", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                _RefreashDataGridView();
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this feature is not implemented yet", "coming soon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void callPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this feature is not implemented yet", "coming soon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Form frm = new DVLD.Users.frmChangePassword((int)dgvUsers.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
