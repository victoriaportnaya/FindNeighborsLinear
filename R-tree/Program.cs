// download data
// console program to get user lat long rad 
// distance to the point (heap) and show if the distance is less than rad
using System;
using System.IO;
using System.Globalization;
using Microsoft.VisualBasic.CompilerServices;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        var sw = new Stopwatch();
        sw.Start();
        string csvCoordinates = "C:\\Users\\victo\\RiderProjects\\R-tree\\R-tree\\positions.csv";

        string[] lines = File.ReadAllLines(csvCoordinates);
        
        // get user`s coordinates
        Console.WriteLine("Your latitude>>");
        double userLat;
        while (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out userLat))
        {
            Console.WriteLine("Invalid input. Please enter a valid number for latitude:");
        }

        
        Console.WriteLine("Enter your longitude>>");
        double userLon;
        while (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out userLon))
        {
            Console.WriteLine("Invalid input. Please enter a valid number for longitude:");
        }
        
        Console.WriteLine("Enter the radius (in km)>>");
        double radius = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

        foreach (string line in lines)
        {
            var parts = line.Split(';');
            if (!double.TryParse(parts[0], out double latitude)) continue;
            if (!double.TryParse(parts[1], out double longitude)) continue;
            string placeType = parts[2];
            double distance = CalculateDistance(userLat, userLon, latitude, longitude);

            if (distance <= radius)
            {
                Console.WriteLine($" TYPE {placeType} | LOCATION ({latitude}, {longitude})");
            }
        }
        sw.Stop();
        Console.WriteLine($"Elapsed time: {sw.Elapsed}");
        

    }

    static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371;

        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var distance = R * c;

        return distance;
    }

    static double ToRadians(double angle)
    {
        return Math.PI * angle / 180.0;
    }
}