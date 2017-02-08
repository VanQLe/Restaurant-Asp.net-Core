using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Entities
{
    public enum CuisineType
    {
        None,
        Italian,
        French,
        Japanese,
        American
    }

    public class Restaurant
    {
        public int Id { get; set; }

        [Required,MaxLength(30)]
        [Display(Name = "Restaurant Name")]
        public string Name { get; set; }

        [Display(Name = "Cuisine Type")]
        public CuisineType Cuisine { get; set; }
    }
}
