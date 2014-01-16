using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotCollege.DAL.DataModels
{   
    [Table("Student")]
    public class StudentDashboardModel
    {
        public List<User> studentlist { get; set; }
        public List<User> studentlist2 { get; set; }
        public List<User> universityList { get; set; }
        public List<User> alertList { get; set; }
    }
    public class StudentEditModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
