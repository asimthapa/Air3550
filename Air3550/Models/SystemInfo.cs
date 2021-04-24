using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Air3550.Models
{
    public class SystemInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public SystemInfoType Type { get; set; }

        public DateTime FlightsGeneratedTill { get; set; }
    }
}