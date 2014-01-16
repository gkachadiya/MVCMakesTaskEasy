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
using System.Web.Services;
using System.Text;

namespace SpotCollege.Account
{
    public partial class RegisterUniversityAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindBasicDropDown();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //User Data
            SpotCollege.DAL.User user = new SpotCollege.DAL.User();
            UserBLL userBll = new UserBLL();
            user.UserName = txtuniversityEmail.Text;
            string password = Guid.NewGuid().ToString().Substring(6, 10);
            user.Password = password;
            user.LoginTypeId = (int)LoginTypes.University;
            user.IsActive = false;
            userBll.Save(user);

            //University Data

            University university = new University();
            UniversityBLL universityBll = new UniversityBLL();
            university.UniversityName = txtunivercitname.Text;
            university.UserName = txtuniversityEmail.Text;
            university.Address = txtaddress.Text;
            university.City = txtunviersitycity.Text;
            university.Country = ddlCountry.SelectedItem.Text;
            university.ContactNo = txtcontactno.Text;
            university.CountryCode = ddlCountryCode.SelectedValue.ToString();
            university.AdminName = txtAuthRepr.Text;
            universityBll.Save(university);
            try
            {
                StringBuilder mailmsg = new StringBuilder(string.Empty);
                mailmsg.Append("<h3>" + "Hi " + txtunivercitname.Text + "</h3>");
                mailmsg.Append("<br/>Your account has been created and spotcollege admin will contact you soon");
                mailmsg.Append("<br/><b>University Name</b>" + txtunivercitname.Text);
                mailmsg.Append("<br/><b>University Email</b>" + txtuniversityEmail.Text);
                mailmsg.Append("<br/><b>University Address</b>" + txtaddress.Text);
                mailmsg.Append("<br/><b>University City</b>" + txtunviersitycity.Text);
                mailmsg.Append("<br/><b>University Country</b>" + ddlCountry.SelectedItem.Text);
                mailmsg.Append("<br/><b>University Contact No</b>" + txtcontactno.Text);

                mailmsg.Append("<br/><b>Thanks & Regards,</b><br/>SpotCollege Team.");
                MailHelper.sendMail(txtuniversityEmail.Text, "balar9426@gmail.com", "Thanks to create Account : SpotCollege", mailmsg.ToString());

                MailHelper.sendMail("admin@gmail.com", "balar9426@gmail.com", "Intrestes University : SpotCollege", mailmsg.ToString());

                txtuniversityEmail.Text = "";
                lblMsg.Text = "Thank You! Our representative will get in touch with you soon";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                clearall();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                this.Page.ClientScript.RegisterStartupScript(typeof(Page), "notification", "window.alert('" + msg + "');", true);
            }

        }

        void bindBasicDropDown()
        {
            //Bind Country Dropdown            					

            ddlCountry.Items.Add(new ListItem("Select Country", "0"));
            ddlCountry.SelectedValue = "0";
            string[] Countrynames = Enum.GetNames(typeof(CountryList));
            Array Countryvalue = Enum.GetValues(typeof(CountryList));
            int[] CountryValues = (int[])Countryvalue;
            for (int i = 0; i < Countrynames.Length; i++)
            {
                ListItem item = new ListItem(Countrynames[i], CountryValues[i].ToString());
                ddlCountry.Items.Add(item);
            }


            //Bind CuntryCode List
            ddlCountryCode.Items.Add(new ListItem("Select Country Code", "0"));
            ddlCountryCode.SelectedValue = "0";

            string[] CountryCodenames = Enum.GetNames(typeof(CountryCode));

            Array value1 = Enum.GetValues(typeof(CountryCode));
            int[] Values1 = (int[])value1;
            for (int i = 0; i < CountryCodenames.Length; i++)
            {
                ListItem item = new ListItem(CountryCodenames[i] + " [+" + Values1[i] + "]", Values1[i].ToString());
                ddlCountryCode.Items.Add(item);
            }
            ReorderAlphabetized(ddlCountryCode);

        }

        public static void ReorderAlphabetized(DropDownList ddl)
        {
            List<ListItem> listCopy = new List<ListItem>();
            foreach (ListItem item in ddl.Items)
                listCopy.Add(item);
            ddl.Items.Clear();
            foreach (ListItem item in listCopy.OrderBy(item => item.Text))
                ddl.Items.Add(item);
        }

        void clearall()
        {
            txtunivercitname.Text = "";
            txtuniversityEmail.Text = "";
            txtaddress.Text = "";
            txtunviersitycity.Text = "";
            ddlCountry.SelectedValue = "0";
            txtcontactno.Text = "";
            txtAuthRepr.Text = "";
            ddlCountryCode.SelectedValue = "0";
        }

        void sendMessageToAdmin()
        {

        }

        [WebMethod()]
        public static string CheckUserName(string UserName)
        {
            UserBLL userbl = new UserBLL();
            string str = string.Empty;
            User user = userbl.Get(UserName);
            if (user != null)
            {
                str = "true";
            }
            else
            {
                str = "false";
            }
            return str;

        }
    }
}