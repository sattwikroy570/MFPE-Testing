using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models
{
    public class CustomerDetails
    {
        [Required]
        [Key]
        public string CustomerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        public string PanNumber { get; set; }
    }
}
