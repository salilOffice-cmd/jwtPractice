using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CarDTOs
{
    public class AddCarDTO
    {
        public string CarName { get; set; }
        public string Contact_Details { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
    }
}
