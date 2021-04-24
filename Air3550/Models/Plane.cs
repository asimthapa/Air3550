using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Air3550.Models
{
    public class Plane
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Model { get; set; }

        public int Capacity { get; set; }
    }
}