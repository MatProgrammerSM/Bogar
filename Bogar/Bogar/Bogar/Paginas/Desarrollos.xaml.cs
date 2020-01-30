using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bogar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Desarrollos : ContentPage
    {
        public Desarrollos()
        {
            InitializeComponent();
            Title = "BOGAR";
            absolutelayout.Children.Add(Perfil.cargar_menu_inferior());
            cargar_desarrollos();
        }

        public async void cargar_desarrollos()
        {
            try
            {
                string uriString2 = string.Format("http://ec2-3-86-100-71.compute-1.amazonaws.com/WS/desarrollos_apartados.php?usuario={0}", Settings.Idusuario);
                var response2 = await httpRequest(uriString2);
                List<class_desarrollos> valor = new List<class_desarrollos>();
                valor = procesar2(response2);
                for (int i = 0; i < valor.Count(); i++)
                {
                    String estatus = valor.ElementAt(i).estatus;
                    string fotoestatus = "est3.png";
                    if(estatus == "1")
                    {
                        fotoestatus = "est2.png";
                    }
                    else if(estatus == "2")
                    {
                        fotoestatus = "est1.png";
                    }
                    Image imagenestatus = new Image() { Source = ImageSource.FromFile(fotoestatus), WidthRequest= 30, HorizontalOptions = LayoutOptions.Center };
                    Label nombre = new Label() { Text = valor.ElementAt(i).nombre, FontSize = 16, TextColor = Color.FromHex("#3E4958"), FontAttributes = FontAttributes.Bold,  };
                    Label ciudad = new Label() { Text = valor.ElementAt(i).ciudad+", "+valor.ElementAt(i).estado, FontSize = 14, TextColor = Color.FromHex("#B6B3B3") };
                    Label fecha = new Label() { Text = valor.ElementAt(i).fecha.Split(' ')[0], FontSize = 14, TextColor = Color.FromHex("#B6B3B3")};
                    StackLayout nombreciudad = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { nombre, ciudad } };
                    StackLayout contenidodesarrollo = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand, Children = { imagenestatus, nombreciudad, fecha } };
                    BoxView linea = new BoxView() { BackgroundColor = Color.FromHex("#D5DDE0"), HeightRequest=1, HorizontalOptions = LayoutOptions.FillAndExpand };
                    StackLayout desarrollo = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, ClassId= valor.ElementAt(i).id, Children={ contenidodesarrollo, linea }, Padding = new Thickness(20,0) };
                    stackdesarrollos.Children.Add(desarrollo);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<class_desarrollos> procesar2(string respuesta)
        {
            List<class_desarrollos> items = new List<class_desarrollos>();
            if (respuesta == "0")
            { }
            else
            {
                var doc = XDocument.Parse(respuesta);
                if (doc.Root != null)
                {
                    items = (from r in doc.Root.Elements("valor")
                             select new class_desarrollos
                             {
                                 id = (string)r.Element("id"),
                                 nombre = (string)r.Element("nombre"),
                                 estatus = (string)r.Element("estatus"),
                                 fecha = (string)r.Element("fecha"),
                                 ciudad = (string)r.Element("ciudad"),
                                 estado = (string)r.Element("estado"),
                             }).ToList();
                }
            }
            return items;
        }

        public async Task<string> httpRequest(string url)
        {
            Uri uri = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            string received;
            using (var response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null)))
            {
                using (var responseStream = response.GetResponseStream())
                { using (var sr = new StreamReader(responseStream)) { received = await sr.ReadToEndAsync(); } }
            }
            return received;
        }

    }
}