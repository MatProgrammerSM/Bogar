using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Plugin.Media.Abstractions;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms.Xaml;

namespace Bogar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {

        MediaFile file = null;
        string ine = "";
        string comprobante = "";
        string foto = "";

        public Registro()
        {
            InitializeComponent();
        }

        public async void Tomar_foto_ine(object sender, EventArgs e)
        {
            Tomar_foto("ine");
        }

        public async void Tomar_foto_perfil(object sender, EventArgs e)
        {
            Tomar_foto("perfil");
        }

        public async void Tomar_foto_comprobante(object sender, EventArgs e)
        {
            Tomar_foto("comprobante");
        }

        public async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                int completo = 1;
                string error = "";
                if (e_nombre.Text == null) { completo = 0; error = error + " Nombre incompleto. \n"; }
                if (e_edad.Text == null) { completo = 0; error = error + " Edad incompleta. \n"; }
                if (p_estadocivil.SelectedItem.ToString().Length < 5) { completo = 0; error = error + " Estado civil incompleto. \n"; }
                if (e_domicilio.Text == null) { completo = 0; error = error + " Domicilio incompleto. \n"; }
                if (e_colonia.Text == null) { completo = 0; error = error + " Colonia incompleta. \n"; }
                if (e_cp.Text == null) { completo = 0; error = error + " CP incompleto. \n"; }
                if (e_entre.Text == null) { completo = 0; error = error + " Entre que calles incompleto. \n"; }
                if (e_ingreso.Text == null) { completo = 0; error = error + " Ingreso incompleto. \n"; }
                if (p_trabajo.SelectedItem.ToString().Length < 1) { completo = 0; error = error + " Trabajo incompleto. \n"; }
                if (p_dependientes.SelectedItem.ToString().Length < 1) { completo = 0; error = error + " Dependientes incompleto. \n"; }
                if (p_hijos.SelectedItem.ToString().Length < 1) { completo = 0; error = error + " Hijos incompleto. \n"; }
                if (p_estudios.SelectedItem.ToString().Length < 1) { completo = 0; error = error + " Estudios incompleto. \n"; }
                if (ine.Length < 5 || ine.Contains("|")) { completo = 0; error = error + " Foto de ine no cargada o en proceso de carga. \n"; }
                if (comprobante.Length < 5 || comprobante.Contains("|")) { completo = 0; error = error + " Foto de comprobante no cargada o en proceso de carga. \n"; }
                if (e_usuario.Text == null) { completo = 0; error = error + " Usuario incompleto. \n"; }
                if (e_contrasena.Text == null) { completo = 0; error = error + " Contraseña incompleto. \n"; }
                if (completo == 1)
                {
                    string uriString2 = string.Format("http://ec2-3-86-100-71.compute-1.amazonaws.com/WS/registro.php?nombre={0}&edad={1}&estado={2}&domicilio={3}&colonia={4}&cp={5}&entre={6}&ingreso={7}&trabajo={8}&dependientes={9}&hijos={10}&estudios={11}&ine={12}&comprobante={13}&usuario={14}&contrasena={15}&correo={16}&foto={17}",
                    e_nombre.Text, e_edad.Text, p_estadocivil.SelectedItem.ToString(), e_domicilio.Text, e_colonia.Text, e_cp.Text, e_entre.Text, e_ingreso.Text, p_trabajo.SelectedItem.ToString(), p_dependientes.SelectedItem.ToString(), p_hijos.SelectedItem.ToString(), p_estudios.SelectedItem.ToString(), ine, comprobante, e_usuario.Text, e_contrasena.Text, e_correo.Text, foto);
                    var response2 = await httpRequest(uriString2);
                    if (response2 == "1")
                    {
                        await DisplayAlert("Pendiente", "Se solicitó tu registro correctamente", "OK");
                        Application.Current.MainPage = new NavigationPage(new Login());
                    }
                    else
                    {
                        await DisplayAlert("Ayuda", "No fué posible completar tu registro", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Ayuda", "Todos los campos son obligatorios, valida que toda la información esté completa\n\n\nRevisar:\n\n" + error, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        #region "tomar foto perfil"
        public async void Tomar_foto(string tipo)
        {
            /*var wifi = Plugin.Connectivity.Abstractions.ConnectionType.WiFi;
            var connectionTypes = CrossConnectivity.Current.ConnectionTypes;
            if (!connectionTypes.Contains(wifi))
            {
                await DisplayAlert("Sin wifi", "No estas conectado mediante una red WIFI, el consumo de datos puede ser significativo. ¿deseas continuar?", "OK");
            }
            else
            {*/
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Sin camara", "No hay una camara disponible.", "OK");
                return;
            }
            if (tipo == "perfil")
            {
                file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                });
            }
            else
            {
                file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());
            }

            if (file == null)
                return;

            string now1 = DateTime.Now.ToString().Replace(' ', '_').Replace('/', '_').Replace(':', '_');
            string nombrefoto = tipo + "_" + now1 + ".jpg";
            if (tipo == "ine")
            {
                labeline.Text = "Cargando INE...";
                ine = "|" + nombrefoto;
                foto_ine.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
            else if (tipo == "comprobante")
            {
                labelcomprobante.Text = "Cargando comprobante...";
                comprobante = "|" + nombrefoto;
                foto_comprobante.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
            else if (tipo == "perfil")
            {
                foto = "|" + nombrefoto;
                perfil.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }


            subir_perfil(nombrefoto);
            //}

        }

        public async void subir_perfil(string nombrefoto)
        {
            try
            {
                if (file == null) { await DisplayAlert("Alerta", "Falta definir una foto.", "OK"); }
                await Upload(file, nombrefoto);
                file.Dispose();
            }
            catch (Exception ex)
            {
            }
        }



        public async Task<bool> Upload(MediaFile mediaFile, string filename)
        {
            try
            {
                byte[] bitmapData;
                var stream = new MemoryStream();
                mediaFile.GetStream().CopyTo(stream);
                bitmapData = stream.ToArray();
                var fileContent = new ByteArrayContent(bitmapData);

                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpg");
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "fileUpload",
                    FileName = filename
                };

                string boundary = "---8393774hhy37373773";
                MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
                multipartContent.Add(fileContent);


                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.PostAsync("http://ec2-3-86-100-71.compute-1.amazonaws.com/WS/subir.php?carpeta=usuarios", multipartContent);

                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (ine == "|" + filename) { ine = filename; labeline.Text = "INE cargada correctamente."; }
                    if (comprobante == "|" + filename) { comprobante = filename; labelcomprobante.Text = "Comprobante cargado correctamente."; }
                    if (foto == "|" + filename) { foto = filename; }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        #endregion

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