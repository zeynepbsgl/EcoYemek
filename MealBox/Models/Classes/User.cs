using System.ComponentModel.DataAnnotations;

namespace MealBox.Models.Classes
{
    public class User
        
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public double? Latitude { get; set; } // Enlem

        public double? Longitude { get; set; } // Boylam

        public ICollection<Product> Products { get; set; }
    }

}
