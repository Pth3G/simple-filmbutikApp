using FilmbutikApp.Pages.Models;
using FilmbutikApp.Pages.Models.Types;

namespace FilmbutikApp.Pages.Services
{
    public class RentalService
    {
        public decimal CalculateTotalPrice(Customer customer)
        {
            if (!customer.RentedMovies.Any())
            {
                return 0;
            }

            if (!customer.IsMember)
            {
                return customer.RentedMovies.Sum(m => m.Price);
            }

            var sortedMovies = customer.RentedMovies.OrderBy(m => m.Price).ToList();

            decimal firstFourMovies = sortedMovies.Take(4).Sum(m => m.Price);
            decimal firstFourFinalPrice = Math.Min(100, firstFourMovies);

            decimal discountedExtraMovies = sortedMovies.Skip(4).Sum(m => CalculateDiscountedPrice(m));

            return firstFourFinalPrice + discountedExtraMovies;
        }

        private decimal CalculateDiscountedPrice(Movie movie)
        {
            return movie.Type == MovieType.DVD ? movie.Price * 0.9m : movie.Price * 0.85m;
        }
    }
}