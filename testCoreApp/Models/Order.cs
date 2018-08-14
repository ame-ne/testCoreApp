using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace testCoreApp.Models
{
    public class Order
    {
        [BindNever]
        public Guid OrderId { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

    }
}
