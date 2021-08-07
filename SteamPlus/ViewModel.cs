using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SteamPlus
{
    class ViewModel : Model, INotifyPropertyChanged
    {
        private string path = string.Empty;
        private string selectedGame = "Не выбрано";
        public static ViewModel Instance = null;
        public static ObservableCollection<string> Games { get; set; }

        private Command selectPathCommand;
        private Command addCommand;
        private Command runGameCommand;

        public Command RunGameCommand
        {
            get
            {
                return runGameCommand ??
                  (runGameCommand = new Command(obj =>
                  {
                      RunGame(selectedGame);
                  },
                  (can) => CheckPath(path) && !string.IsNullOrEmpty(selectedGame) && selectedGame != "Не выбрано"));
            }
        }

        public Command SelectPathCommand
        {
            get
            {
                return selectPathCommand ??
                  (selectPathCommand = new Command(obj =>
                  {;
                      Path = GetPath();
                  }));
            }
        }

        public Command AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new Command(obj =>
                  {
                      Games.Add(Game.AddGame(ShowAddGameDialog()));
                      OnPropertyChanged("Games");
                  }));
            }
        }

        public string Path
        {
            get => path;
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        public string SelectedGame
        {
            get { return selectedGame; }
            set
            {
                selectedGame = value;
                OnPropertyChanged("SelectedGame");
            }
        }

        public ViewModel()
        {
            Instance = this;
            OnPropertyChanged("SelectedGame");
            Games = Game.GetGames();
        }
    }
}
