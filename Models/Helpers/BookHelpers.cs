using System.Collections.Generic;

namespace LibraryManagementApp.Models.Helpers;

public static class BookHelpers
{
    public static bool HasUserGivenRating(int userId, Dictionary<int, double> ratings)
    {
        bool hasUserRated = ratings.ContainsKey(userId);
        
        return hasUserRated;
    }

    public static double CalculateAverageRating(Dictionary<int, double> ratings)
    {
        var ratingsSum = 0.0;
        
        foreach(KeyValuePair<int, double> rating in ratings)
        {
            ratingsSum += rating.Value;
        }
        
        return ratingsSum / ratings.Count;
    }
}