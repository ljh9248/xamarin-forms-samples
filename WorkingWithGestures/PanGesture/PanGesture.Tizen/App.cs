
namespace PanGesture.Tizen
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        void CalculateScaledScreenSize()
        {
            int dpi = 0;
            global::Tizen.System.Information.TryGetValue<int>("http://tizen.org/feature/screen.dpi", out dpi);

            int width = 0;
            int height = 0;
            global::Tizen.System.Information.TryGetValue("http://tizen.org/feature/screen.width", out width);
            global::Tizen.System.Information.TryGetValue("http://tizen.org/feature/screen.height", out height);

            double scalingFactor = dpi / 160.0;
            App.ScreenWidth = width / scalingFactor;
            App.ScreenHeight = height / scalingFactor;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            CalculateScaledScreenSize();

            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app, true);
            app.Run(args);
        }
    }
}
