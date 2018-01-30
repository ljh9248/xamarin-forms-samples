using System;
using Tizen.Applications;
using Tizen.Security;
using Xamarin.Forms;

[assembly: Dependency(typeof(Phoneword.PhoneDialer))]

namespace Phoneword
{
    class PhoneDialer : IDialer
    {
        private string _number;

        private const string _callPrivilege = "http://tizen.org/privilege/call";

        protected const string Tag = "PhoneWordMultiscreen";

        public PhoneDialer()
        {
            SetupPrivilegeHandler(_callPrivilege);
        }
        public bool Dial(string number)
        {
            _number = number;
            try
            {
                CheckResult result = PrivacyPrivilegeManager.CheckPermission(_callPrivilege);
                switch (result)
                {
                    case CheckResult.Allow:
                        Call();
                        return true;
                    case CheckResult.Deny:
                        return false;
                    case CheckResult.Ask:
                        PrivacyPrivilegeManager.RequestPermission(_callPrivilege);
                        // Dial() method is synchronous, return true
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception exc)
            {
                Tizen.Log.Error(Tag, "Dialing failed: " + exc.Message);
                return false;
            }
        }

        private void SetupPrivilegeHandler(string privilege)
        {
            PrivacyPrivilegeManager.ResponseContext context = null;
            if (PrivacyPrivilegeManager.GetResponseContext(privilege).TryGetTarget(out context))
            {
                context.ResponseFetched += PrivilegeResponseHandler;
            }
        }

        void PrivilegeResponseHandler(object sender, RequestResponseEventArgs e)
        {
            if (e.cause == CallCause.Error)
            {
                return;
            }

            switch (e.result)
            {
                case RequestResult.AllowForever:
                    Call();
                    break;
                case RequestResult.DenyForever:
                    break;
                case RequestResult.DenyOnce:
                    break;
            }
        }

        private void Call()
        {
            var request = new Tizen.Applications.AppControl()
            {
                Operation = Tizen.Applications.AppControlOperations.Call,
                Uri = "tel:" + _number
            };
            Tizen.Log.Info(Tag, "Calling " + request.Uri);
            Tizen.Applications.AppControl.SendLaunchRequest(request, OnLaunchResult);
        }

        private void OnLaunchResult(AppControl launchRequest, AppControl replyRequest, AppControlReplyResult result)
        {
            Tizen.Log.Info(Tag, $"Result of the request: {result}");
        }
    }
}
