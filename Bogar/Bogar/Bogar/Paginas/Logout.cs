using System;

using Xamarin.Forms;

namespace Bogar
{
    public class Logout : ContentPage
    {
        public Logout()
        {
            try
            {
                Settings.Idusuario = "";
                Application.Current.MainPage = new NavigationPage(new Login());
            }
            catch (System.Exception ex)
            {

            }
            StackLayout st1 = new StackLayout()
            {

            };
            this.Content = st1;
        }
    }
}
