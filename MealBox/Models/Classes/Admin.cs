using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace MealBox.Models.Classes
{
    public class Admin
    {


        [Key]

       
        public int AdminID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Surname { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Password { get; set; }


        [Column(TypeName = "Varchar")]
		[StringLength(30)]
		public string? Province { get; set; } 
        [Column(TypeName = "Varchar")]
		[StringLength(30)]
		public string? City { get; set; } 
        [Column(TypeName = "Varchar")]
		[StringLength(250)]
		public string? Address { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string Mail { get; set; }
        [Column(TypeName = "Varchar")]
		[StringLength(30)]
		public string Phone { get; set; } 
        [Column(TypeName = "Varchar")]
		[StringLength(30)]
		public string? Gender { get; set; } 

        [Column(TypeName = "Varchar")]
		[StringLength(30)]
		public string? DateOfBirth { get; set; } 

        [Column(TypeName = "Char")]
        [StringLength(1)]
        public string? Authority {  get; set; } 

        public ICollection<Product> Products { get; set; }


    }
}
