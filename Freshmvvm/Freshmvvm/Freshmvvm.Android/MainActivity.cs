using Android.App;
using Android.Content.PM;
using Android.OS;
using FreshMvvm;

namespace Freshmvvm.Droid
{
    [Activity(Label = "Freshmvvm", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            var repository = new Repository(FileAccessHelper.GetLocalFilePath("contacts.db3"));
            FreshIOC.Container.Register(repository);

            LoadApplication(new App());
        }
    }
}

