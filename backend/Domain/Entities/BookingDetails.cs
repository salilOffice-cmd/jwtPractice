using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookingDetails
    {
        [Key]
        public int BookingId { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string Contact_Details { get; set; }
        public int Price { get; set; }
        public string LicenseNumber { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public string Category { get; set; }
    }
}
