using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace SpotCollege.DAL.DataModels
{
    public class StudentDashboardModel
    {
        public List<Student> studentlist { get; set; }
        public List<Student> studentlist2 { get; set; }
        public List<University> universityList { get; set; }
        public List<Alert> alertList { get; set; }
    }
    public class StudentProfileModel
    {
        public StudentProfileModel()
        {
            testList = new StudentTestModel();
            registerModel = new RegisterModel();
            student = new StudentEditModel();
            studentList = new List<Student>();
            universityList = new List<University>();
            studentInterestList = new List<StudentInterest>();
            studentWorkHistoryList = new List<StudentWorkHistory>();
            alertList = new List<Alert>();
            surveyModel = new SurveyModel();
            reviewList = new List<SurveyDetail>();
        }
        public StudentEditModel student { get; set; }
        public List<Student> studentList { get; set; }

        public StudentAcademicModel studentAcademic { get; set; }
        public List<StudentAcademic> studentAcademicList { get; set; }

        public StudentInterest studentInterest { get; set; }
        public List<StudentInterest> studentInterestList { get; set; }
        public StudentExtraActivityModel studentExtraActivity { get; set; }
        public StudentEducationalPreferenceModel studentEduPreference { get; set; }

        public StudentTestModel testList { get; set; }

        public StudentWorkHistoryModel studentWork { get; set; }
        public List<StudentWorkHistory> studentWorkHistoryList { get; set; }
        public List<University> universityList { get; set; }

        public List<Alert> alertList { get; set; }

        public RegisterModel registerModel { get; set; }

        public SurveyModel surveyModel { get; set; }
        public List<SurveyDetail> reviewList { get; set; }

    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [EmailAddress(ErrorMessage = "Enter correct email address")]
        [Display(Name = "Enter User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Enter Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class StudentEditModel
    {
        public StudentEditModel()
        {
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        public string Photo { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required(ErrorMessage = "ZipCode is required.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid Zip code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }


        [Range(1, 233, ErrorMessage = "Please select country")]
        public string Country { get; set; }






        [Range(1, 5, ErrorMessage = "Please select Phone type")]
        public string PrimaryType { get; set; }

        [Range(1, 226, ErrorMessage = "Please select Area code")]
        public string CoutryCodePrimary { get; set; }

        [RegularExpression(@"\d{2,10}", ErrorMessage = "Enter valid number")]
        public string PrimaryAreaCode { get; set; }

        [Required(ErrorMessage = "Primary Number Required")]
        [RegularExpression(@"\d{6,10}", ErrorMessage = "Enter valid number")]
        public string PrimaryNumber { get; set; }



        public string SecondaryType { get; set; }
        public string CountryCodeSecondary { get; set; }
        [RegularExpression(@"\d{2,10}", ErrorMessage = "Enter valid number")]
        public string SecondaryAreaCode { get; set; }
        [RegularExpression(@"\d{6,10}", ErrorMessage = "Enter valid number")]
        public string SecondaryNumber { get; set; }



        [Range(1, 31, ErrorMessage = "Please select the born day")]
        public string day { get; set; }
        [Range(1950, 2020, ErrorMessage = "Please select the born year")]
        public string year { get; set; }
        [Range(1, 12, ErrorMessage = "Please select the born Month")]
        public string month { get; set; }


        public string PrimaryEmail { get; set; }


        public Nullable<System.DateTime> BirthDate { get; set; }

        [Range(1, 233, ErrorMessage = "Please select country")]
        public string Citizenship { get; set; }
        //This fields are not useful in Edit but this fields are usreful in profile
        public int StudyingIn { get; set; }
        public int LookingFor { get; set; }
        public string LookingForCountry { get; set; }
        public int StartDate { get; set; }
        public int Payout { get; set; }
        public string DesiredFieldofStudy { get; set; }
        public string OtherStudy { get; set; }

        public string SportActivities { get; set; }
        public string LeadershipActivies { get; set; }
        public string OtherActivities { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
    }

    public class StudentEducationalPreferenceModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        [Range(1, 6, ErrorMessage = "Please select any")]
        public int StudyingIn { get; set; }

        [Range(1, 4, ErrorMessage = "Please select any")]
        public int LookingFor { get; set; }

        [Range(1, 233, ErrorMessage = "Please select any")]
        public string LookingForCountry { get; set; }

        [Range(1, 8, ErrorMessage = "Please select any")]
        public int StartDate { get; set; }

        [Range(1, 4, ErrorMessage = "Please select any")]
        public int Payout { get; set; }

        [Range(1, 109, ErrorMessage = "Please select any")]
        public string DesiredFieldofStudy { get; set; }
        public string OtherStudy { get; set; }
    }
    public class StudentExtraActivityModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        public string SportActivities { get; set; }
        public string LeadershipActivies { get; set; }
        public string OtherActivities { get; set; }
    }
    public class StudentAcademicModel
    {
        public StudentAcademicModel()
        {
            Graduate = 0;
        }
        public int StudentAcademicsId { get; set; }
        public int StudentId { get; set; }

        [Required(ErrorMessage = "School Name is Required")]
        public string SchoolName { get; set; }

        [Required(ErrorMessage = "School City is Required")]
        public string SchoolCity { get; set; }

        [Range(1, 232, ErrorMessage = "School country is Required")]
        public string SchoolCountry { get; set; }


        public int Graduate { get; set; }

        //[Range(1985, 2013, ErrorMessage = "Graduation Year is Required")]
        public Nullable<int> GraduationYear { get; set; }

        //[Range(1, 6, ErrorMessage = "Graduation level is Required")]
        public Nullable<int> GraduationLevel { get; set; }

        [RegularExpression(@"^(?:[1-9]\d{0,9}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public Nullable<decimal> Rank { get; set; }

        public string CertificatePath { get; set; }

        public HttpPostedFileBase fileupload { get; set; }

        //[Range(1, 6, ErrorMessage = "Graduation Year is Required")]
        public Nullable<int> DegreebeingPursued { get; set; }

        //[Range(2013, 2023, ErrorMessage = "Expected Graduation Year is Required")]
        public Nullable<int> ExpectedYearofGraduation { get; set; }

        //[Required(ErrorMessage = "Field of study required.")]
        public string FieldofStudy { get; set; }
    }
    public class StudentInterestModel
    {
        public int StudentInterestId { get; set; }
        public string UniversityUserName { get; set; }
        public string StudentUserName { get; set; }
        public int Approved { get; set; }

        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
    public class StudentTestModel
    {
        public StudentTestModel()
        {
            testList = new List<StudentTest>();
            testSat = new testSAT();
            testAct = new testACT();
            testAp = new testAP();
            testGmat = new testGMAT();
            testGre = new testGRE();
            testIB = new testIB();
            testIelts = new testIELTS();
            testLsat = new testLSAT();
            testMcat = new testMCAT();
            testPsat = new testPSAT();
            testSat2 = new testSAT2();
            testToeflI = new testTOEFL_I();
            testToeflP = new testTOEFL_P();
        }
        public List<StudentTest> testList { get; set; }
        public testACT testAct { get; set; }
        public testSAT testSat { get; set; }
        public testAP testAp { get; set; }
        public testGMAT testGmat { get; set; }
        public testGRE testGre { get; set; }
        public testIB testIB { get; set; }
        public testIELTS testIelts { get; set; }
        public testLSAT testLsat { get; set; }
        public testMCAT testMcat { get; set; }
        public testPSAT testPsat { get; set; }
        public testSAT2 testSat2 { get; set; }
        public testTOEFL_I testToeflI { get; set; }
        public testTOEFL_P testToeflP { get; set; }

        #region old
        //public int StudentTestId { get; set; }
        //public int StudentId { get; set; }
        //public int TestId { get; set; }
        //public Nullable<int> SectorId { get; set; }
        //public string SchoolName { get; set; }
        //public Nullable<int> StartYear { get; set; }
        //public Nullable<int> EndYear { get; set; }

        //[Required]
        //[RegularExpression(@"\d{0,2}.\d{0,2}")]
        //public string Composite { get; set; }

        //[Required]
        //[RegularExpression(@"\d{0,2}.\d{0,2}")]
        //public string English { get; set; }

        //[Required]
        //[RegularExpression(@"\d{0,2}.\d{0,2}")]
        //public string Math { get; set; }

        //[Required]
        //[RegularExpression(@"\d{0,2}.\d{0,2}")]
        //public string Reading { get; set; }

        //[Required]
        //[RegularExpression(@"\d{0,2}.\d{0,2}")]
        //public string Science { get; set; }

        //[Required]
        //[RegularExpression(@"\d{0,2}.\d{0,2}")]
        //public string Writing { get; set; }

        //[Required]
        //public string Date { get; set; }

        //public string Subject { get; set; }
        //public string Score { get; set; }
        //public string VerbalReasoning { get; set; }
        //public string QuantitativeReasoning { get; set; }
        //public string AnalyticalWriting { get; set; }
        //public string Verbal { get; set; }
        //public string Total { get; set; }
        //public string Listening { get; set; }
        //public string Speaking { get; set; }
        //public string Biology { get; set; }
        //public string Physics { get; set; }

        //public virtual Student Student { get; set; } 
        #endregion
    }
    public class StudentWorkHistoryModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentWorkHistoryId { get; set; }

        public int StudentId { get; set; }
        [Required(ErrorMessage = "Compantname is required.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Position in company is required.")]
        public string Position { get; set; }

        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Responsibilities in company is required.")]
        public string Responsibilities { get; set; }

        [Range(1, 31, ErrorMessage = "Please select the day")]
        public string start_day { get; set; }

        [Range(1950, 2020, ErrorMessage = "Please select the year")]
        public string start_year { get; set; }

        [Range(1, 12, ErrorMessage = "Please select the Month")]
        public string start_month { get; set; }

        [Range(1, 31, ErrorMessage = "Please select the day")]
        public string end_day { get; set; }

        [Range(1950, 2020, ErrorMessage = "Please select the year")]
        public string end_year { get; set; }

        [Range(1, 12, ErrorMessage = "Please select the Month")]
        public string end_month { get; set; }
    }

    public class testSAT2
    {
        public int StudentTestId { get; set; }
        //public int StudentId { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Subject { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Score { get; set; }

        [Required]
        public string Date { get; set; }
    }

    public class testMCAT
    {
        public int StudentTestId { get; set; }
        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Biology { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Physics { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Verbal { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Writing { get; set; }

        [Required]
        public string Date
        {
            get;
            set;
        }

    }

    public class testPSAT
    {
        public int StudentTestId { get; set; }
        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Reading { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Math { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Writing { get; set; }

        [Required]
        public string Date
        {
            get;
            set;
        }

    }

    public class testIB
    {
        public int StudentTestId { get; set; }
        //public int StudentId { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Score { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Date { get; set; }
    }

    public class testLSAT
    {
        public int StudentTestId { get; set; }
        //public int StudentId { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Score { get; set; }

        [Required]
        public string Date { get; set; }
    }

    public class testACT
    {
        public int StudentTestId { get; set; }
        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Composite { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string English { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Math { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Reading { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Science { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Writing { get; set; }

        [Required]
        public string Date
        {
            get;
            set;
        }

    }

    public class testSAT
    {

        public int StudentTestId { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Math { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Reading { get; set; }


        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Writing { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Composite { get; set; }

        [Required]
        public string Date { get; set; }
    }

    public class testAP
    {
        [Key]
        public int StudentTestId { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Score { get; set; }

        [Required]
        public string Subject { get; set; }
    }

    public class testGRE
    {
        public int StudentTestId { get; set; }
        //public int StudentId { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string VerbalReasoning { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string QuantitativeReasoning { get; set; }


        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string AnalyticalWriting { get; set; }

        [Required]
        public string Date { get; set; }
    }

    public class testGMAT
    {
        public int StudentTestId { get; set; }
        //public int StudentId { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Verbal { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string QuantitativeReasoning { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Total { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string AnalyticalWriting { get; set; }

        [Required]
        public string Date { get; set; }
    }

    public class testIELTS
    {
        public int StudentTestId { get; set; }
        //public int StudentId { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Listening { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Reading { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Writing { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Speaking { get; set; }

        [Required]
        public string Date { get; set; }
    }

    public class testTOEFL_I
    {
        public int StudentTestId { get; set; }
        //public int StudentId { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Listening { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Reading { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Writing { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Speaking { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Total { get; set; }

        [Required]
        public string Date { get; set; }
    }

    public class testTOEFL_P
    {
        public int StudentTestId { get; set; }
        //public int StudentId { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Listening { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Reading { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Writing { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Speaking { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string Total { get; set; }

        [Required]
        public string Date { get; set; }
    }

    public class SettingsModel
    {
        public SettingsModel()
        {
            privacySetting = new PrivacySettingModel();
            commnicationSetting = new CommnicationSettingModel();
        }

        public PrivacySettingModel privacySetting { get; set; }

        public CommnicationSettingModel commnicationSetting { get; set; }
    }

    public class PrivacySettingModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SettingId { get; set; }

        public string UserName { get; set; }

        public int SettingType { get; set; }

        [Display(Name = "I do not want to be contacted by other students (you will not show up in their search results)")]
        public bool Students { get; set; }

        [Display(Name = "I do not want to be contacted by Universities (you will not show up in their search results)")]
        public bool University { get; set; }
    }

    public class CommnicationSettingModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SettingId { get; set; }

        public string UserName { get; set; }

        public int SettingType { get; set; }

        [Display(Name = "Send me daily emails of students joining from my Country")]
        public bool DailyEmail { get; set; }

        [Display(Name = "Send me instant email when I receive a messages from Students")]
        public bool StudentsEmail { get; set; }

        [Display(Name = "Send me instant email when I receive a messages from Universities")]
        public bool UniversityEmail { get; set; }

        [Display(Name = "Do not send me promotional emails from SpotCollege")]
        public bool PromotionalEmail { get; set; }
    }
}