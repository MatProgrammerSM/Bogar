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
            absolutelayout.Children.Add(cargar_menu_inferior());
        }

        public static AbsoluteLayout cargar_menu_inferior()
        {
            try
            {
                AbsoluteLayout absoluteLayout1 = new AbsoluteLayout();
                AbsoluteLayout.SetLayoutFlags(absoluteLayout1, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(absoluteLayout1, new Rectangle(0, 1, 1, 0.1));
                StackLayout menu = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.End, Padding = new Thickness(10, 5, 10, 5), BackgroundColor = Color.FromHex("#ffffff"), Orientation = StackOrientation.Horizontal };
                AbsoluteLayout.SetLayoutFlags(menu, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(menu, new Rectangle(1, 1, 1, 1));
                absoluteLayout1.Children.Add(menu);

                var icono_home = new Image() { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("main.png"), HeightRequest = 32 };
                menu.Children.Add(icono_home);
                icono_home.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => { try { Application.Current.MainPage = new NavigationPage(new Perfil()); } catch (Exception ex) { Application.Current.MainPage.DisplayAlert("Ayuda", ex.Message, "OK"); } }), NumberOfTapsRequired = 1 });

                var icono_nuevo = new Image() { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("location.png"), HeightRequest = 30 };
                menu.Children.Add(icono_nuevo);
                icono_nuevo.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => { try { Application.Current.MainPage.Navigation.PushAsync(new Perfil()); } catch (Exception ex) { Application.Current.MainPage.DisplayAlert("Ayuda", ex.Message, "OK"); } }), NumberOfTapsRequired = 1 });

                
                var icono_estadisticas = new Image() { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand, Source = ImageSource.FromFile("plus.png"), HeightRequest = 22 };
                Frame circulo = new Frame() {Padding = new Thickness(3), CornerRadius = 30, WidthRequest = 30, HeightRequest = 30, BackgroundColor = Color.FromHex("#DD463C"), Content = icono_estadisticas };
                menu.Children.Add(circulo);
                icono_estadisticas.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => { try { Application.Current.MainPage.Navigation.PushAsync(new NuevoDesarrollo()); } catch (Exception ex) { Application.Current.MainPage.DisplayAlert("Ayuda", ex.Message, "OK"); } }), NumberOfTapsRequired = 1 });

                var icono_menu = new Image() { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("menu.png"), HeightRequest = 30 };
                menu.Children.Add(icono_menu);
                icono_menu.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => { try { Application.Current.MainPage.Navigation.PushAsync(new Desarrollos()); } catch (Exception ex) { Application.Current.MainPage.DisplayAlert("Ayuda", ex.Message, "OK"); } }), NumberOfTapsRequired = 1 });

                var icono_user = new Image() { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("user.png"), HeightRequest = 30 };
                menu.Children.Add(icono_user);
                icono_user.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => { try { Application.Current.MainPage.Navigation.PushAsync(new Perfil()); } catch (Exception ex) { Application.Current.MainPage.DisplayAlert("Ayuda", ex.Message, "OK"); } }), NumberOfTapsRequired = 1 });

                return absoluteLayout1;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        void NuevoDesarrollo(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new NuevoDesarrollo());
        }

        private void ActualizarDesarrollo(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new NuevoDesarrollo());
        }

        private void BtnNotificacion_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new Notificaciones());
        }

        private void BtnConfiguracion_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new Configuracion());
        }

    }
}