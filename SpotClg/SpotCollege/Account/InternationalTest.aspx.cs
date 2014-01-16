using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpotCollege.Common;
using SpotCollege.DAL;
using SpotCollege.BLL;
using System.Configuration;
using System.IO;

namespace SpotCollege.Account
{
    public partial class InternationalTest : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
              //  BindDropDownList();
            }
        }

        //void BindInternationalList()
        //{
        //    string[] IntTest = Enum.GetNames(typeof(InternationalTest));
        //    Array value = Enum.GetValues(typeof(InternationalTest));
        //    int[] Values = (int[])value;
        //    List<SelectListItem> testlist = new List<SelectListItem>();
        //    for (int i = 0; i < IntTest.Length; i++)
        //    {
        //        ListItem item = new ListItem(IntTest[i], Values[i].ToString());
        //        testlist.Add(item);
        //    }
        //}

        void BindInterNationalTestDropDownList()
        {
            string[] names = null;
            Array value = null;
            int[] Values = null;

            //Bind GraduateStatus In RadioButtonList
            names = Enum.GetNames(typeof(Sector));
            value = Enum.GetValues(typeof(Sector));
            Values = (int[])value;

            for (int i = 0; i < names.Length; i++)
            {
                ListItem item = new ListItem(names[i], Values[i].ToString());
                rdBtnSector.Items.Add(item);
            }

            //Bind Start Year  In Dropdown
            ddlstartyear.Items.Add(new ListItem("Select Any", "0"));
            ddlstartyear.SelectedValue = "0";
            for (int i = 1999; i < 2013; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                ddlstartyear.Items.Add(item);
            }

            //Bind YearOfGraduation  In Dropdown
            ddlGraduationyear.Items.Add(new ListItem("Select Any", "0"));
            ddlGraduationyear.SelectedValue = "0";
            for (int i = 1999; i < 2013; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                ddlGraduationyear.Items.Add(item);
            }

        }

        protected void lnkBtnAddHighSchool_Click(object sender, EventArgs e)
        {
            pnlHighSchoolEnrollment.Visible = true;

            pnlACT.Visible = false;
            pnlSAT.Visible = false;
            pnlAP.Visible = false;
            pnlGRE.Visible = false;
            pnlGMAT.Visible = false;
            pnlIB.Visible = false;
            pnlIELTS.Visible = false;
            pnlLSAT.Visible = false;
            pnlMCAT.Visible = false;
            pnlPSAT.Visible = false;
            pnlSAT2.Visible = false;
            pnlTOEFLInternetbased.Visible = false;
            pnlTOEFLPaperbased.Visible = false;
        }

        protected void lnkBtnAddACTScore_Click(object sender, EventArgs e)
        {
            pnlACT.Visible = true;

            pnlHighSchoolEnrollment.Visible = false;
            pnlSAT.Visible = false;
            pnlAP.Visible = false;
            pnlGRE.Visible = false;
            pnlGMAT.Visible = false;
            pnlIB.Visible = false;
            pnlIELTS.Visible = false;
            pnlLSAT.Visible = false;
            pnlMCAT.Visible = false;
            pnlPSAT.Visible = false;
            pnlSAT2.Visible = false;
            pnlTOEFLInternetbased.Visible = false;
            pnlTOEFLPaperbased.Visible = false;
        }

        protected void lnkBtnAddSATScore_Click(object sender, EventArgs e)
        {
            pnlSAT.Visible = true;

            pnlACT.Visible = false;
            pnlHighSchoolEnrollment.Visible = false;
            pnlAP.Visible = false;
            pnlGRE.Visible = false;
            pnlGMAT.Visible = false;
            pnlIB.Visible = false;
            pnlIELTS.Visible = false;
            pnlLSAT.Visible = false;
            pnlMCAT.Visible = false;
            pnlPSAT.Visible = false;
            pnlSAT2.Visible = false;
            pnlTOEFLInternetbased.Visible = false;
            pnlTOEFLPaperbased.Visible = false;
        }

        protected void lnkBtnAddAPScore_Click(object sender, EventArgs e)
        {
            pnlAP.Visible = true;

            pnlACT.Visible = false;
            pnlSAT.Visible = false;
            pnlHighSchoolEnrollment.Visible = false;
            pnlGRE.Visible = false;
            pnlGMAT.Visible = false;
            pnlIB.Visible = false;
            pnlIELTS.Visible = false;
            pnlLSAT.Visible = false;
            pnlMCAT.Visible = false;
            pnlPSAT.Visible = false;
            pnlSAT2.Visible = false;
            pnlTOEFLInternetbased.Visible = false;
            pnlTOEFLPaperbased.Visible = false;
        }

        protected void lnkBtnAddGREScore_Click(object sender, EventArgs e)
        {
            pnlGRE.Visible = true;

            pnlACT.Visible = false;
            pnlSAT.Visible = false;
            pnlAP.Visible = false;
            pnlHighSchoolEnrollment.Visible = false;
            pnlGMAT.Visible = false;
            pnlIB.Visible = false;
            pnlIELTS.Visible = false;
            pnlLSAT.Visible = false;
            pnlMCAT.Visible = false;
            pnlPSAT.Visible = false;
            pnlSAT2.Visible = false;
            pnlTOEFLInternetbased.Visible = false;
            pnlTOEFLPaperbased.Visible = false;
        }

        protected void lnkBtnAddGMATScore_Click(object sender, EventArgs e)
        {
            pnlGMAT.Visible = true;

            pnlACT.Visible = false;
            pnlSAT.Visible = false;
            pnlAP.Visible = false;
            pnlGRE.Visible = false;
            pnlHighSchoolEnrollment.Visible = false;
            pnlIB.Visible = false;
            pnlIELTS.Visible = false;
            pnlLSAT.Visible = false;
            pnlMCAT.Visible = false;
            pnlPSAT.Visible = false;
            pnlSAT2.Visible = false;
            pnlTOEFLInternetbased.Visible = false;
            pnlTOEFLPaperbased.Visible = false;
        }

        protected void lnkBtnAddIBScore_Click(object sender, EventArgs e)
        {
            pnlIB.Visible = true;

            pnlACT.Visible = false;
            pnlSAT.Visible = false;
            pnlAP.Visible = false;
            pnlGRE.Visible = false;
            pnlGMAT.Visible = false;
            pnlHighSchoolEnrollment.Visible = false;
            pnlIELTS.Visible = false;
            pnlLSAT.Visible = false;
            pnlMCAT.Visible = false;
            pnlPSAT.Visible = false;
            pnlSAT2.Visible = false;
            pnlTOEFLInternetbased.Visible = false;
            pnlTOEFLPaperbased.Visible = false;
        }

        protected void lnkBtnAddIELTSScore_Click(object sender, EventArgs e)
        {
            pnlIELTS.Visible = true;

            pnlACT.Visible = false;
            pnlSAT.Visible = false;
            pnlAP.Visible = false;
            pnlGRE.Visible = false;
            pnlGMAT.Visible = false;
            pnlIB.Visible = false;
            pnlHighSchoolEnrollment.Visible = false;
            pnlLSAT.Visible = false;
            pnlMCAT.Visible = false;
            pnlPSAT.Visible = false;
            pnlSAT2.Visible = false;
            pnlTOEFLInternetbased.Visible = false;
            pnlTOEFLPaperbased.Visible = false;
        }

        protected void LinlnkBtnAddLSATScore_Click(object sender, EventArgs e)
        {
            pnlLSAT.Visible = true;

            pnlACT.Visible = false;
            pnlSAT.Visible = false;
            pnlAP.Visible = false;
            pnlGRE.Visible = false;
            pnlGMAT.Visible = false;
            pnlIB.Visible = false;
            pnlIELTS.Visible = false;
            pnlHighSchoolEnrollment.Visible = false;
            pnlMCAT.Visible = false;
            pnlPSAT.Visible = false;
            pnlSAT2.Visible = false;
            pnlTOEFLInternetbased.Visible = false;
            pnlTOEFLPaperbased.Visible = false;
        }

        protected void LinlnkBtnAddMCATScore_Click(object sender, EventArgs e)
        {
            pnlMCAT.Visible = true;

            pnlACT.Visible = false;
            pnlSAT.Visible = false;
            pnlAP.Visible = false;
            pnlGRE.Visible = false;
            pnlGMAT.Visible = false;
            pnlIB.Visible = false;
            pnlIELTS.Visible = false;
            pnlLSAT.Visible = false;
            pnlHighSchoolEnrollment.Visible = false;
            pnlPSAT.Visible = false;
            pnlSAT2.Visible = false;
            pnlTOEFLInternetbased.Visible = false;
            pnlTOEFLPaperbased.Visible = false;
        }

        protected void LinlnkBtnAddPSATScore_Click(object sender, EventArgs e)
        {
            pnlPSAT.Visible = true;

            pnlACT.Visible = false;
            pnlSAT.Visible = false;
            pnlAP.Visible = false;
            pnlGRE.Visible = false;
            pnlGMAT.Visible = false;
            pnlIB.Visible = false;
            pnlIELTS.Visible = false;
            pnlLSAT.Visible = false;
            pnlMCAT.Visible = false;
            pnlHighSchoolEnrollment.Visible = false;
            pnlSAT2.Visible = false;
            pnlTOEFLInternetbased.Visible = false;
            pnlTOEFLPaperbased.Visible = false;
        }

        protected void LinlnkBtnAddSAT_2Score_Click(object sender, EventArgs e)
        {
            pnlSAT2.Visible = true;

            pnlACT.Visible = false;
            pnlSAT.Visible = false;
            pnlAP.Visible = false;
            pnlGRE.Visible = false;
            pnlGMAT.Visible = false;
            pnlIB.Visible = false;
            pnlIELTS.Visible = false;
            pnlLSAT.Visible = false;
            pnlMCAT.Visible = false;
            pnlPSAT.Visible = false;
            pnlHighSchoolEnrollment.Visible = false;
            pnlTOEFLInternetbased.Visible = false;
            pnlTOEFLPaperbased.Visible = false;
        }

        protected void LinlnkBtnAddTOEFLInternetbasedScore_Click(object sender, EventArgs e)
        {
            pnlTOEFLInternetbased.Visible = true;

            pnlACT.Visible = false;
            pnlSAT.Visible = false;
            pnlAP.Visible = false;
            pnlGRE.Visible = false;
            pnlGMAT.Visible = false;
            pnlIB.Visible = false;
            pnlIELTS.Visible = false;
            pnlLSAT.Visible = false;
            pnlMCAT.Visible = false;
            pnlPSAT.Visible = false;
            pnlSAT2.Visible = false;
            pnlHighSchoolEnrollment.Visible = false;
            pnlTOEFLPaperbased.Visible = false;
        }

        protected void LinlnkBtnAddTOEFLPaperbasedScore_Click(object sender, EventArgs e)
        {
            pnlTOEFLPaperbased.Visible = true;

            pnlACT.Visible = false;
            pnlSAT.Visible = false;
            pnlAP.Visible = false;
            pnlGRE.Visible = false;
            pnlGMAT.Visible = false;
            pnlIB.Visible = false;
            pnlIELTS.Visible = false;
            pnlLSAT.Visible = false;
            pnlMCAT.Visible = false;
            pnlPSAT.Visible = false;
            pnlSAT2.Visible = false;
            pnlTOEFLInternetbased.Visible = false;
            pnlHighSchoolEnrollment.Visible = false;
        }

        protected void btnHighSchoolEnrollmentCancel_Click(object sender, EventArgs e)
        {
            pnlHighSchoolEnrollment.Visible = false;
        }

        protected void btnACTCancel_Click(object sender, EventArgs e)
        {
            pnlACT.Visible = false;
        }

        protected void btnSATCancel_Click(object sender, EventArgs e)
        {
            pnlSAT.Visible = false;

        }

        protected void btnAPCancel_Click(object sender, EventArgs e)
        {
            pnlSAT2.Visible = false;

        }

        protected void btnGRECancel_Click(object sender, EventArgs e)
        {
            pnlGRE.Visible = false;

        }

        protected void btnGMATCancel_Click(object sender, EventArgs e)
        {
            pnlGMAT.Visible = false;

        }

        protected void btnIBCancel_Click(object sender, EventArgs e)
        {
            pnlIB.Visible = false;

        }

        protected void btnIELTSCancel_Click(object sender, EventArgs e)
        {
            pnlIELTS.Visible = false;

        }

        protected void btnLSATCancel_Click(object sender, EventArgs e)
        {
            pnlLSAT.Visible = false;

        }

        protected void btnMCATCancel_Click(object sender, EventArgs e)
        {
            pnlMCAT.Visible = false;

        }

        protected void btnPSATCancel_Click(object sender, EventArgs e)
        {
            pnlPSAT.Visible = false;

        }

        protected void btnSAT_IICancel_Click(object sender, EventArgs e)
        {
            pnlSAT2.Visible = false;
            
        }

        protected void btnTOEFLInternetbasedCancel_Click(object sender, EventArgs e)
        {
            pnlTOEFLInternetbased.Visible = false;

        }

        protected void btnTOEFLPaperbasedCancel_Click(object sender, EventArgs e)
        {
            pnlTOEFLPaperbased.Visible = false;

        }

        protected void btnTOEFLPaperbasedSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnTOEFLInternetbasedSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnSAT_IISave_Click(object sender, EventArgs e)
        {

        }

        protected void btnPSATSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnMCATSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnLSATSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnIELTSSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnIBSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnGMATSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnGRESave_Click(object sender, EventArgs e)
        {

        }

        protected void btnAPSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnSATSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnACTSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnHighSchoolEnrollmentSave_Click(object sender, EventArgs e)
        {

        }
    }
}