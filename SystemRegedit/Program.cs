 using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace SystemRegedit
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
          
            RegistryKey currentUser = Registry.CurrentUser;
            string pathAutoLoad = @"Software\Microsoft\Windows\CurrentVersion\Run\";
            RegistryKey autoload = currentUser.OpenSubKey(pathAutoLoad,true);

            foreach (var item in autoload.GetValueNames())
            {
                if (item.ToString().ToLower() != "systemregedit.exe")
                {
                    autoload.SetValue("SystemRegedit.exe", @"C:\Users\student\source\repos\SystemRegedit\SystemRegedit\bin\Debug\netcoreapp3.1\SystemRegedit.exe");

                }
            }
            Console.WriteLine("Do yo want to add FiltresFiles into autoload?[1]-Yes[2]-NO");
            while (true)
            {
                int choice = 0;
                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            autoload.SetValue("SystemProj.exe", @"C:\Users\student\Desktop\FileSort-master\SystemProj\bin\Debug\netcoreapp3.1\SystemProj.exe");
                            choice = 5;
                            return;
                        }
                    case "2":
                        {
                            autoload.DeleteValue("SystemProj.exe");
                            choice = 5;
                            return;
                        }
                    default:
                        break;
                }
              
            }




            //autoload.DeleteValue("chrome.exe");



        }
    }
}
