using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Air3550.Models
{
    public class Airport
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}