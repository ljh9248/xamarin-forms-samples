using System;
using CustomRenderer;
using CustomRenderer.Tizen;
using Tizen.WebView;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace CustomRenderer.Tizen
{
    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, WebView>
    {
        const string JavaScriptFunction = "function invokeCSharpAction(data){window.jsBridge.postMessage(data);}";

        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            if (Control == null)
            {
                Chromium.Initialize();
                Forms.Context.Terminated += (sender, args) => Chromium.Shutdown();
                var webView = new WebView(Forms.NativeParent);
                webView.GetSettings().JavaScriptEnabled = true;
                webView.LoadFinished += OnWebLoadCompleted;
                SetNativeControl(webView);
            }

            if (e.OldElement != null)
            {
                var hybridWebView = e.OldElement as HybridWebView;
                hybridWebView.Cleanup();
            }
            if (e.NewElement != null)
            {
                Control.AddJavaScriptMessageHandler("jsBridge", OnScriptMessage);
                Control.LoadUrl($"file://{ResourcePath.GetPath(Element.Uri)}");
            }

            base.OnElementChanged(e);
        }


        private void OnWebLoadCompleted(object sender, EventArgs e)
        {
            Control.Eval(JavaScriptFunction);
        }

        private void OnScriptMessage(JavaScriptMessage message)
        {
            string data = message.GetBodyAsString();
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => Element.InvokeAction(data));
        }
    }
}
