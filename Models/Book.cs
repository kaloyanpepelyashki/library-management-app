using System;
using System.Collections.Generic;
using LibraryManagementApp.Models.Helpers;

namespace LibraryManagementApp.Models;

public class Book
{
   public int Id { get; set; }
   public string Title { get; set; }
   public string Description { get; set; }
   public string Author { get; set; }
   public string ISBN { get; set; }
   public bool IsBorrowed { get; set; }
   private bool HasBeenBorrowed { get; set; }
   
   /// <summary>
   /// Stores all ratings given by users.
   /// The key is user Id, and the value is the rating itself. 
   /// </summary>
   private Dictionary<int, double> Ratings { get; set; }
   
   public double AverageRating { get; set; }

   public void SetBorrowed(bool isCurrentlyBorrowed)
   {
      if (isCurrentlyBorrowed && !HasBeenBorrowed)
      {
         HasBeenBorrowed = true;
      }
      
      IsBorrowed = isCurrentlyBorrowed;
   }

   public void AddRating(int userId, double rating)
   {
      bool hasUserRated = BookHelpers.HasUserGivenRating(userId, Ratings);

      if (hasUserRated)
      {
         throw new Exception("User has already rated the book");
      }
      
      Ratings.Add(userId, rating);
   }

   public void SetAverageRating()
   {
      AverageRating = BookHelpers.CalculateAverageRating(Ratings);
   }
   
}