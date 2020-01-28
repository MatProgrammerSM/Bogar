using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
namespace Bogar
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters.
    /// </summary>
    public static class Settings
    {


        private static ISettings AppSettings =>
        CrossSettings.Current;

        public static string UserName
        {
            get => AppSettings.GetValueOrDefault(nameof(UserName), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UserName), value);
        }

        public static string Ruta
        {
            get => AppSettings.GetValueOrDefault(nameof(Ruta), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Ruta), value);
        }

        public static string Idusuario
        {
            get => AppSettings.GetValueOrDefault(nameof(Idusuario), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Idusuario), value);
        }

        public static string Nombre
        {
            get => AppSettings.GetValueOrDefault(nameof(Nombre), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Nombre), value);
        }

        public static string Ancho
        {
            get => AppSettings.GetValueOrDefault(nameof(Ancho), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Ancho), value);
        }

        public static string Foto
        {
            get => AppSettings.GetValueOrDefault(nameof(Foto), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Foto), value);
        }

        public static string Telefono
        {
            get => AppSettings.GetValueOrDefault(nameof(Telefono), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Telefono), value);
        }

        public static string Correo
        {
            get => AppSettings.GetValueOrDefault(nameof(Correo), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Correo), value);
        }

        public static string Filtros
        {
            get => AppSettings.GetValueOrDefault(nameof(Filtros), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Filtros), value);
        }

        public static string Exep
        {
            get => AppSettings.GetValueOrDefault(nameof(Exep), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Exep), value);
        }
    }
}
