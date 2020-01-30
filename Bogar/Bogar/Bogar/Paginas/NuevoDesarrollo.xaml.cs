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
using Plugin.Media.Abstractions;
using Xamarin.Forms.GoogleMaps;
using System.Xml.Linq;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Bogar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoDesarrollo : ContentPage
    {

        string lat, lon, tienda = "";
        Entry entry1 = new Entry();
        Image foto_formulario = null;
        string nombre_foto = "";
        MediaFile file = null;
        Entry direccion = null;
        double lat1 = 0;
        double lon1 = 0;
        Map mapa = null;
        Pin pincentro = null;
        readonly Tuple<string, Color>[] _colors =
        {
            new Tuple<string, Color>("Blue", Color.FromHex("#006fa8")),
            new Tuple<string, Color>("Gray", Color.FromHex("#686868")),
            new Tuple<string, Color>("Aqua", Color.Aqua)
        };


        public NuevoDesarrollo()
        {
            InitializeComponent();
            try
            {
                string estilo = "[{'elementType': 'geometry','stylers': [{'color': '#f5f5f5'}]},{'elementType': 'labels.icon','stylers': [{'visibility': 'off'}]},{'elementType': 'labels.text.fill','stylers': [{'color': '#616161'}]},{'elementType': 'labels.text.stroke','stylers': [{'color': '#f5f5f5'}]},{'featureType': 'administrative','elementType': 'geometry.stroke','stylers': [{'visibility': 'on'}]},{'featureType': 'administrative.land_parcel','elementType': 'labels.text.fill','stylers': [{'color': '#bdbdbd'}]},{'featureType': 'poi','elementType': 'geometry','stylers': [{'color': '#eeeeee'}]},{'featureType': 'poi','elementType': 'labels.text.fill','stylers': [{'color': '#757575'}]},{'featureType': 'poi.park','elementType': 'geometry','stylers': [{'color': '#e5e5e5'}]},{'featureType': 'poi.park','elementType': 'labels.text.fill','stylers': [{'color': '#9e9e9e'}]},{'featureType': 'road','elementType': 'geometry','stylers': [{'color': '#ffffff'}]},{'featureType': 'road.arterial','elementType': 'geometry.fill','stylers': [{'color': '#a9a9a9'}]},{'featureType': 'road.arterial','elementType': 'labels.text.fill','stylers': [{'color': '#757575'}]},{'featureType': 'road.highway','elementType': 'geometry','stylers': [{'color': '#dadada'}]},{'featureType': 'road.highway','elementType': 'geometry.fill','stylers': [{'color': '#941100'}]},{'featureType': 'road.highway','elementType': 'labels.text.fill','stylers': [{'color': '#616161'}]},{'featureType': 'road.local','elementType': 'geometry.fill','stylers': [{'color': '#d6d6d6'}]},{'featureType': 'road.local','elementType': 'labels.text.fill','stylers': [{'color': '#9e9e9e'}]},{'featureType': 'transit.line','elementType': 'geometry','stylers': [{'color': '#e5e5e5'}]},{'featureType': 'transit.station','elementType': 'geometry','stylers': [{'color': '#eeeeee'}]},{'featureType': 'water','elementType': 'geometry','stylers': [{'color': '#c9c9c9'}]},{'featureType': 'water','elementType': 'labels.text.fill','stylers': [{'color': '#9e9e9e'}]}]";
                mapa = new Map() { HeightRequest = 150, InitialCameraUpdate = CameraUpdateFactory.NewCameraPosition(new CameraPosition(new Position(20.673755, -103.376783), 10d, 0, 0)) };
                mapcont1.Children.Add(mapa);
                mapa.MyLocationEnabled = true;
                mapa.MapStyle = MapStyle.FromJson(estilo);
                mapa.UiSettings.MyLocationButtonEnabled = true;
                Color color1 = new Color();
                color1 = _colors[1].Item2;
                pincentro = new Pin()
                {
                    Label = "Nuevo desarrollo",
                    Icon = BitmapDescriptorFactory.DefaultMarker(color1),
                    Position = new Position(mapa.CameraPosition.Target.Latitude, mapa.CameraPosition.Target.Longitude),
                };
                mapa.Pins.Add(pincentro);
                
                mapa.CameraChanged += Mapa_CameraChanged;
            }
            catch (Exception ex)
            {

            }
            
        }

        private async void act_dire()
        {
            try
            {
                string uriString2 = string.Format("https://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&sensor=false&key=AIzaSyAphxfCDneoZOFBl3hckQISMSzM-74i2lY", WebUtility.UrlEncode(lat1.ToString()), WebUtility.UrlEncode(lon1.ToString()));
                var response2 = await httpRequest(uriString2);
                Rootobject mapdata = JsonConvert.DeserializeObject<Rootobject>(response2);
                if (mapdata.status == "OK")
                {
                    int contador = mapdata.results[0].address_components.Length;
                    for (int i = 0; i < contador; i++)
                    {
                        string types = mapdata.results[0].address_components[i].types[0];
                        
                        if (types.Contains("postal_code"))
                        {
                            e_cp.Text = (string)mapdata.results[0].address_components[i].long_name;
                        }
                        if (types.Contains("locality"))
                        {
                            e_ciudad.Text = (string)mapdata.results[0].address_components[i].long_name;
                        }
                        if (types.Contains("administrative_area_level_1"))
                        {
                            e_estado.Text = (string)mapdata.results[0].address_components[i].long_name;
                        }
                        if (types.Contains("postal_code"))
                        {
                            e_cp.Text = (string)mapdata.results[0].address_components[i].long_name;
                        }
                    }
                    string direccion = mapdata.results[0].formatted_address;
                    e_domicilio.Text = direccion;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                error = "";
            }
        }

        

        private void Mapa_CameraChanged(object sender, CameraChangedEventArgs e)
        {
            pincentro.Position = new Position(mapa.CameraPosition.Target.Latitude, mapa.CameraPosition.Target.Longitude);
            lat1 = mapa.CameraPosition.Target.Latitude;
            lon1 = mapa.CameraPosition.Target.Longitude;
            act_dire();
        }

        private async void BtnNuevoDesarrollo_Clicked(object sender, EventArgs e)
        {


            BtnNuevoDesarrollo.IsEnabled = false;
            int completo = 1;
            string error = "";
            if (e_nombre.Text == null) { completo = 0; error = error + " Nombre incompleto. \n"; }
            if (e_estado.Text == null) { completo = 0; error = error + " Estado incompleto. \n"; }
            if (e_ciudad.Text == null) { completo = 0; error = error + " Ciudad incompleta. \n"; }
            if (e_cp.Text == null) { completo = 0; error = error + " CP incompleto. \n"; }
            if (e_domicilio.Text == null) { completo = 0; error = error + " Domicilio incompleto. \n"; }
            if (e_referencia.Text == null) { completo = 0; error = error + " Referencia. \n"; }
            if (e_desarrolladora.Text == null) { completo = 0; error = error + " Nombre de desarrolladora incompleto. \n"; }
            if (completo == 1)
            {
                string uriString2 = string.Format("http://ec2-3-86-100-71.compute-1.amazonaws.com/WS/alta.php?" +
                    "lat={0}&lon={1}&nombre={2}&estado={3}&ciudad={4}&cp={5}&domicilio={6}&referencia={7}&tipo={8}" +
                    "&desarrolladora={9}&inmobiliaria={10}&asesor={11}&contacto={12}&web={13}&facebook={14}&instagram={15}&usuario={16}&lat1={17}&lon1={18}",
                    lat, lon, e_nombre.Text, e_estado.Text, e_ciudad.Text, e_cp.Text, e_domicilio.Text, e_referencia.Text, p_tipo.SelectedItem.ToString(), e_desarrolladora.Text, e_inmobiliaria.Text, e_asesor.Text, e_contacto.Text, e_web.Text, e_facebook.Text, e_instagram.Text, Settings.Idusuario, lat1, lon1);
                var response2 = await httpRequest(uriString2);
                await DisplayAlert("Registro", "Registrado correctamente", "ok");
                
                var pr = new Gracias();
                var scaleAnimation = new ScaleAnimation
                {
                    PositionIn = MoveAnimationOptions.Right,
                    PositionOut = MoveAnimationOptions.Left
                };

                pr.Animation = scaleAnimation;
                await PopupNavigation.PushAsync(pr);
                App.Current.MainPage = new NavigationPage(new Perfil());
            }
            else
            {
                await DisplayAlert("Ayuda", "Los siguientes campos son obligatorios, valida que toda la información esté completa\n\n\nRevisar:\n\n" + error, "OK");
            }
            BtnNuevoDesarrollo.IsEnabled = true;
        }

        public List<class_marcadores> procesar2(string respuesta)
        {
            List<class_marcadores> items = new List<class_marcadores>();
            if (respuesta == "0")
            { }
            else
            {
                var doc = XDocument.Parse(respuesta);
                if (doc.Root != null)
                {
                    items = (from r in doc.Root.Elements("valor")
                             select new class_marcadores
                             {
                                 idpin = (string)r.Element("idtienda"),
                                 nombre = (string)r.Element("nombre"),
                                 direccion = (string)r.Element("direccion"),
                                 lat = (string)r.Element("lat"),
                                 lon = (string)r.Element("lon"),
                                 dias = (string)r.Element("dias"),
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

        public class Rootobject
        {
            public Result[] results { get; set; }
            public string status { get; set; }
        }

        public class Result
        {
            public Address_Components[] address_components { get; set; }
            public string formatted_address { get; set; }
            public Geometry geometry { get; set; }
            public string place_id { get; set; }
            public string[] types { get; set; }
        }

        public class Geometry
        {
            public Location location { get; set; }
            public string location_type { get; set; }
            public Viewport viewport { get; set; }
        }

        public class Location
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Viewport
        {
            public Northeast northeast { get; set; }
            public Southwest southwest { get; set; }
        }

        public class Northeast
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Southwest
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Address_Components
        {
            public string long_name { get; set; }
            public string short_name { get; set; }
            public string[] types { get; set; }
        }
    }
}