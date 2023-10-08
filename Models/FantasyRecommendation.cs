using System;

namespace FPLWhiz.Models
{
    public class FantasyRecommendation
    {
        public string GetRecommendation(string playerName)
        {
            if (playerName.Contains("Saka"))
            {
                return $"Recommendation for {playerName}: Buy!";
            }
            else
            {
                return $"Recommendation for {playerName}: Hold or Sell.";
            }
        }
    }
}
