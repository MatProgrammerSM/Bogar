using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;

namespace Bogar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoDesarrollo : ContentPage
    {
        public NuevoDesarrollo()
        {
            InitializeComponent();
        }

        private async void BtnNuevoDesarrollo_Clicked(object sender, EventArgs e)
        {
            var pr = new Gracias();
            var scaleAnimation = new ScaleAnimation
            {
                PositionIn = MoveAnimationOptions.Right,
                PositionOut = MoveAnimationOptions.Left
            };

            pr.Animation = scaleAnimation;
            await PopupNavigation.PushAsync(pr);
        }
    }
}