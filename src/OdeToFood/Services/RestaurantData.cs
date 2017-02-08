using OdeToFood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);

        Restaurant Add(Restaurant restaurant);
        void Commit();
    }

    public class SqlRestaurantData : IRestaurantData
    {
        private OdeToFoodDbContext _context;

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }
        public Restaurant Add(Restaurant restaurant)
        {
            _context.Add(restaurant);
            //_context.SaveChanges();//saves the changes inside the database. (Commit)
            Commit();

            return restaurant;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants;
        }
    }


    public class InMemoryRestaurantData : IRestaurantData
    {
        static InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant {Id = 1, Name = "Van's Restaurant" },
                new Restaurant {Id = 2, Name = "Thien's Restaurant" },
                new Restaurant {Id = 3, Name = "Duc's Restaurant" },
                new Restaurant {Id = 4, Name = "Huy's Restaurant" }

            };//Not thread safe
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants;
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {

            newRestaurant.Id = _restaurants.Max(r => r.Id) + 1;

            _restaurants.Add(newRestaurant);

            return newRestaurant;
        }

        public void Commit()
        {
            // Not needed
        }

        static List<Restaurant> _restaurants;
    }
}
