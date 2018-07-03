using System;
using System.Globalization;
using System.Threading;
using Tizen.System;
using UsingResxLocalization.Tizen;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]
namespace UsingResxLocalization.Tizen
{
    class Localize : ILocalize
    {
        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Console.WriteLine("CurrentCulture set: " + ci.Name);
        }

        public CultureInfo GetCurrentCultureInfo()
        {
            var tizenLocale = SystemSettings.LocaleLanguage;
            var netLanguage = TizenToDotnetLanguage(tizenLocale.ToString().Replace("_", "-"));

            System.Globalization.CultureInfo ci = null;
            try
            {
                ci = new System.Globalization.CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException e1)
            {
                // fallback to first characters, in this case "en"
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                    Console.WriteLine(netLanguage + " failed, trying " + fallback + " (" + e1.Message + ")");
                    ci = new System.Globalization.CultureInfo(fallback);
                }
                catch (CultureNotFoundException e2)
                {
                    Console.WriteLine(netLanguage + " couldn't be set, using 'en' (" + e2.Message + ")");
                    ci = new System.Globalization.CultureInfo("en");
                }
            }

            return ci;
        }

        string TizenToDotnetLanguage(string tizenLanguage)
        {
            Console.WriteLine("Tizen Language:" + tizenLanguage);
            var netLanguage = tizenLanguage;

            switch (tizenLanguage)
            {
                case "az-AZ":
                    netLanguage = "az";
                    break;
                case "sr-RS":
                    netLanguage = "sr";
                    break;
                case "pa-PK":
                    netLanguage = "pa";
                    break;
                case "uz-UZ":
                    netLanguage = "uz";
                    break;
                case "my-MM":
                    netLanguage = "my";
                    break;
                case "ckb-IQ":
                    netLanguage = "irc";
                    break;
                case "ku-TR":
                    netLanguage = "tr";
                    break;
                case "bs-BA":
                    netLanguage = "bs";
                    break;
                case "ks-IN":
                    netLanguage = "ks";
                    break;
                case "mai-IN":
                case "sat-IN":
                case "sd-IN":
                    netLanguage = "sd";
                    break;
            }

            Console.WriteLine(".NET Language/Locale:" + netLanguage);
            return netLanguage;
        }

        string ToDotnetFallbackLanguage(PlatformCulture platCulture)
        {
            Console.WriteLine(".NET Fallback Language:" + platCulture.LanguageCode);
            var netLanguage = platCulture.LanguageCode; // use the first part of the identifier (two chars, usually);

            switch (platCulture.LanguageCode)
            {
                case "gsw":
                    netLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
                    break;
                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }

            Console.WriteLine(".NET Fallback Language/Locale:" + netLanguage + " (application-specific)");
            return netLanguage;
        }
    }
}
