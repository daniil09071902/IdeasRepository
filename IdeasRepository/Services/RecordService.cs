using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdeasRepository.Models;
using System.Data.Entity;

namespace IdeasRepository.Services
{
    public class RecordService : IRecordService 
    {
        public void Save(Record record, string authorEmail)
        {
            using (IdeasContext context = new IdeasContext())
            {
                User user = context.Users.FirstOrDefault(u => u.Email == authorEmail);
                user.Records.Add(record);
                context.SaveChanges();
            }
        }

        public void Update(Record record)
        {
            using (IdeasContext context = new IdeasContext())
            {
                record.UpdateDate = DateTime.Now;
                context.Entry(record).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(int recordId, string removerEmail)
        {
            using (IdeasContext context = new IdeasContext())
            {
                Record record = context.Records.Include(r => r.Author).First(r => r.Id == recordId);
                User remover = context.Users.First(u => u.Email == removerEmail);
                User author = record.Author;
                if ((author.Equals(remover) && remover.Type.Equals(UserType.Admin)) ||
                    (author.Equals(remover) && record.Status.Equals(RecordStatus.RemovedByAdmin)) ||
                    (remover.Type.Equals(UserType.Admin) && record.Status.Equals(RecordStatus.RemovedByUser)))
                {
                    context.Records.Remove(record);
                    context.SaveChanges();
                    return;
                }
                if (remover.Type.Equals(UserType.User))
                {
                    record.Status = RecordStatus.RemovedByUser;
                }
                else
                {
                    record.Status = RecordStatus.RemovedByAdmin;
                }
                context.SaveChanges();
            }
        }

        public void ConfirmDeletion(int recordId)
        {
            using (IdeasContext context = new IdeasContext())
            {
                Record record = context.Records.First(r => r.Id == recordId);
                context.Records.Remove(record);
                context.SaveChanges();
            }    
        }

        public void RestoreRecord(int recordId)
        {
            using (IdeasContext context = new IdeasContext())
            {
                Record record = context.Records.First(r => r.Id == recordId);
                record.Status = RecordStatus.Normal;
                context.SaveChanges();
            }
        }

        public IEnumerable<Record> GetUserRecords(string authorEmail)
        {
            using (IdeasContext context = new IdeasContext())
            {
                return context.Records.Include(r => r.Author).Where(r => r.Author.Email == authorEmail)
                    .OrderBy(r => r.UpdateDate).ToList();
            }
        }

        public IEnumerable<Record> GetRecords()
        {
            using (IdeasContext context = new IdeasContext())
            {
                return context.Records.Include(r => r.Author).OrderBy(r => r.UpdateDate).ToList();
            }
        }

    }
}