namespace API.Services;

public class PlanetService
{
    private static PlanetService? _instance;
    
    public static PlanetService Instance
    {
        get { return _instance ??= new PlanetService(); }
    }

    private PlanetService()
    { }

    public PlanetResponse GetPlanet()
    {
        using var activity = MonitorService.ActivitySource.StartActivity();
        MonitorService.Log.Debug("Entered GetPlanet");
        
        var planets = new[]
            {
                "Mercury",
                "Venus",
                "Earth",
                "Mars",
                "Jupiter",
                "Saturn",
                "Uranus",
                "Neptune"
            };

            var index = new Random(DateTime.Now.Millisecond).Next(0, planets.Length);
            
            var selectedPlanet = planets[index];
            MonitorService.Log.Debug("Selected planet: {Planet}", selectedPlanet);
            
            return new PlanetResponse
            {
                Planet = selectedPlanet
            };
        }
}

public class PlanetResponse
{
    public string? Planet { get; set; }
}