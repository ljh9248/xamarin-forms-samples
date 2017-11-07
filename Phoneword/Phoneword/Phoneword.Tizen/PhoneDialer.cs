using System;
using Tizen.Applications;
using Xamarin.Forms;

[assembly: Dependency(typeof(Phoneword.PhoneDialer))]

namespace Phoneword
{
    class PhoneDialer : IDialer
    {
        public bool Dial(string number)
        {
            var request = new Tizen.Applications.AppControl()
            {
                Operation = Tizen.Applications.AppControlOperations.Call,
                Uri = "tel:" + number
            };
            try
            {
                Tizen.Log.Info("Phoneword", "Calling " + request.Uri);
                Tizen.Applications.AppControl.SendLaunchRequest(request, OnLaunchResult);
                return true;
            }
            catch (Exception exc)
            {
                Tizen.Log.Error("Phoneword", "Failed to dial: " + exc.Message);
                return false;
            }
        }

        private void OnLaunchResult(AppControl launchRequest, AppControl replyRequest, AppControlReplyResult result)
        {
            Tizen.Log.Info("Phoneword", $"Result of the request: {result}");
        }
    }
}
