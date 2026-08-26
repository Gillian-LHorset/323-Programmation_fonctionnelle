// See https://aka.ms/new-console-template for more information
using DataSerie;
using EsportApp_FilRouge;

Console.WriteLine("Hello, World!");

var valorantMatches = new[]
{
    new ValorantMatch("Léa", "Jett",  18, 6, 4, 8,  13, true),
    new ValorantMatch("Léa", "Reyna", 22, 8, 2, 11,  9, false),
    new ValorantMatch("Léa", "Neon",  20, 7, 5,  9, 13, true),
};

var valorant = DataSeries<ValorantMatch>.From(valorantMatches);
Console.WriteLine(valorant.Count); // 3