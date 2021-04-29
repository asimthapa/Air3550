using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Models
{
    public class LoggedUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public long Id { get; set; }
        public bool IsCustomer { get; set; }
    }
}
