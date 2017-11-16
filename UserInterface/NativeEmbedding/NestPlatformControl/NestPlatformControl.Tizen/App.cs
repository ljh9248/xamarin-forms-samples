namespace NestPlatformControl.Tizen
{
	class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
	{
		public static ElmSharp.Window ElmWindow;

		protected override void OnCreate()
		{
			ElmWindow = MainWindow;
			base.OnCreate();
			LoadApplication(new App());
		}

		static void Main(string[] args)
		{
			var app = new Program();
			global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
			app.Run(args);
		}
	}

}
