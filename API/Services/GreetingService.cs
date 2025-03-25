using API.Services;

public class GreetingService
{
    private static GreetingService? _instance;
    
    public static GreetingService Instance
    {
        get { return _instance ??= new GreetingService(); }
    }
    
    private GreetingService()
    { }
    
    public GreetingResponse Greet(Messages.GreetingRequest request)
    {
        using var activity = MonitorService.ActivitySource.StartActivity();
        
            MonitorService.Log.Debug("Entered Greet");

            var language = request.LanguageCode;
            var greeting = language switch
            {
                "en" => "Hello",
                "es" => "Hola",
                "fr" => "Bonjour",
                "de" => "Hallo",
                "it" => "Ciao",
                "pt" => "Olá",
                "ru" => "Привет",
                "zh" => "你好",
                "ja" => "こんにちは",
                "ar" => "مرحبا",
                "hi" => "नमस्ते",
                "sw" => "Hujambo",
                _ => "Hello" // Default case for unknown languages
            };

            // Use the PlanetService to get the planet
            // This will maintain the activity context
            var planetResponse = PlanetService.Instance.GetPlanet();

            return new GreetingResponse 
            { 
                Greeting = greeting,
                Planet = planetResponse.Planet
            };
    }
    
    public string[] GetLanguages()
    {
        MonitorService.Log.Debug("Entered GetLanguages array");

        return new [] { "en", "es", "fr", "de", "it", "pt", "ru", "zh", "ja", "ar", "hi", "sw" };
    }
}