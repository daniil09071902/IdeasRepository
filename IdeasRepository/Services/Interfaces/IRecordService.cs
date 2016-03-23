using IdeasRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeasRepository.Services
{
    public interface IRecordService
    {
        void Save(Record record, string authorEmail);
        void Update(Record record); 
        void Delete(int recordId, string removerEmail);
        void ConfirmDeletion(int recordId);
        void RestoreRecord(int recordId);
        IEnumerable<Record> GetUserRecords(string authorEmail);
        IEnumerable<Record> GetRecords();
    }
}
