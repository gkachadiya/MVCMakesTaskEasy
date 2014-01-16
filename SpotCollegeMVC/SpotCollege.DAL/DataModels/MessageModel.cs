using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotCollege.DAL.DataModels
{
    public class MessageModel
    {
        public int MessageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public int NoofChildMessage { get; set; }
        public string NameColor { get; set; }
        public string IsShow { get; set; }


    }


}
