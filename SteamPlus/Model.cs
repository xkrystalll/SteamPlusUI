using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;

namespace SteamPlus
{
    public class Model : INotifyPropertyChanged
    {
        public static string ShowAddGameDialog()
        {
            var dialog = new AddGameDialog();
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                return dialog.Game;
            }
            return null;
        }
        public void TryEnableRunGameButton(string selectedGame, string Path)
        {
            if (selectedGame != "Не выбрано" && !string.IsNullOrEmpty(Path))
                if (Path.EndsWith("steam.exe"))
                    MainWindow.Instance.runGame.IsEnabled = true;
        }

        public static string GetPath()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Executable documents (*.exe)|*.exe";
            dialog.FilterIndex = 2;

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                return dialog.FileName;
            }
            return null;
        }
        private void KillSteam()
        {
            Process[] etc = Process.GetProcesses();
            foreach (Process anti in etc.ToList().Where(x => !x.ProcessName.ToLower().Contains("plus")))
            {
                if (anti.ProcessName.ToLower().Contains("steam"))
                {
                    anti.Kill();
                }
            }
        }

        public void RunGame(string appID)
        {
            KillSteam();

            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = ViewModel.Instance.Path,
                Arguments = $"-noshaders -no-shared-textures -disablehighdpi -cef-single-process -cef-in-process-gpu -single_core -cef-disable-d3d11 -cef-disable-sandbox -disable-winh264 -cef-force-32bit -no-cef-sandbox -cef-disable-breakpad -skipstreamingdrivers -vrdisable -nocrashdialog -nocrashmonitor -norepairfiles -noverifyfiles -nodircheck -nocache -noaafonts -no-dwrite -single_core -voice_quality 1 -nofriendsui -no-browser  -applaunch {appID}"
            };
            process.StartInfo = info;
            process.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
