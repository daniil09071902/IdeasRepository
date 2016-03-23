using IdeasRepository.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdeasRepository.Models
{

    public class Record
    {
        public Record() 
        {
            UpdateDate = DateTime.Now;
            Status = RecordStatus.Normal;
        }

        public int Id { get; set; }

        [Required]
        public String Text { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }

        [Required]
        [Column("CreationDate")]
        public DateTime UpdateDate { get; set; }
        
        [Required]
        [Column("Status")]
        public string StatusName { get; set; }

        [NotMapped]
        public RecordStatus Status 
        {
            get
            {
                return EnumExtensions.GetEnumValue<RecordStatus>(StatusName);
            }
            set
            {
                StatusName = value.ToString();
            }
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            Record record = (Record)obj;
            if (!record.Author.Equals(Author) && !record.UpdateDate.Equals(UpdateDate))
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return UpdateDate.GetHashCode();
        }

    }

    public enum RecordStatus
    {
        Normal,
        RemovedByUser,
        RemovedByAdmin
    }

}