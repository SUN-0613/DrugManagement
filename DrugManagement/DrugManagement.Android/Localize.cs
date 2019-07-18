using AYam.Common.Interface;
using AYam.Common.Platform;
using System;
using System.Threading;
using System.Globalization;
using Xamarin.Forms;

[assembly:Xamarin.Forms.Dependency(typeof(DrugManagement.Droid.Localize))]
namespace DrugManagement.Droid
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

            var androidLocal = Java.Util.Locale.Default;
            string netLanguage = GetOsLanguage(androidLocal.ToString().Replace("_", "-"));

            CultureInfo cInfo = null;

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

                case "ms-BN":
                case "ms-MY":
                case "ms-SG":
                    netLanguage = "ms";
                    break;

                case "in-ID":
                    netLanguage = "id-ID";
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

            string netLanguage = platCulture.LanguageCode;

            switch(platCulture.LanguageCode)
            {

                case "gsw":
                    netLanguage = "de-CH";
                    break;

            }

            return netLanguage;

        }

    }
}