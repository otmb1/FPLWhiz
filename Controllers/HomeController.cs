using FPLWhiz.Models;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public class HomeController : Controller
{
    private readonly FantasyRecommendation _fantasyRecommendation;
    private readonly List<Player> _players;

    public HomeController(FantasyRecommendation fantasyRecommendation)
    {
        _fantasyRecommendation = fantasyRecommendation;

        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
        using (var reader = new StreamReader("FPLData.csv"))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            _players = csv.GetRecords<Player>().ToList();
        }
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

    [HttpGet]
    public IActionResult ComparePlayers()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ComparePlayers(string player1Name, string player2Name)
    {
        var player1 = _players.FirstOrDefault(p => p.second_name.Contains(player1Name, StringComparison.OrdinalIgnoreCase));
        var player2 = _players.FirstOrDefault(p => p.second_name.Contains(player2Name, StringComparison.OrdinalIgnoreCase));

        if (player1 == null || player2 == null)
        {
            return View("ComparePlayers", new CompareViewModel
            {
                Player1 = player1,
                Player2 = player2,
                ErrorMessage = "One or both players not found."
            });
        }

        var viewModel = new CompareViewModel
        {
            Player1 = player1,
            Player2 = player2
        };

        return View("ComparePlayers", viewModel);
    }
}
