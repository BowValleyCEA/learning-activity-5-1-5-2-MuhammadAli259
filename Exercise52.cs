using System;
using System.Collections.Generic; 
public class Video 
{ 
    public string Name { get; set; } 
    public int VideoID { get; set; } 
    public string Genre { get; set; } 
    public int Duration { get; set; } 
    public bool IsRented { get; private set; } 
    public Video(string name, int videoID, string genre, int duration) 
    { 
        Name = name; 
        VideoID = videoID; 
        Genre = genre; 
        Duration = duration; 
        IsRented = false; 
    } 
    public void ToggleRentalStatus() 
    { 
        IsRented = !IsRented; 
    } 
    public override string ToString() 
    { 
        return $"{VideoID}: {Name} - {Genre} ({Duration} mins)"; 
    }
} 
public class User 
{ 
    public string Name { get; set; } 
    public int UserID { get; set; } 
    public List<Video> CurrentRentals { get; private set; } 
    public List<Video> PastRentals { get; private set; } 
    public User(string name, int userID) 
    { 
        Name = name; 
        UserID = userID; 
        CurrentRentals = new List<Video>(); 
        PastRentals = new List<Video>(); 
    } 
    public void RentVideo(Video video) 
    { 
        if (video.IsRented) 
        { 
            Console.WriteLine($"{video.Name} is unavailable."); 
        } 
        else 
        { 
            CurrentRentals.Add(video); 
            video.ToggleRentalStatus(); 
            Console.WriteLine($"{Name} rented {video.Name}."); 
        } 
    }
    public void ReturnVideo(Video video) 
    { 
        if (CurrentRentals.Contains(video)) 
        { 
            CurrentRentals.Remove(video); 
            PastRentals.Add(video); 
            video.ToggleRentalStatus(); 
            Console.WriteLine($"{Name} returned {video.Name}."); 
        } 
        else 
        { 
            Console.WriteLine($"{video.Name} was not rented by {Name}."); 
        }
    } 
    public void ShowRentals() 
    { 
        Console.WriteLine($"\nCurrent Rentals for {Name}:"); 
        if (CurrentRentals.Count == 0) 
        { 
            Console.WriteLine("No videos currently rented."); 
        } 
        else 
        { 
            foreach (var video in CurrentRentals) 
            { 
                Console.WriteLine(video.ToString()); 
            } 
        } 
        Console.WriteLine($"\nPast Rentals for {Name}:"); 
        if (PastRentals.Count == 0) 
        { 
            Console.WriteLine("No past rentals."); 
        } 
        else 
        {
            foreach (var video in PastRentals) 
            { 
                Console.WriteLine(video.ToString()); 
            } 
        } 
    } 
} 
public class RentalService 
{ 
    private List<User> users; 
    private List<Video> videos; 
    public RentalService() 
    { 
        users = new List<User>(); 
        videos = new List<Video>(); 
    } 
    public void AddUser(string name)
    { 
        int userID = users.Count + 1; 
        users.Add(new User(name, userID)); 
        Console.WriteLine($"User {name} added with ID {userID}."); 
    }
    public void AddVideo(string name, string genre, int duration)
    { 
        int videoID = videos.Count + 1; 
        videos.Add(new Video(name, videoID, genre, duration)); 
        Console.WriteLine($"Video {name} added with ID {videoID}."); 
    } 
    public void ProcessRental(int userID, int videoID, bool isReturning) 
    { 
        User user = users.Find(u => u.UserID == userID); 
        Video video = videos.Find(v => v.VideoID == videoID); 
        if (user == null) 
        { 
            Console.WriteLine("User not found."); 
            return; 
        }
        if (video == null) 
        { 
            Console.WriteLine("Video not found."); 
            return;
        } 
        if (isReturning) 
        { 
            user.ReturnVideo(video); 
        } 
        else 
        { 
            user.RentVideo(video); 
        } 
    } 
    public void ShowAvailableVideos() 
    { 
        Console.WriteLine("\nAvailable Videos:"); 
        foreach (var video in videos) 
        { 
            if (!video.IsRented) 
            { 
                Console.WriteLine(video.ToString()); 
            } 
        } 
    } 
    public void ShowUserRentals(int userID) 
    { 
        User user = users.Find(u => u.UserID == userID); 
        if (user != null) 
        {
            user.ShowRentals(); 
        } 
        else 
        { 
            Console.WriteLine("User not found."); 
        } 
    }
} 

class Program
{ 
    static void Main(string[] args) 
    { 
        RentalService system = new RentalService(); 
        system.AddUser("John"); 
        system.AddUser("Alice"); 
        system.AddVideo("Inception", "Sci-Fi", 148); 
        system.AddVideo("The Matrix", "Action", 136); 
        system.AddVideo("Finding Nemo", "Animation", 100); 
        system.ShowAvailableVideos(); 
        system.ProcessRental(1, 1, false); // John rents Inception 
        system.ProcessRental(2, 2, false); // Alice rents The Matrix 
        system.ShowAvailableVideos(); 
        system.ProcessRental(1, 1, true); // John returns Inception
        system.ShowAvailableVideos(); 
        system.ShowUserRentals(1); // Show John's rentals 
    }
}