using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UI.VM
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string scoreText;
        private string lifeText;

        public string ScoreText { get => scoreText; set { scoreText = value; this.OnPropertyChanged(); } }
        public string LifeText { get => lifeText; set { lifeText = value; this.OnPropertyChanged(); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
