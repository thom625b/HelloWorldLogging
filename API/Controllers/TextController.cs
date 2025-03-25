using API.Models;
using API.Services;
using Messages;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TextController : ControllerBase
{
    [HttpGet]
    public IActionResult Get(string languageCode)
    {
        try
        { 
            //sets the name as ProcessGreeting
            using (var activity = MonitorService.ActivitySource.StartActivity("ProcessGreeting"))
            {
                var greeting = GreetingService.Instance.Greet(new GreetingRequest { LanguageCode = languageCode });
                
                var response = new GetGreetingModel.Response
                {
                    Greeting = greeting.Greeting,

                    Planet = greeting.Planet
                };
                return Ok(response);
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, "An error occurred");
        }
    }
}
