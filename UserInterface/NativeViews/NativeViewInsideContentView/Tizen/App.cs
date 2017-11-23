using ElmSharp;
using Xamarin.Forms.Platform.Tizen;

namespace NativeViewInsideContentView.Tizen
{
    class Program : FormsApplication
    {
        public static Window ElmWindow;

        protected override void OnCreate()
        {
            ElmWindow = MainWindow;
            base.OnCreate();
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            app.Run(args);
        }
    }

}
