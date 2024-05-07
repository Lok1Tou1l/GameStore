namespace Webapplication.Contracts;

public record class CreateGameContract(
    string Name , 
    string Genre , 
    decimal Price , 
    DateOnly ReleaseDate) ;
