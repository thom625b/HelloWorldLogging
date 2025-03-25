using Messages;

namespace API.Services;

public class LanguageService
{
    private static LanguageService? _instance;
    
    public static LanguageService Instance
    {
        get { return _instance ??= new LanguageService(); }
    }
    
    private LanguageService()
    { }
    
    public LanguageResponse GetLanguages()
    {
        using var activity = MonitorService.ActivitySource.StartActivity();
        MonitorService.Log.Debug("Entered GetLanguages");

        return new LanguageResponse
        {
            Languages = GreetingService.Instance.GetLanguages()
        };
    }
}