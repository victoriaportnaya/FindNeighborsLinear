// download data
// console program to get user lat long rad 
// distance to the point (heap) and show if the distance is less than rad
using System;
using System.IO;
using System.Globalization;
using Microsoft.VisualBasic.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        string csvCoordinates = "coordinates.csv";

        string[] lines = File.ReadAllLines(csvCoordinates);
        
        // get user`s coordinates
        Console.WriteLine("Your latitude>>");
        double userLat = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        
        Console.WriteLine("Enter your longitude>>");
        double userLon = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        
        Console.WriteLine("Enter the radius (in km)>>");
        double radius = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

        string line;
        while ((line = Console.ReadLine()) != null && line != "exit")
        {
            var parts = line.Split(',');
            if (parts.Length < 2)
            {
                Console.WriteLine("Invalid Input");
                continue;
            }

            double pointLat, pointLon;
            if (!double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out pointLat)
                || !double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out pointLon))
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            double distance = CalculateDistance(userLat, userLon, pointLat, pointLon);

            if (distance <= radius)
            {
                Console.WriteLine($"Place at ({pointLat}, {pointLon}) is within {radius} km from you.");
            }
        }
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