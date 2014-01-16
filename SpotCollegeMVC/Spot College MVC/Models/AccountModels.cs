using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using SpotCollege.DAL;

namespace Spot_College_MVC.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class HomaPageModel
    {
        public HomaPageModel()
        {
            surveyModel = new SurveyModel();
        }
        public LoginModel loginModel { get; set; }
        public RegisterModel registerModel { get; set; }
        public SurveyModel surveyModel { get; set; }
    }

    public class ForgetPasswordModel
    {
        [Required(ErrorMessage="UserName is Required.")]
        [EmailAddress(ErrorMessage="Enter Valid UserName")]
        public string UserName { get; set; } 
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        
        [Required(ErrorMessage="Username is required")]
        [EmailAddress(ErrorMessage="Invalid Username")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage="Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Display(Name = "Remember me?")]
        //public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage="Username is required.")]
        [EmailAddress(ErrorMessage="Enter correct email address")]
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
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }

    //add for facebook login

    public class FacebookLoginModel
    {
        public string uid { get; set; }
        public string accessToken { get; set; }
    }
    public class SurveyModel
    {
        public SurveyModel()
        {
            surveyList = new List<SurveyDetail>();
        }

        public List<SurveyDetail> surveyList { get; set; }
    }
}
