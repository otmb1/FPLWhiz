using FPLWhiz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPLWhiz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult GetRecommendation(string playerName)
        {
            try
            {
                var fantasyRecommendation = new FantasyRecommendation();
                string recommendation = fantasyRecommendation.GetRecommendation(playerName);
                return View("Recommendation", recommendation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating a recommendation.");
                throw; // Rethrow the exception for further handling
            }
        }
    }
}
