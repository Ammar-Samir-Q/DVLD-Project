using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DVLD_BLL
{
    public class clsTestType
    {
         enum enMode {eAdd=0,eUpdate=1 };

        public enum enTestType {VisionTest=1,WrittenTest=2,StreetTest=3 };
        public clsTestType.enTestType ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public float Fees { get; set; }

        private enMode _Mode;

        //-----------
      public clsTestType()
        {
            this.ID =0;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;
            _Mode = enMode.eAdd;
        }

      public clsTestType(int ID,string Title,string Description,float Fees)
        {
            this.ID=(enTestType)ID;
            this.Title=Title;
            this.Description=Description;
            this.Fees=Fees;
            _Mode=enMode.eUpdate;
        }

        //-----------
        private bool _Add()
        {
            this.ID=(enTestType)DVLD_DAL.clsTestTypesData.AddNewTestType(Title,Description,Fees);
            return this.ID>0;

        }
        private bool _Update()
        {
            return DVLD_DAL.clsTestTypesData.UpdateTestType((int)this.ID, Title, Description, Fees);
        }

        public bool Save()
        {
        switch(_Mode)
            {
                case enMode.eAdd:
                    _Mode = enMode.eUpdate;
                    return _Add();
                case enMode.eUpdate:
                    return _Update();
            }
        return false;
        }

        static public clsTestType Find(int ID)
        {
            string Title = "", Description = "";
            float Fees = 0;
            if(DVLD_DAL.clsTestTypesData.Find(ID,ref Title,ref Description,ref Fees))
                return new clsTestType(ID,Title,Description,Fees);
            else
                return null;
        }

        static public DataTable GetAllTestTypes()
        {
            return DVLD_DAL.clsTestTypesData.GetAllTestTypes();
        }

    }
}
