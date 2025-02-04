using FilmbutikApp.Pages.Models;
using FilmbutikApp.Pages.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FilmbutikApp.Pages
{
    public class RegisteredCustomersModel(RentalService rentalService, UserService userService) : PageModel
    {
        private readonly RentalService _rentalService = rentalService;
        private readonly UserService _userService = userService;

        public IReadOnlyList<Customer>? RegisteredCustomers { get; private set; }

        public void OnGet()
        {
            RegisteredCustomers = _userService.GetRegisteredCustomers();
        }

        public decimal CalculateTotalPrice(Customer customer)
        {
            return _rentalService.CalculateTotalPrice(customer);
        }
    }
}
