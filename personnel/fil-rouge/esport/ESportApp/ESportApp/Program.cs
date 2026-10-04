// See https://aka.ms/new-console-template for more information
using DataSeries;
using ESportApp;

Console.WriteLine("Lancement du programme.");

DataSeries<ValorantMatch> valorant;
DataSeries<Cs2Match> cs2;
DataSeries<LolMatch> lol;

valorant = DataSeries<ValorantMatch>.FromCsv(@"./data/valorant.csv", ParseValorant);
cs2 = DataSeries<Cs2Match>.FromCsv(@"./data/cs2.csv", ParseCS2);
lol = DataSeries<LolMatch>.FromCsv(@"./data/lol.csv", ParseLoL);

if (args.Length == 0 || args.Contains("--help")) {
    Console.WriteLine("Commands :");
    Console.WriteLine("[--game valorant|cs2|lol]");
    Console.WriteLine("[--generate player]");
    return;
}

string? game = GetArgsValue("--game");
string? playerToGenerate = GetArgsValue("--generate");
Console.WriteLine(playerToGenerate);

if (playerToGenerate is not null) {
    // ne génère uniquement des matchs cs2
    var generateMatch = MatchGenerator.GenerateCs2(playerToGenerate, 5).ToList();
    generateMatch.ForEach(m => Console.WriteLine(m.ToString()));
    //Console.ReadKey();
    return;
}

switch (game) {
    case "valorant":
        Console.WriteLine("Valorant match\ndate,player,agent,kills,deaths,assists,headshots,rounds_won,won");
        Console.WriteLine(valorant.ToString());
        break;
    case "cs2":
        Console.WriteLine("Cs2 matchs\ndate,player,map,start_side,kills,deaths,assists,mvps,won");
        Console.WriteLine(cs2.ToString());
        break;
    case "lol":
        Console.WriteLine("Lol matchs\ndate,player,champion,role,kills,deaths,assists,cs,vision_score,won");
        Console.WriteLine(lol.ToString());
        break;
    default:
        Console.WriteLine("Valorant match\ndate,player,agent,kills,deaths,assists,headshots,rounds_won,won");
        Console.WriteLine(valorant.ToString());
        Console.WriteLine("Cs2 matchs\ndate,player,map,start_side,kills,deaths,assists,mvps,won");
        Console.WriteLine(cs2.ToString());
        Console.WriteLine("Lol matchs\ndate,player,champion,role,kills,deaths,assists,cs,vision_score,won");
        Console.WriteLine(lol.ToString());
        break;
}



//Console.WriteLine($"Il y a  {valorant.Count} matches dans la série Valorant"); // 3
//Console.WriteLine($"Il y a  {cs2.Count} matches dans la série CS2"); // 3
//Console.WriteLine($"Il y a  {lol.Count} matches dans la série LoL"); // 3

//var biggame = valorant.Values
//    .Where(vm => vm.Value.Player == "Léa")
//    .Where(vm => vm.Value.Kills >= 20)
//    .Where(vm => vm.Value.Assists >= 5)
//    .OrderBy(vm => vm.Timestamp)
//    .Last();
//Console.WriteLine(biggame.Timestamp.ToString("dd/MM/yyyy") + " : " + biggame.Value.Player + " a fait un gros match avec " + biggame.Value.Kills + " kills et " + biggame.Value.Assists + " assists.");

//ValorantMatch dylanthird = valorant.Values
//    .Where(ValorantMatch => ValorantMatch.Value.Player == "Dylan")
//    .ElementAt(3)
//    .Value;
//Console.WriteLine("Dans son 4ème match, Dylan a fait " + dylanthird.Kills + " kills.");

//DataSeries<DataPoint<LolMatch>> noeswins = DataSeries<DataPoint<LolMatch>>.From(
//    lol
//    .Values
//    .Where(lolmatch => lolmatch.Value.Player == "Noé" && lolmatch.Value.Won)
//    .ToList()
//    );

////Console.WriteLine(noeswins);

//// ex2-etape2
//Func<Cs2Match, bool> isValid = m =>
//    m.Kills + m.Assists <= 50 &&
//    m.Deaths >= 1;


//var raphaelValid = raphaelGenerated.Where(isValid);
//Console.WriteLine($"Avant : {raphaelGenerated.Count}, après : {raphaelValid.Count()}");

//ExportCs2("Maurice", raphaelValid, @"data\createdCs2Match.csv");
//Console.WriteLine("Matchs inscrit dans le fichier.");

Console.WriteLine("Fin du programme.");
//Console.ReadKey();

//methods
string? GetArgsValue(string flag) {
    int index = Array.IndexOf(args, flag);
    if (index < 0) {
        return null;
    }
    return args[index + 1];
}

ValorantMatch ParseValorant(string[] cols) {
    return new ValorantMatch(
        cols[1],
        cols[2],
        int.Parse(cols[3]),
        int.Parse(cols[4]),
        int.Parse(cols[5]),
        int.Parse(cols[6]),
        int.Parse(cols[7]),
        bool.Parse(cols[8])
    );
}
Cs2Match ParseCS2(string[] cols) {
    return new Cs2Match(
        cols[1],
        cols[2],
        cols[3],
        int.Parse(cols[4]),
        int.Parse(cols[5]),
        int.Parse(cols[6]),
        int.Parse(cols[7]),
        bool.Parse(cols[8])
    );
}

LolMatch ParseLoL(string[] cols) {
    return new LolMatch(
        cols[1],
        cols[2],
        int.Parse(cols[4]),
        int.Parse(cols[5]),
        int.Parse(cols[6]),
        int.Parse(cols[7]),
        int.Parse(cols[8]),
        bool.Parse(cols[9])
    );
}

void ExportCs2(string player, IEnumerable<Cs2Match> matches, string path) {
    var header = "date,player,map,start_side,kills,deaths,assists,mvps,won";
    var lines = matches.Select((m) => $"2026-09-27,{m.Player},{m.Map},{m.StartSide},{m.Kills},{m.Deaths},{m.Assists},{m.Mvps},{m.Won}"
    );
    File.WriteAllLines(path, lines.Prepend(header));
}

public static class MatchGenerator {
    public static IEnumerable<Cs2Match> GenerateCs2(string player, int count, int seed = 42) {
        var rng = new Random(seed);
        var maps = new[] { "Dust2", "Mirage", "Inferno", "Nuke", "Ancient" };
        var sides = new[] { "CT", "T" };
        var start = new DateTime(2023, 9, 1);

        return Enumerable.Range(1, count)
            .Select(i => new Cs2Match(
                    player,
                    maps[rng.Next(maps.Length)],
                    sides[rng.Next(2)],
                    rng.Next(10, 28),   // kills
                    rng.Next(6, 18),    // deaths
                    rng.Next(0, 8),     // assists
                    rng.Next(0, 5),     // mvps
                    rng.Next(2) == 0    // won
                )

        );
    }
}