using Microsoft.AspNetCore.Mvc;

namespace GamingHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Health check endpoint - Vérifie que l'API fonctionne
    /// </summary>
    /// <returns>Status de l'API</returns>
    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Health check requested");

        return Ok(new
        {
            status = "Healthy",
            timestamp = DateTime.UtcNow,
            message = "Gaming Hub API is running! 🎮"
        });
    }
}
