using System.ComponentModel.DataAnnotations;

namespace _22DH112705_TranTanPhat.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ManufacturingDate { get; set; }
        public decimal Price { get; set; }
    }
}