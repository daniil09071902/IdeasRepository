using IdeasRepository.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdeasRepository.Models 
{

    public class User
    {
        public User() 
        {
            Type = UserType.User;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column("Type")]
        public string TypeName { get; set; }

        public virtual ICollection<Record> Records { get; set; }

        [NotMapped]
        public UserType Type 
        {
            get 
            {
                return EnumExtensions.GetEnumValue<UserType>(TypeName);
            }
            set 
            {
                TypeName = value.ToString();
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
            User user = (User)obj;
            if (!user.Email.Equals(Email))
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return Email.GetHashCode();
        }

    }

    public enum UserType
    {
        Admin,
        User
    }

}