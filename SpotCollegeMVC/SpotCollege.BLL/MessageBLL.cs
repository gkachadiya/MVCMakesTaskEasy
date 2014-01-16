using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpotCollege.DAL;
using System.Threading.Tasks;

namespace SpotCollege.BLL
{
  public class MessageBLL
    {
        public List<Message> GetAll()
        {
            //for list
            return MessageDAL.FetchAll().ToList();
        }

        public Message Get(int MessageId)
        {
            //for single record
            return MessageDAL.Get(x => x.MessageId == MessageId);
        }



        public void Save(Message msg)
        {
            Message entity = Get(msg.MessageId);
            if (entity != null)
            {                //update


                entity.Title = msg.Title;
                entity.Description = msg.Description;
                entity.ParentId = msg.ParentId;
                entity.FromUserName = msg.FromUserName;
                entity.ToUserName = msg.ToUserName;
                entity.IsRead = msg.IsRead;
                entity.Flag = msg.Flag;
                MessageDAL.Update(entity);
            }
            else
            {
                //save record
                MessageDAL.Add(msg);
            }
        }

        public bool delete(int MessageId)
        {
            Message entity = Get(MessageId);
            if (entity != null)
            {
                //delete
                MessageDAL.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deleteByUserName(string UserName)
        {
            List<Message> entity = MessageDAL.FetchAll().Where(x => x.FromUserName== UserName || x.ToUserName == UserName).ToList();
            foreach (Message sw in entity)
            {
                //delete
                MessageDAL.Delete(sw);
            }
            return true;
        }
    }
}
