namespace EsportApp_FilRouge {
    internal class ValorantMatch {
        public ValorantMatch(string player, string agent, int kills, int deaths, int assists, int headshots, int roundsWin, bool won) {
            Player = player;
            Agent = agent;
            Kills = kills;
            Deaths = deaths;
            Assists = assists;
            Headshots = headshots;
            RoundsWin = roundsWin;
            Won = won;
        }

        public string Player { get; }
        public string Agent { get; }
        public int Kills { get; }
        public int Deaths { get; }
        public int Assists { get; }
        public int Headshots { get; }
        public int RoundsWin { get; }
        public bool Won { get; }

    }
}
