// See https://aka.ms/new-console-template for more information
using DataSerie;
using EsportApp_FilRouge;

Console.WriteLine("Hello, World!");

ValorantMatch ParseValorant(string[] cols) => new ValorantMatch(
    cols[1],              // player
    cols[2],              // agent
    int.Parse(cols[3]),   // kills
    int.Parse(cols[4]),   // deaths
    int.Parse(cols[5]),   // assists
    int.Parse(cols[6]),   // headshots
    int.Parse(cols[7]),   // roundsWon
    bool.Parse(cols[8])   // won
);

Cs2Match ParseCs2(string[] cols) => new Cs2Match(
    cols[1],              // player
    cols[2],              // map
    cols[3],              // startSide (côté joué en 1re mi-temps — CT ou T)
    int.Parse(cols[4]),   // kills
    int.Parse(cols[5]),   // deaths
    int.Parse(cols[6]),   // assists
    int.Parse(cols[7]),   // mvps
    bool.Parse(cols[8])   // won
);

LolMatch ParseLol(string[] cols) => new LolMatch(
    cols[1],              // player
    cols[2],              // champion
    int.Parse(cols[4]),   // kills
    int.Parse(cols[5]),   // deaths
    int.Parse(cols[6]),   // assists
    int.Parse(cols[7]),   // cs
    int.Parse(cols[8]),   // visionScore
    bool.Parse(cols[9])   // won
);

var valorant = DataSeries<ValorantMatch>.FromCsv("C:/Users/pq60soi/Desktop/323-Programmation_fonctionnelle/personnel/fil-rouge/code/data/valorant.csv", ParseValorant);
var cs2 = DataSeries<Cs2Match>.FromCsv("C:/Users/pq60soi/Desktop/323-Programmation_fonctionnelle/personnel/fil-rouge/code/data/cs2.csv", ParseCs2);
var lol = DataSeries<LolMatch>.FromCsv("C:/Users/pq60soi/Desktop/323-Programmation_fonctionnelle/personnel/fil-rouge/code/data/lol.csv", ParseLol);

//var valorant = DataSeries<ValorantMatch>.From(new[]
//{
//    new DataPoint<ValorantMatch>(new DateTime(2024, 1, 15), new ValorantMatch("Léa", "Jett",  18, 6, 4, 8,  13, true)),
//    new DataPoint<ValorantMatch>(new DateTime(2024, 2,  3), new ValorantMatch("Léa", "Reyna", 22, 8, 2, 11,  9, false)),
//    new DataPoint<ValorantMatch>(new DateTime(2024, 3, 10), new ValorantMatch("Léa", "Neon",  20, 7, 5,  9, 13, true)),
//});

//var cs2 = DataSeries<Cs2Match>.From(new[]
//{
//    new DataPoint<Cs2Match>(new DateTime(2024, 1, 20), new Cs2Match("Raphaël", "Mirage",  "CT", 21, 14, 5, 2, true)),
//    new DataPoint<Cs2Match>(new DateTime(2024, 2,  7), new Cs2Match("Kiara",   "Dust2",   "T",  26, 11, 1, 4, true)),
//    new DataPoint<Cs2Match>(new DateTime(2024, 3,  1), new Cs2Match("Raphaël", "Inferno", "T",  14, 16, 6, 1, false)),
//});

//var lol = DataSeries<LolMatch>.From(new[]
//{
//    new DataPoint<LolMatch>(new DateTime(2024, 1, 22), new LolMatch("Noé", "Thresh", 2, 4, 18, 42, 71, true)),
//    new DataPoint<LolMatch>(new DateTime(2024, 2, 10), new LolMatch("Noé", "Thresh", 1, 6, 12, 35, 64, false)),
//});

Console.WriteLine(valorant.Count); // 3
Console.WriteLine(cs2.Count); // 3
Console.WriteLine(lol.Count); // 2