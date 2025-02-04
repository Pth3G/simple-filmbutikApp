namespace FilmbutikApp.Pages.Models;
public class Customer
{
    public string Name { get; }
    public bool IsMember { get; }
    public IReadOnlyList<Movie> RentedMovies => _rentedMovies;

    private readonly List<Movie> _rentedMovies;

    public Customer(string name, bool isMember)
    {
        Name = name;
        IsMember = isMember;
        _rentedMovies = new List<Movie>();
    }

    public void RentMovie(Movie movie)
    {
        if (movie == null) throw new ArgumentNullException(nameof(movie), "Movie cannot be null");
        _rentedMovies.Add(movie);
    }
}