using System;

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


        public DataSeries<TResult> Transform<TResult>(Func<T, TResult> mapper) => new DataSeries<TResult>(_data.Select(mapper));

        // transforme toutes les valeurs numérique entre 1 ou 0
        public DataSeries<double> Normalize() {
            var values = _data.Cast<double>().ToList();
            var min = values.Min();
            var max = values.Max();
            var range = max - min;
            return DataSeries<double>.From(
                values.Select(v => range == 0 ? 0.0 : (v - min) / range)
            );
        }

        public DataSeries<double> Smooth(int windowSize)
        {
            var values = _data.Cast<double>().ToList();
            return DataSeries<double>.From(
                Enumerable.Range(0, values.Count)
                    .Select(i => {
                        // on va chercher les "windowSize" dernières valeurs depuis l'indice de la valeur windowSize
                            // ex : on va chercher les 3 dernières valeurs depuis 3
                            // donc par ex : [10, 20, 50]
                        // Gemini : Pour chaque élément d'indice i, on remonte jusqu'à windowSize valeurs en arrière (en incluant i)
                        var window = values.Skip(Math.Max(0, i - windowSize + 1)).Take(windowSize);
                        // puis on retourne la moyenne 
                            // donc ex : 26.67
                        return window.Average();
                    })
            );
        }


        public override string ToString() {
            return $"DataSerie<{typeof(T).Name}>: {Count} points: {Environment.NewLine}{String.Join(Environment.NewLine, _data.Select(s => s).ToArray())}";
        }
    }
}
