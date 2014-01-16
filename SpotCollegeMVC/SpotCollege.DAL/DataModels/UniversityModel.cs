using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace SpotCollege.DAL.DataModels
{
    //<summary>
    // This model is used in University Profile Edit
    //</summary>
    public class UniversityEditModel
    {
        public UniversityEditModel()
        {
            registerModel = new RegisterModel();
            university = new UniversityModel();
        }

        public UniversityModel university { get; set; }
        public RegisterModel registerModel { get; set; }
    }


    public class UniversityRegistrationModel
    {
        [Key]
        public int UniversityId { get; set; }

        [Required(ErrorMessage = "EmailId is required.")]
        [EmailAddress(ErrorMessage = "Enter valid EmailID")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "University Name is required.")]
        public string UniversityName { get; set; }

        [Required(ErrorMessage = "University address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "University Administrator name is required.")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "University city is required.")]
        public string City { get; set; }

        [Range(1, 232, ErrorMessage = "University country is required.")]
        public string Country { get; set; }


        [Required(ErrorMessage = "Contact no is required")]
        
        //[RegularExpression("\\d{6,10}", ErrorMessage = "Enter valid no")]
        [RegularExpression(@"^\(\d{3}\) ?\d{3}( |-)?\d{4}|^\d{3}( |-)?\d{3}( |-)?\d{4}", ErrorMessage = "Enter valid no format")]
        //[RegularExpression(@"^\(?([0-9]{3}\)?[-]([0-9]{6})$", ErrorMessage = "Enter valid no")]
        public string ContactNo { get; set; }

        [Range(1, 1876, ErrorMessage = "Country code is required")]
        public string CountryCode { get; set; }

    }

    public class UniversityModel
    {
        [Key]
        public int UniversityId { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "University Name is required.")]
        public string UniversityName { get; set; }

        [Required(ErrorMessage = "University address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "University Administrator name is required.")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "University city is required.")]
        public string City { get; set; }

        [Range(1, 232, ErrorMessage = "University country is required.")]
        public string Country { get; set; }


        [Required(ErrorMessage = "University established year is required.")]
        [Range(1000, 2500, ErrorMessage = "Enter valid year")]
        public Nullable<int> EstablishedYear { get; set; }

        [Required(ErrorMessage = "University description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Contact no is required")]
       // [RegularExpression("\\d{6,10}", ErrorMessage = "Enter valid no")]
        [RegularExpression(@"^\(\d{3}\) ?\d{3}( |-)?\d{4}|^\d{3}( |-)?\d{3}( |-)?\d{4}", ErrorMessage = "Enter valid no format")]
        public string ContactNo { get; set; }

        [Range(1, 1876, ErrorMessage = "Country code is required")]
        public string CountryCode { get; set; }

        public HttpPostedFileWrapper logo_image { get; set; }

        public HttpPostedFileWrapper large_image { get; set; }

        public string Image { get; set; }

        public string UniversityImage { get; set; }


        public string UnderGraduateFeeCurrency { get; set; }

        [RegularExpression(@"^[0-9-]*$", ErrorMessage = "Enter valid data")]
        public string UnderGraduateFee { get; set; }

        public string UnderGraduateFeeUnit { get; set; }

        public string GraduateFeeCurrency { get; set; }
        [RegularExpression(@"^[0-9-]*$", ErrorMessage = "Enter valid data")]
        public string GraduateFee { get; set; }
        public string GraduateFeeUnit { get; set; }

        public string BookFeeCurrency { get; set; }

        [RegularExpression(@"^[0-9-]*$", ErrorMessage = "Enter valid data")]
        public string BookFee { get; set; }

        public string BookFeeUnit { get; set; }

        public string BoardFeeCurrency { get; set; }

        [RegularExpression(@"^[0-9-]*$", ErrorMessage = "Enter valid data")]
        public string BoardFee { get; set; }

        public string BoardFeeUnit { get; set; }

        public string ApplicationFeeCurrency { get; set; }

        [RegularExpression(@"^[0-9-]*$", ErrorMessage = "Enter valid data")]
        public string ApplicationFee { get; set; }

        [RegularExpression("\\d{0,15}", ErrorMessage = "please enter valid number")]
        public Nullable<int> Graduates { get; set; }

        [RegularExpression("\\d{0,15}", ErrorMessage = "please enter valid number")]
        public Nullable<int> UnderGraduates { get; set; }

        [RegularExpression("\\d{0,15}", ErrorMessage = "please enter valid number")]
        public Nullable<int> InternationalGraduate { get; set; }

        [RegularExpression("\\d{0,15}", ErrorMessage = "please enter valid number")]
        public Nullable<int> PersuingAssociateDegree { get; set; }

        [RegularExpression("\\d{0,15}", ErrorMessage = "please enter valid number")]
        public Nullable<int> EnroledPerYearChina { get; set; }

        [RegularExpression("\\d{0,15}", ErrorMessage = "please enter valid number")]
        public Nullable<int> EnroledPerYearIndianSub { get; set; }

        [RegularExpression("\\d{0,15}", ErrorMessage = "please enter valid number")]
        public Nullable<int> EnroledPerYearAfrica { get; set; }

        [RegularExpression("\\d{0,15}", ErrorMessage = "please enter valid number")]
        public Nullable<int> EnroledPerYearMidEast { get; set; }

        [RegularExpression("\\d{0,15}", ErrorMessage = "please enter valid number")]
        public Nullable<int> EnroledPerYearSouthAmerica { get; set; }

        [RegularExpression("\\d{0,15}", ErrorMessage = "please enter valid number")]
        public Nullable<int> EnroledPerYearEurope { get; set; }

        public string Faqs { get; set; }
        public string Courses { get; set; }
        public string Degree { get; set; }
        public virtual User User { get; set; }
    }

    //This model is used to managa basic univesity details
    public class UniversityBasicModel
    {
        [Key]
        public int UniversityId { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "University Name is required.")]
        public string UniversityName { get; set; }

        [Required(ErrorMessage = "University address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "University Administrator name is required.")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "University city is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "University coutry is required.")]
        public string Country { get; set; }


        public string Image { get; set; }

        [Required(ErrorMessage = "University established year is required.")]
        [Range(1000, 2500, ErrorMessage = "Enter valid year")]
        public Nullable<int> EstablishedYear { get; set; }

        [Required(ErrorMessage = "University description is required.")]
        public string Description { get; set; }


    }

    public class UniversityCostModel
    {
        public int UniversityId { get; set; }

        public string UnderGraduateFeeCurrency { get; set; }
        public string UnderGraduateFee { get; set; }
        public string UnderGraduateFeeUnit { get; set; }

        public string GraduateFeeCurrency { get; set; }
        public string GraduateFee { get; set; }
        public string GraduateFeeUnit { get; set; }

        public string BookFeeCurrency { get; set; }
        public string BookFee { get; set; }
        public string BookFeeUnit { get; set; }

        public string BoardFeeCurrency { get; set; }
        public string BoardFee { get; set; }
        public string BoardFeeUnit { get; set; }
    }

    public class UniversityDashboardModel
    {
        public UniversityDashboardModel()
        {
            studentInterestList = new List<Student>();
            studentRecentJoinList = new List<Student>();
            viewPropfileList = new List<ViewProfile>();
        }
        public List<Student> studentInterestList { get; set; }
        public List<Student> studentRecentJoinList { get; set; }
        public List<ViewProfile> viewPropfileList { get; set; }
    }

   
    #region Not Used Now
    public class UserModel
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public int LoginTypeId { get; set; }
        public bool IsActive { get; set; }

        public virtual LoginType LoginType { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<StudentInterest> StudentInterests { get; set; }
        public virtual ICollection<StudentInterest> StudentInterests1 { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
        public virtual ICollection<Alert> Alerts1 { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Message> Messages1 { get; set; }
        public virtual ICollection<University> Universities { get; set; }
    }
    public class AlertModel
    {
        public int AlertId { get; set; }
        public string Message { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UserName { get; set; }

        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
    public class LoginTypeModel
    {
        public int LoginTypeId { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }

    public class sp_NoOFunApprovedMsg_ResultModel
    {
        public Nullable<int> cnt { get; set; }
        public string UniversityName { get; set; }
        public string Image { get; set; }
        public string uname { get; set; }
    }
    public class sp_StudentUnApprovedMsg_ResultModel
    {
        public Nullable<int> cnt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public string uname { get; set; }
    }
    #endregion
}

