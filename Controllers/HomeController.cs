using FPLWhiz.Models;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly FantasyRecommendation _fantasyRecommendation;

    public HomeController(FantasyRecommendation fantasyRecommendation)
    {
        _fantasyRecommendation = fantasyRecommendation;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GetRecommendation(string playerName)
    {
        try
        {
            string recommendation = _fantasyRecommendation.GetRecommendation(playerName);
            return PartialView("_RecommendationPartial", recommendation);
        }
        catch (Exception ex)
        {
            return PartialView("_RecommendationPartial", "An error occurred.");
        }
    }
}
