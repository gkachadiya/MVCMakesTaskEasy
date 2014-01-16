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
    class AdminModel
    {
    }
    public class StudentSearchModel
    {
        public StudentSearchModel()
        {
            studentList = new List<Student>();
        }
        public List<Student> studentList { get; set; }
        public string StudentFrom { get; set; }

        [Range(1, 12, ErrorMessage = "enter valid month")]
        public string FromDateMonth { get; set; }
        [Range(1, 12, ErrorMessage = "enter valid month")]
        public string ToDateMonth { get; set; }

        [Range(0000, 9999, ErrorMessage = "enter valid year")]
        public string FromDateYear { get; set; }
        [Range(0000, 9999, ErrorMessage = "enter valid year")]
        public string ToDateYear { get; set; }

        public string DesiredCountry { get; set; }
        public string DesiredTermStart { get; set; }
        public string DesiredLevel { get; set; }
        public string DesiredProgram { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Not entered any education information")]
        public bool NotEnteredEduInfo { get; set; }
        [Display(Name = "Not uploaded any certificate images")]
        public bool NotUploadCertificate { get; set; }
        [Display(Name = "Not uploaded self photo")]
        public bool NotUploadSelfImg { get; set; }
        [Display(Name = "Not completed educational prefrences")]
        public bool NotComplEduPreferences { get; set; }
        [Display(Name = "Not entered any International test scores")]
        public bool NotEnteredIntScore { get; set; }
        [Display(Name = "Is not active")]
        public bool IsNotActive { get; set; }
    }

    public class UniversitySearchModel
    {
        public UniversitySearchModel()
        {
            UniversityList = new List<University>();
        }

        public List<University> UniversityList { get; set; }
        public string CountryFrom { get; set; }

        [Display(Name = "Incomplete cost of International students")]
        public bool Incompletecost { get; set; }
        [Display(Name = "Incomplete Enrollment numbers")]
        public bool IncompleteEnrollment { get; set; }
        [Display(Name = "No logo image")]
        public bool Nologoimage { get; set; }
        [Display(Name = "No large image")]
        public bool Nolargeimage { get; set; }
        [Display(Name = "Is active")]
        public bool Isactive { get; set; }
        [Display(Name = "Is not active")]
        public bool IsNotActive { get; set; }
    }

    public class MessageCenterModel
    {
        public List<sp_NoOFunApprovedMsg_Result> universityList { get; set; }
        public List<sp_StudentUnApprovedMsg_Result> studentList { get; set; }
        public List<Message> messageList { get; set; }
        public string userType { get; set; }
    }

    public class AdminResourceModel
    {
        public AdminResourceModel()
        {
            resourceCategory = new ResourceCategoryModel();
            resource = new ResourceModel();
            categoryList = new List<ResourceCategory>();
            resourceList = new List<Resource>();
        }
        public ResourceCategoryModel resourceCategory { get; set; }
        public List<ResourceCategory> categoryList { get; set; }

        public ResourceModel resource { get; set; }
        public List<Resource> resourceList { get; set; }
    }

    public class ResourceCategoryModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|0)?(?:\.\d{1,2})?$", ErrorMessage = "Enter proper value")]
        public string SortingIndex { get; set; }
    }

    public class ResourceModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ResourceId { get; set; }

        [UIHint("DropDownList")]
        [Required(ErrorMessage = "Please select a Category")]
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ImagePath { get; set; }

        public HttpPostedFileBase ImageResource { get; set; }

       
    }

    public class SubUserManagementModel
    {
        public SubUserManagementModel()
        {
            permissionModel = new PermissionModel();
            permissionList = new List<Permission>();
        }

        public PermissionModel permissionModel { get; set; }
        public List<Permission> permissionList { get; set; }
    }

    public class PermissionModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PermissionId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Enter correct email address")]
        [Required(ErrorMessage = "Email is required.")]
        public string UserName { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "PhoneNo is required.")]
        [RegularExpression(@"\d{6,10}", ErrorMessage = "Enter proper value")]
        public string Phone { get; set; }

        [Display(Name = "Student Message Management")]
        public bool StudentMessage { get; set; }

        [Display(Name = "College Profile Management")]
        public bool CollegeProfile { get; set; }

        [Display(Name = "College Message management")]
        public bool CollegeMessage { get; set; }

        [Display(Name = "Article management")]
        public bool Article { get; set; }

        [Display(Name = "Review management")]
        public bool Review { get; set; }

        public System.DateTime CreatedDate { get; set; }
    }

    public class SurveyModel
    {
        public SurveyModel()
        {
            surveyList = new List<SurveyDetail>();
            universityList = new List<University>();
        }

        public List<SurveyDetail> surveyList { get; set; }
        public List<University> universityList { get; set; }
    }
    
}
