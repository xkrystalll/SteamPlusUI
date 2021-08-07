namespace SteamPlus
{
    public class AddDialogViewModel : AddDialogModel
    {
        private string name;
        private Command addGameCommand;
        private Command cancelCommand;

        public static AddDialogViewModel Instance = null;

        public AddDialogViewModel() => Instance = this;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
                SwitchEnableAddGameButton(value);
            }
        }

        public Command CancelCommand
        {
            get
            {
                return cancelCommand ??
                  (cancelCommand = new Command(obj =>
                  {
                      AddGameDialog.Instance.SetDialogResult(false);
                  }));
            }
        }

        public Command AddGameCommand
        {
            get
            {
                return addGameCommand ??
                  (addGameCommand = new Command(obj =>
                  {
                      AddGameDialog.Instance.SetDialogResult(true);
                  }));
            }
        }
    }
}
