using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;

namespace SteamPlus
{
    class Game
    {
        private string appID;

        public string AppID
        {
            get => appID;
            set => appID = value;
        }

        public static string AddGame(string appID)
        {
            File.AppendAllText("games.txt", $"\n{appID}");
            return appID;
        }

        public static ObservableCollection<string> GetGames()
        {
            var collection = new ObservableCollection<string>();
            try
            {
                using (FileStream stream = File.Open("games.txt", FileMode.OpenOrCreate, FileAccess.Read))
                {
                    byte[] array = new byte[stream.Length];
                    stream.Read(array, 0, array.Length);
                    string textFromFile = Encoding.Default.GetString(array);
                    foreach (var str in textFromFile.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(str))
                            collection.Add(str);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при открытии файла (открыт блокнот?)");
                return null;
            }
            return collection;
        }
    }
}
