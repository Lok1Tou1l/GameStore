using Webapplication.Contracts;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string GetGameEndpoint = "GetGame";

List<GameContract> games = new List<GameContract>
            { new  (1,
              "Cyberpunk 2077", 
              "Action RPG", 
               59.99m, 
               new DateOnly(2020, 12, 10)
                ),
            new  (2,  
            "The Legend of Zelda: Breath of the Wild",
              "Action-Adventure", 
               59.99m,  
               new DateOnly(2017, 03, 03)
                ),
            new  (3, 
            "Doom Eternal",
            "First-Person Shooter",  
            39.99m,  
            new DateOnly(2020, 03, 20)
             ),
            new(4, 
            "Among Us", 
            "Party Game", 
            4.99m, 
            new DateOnly(2018, 06, 15)) };

//GET a specific game
app.MapGet("/games/{id}", (int id) =>
{
    var game = games.FirstOrDefault(g => g.Id == id);
    if (game == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(game);
}).WithName(GetGameEndpoint);

//Get all games
app.MapGet("/games", () => games);

//POST a new game
app.MapPost("/games",(CreateGameContract contract) =>
{
    var newGame = new GameContract(games.Max(g => g.Id) + 1, contract.Name, contract.Genre, contract.Price, contract.ReleaseDate);
    games.Add(newGame);
    return Results.CreatedAtRoute(GetGameEndpoint,$"/games/{newGame.Id}", newGame);
});


app.Run();
