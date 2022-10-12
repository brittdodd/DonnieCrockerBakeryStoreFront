using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjName.DATA.EF.Models//Metadata
{
    #region BakeryProducts
    public class BakeryProductsMetadata
    {
        
        public int ProductID { get; set; }

        public int CategoryID { get; set; }
        
        [Required]
        [StringLength(200, ErrorMessage = "Cannot exceed 200 characters")]
        [Display(Name = "Bakery Items")]
        public string BakeryItems { get; set; }
        
        [Required]
        [StringLength(2, ErrorMessage = "Cannot exceed 2 characters")]
        [Display(Name = "Quantity")]
        public string QuantityPerUnit { get; set; }
        
        [Required]
        [Display(Name = "Quantity")]
        public decimal Price { get; set; }

        public bool? Discontinued { get; set; }

        
        public int SeasonalID { get; set; }

    }
        #endregion

    #region Categories
    public class CategoryMetadata
    {
        public int CategoryID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Cannot exceed 25 characters")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        
        [StringLength(500, ErrorMessage = "Cannot exceed 500 characters")]
        [Display(Name = "Description")]
        public string? CategoryDescription { get; set; }

        [StringLength(75, ErrorMessage = "Cannot exceed 75 characters")]
        [Display(Name = "Image")]
        public string? Picture { get; set; }
    }
    #endregion

    #region Seasons

    public class SeasonsMetadata
    {
        public int SeasonID { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "Cannot exceed 6 characters")]
        [Display(Name = "Season Name")]
        public string SeasonName { get; set; }
    }

    #endregion

    #region UserDetails

    public class UserDetailsMetadata
    {
        [Required]
        [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(150, ErrorMessage = "*Cannot exceed 150 characters")]
        public string? Address { get; set; }
        [StringLength(50, ErrorMessage = "Cannot exceep 50 characters")]
        public string? City { get; set; }
        [StringLength(2, ErrorMessage = "Cannot exceep 2 characters")]
        public string? State { get; set; }
        [StringLength(5, ErrorMessage = "Cannot exceep 5 characters")]
        public string? Zip { get; set; }
        [StringLength(24, ErrorMessage = "Cannot exceep 24 characters")]
        public string? Phone { get; set; }

    }
    #endregion

}
