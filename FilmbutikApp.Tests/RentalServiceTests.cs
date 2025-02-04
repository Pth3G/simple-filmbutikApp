using FilmbutikApp.Pages.Models;
using FilmbutikApp.Pages.Models.Types;
using FilmbutikApp.Pages.Services;
using Xunit;

namespace FilmbutikApp.Tests;

public class RentalServiceTests
{
    private readonly RentalService _rentalService;

    public RentalServiceTests()
    {
        _rentalService = new RentalService();
    }

    [Theory]
    [InlineData(4, 0, 100)]
    [InlineData(0, 4, 100)]
    [InlineData(2, 3, 133.15)]
    [InlineData(3, 2, 133.15)]
    [InlineData(4, 4, 232.60)]
    [InlineData(8, 0, 204.40)]
    [InlineData(0, 8, 232.60)]
    [InlineData(5, 4, 258.70)]
    [InlineData(4, 5, 265.75)]
    public void ClubMember_RentsMovies_CorrectTotalPrice(int dvdUnit, int blueRayUnit, decimal expectedPrice)
    {
        var customer = new Customer("Member", true);

        for (int i = 0; i < dvdUnit; i++)
            customer.RentMovie(new Movie(MovieType.DVD));

        for (int i = 0; i < blueRayUnit; i++)
            customer.RentMovie(new Movie(MovieType.BluRay));

        decimal totalPrice = _rentalService.CalculateTotalPrice(customer);

        Assert.Equal(expectedPrice, totalPrice);
    }

    [Theory]
    [InlineData(4, 0, 116)]
    [InlineData(0, 4, 156)]
    [InlineData(2, 3, 175)]
    [InlineData(4, 4, 272)]
    [InlineData(0, 8, 312)]
    [InlineData(8, 0, 232)]
    [InlineData(5, 4, 301)]
    [InlineData(4, 5, 311)]
    public void NonMember_RentsMovies_CorrectTotalPrice(int dvdUnit, int blueRayUnit, decimal expectedPrice)
    {
        var customer = new Customer("Non-Member", false);

        for (int i = 0; i < dvdUnit; i++)
            customer.RentMovie(new Movie(MovieType.DVD));

        for (int i = 0; i < blueRayUnit; i++)
            customer.RentMovie(new Movie(MovieType.BluRay));

        decimal totalPrice = _rentalService.CalculateTotalPrice(customer);

        Assert.Equal(expectedPrice, totalPrice);
    }

    [Theory]
    [InlineData(0, 0, true, 0)]
    [InlineData(0, 0, false, 0)]
    public void Customer_RentsNoMovies_ShouldBeZero(int dvdUnit, int blueRayUnit, bool isMember, decimal expectedPrice)
    {
        var customer = new Customer("AllTypeCustomer", isMember);

        for (int i = 0; i < dvdUnit; i++)
            customer.RentMovie(new Movie(MovieType.DVD));

        for (int i = 0; i < blueRayUnit; i++)
            customer.RentMovie(new Movie(MovieType.BluRay));

        decimal totalPrice = _rentalService.CalculateTotalPrice(customer);

        Assert.Equal(expectedPrice, totalPrice);
    }
}