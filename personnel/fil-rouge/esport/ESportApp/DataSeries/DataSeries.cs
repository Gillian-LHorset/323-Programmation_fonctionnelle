namespace DataSeries {
    public class DataSeries<T> {
        private readonly IEnumerable<T> _data;

        private DataSeries(IEnumerable<T> data) => _data = data;

        public static DataSeries<T> From(IEnumerable<T> source) => new DataSeries<T>(source);
        
        public static DataSeries<T> FromCsv(string filename, Func<string[], T> parser) {
            List<T> data = new List<T>();
            try {
                List<string> content = File.ReadAllLines(filename).ToList();
                foreach (string line in content.Skip(1)) {
                    string[] cols = line.Split(',');
                    data.Add(parser(cols));
                }
            } catch (Exception e) {
                Console.WriteLine($"Erreur d'ouverture du fichier {e.Message}");
            }
            return From(data);
        }

        public int Count => _data.Count();
        //public IEnumerable<T> Values => _data.Select(dp => dp.Value);
        public IEnumerable<T> Values => _data;
        
        // doc : Func<T, bool> car .Filter(x => x % 2 == 0) T = x et bool = x % 2
        public DataSeries<T> Filter(Func<T, bool> predicate) => new DataSeries<T>(_data.Where(predicate));

        public DataSeries<T> RemoveOutliers(Func<T, bool> isValid) => Filter(isValid);

        public bool HasAny(Func<T, bool> predicate) => _data.Any(predicate);
        public bool AllMatch(Func<T, bool> predicate) => _data.All(predicate);




        public override string ToString() {
            return $"DataSerie<{typeof(T).Name}>: {Count} points: {Environment.NewLine}{String.Join(Environment.NewLine, _data.Select(s => s).ToArray())}";
        }
    }
}
