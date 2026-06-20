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
    public partial class ctrlPersonCardWithFilter : UserControl
    {

        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> Handler = OnPersonSelected;
            if(Handler != null)
                Handler(PersonID);
        }


        private bool _EnableFilter = true;
        private bool _ShowAddPersonButton = true;
    
        public void FilterFocus()
        {
            txtFilter.Focus();
        }
        public bool ShowAddPersonButton
        {
            get
                {
                return _ShowAddPersonButton;
                }
            set
            { 
            _ShowAddPersonButton=value;
            btnAdd.Enabled=_ShowAddPersonButton;
            }
        }
        public bool EnableFilter
        {
            get { return _EnableFilter; }
            set {
                _EnableFilter = value;
                   gbFilter.Enabled = _EnableFilter;
                   }
        }
        public int PersonID
        {
            get { return ctrlPersonInfo1.PersonID; }
        }
        public DVLD_BLL.clsPeople PersonInfo
        {
            get { return ctrlPersonInfo1.PersonInfo; }
        }

        public void LoadPersonInfo(int PeronsID)
        {
            cbFilter.SelectedIndex = 0;
            txtFilter.Text= PeronsID.ToString();
            _FindNow();

        }

        private void _FindNow()
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                MessageBox.Show("Please Enter The " + cbFilter.SelectedItem.ToString() + " In The Text Box ", "Insert",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            switch(cbFilter.Text)
            {
                case "PersonID":
                    ctrlPersonInfo1.LoadPersonInfo(Convert.ToInt32(txtFilter.Text.ToString()));
                    break;
                case "NationalNo":
                    ctrlPersonInfo1.LoadPersonInfo(txtFilter.Text.ToString());
                    break;
                default:
                    MessageBox.Show("Error : Error With The Filters Box ", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
            }

            if (OnPersonSelected != null&&gbFilter.Enabled)
                PersonSelected(ctrlPersonInfo1.PersonID);

        }
        //Learn Delegate And Return
        private void button1_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.DataBack += PersonID_Back;
            frm.ShowDialog();
            if (OnPersonSelected != null)
                PersonSelected(ctrlPersonInfo1.PersonID);
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilter.Items.Add("PersonID");
            cbFilter.Items.Add("NationalNo");
            cbFilter.SelectedIndex = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFilter.Clear();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
          _FindNow();
        }

        private void PersonID_Back(object obj,int PersonID)
        {
            cbFilter.SelectedIndex=0;
            txtFilter.Clear();
            txtFilter.Text=PersonID.ToString();
            ctrlPersonInfo1.LoadPersonInfo(PersonID);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                btnFind.PerformClick();
            }

            if(cbFilter.SelectedItem.ToString()=="PersonID")
            {
                e.Handled=!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar);
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear();
            txtFilter.Focus();
        }
    }
}
