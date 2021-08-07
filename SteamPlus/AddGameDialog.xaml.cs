using System.Windows;

namespace SteamPlus
{
    /// <summary>
    /// Логика взаимодействия для AddGameDialog.xaml
    /// </summary>
    public partial class AddGameDialog : Window
    {
        public static AddGameDialog Instance = null;
        public AddGameDialog()
        {
            Instance = this;
            InitializeComponent();
            DataContext = new AddDialogViewModel();
        }

        public string Game
        {
            get => AddDialogViewModel.Instance.Name;
        }

        public void SetDialogResult(bool b) 
        {
            this.DialogResult = b;
        }
    }
}
