using Microsoft.Win32;
using System;

namespace StartupInterface
{
    public static class RegistryStartup
    {
        private static readonly RegistryKey Run = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

        public static bool HasProgram(string name)
        {
            return Run.GetValue(name) != null;
        }

        public static void DeleteProgram(string name)
        {
            Run.DeleteValue(name);
        }

        public static void SetProgram(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            Run.SetValue(name, value);
        }
    }
}
