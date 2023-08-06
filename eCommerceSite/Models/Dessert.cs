using System.ComponentModel.DataAnnotations;

namespace eCommerceSite.Models
{
    /// <summary>
    /// Represents a single dessert
    /// </summary>
    public class Dessert
    {
        /// <summary>
        /// The unique identifier for each game product
        /// </summary>
        [Key]
        public int DessertId { get; set; }

        /// <summary>
        /// The name of the dessert
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The sales price of the dessert
        /// </summary>
        [Range(0, 1000)]
        public double Price { get; set; }

    }
}
