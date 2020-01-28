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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            stack1.SizeChanged += Cambiotamano;
        }

        public void Cambiotamano(object sender, EventArgs e)
        {
            Settings.Ancho = stack1.Width.ToString();
        }

        public async void OnLoginClicked(object sender, EventArgs e)
        {
            try{
                string uriString2 = string.Format("http://ec2-3-86-100-71.compute-1.amazonaws.com/WS/login.php?usuario={0}&password={1}", entry1.Text, entry2.Text);
                var response2 = await httpRequest(uriString2);
                List<class_usuarios> valor = new List<class_usuarios>();
                valor = procesar2(response2);
                if (valor.Count == 0)
                {
                    await DisplayAlert("Ayuda", "Usuario y/o contraseña incorrectos", "OK");
                }
                for (int i = 0; i < valor.Count(); i++)
                {
                    if (int.Parse(valor.ElementAt(i).idusuario) > 0)
                    {
                            if(valor.ElementAt(i).estatus == "2")
                            {
                                Settings.Idusuario = valor.ElementAt(i).idusuario;
                                Settings.Nombre = valor.ElementAt(i).nombre;
                                App.Current.MainPage = new Perfil();
                            }else if (valor.ElementAt(i).estatus == "1")
                            {
                                await DisplayAlert("Pendiente", "Usuario pendiente de aprobación", "OK");
                            }else if (valor.ElementAt(i).estatus == "0")
                            {
                                await DisplayAlert("Alerta", "Usuario desactivado", "OK");
                            }


                        }
                    else
                    {
                        await DisplayAlert("Ayuda", "Usuario y/o contraseña incorrectos", "OK");
                    }

                }
            }catch(Exception ex){
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public List<class_usuarios> procesar2(string respuesta)
        {
            List<class_usuarios> items = new List<class_usuarios>();
            if (respuesta == "0")
            { }
            else
            {
                var doc = XDocument.Parse(respuesta);
                if (doc.Root != null)
                {
                    items = (from r in doc.Root.Elements("valor")
                             select new class_usuarios
                             {
                                 idusuario = (string)r.Element("idusuario"),
                                 nombre = (string)r.Element("nombre"),
                                 estatus = (string)r.Element("estatus"),
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

        private void btnRecovery_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new Recuperar());

        }
    }
}