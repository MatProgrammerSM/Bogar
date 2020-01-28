
using Android.App;

namespace Bogar.Droid
{
    [Activity(Label = "Bogar", Icon = "@drawable/icon2", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
public class SplashActivity : Activity
{
    protected override void OnResume()
    {
        base.OnResume();
        StartActivity(typeof(MainActivity));
    }
 }
}
