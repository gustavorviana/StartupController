using Newtonsoft.Json;
using System;
using System.IO;

namespace StartupInterface.Settings
{
    public abstract class SettingsController<T> where T : class, new()
    {
        public string SettingsPath { get; }
        protected bool HasSettingsFile => File.Exists(SettingsPath);

        private T settings;

        public T Settings
        {
            get => settings;
            set
            {
                this.settings = value ?? throw new Exception("The setting cannot be null");
            }
        }

        public bool NeedReload => this.settings == null;

        public SettingsController(string fileName)
        {
            this.SettingsPath = Path.Combine(Program.AppDataDir, fileName);
        }

        public virtual void Save()
        {
            this.SaveSettings(this.Settings);
        }

        public virtual void Reload()
        {
            this.Settings = this.LoadSettings() ?? new T();
        }

        protected void SaveSettings(T settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            var content = JsonConvert.SerializeObject(settings);
            File.WriteAllText(SettingsPath, content);
        }

        protected T LoadSettings()
        {
            if (!this.HasSettingsFile)
                return null;

            try
            {
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(SettingsPath));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
