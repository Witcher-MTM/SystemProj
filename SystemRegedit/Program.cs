using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading.Tasks;

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
        static string user_choice = String.Empty;

        static void Main(string[] args)
        {

            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            bool visible = false;

            while (true)
            {
                Console.Clear();
                var key_a = Console.ReadKey();
                if (key_a.Key == ConsoleKey.UpArrow)
                {
                    if (visible == false)
                    {
                        ShowWindow(handle, SW_SHOW);

                        visible = true;
                    }
                    else
                    {
                        ShowWindow(handle, SW_HIDE);
                        visible = false;
                    }
                }


                if (ShowWindow(handle, SW_SHOW))
                {
                    RegistryKey currentUser = Registry.CurrentUser;
                    string pathAutoLoad = @"Software\Microsoft\Windows\CurrentVersion\Run\";
                    RegistryKey autoload = currentUser.OpenSubKey(pathAutoLoad, true);

                    foreach (var item in autoload.GetValueNames())
                    {
                        if (item.ToString().ToLower() != "systemregedit.exe")
                        {
                            autoload.SetValue("SystemRegedit.exe", @"..repos\SystemProj\SystemRegedit\SystemRegedit.exe");

                        }
                    }
                    do
                    {
                        Console.WriteLine("Do yo want to add FiltresFiles into autoload?\n[1]-Yes[2]-NO");
                        user_choice = Console.ReadLine();
                    } while (user_choice != "1" && user_choice != "2");

                    int choice = 0;
                    switch (user_choice)
                    {
                        case "1":
                            {
                                autoload.SetValue("SystemProj.exe", @"..repos\SystemProj\SystemRegedit\SystemProj.exe");
                                choice = 5;

                                break;
                            }
                        case "2":
                            {
                                try
                                {
                                    autoload.DeleteValue("SystemProj.exe");
                                }
                                catch (Exception)
                                {
                                }

                                choice = 5;
                                break;

                            }
                        default:
                            break;
                    }
                    ShowWindow(handle, SW_HIDE);
                }
            }


        }

    }
}
