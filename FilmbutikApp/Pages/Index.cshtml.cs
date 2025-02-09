using FilmbutikApp.Pages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FilmbutikApp.Pages
{
    public class IndexModel(RentalService rentalService, UserService userService) : PageModel
    {
        private readonly RentalService _rentalService = rentalService;
        private readonly UserService _userService = userService;

        [BindProperty]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        public bool IsMember { get; set; }

        [BindProperty]
        [Range(0, 100, ErrorMessage = "Antalet måste vara mellan 0 och 100.")]
        public int DvdUnit { get; set; } = 0;

        [BindProperty]
        [Range(0, 100, ErrorMessage = "Antalet måste vara mellan 0 och 100.")]
        public int BluRayUnit { get; set; } = 0;

        public decimal? TotalPrice { get; set; }

        public IActionResult OnPostCalculate()
        {
            ModelState.Remove(nameof(Name));

            var customer = _userService.CreateCustomer(Name, IsMember, DvdUnit, BluRayUnit);
            TotalPrice = _rentalService.CalculateTotalPrice(customer);

            return Page();
        }

        public IActionResult OnPostRegister()
        {
            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(Name) || !Regex.IsMatch(Name, @"^[a-zA-ZåäöÅÄÖ\s]+$"))
            {
                ModelState.AddModelError(nameof(Name), "Namn på kund får endast innehålla bokstäver och mellanslag.");
            }

            if (DvdUnit < 0 || DvdUnit > 100)
            {
                ModelState.AddModelError(nameof(DvdUnit), "Antalet måste vara mellan 0 och 100.");
            }

            if (BluRayUnit < 0 || BluRayUnit > 100)
            {
                ModelState.AddModelError(nameof(BluRayUnit), "Antalet måste vara mellan 0 och 100.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customer = _userService.CreateCustomer(Name, IsMember, DvdUnit, BluRayUnit);
            _userService.RegisterCustomer(customer);

            return RedirectToPage("RegisteredCustomers");
        }
    }
}