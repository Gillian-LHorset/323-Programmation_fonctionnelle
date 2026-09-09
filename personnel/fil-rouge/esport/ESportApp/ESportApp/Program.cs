// See https://aka.ms/new-console-template for more information
using DataSeries;
using ESportApp;

DataSeries<DataPoint<ValorantMatch>> valorant;
DataSeries<DataPoint<Cs2Match>> cs2;
DataSeries<DataPoint<LolMatch>> lol;

valorant = DataSeries<DataPoint<ValorantMatch>>.FromCsv(@"data\valorant.csv", ParseValorant);
cs2 = DataSeries<DataPoint<Cs2Match>>.FromCsv(@"data\cs2.csv", ParseCS2);
lol = DataSeries<DataPoint<LolMatch>>.FromCsv(@"data\Lol.csv", ParseLoL);

Console.WriteLine($"Il y a  {valorant.Count} matches dans la série Valorant"); // 3
Console.WriteLine($"Il y a  {cs2.Count} matches dans la série CS2"); // 3
Console.WriteLine($"Il y a  {lol.Count} matches dans la série LoL"); // 3

var biggame = valorant.Values
    .Where(vm => vm.Value.Player == "Léa")
    .Where(vm => vm.Value.Kills >= 20)
    .Where(vm => vm.Value.Assists >= 5)
    .OrderBy(vm => vm.Timestamp)
    .Last();
Console.WriteLine(biggame.Timestamp.ToString("dd/MM/yyyy") + " : " + biggame.Value.Player + " a fait un gros match avec " + biggame.Value.Kills + " kills et " + biggame.Value.Assists + " assists.");

ValorantMatch dylanthird = valorant.Values
    .Where(ValorantMatch => ValorantMatch.Value.Player == "Dylan")
    .ElementAt(3)
    .Value;
Console.WriteLine("Dans son 4ème match, Dylan a fait " + dylanthird.Kills + " kills.");

DataSeries<DataPoint<LolMatch>> noeswins = DataSeries<DataPoint<LolMatch>>.From(
    lol
    .Values
    .Where(lolmatch => lolmatch.Value.Player == "Noé" && lolmatch.Value.Won)
    .ToList()
    );

//Console.WriteLine(noeswins);

// ex2-etape2
var raphaelGenerated = MatchGenerator.GenerateCs2("Raphaël", 20);
Console.WriteLine(raphaelGenerated.Count); // 20

Console.ReadKey();

DataPoint<ValorantMatch> ParseValorant(string[] cols) {
    ValorantMatch match = new ValorantMatch(cols[1], cols[2], int.Parse(cols[3]), int.Parse(cols[4]), int.Parse(cols[5]), int.Parse(cols[6]), int.Parse(cols[7]), bool.Parse(cols[8]));
    DateTime date = DateTime.Parse(cols[0]);
    return new DataPoint<ValorantMatch>(date, match);
}
DataPoint<Cs2Match> ParseCS2(string[] cols) {
    return new DataPoint<Cs2Match>(DateTime.Parse(cols[0]), new Cs2Match(cols[1], cols[2], cols[3], int.Parse(cols[4]), int.Parse(cols[5]), int.Parse(cols[6]), int.Parse(cols[7]), bool.Parse(cols[8])));
}
DataPoint<LolMatch> ParseLoL(string[] cols) {
    return new DataPoint<LolMatch>(DateTime.Parse(cols[0]), new LolMatch(cols[1], cols[2], int.Parse(cols[4]), int.Parse(cols[5]), int.Parse(cols[6]), int.Parse(cols[7]), int.Parse(cols[8]), bool.Parse(cols[9])));
}

public static class MatchGenerator {
    public static DataSeries<DataPoint<Cs2Match>> GenerateCs2(string player, int count, int seed = 42) {
        var rng = new Random(seed);
        var maps = new[] { "Dust2", "Mirage", "Inferno", "Nuke", "Ancient" };
        var sides = new[] { "CT", "T" };
        var start = new DateTime(2023, 9, 1);

        return DataSeries<DataPoint<Cs2Match>>.From(
            Enumerable.Range(1, count)
                .Select(i => new DataPoint<Cs2Match>(
                    start.AddDays(i),
                    new Cs2Match(
                        player,
                        maps[rng.Next(maps.Length)],
                        sides[rng.Next(2)],
                        rng.Next(10, 28),   // kills
                        rng.Next(6, 18),    // deaths
                        rng.Next(0, 8),     // assists
                        rng.Next(0, 5),     // mvps
                        rng.Next(2) == 0    // won
                    )
                ))
        );
    }
}