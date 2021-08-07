using System.Windows;

namespace SteamPlus
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance = null;
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            DataContext = new ViewModel();
        }
    }
}
