using PlatformTizen = Xamarin.Forms.Platform.Tizen;

namespace ThemesDemo.Tizen
{
    class Program : PlatformTizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            PlatformTizen.Forms.Init(app);
            app.Run(args);
        }
    }
}
