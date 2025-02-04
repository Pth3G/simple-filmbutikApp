using FilmbutikApp.Pages.Models.Types;

namespace FilmbutikApp.Pages.Models;

public class Movie
{
    public const decimal DVDPrice = 29m;
    public const decimal BluRayPrice = 39m;

    public MovieType Type { get; }
    public decimal Price => Type == MovieType.DVD ? DVDPrice : BluRayPrice;

    public Movie(MovieType type)
    {
        Type = type;
    }
}
