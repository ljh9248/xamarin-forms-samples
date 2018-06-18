using ElmSharp;
using Phoneword.Tizen.Views;
using System;
using Tizen.Applications;
using Xamarin.Forms.Platform.Tizen;

namespace Phoneword.Tizen
{
    public class App : CoreUIApplication
    {
        public static App Instance;

        private Window window;
        private Naviframe naviFrame;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Forms.Init(this);

            Instance = this;
            window = new Window("Phoneword");
            window.BackButtonPressed += (s, e) =>
            {
                if (naviFrame.NavigationStack.Count > 1)
                {
                    naviFrame.Pop();
                }
                else
                {
                    Exit();
                }
            };
            window.Show();

            var conformant = new Conformant(window);
            conformant.Show();

            naviFrame = new Naviframe(window);
            conformant.SetContent(naviFrame);
            naviFrame.Show();

            var mainPage = new PhonewordPage().CreateEvasObject(window);
            naviFrame.Push(mainPage);
        }

        public void NavigateToCallHistoryPage()
        {
            var callHistoryPage = new CallHistoryPage().CreateEvasObject(window);
            naviFrame.Push(callHistoryPage);
        }

        static void Main(string[] args)
        {
            Elementary.Initialize();
            Elementary.ThemeOverlay();
            App app = new App();
            app.Run(args);
        }
    }
}
