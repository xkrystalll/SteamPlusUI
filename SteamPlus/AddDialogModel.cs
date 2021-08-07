using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SteamPlus
{
    public class AddDialogModel : INotifyPropertyChanged
    {
        public static bool CanAddGame(string appID)
        {
            if (ViewModel.Games.Contains(appID))
                return false;
            if (string.IsNullOrEmpty(appID))
                return false;
            return true;
        }

        public void SwitchEnableAddGameButton(string s)
        {
            if (CanAddGame(s))
                AddGameDialog.Instance.AddGame.IsEnabled = true;
            else
                AddGameDialog.Instance.AddGame.IsEnabled = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
