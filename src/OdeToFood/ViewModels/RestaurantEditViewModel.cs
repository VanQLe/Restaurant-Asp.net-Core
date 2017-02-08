using OdeToFood.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.ViewModels
{
    public class RestaurantEditViewModel
    {
        [Required, MaxLength(30)]
        public string  Name{ get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
