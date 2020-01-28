using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bogar
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Perfil());

            //{ BarBackgroundColor = Color.FromHex("DD463C") }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
