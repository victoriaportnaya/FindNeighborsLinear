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
            
        }
    }
}