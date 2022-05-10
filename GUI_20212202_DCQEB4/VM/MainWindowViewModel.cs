using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UI.VM
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string scoreText;
        private string lifeText;
        private string ammoText;
        private List<Score> scores;
        private long currentScore;
        public long CurrentScore { get => currentScore; set { currentScore = value; this.OnPropertyChanged(); } }

        public List<Score> Scores
        {
            get
            {
                scores = new List<Score>();
                try
                {
                    StreamReader streamReader = new StreamReader("scores.txt");
                    string[] line = streamReader.ReadToEnd().Split(' ','\n');
                    for (int i = 0; i < line.Length-1; i += 2)
                    {
                        Score score = new Score(int.Parse(line[i + 1]), line[i]);
                        scores.Add(score);
                    }
                    scores = scores.OrderByDescending(x => x.TheScore).ToList();
                    streamReader.Close();
                }
                catch (FileNotFoundException)
                {

                    StreamWriter streamWriter = new StreamWriter("scores.txt");
                    streamWriter.Close();
                }
                return scores;
            }
            set
            {
                Scores = value;
                OnPropertyChanged();
            }
        }
        public string ScoreText { get => scoreText; set { scoreText = value; this.OnPropertyChanged(); } }
        public string LifeText { get => lifeText; set { lifeText = value; this.OnPropertyChanged(); } }
        public string AmmoText { get => ammoText; set { ammoText = value; this.OnPropertyChanged(); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class Score
    {
        public Score(int theScore, string name)
        {
            TheScore = theScore;
            Name = name;
        }

        public int TheScore { get; }
        public string Name { get; }
    }
}
