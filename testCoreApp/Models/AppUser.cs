using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using testCoreApp.Extentions;

namespace testCoreApp.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser(string userName) : base(userName)
        {
        }

        public AppUser() : base()
        {
        }

        [Display(Name = "Имя")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [NotMapped]
        [Display(Name = "Пароль")]
        public string TempPassword { get; set; }

        public override string ToString()
        {
            return this.GetFullName();
        }
    }
}
