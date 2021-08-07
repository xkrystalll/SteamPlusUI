using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

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

        protected bool CheckPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            if (!path.Contains("steam.exe"))
                return false;

            return true;
        }

        public static string GetPath()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Steam exe file (steam.exe)|steam.exe",
                FilterIndex = 2
            };

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
