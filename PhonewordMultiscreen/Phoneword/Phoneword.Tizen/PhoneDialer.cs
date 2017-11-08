using System;
using Tizen.Applications;
using Xamarin.Forms;

[assembly: Dependency(typeof(Phoneword.PhoneDialer))]

namespace Phoneword
{
    class PhoneDialer : IDialer
    {
        protected const string Tag = "PhoneWordMultiscreen";

        public bool Dial(string number)
        {
            var request = new Tizen.Applications.AppControl()
            {
                Operation = Tizen.Applications.AppControlOperations.Call,
                Uri = "tel:" + number
            };
            try
            {
                Tizen.Log.Info(Tag, "Calling " + request.Uri);
                Tizen.Applications.AppControl.SendLaunchRequest(request, OnLaunchResult);
                return true;
            }
            catch (Exception exc)
            {
                Tizen.Log.Error(Tag, "Dialing failed: " + exc.Message);
                return false;
            }
        }

        private void OnLaunchResult(AppControl launchRequest, AppControl replyRequest, AppControlReplyResult result)
        {
            Tizen.Log.Info(Tag, $"Result of the request: {result}");
        }
    }
}
