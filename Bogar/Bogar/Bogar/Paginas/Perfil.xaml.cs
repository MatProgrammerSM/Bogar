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
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
        }

        void NuevoDesarrollo(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new NuevoDesarrollo());
        }

        private void ActualizarDesarrollo(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new NuevoDesarrollo());
        }
    }
}