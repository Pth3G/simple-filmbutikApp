using FilmbutikApp.Pages.Models;
using FilmbutikApp.Pages.Models.Types;

namespace FilmbutikApp.Pages.Services;

public class UserService
{
    private readonly List<Customer> _registeredCustomers = new();

    public Customer CreateCustomer(string name, bool isMember, int dvdUnit, int blueRayUnit)
    {
        var customer = new Customer(name, isMember);

        for (int i = 0; i < dvdUnit; i++)
            customer.RentMovie(new Movie(MovieType.DVD));

        for (int i = 0; i < blueRayUnit; i++)
            customer.RentMovie(new Movie(MovieType.BluRay));

        return customer;
    }

    public void RegisterCustomer(Customer customer)
    {
        _registeredCustomers.Add(customer);
    }

    public IReadOnlyList<Customer> GetRegisteredCustomers()
    {
        return _registeredCustomers.AsReadOnly();
    }
}