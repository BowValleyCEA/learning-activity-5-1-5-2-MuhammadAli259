using System;
using System.Collections.Generic;
public class HighScore
{
    public string Initials { get; set; }
    public int Score { get; set; }
    public HighScore(string initials, int score)
    {
        Initials = initials;
        Score = score;
    }
    public override string ToString()
    {
        return $"{Initials} - {Score}";
    }
}

class Program
{
    static List<HighScore> scores = new();
    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\nPac-Man High Score Board");
            Console.WriteLine("1. Add Score");
            Console.WriteLine("2. View Scores");
            Console.WriteLine("3. Quit");
            Console.Write("Pick an option: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddScore(); break;
                case "2":
                    ViewScores(); break;
                case "3":
                    running = false; Console.WriteLine("Bye!"); break;
                default:
                    Console.WriteLine("Oops, wrong choice. Try again."); break;
            }
        }
    }
    static void AddScore()
    {
        Console.Write("Your initials (3 letters): ");
        string initials = Console.ReadLine()?.ToUpper();
        if (string.IsNullOrWhiteSpace(initials) || initials.Length != 3)
        {
            Console.WriteLine("Use exactly 3 letters."); return;
        }
        Console.Write("Your score: ");
        if (int.TryParse(Console.ReadLine(), out var score))
        {
            scores.Add(new HighScore(initials, score));
            Console.WriteLine("Score added!");
        }
        else
        {
            Console.WriteLine("Enter a valid number.");
        }
    }
    static void ViewScores()
    {
        if (scores.Count == 0)
        {
            Console.WriteLine("No scores to show.");
            return;
        }
        scores.Sort((x, y) => y.Score.CompareTo(x.Score));
        Console.WriteLine("\n=== Scores ===");
        scores.ForEach(score => Console.WriteLine(score));
    }
}