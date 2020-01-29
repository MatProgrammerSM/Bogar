using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bogar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Configuracion : ContentPage
    {
        public Configuracion()
        {
            InitializeComponent();
        }

        private void BtnLogout(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new MainPage());
        }
    }
}