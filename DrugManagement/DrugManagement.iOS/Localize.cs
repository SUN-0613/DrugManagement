using AYam.Common.Interface;
using AYam.Common.Platform;
using System.Globalization;
using System.Threading;
using Foundation;

[assembly: Xamarin.Forms.Dependency(typeof(DrugManagement.iOS.Localize))]
namespace DrugManagement.iOS
{

    /// <summary>
    /// 言語選択
    /// </summary>
    public class Localize : ILocalize
    {

        /// <summary>
        /// 言語セット
        /// </summary>
        public void SetLocal(CultureInfo cultureInfo)
        {

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

        }

        /// <summary>
        /// 選択言語の取得
        /// </summary>
        public CultureInfo GetCurrentCultureInfo()
        {

            string netLanguage = "ja";

            if (NSLocale.PreferredLanguages.Length > 0)
            {
                netLanguage = GetOsLanguage(NSLocale.PreferredLanguages[0]);
            }

            CultureInfo cInfo;

            try
            {
                cInfo = new CultureInfo(netLanguage);
            }
            catch
            {

                try
                {
                    cInfo = new CultureInfo(GetDotNetLanguage(new PlatformCulture(netLanguage)));
                }
                catch
                {
                    cInfo = new CultureInfo("ja");
                }

            }

            return cInfo;

        }

        /// <summary>
        /// 選択言語から.Net形式取得
        /// </summary>
        /// <param name="osLanguage"></param>
        /// <returns></returns>
        private string GetOsLanguage(string osLanguage)
        {

            var netLanguage = osLanguage;

            switch (osLanguage)
            {

                case "ms-MY":
                case "ms-SG":
                    netLanguage = "ms";
                    break;

                case "gsw-CH":
                    netLanguage = "de-CH";
                    break;

            }

            return netLanguage;

        }

        /// <summary>
        /// プラットフォームの言語から.Net形式取得
        /// </summary>
        /// <param name="platCulture"></param>
        /// <returns></returns>
        private string GetDotNetLanguage(PlatformCulture platCulture)
        {

            var netLanguage = platCulture.LanguageCode;

            switch (platCulture.LanguageCode)
            {

                case "pt":
                    netLanguage = "pt-PT";
                    break;

                case "gsw":
                    netLanguage = "de-CH";
                    break;

            }

            return netLanguage;

        }

    }
}