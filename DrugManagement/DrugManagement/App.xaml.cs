using AYam.Common.Interface;
using DrugManagement.Data.Info;
using DrugManagement.Timer;
using Xamarin.Forms;

namespace DrugManagement
{

    /// <summary>
    /// Appクラス
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// パラメータ
        /// </summary>
        public Parameter Parameter { get; set; }

        /// <summary>
        /// タイマ処理
        /// </summary>
        private MainThread _Timer;

        /// <summary>
        /// Appクラス
        /// </summary>
        public App()
        {

            InitializeComponent();

            // 言語選択
            switch (Device.RuntimePlatform)
            {

                case Device.iOS:
                case Device.Android:

                    var cultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

                    Resx.Parameter.Culture = cultureInfo;
                    DependencyService.Get<ILocalize>().SetLocal(cultureInfo);

                    break;

            }

            // 初期化
            Parameter = new Parameter();
            _Timer = new MainThread(Parameter);

            // 画面表示
            MainPage = new Pages.View.Main();

        }

        /// <summary>
        /// 起動処理
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// スリープ処理
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
