
namespace CustomRenderer.TizenMobile
{
    public class Program : Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            // Please follow this guide to get your HERE map key 
            // https://developer.tizen.org/development/guides/native-application/location-and-sensors/maps-and-maps-service/getting-here-maps-credentials
            Xamarin.FormsMaps.Init("HERE", "Please enter your HERE map key");
            app.Run(args);
        }
    }
}
