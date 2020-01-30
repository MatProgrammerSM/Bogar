using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using System.Threading.Tasks;
using Android;

namespace Bogar.Droid
{
    [Activity(Label = "Bogar",  Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TryToGetPermissions();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            base.OnCreate(savedInstanceState);
            ImageCircleRenderer.Init();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        #region RuntimePermissions

        async Task TryToGetPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                await GetPermissionsAsync();
                return;
            }


        }
        const int RequestLocationId = 0;

        readonly string[] PermissionsGroup =
            {
                            //TODO add more permissions
                            Manifest.Permission.AccessCoarseLocation,
                            Manifest.Permission.AccessFineLocation,
                            Manifest.Permission.AccessLocationExtraCommands,
                            Manifest.Permission.ReadExternalStorage,
                            Manifest.Permission.WriteExternalStorage,
                            Manifest.Permission.Internet,
                            Manifest.Permission.Camera,

             };

        async Task GetPermissionsAsync()
        {
            const string permission = Manifest.Permission.ReadContacts;

            if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)
            {
                //TODO change the message to show the permissions name
                return;
            }

            if (ShouldShowRequestPermissionRationale(permission))
            {
                //set alert for executing the task
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Permisos");
                alert.SetMessage("La aplicación necesita tu autorización para continuar.");
                alert.SetPositiveButton("Solicitar permisos", (senderAlert, args) =>
                {
                    RequestPermissions(PermissionsGroup, RequestLocationId);
                });

                alert.SetNegativeButton("Cancelar", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Cancelados!", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();


                return;
            }

            RequestPermissions(PermissionsGroup, RequestLocationId);

        }
        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            //Toast.MakeText(this, "Permisos listos", ToastLength.Short).Show();

                        }
                        else
                        {
                            //Permission Denied :(
                            Toast.MakeText(this, "Permisos pendientes", ToastLength.Short).Show();

                        }
                    }
                    break;
            }
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion


        public static MainActivity Current { private set; get; }
    }
}