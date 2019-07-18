using AYam.Common.Interface;
using Foundation;
using UIKit;
using UserNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(DrugManagement.iOS.NotificationService))]
namespace DrugManagement.iOS
{

    /// <summary>
    /// 通知
    /// </summary>
    public class NotificationService : INotificationService
    {

        /// <summary>
        /// The notify key.
        /// </summary>
        private const string _RequestID = "notifyKey";

        /// <summary>
        /// The notify value.
        /// </summary>
        private const string _RequestValue = "notifyValue";

        /// <summary>
        /// 通知許可
        /// </summary>
        public void Allow()
        {
            
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {

                // 許可が欲しい通知タイプの選択
                // 通知タイプは「テキスト」「アイコンバッチ」「サウンド」
                // 追加するときは"|"で区切る
                var types = UNAuthorizationOptions.Alert | 
                            UNAuthorizationOptions.Badge | 
                            UNAuthorizationOptions.Sound;

                // 許可申請
                UNUserNotificationCenter.Current.RequestAuthorization(types, (granted, err) =>
                {

#if DEBUG
                    // エラー発生チェック
                    if (err != null)
                    {
                        System.Diagnostics.Debug.WriteLine(err.LocalizedFailureReason + "\n" + err.LocalizedDescription);
                    }

#endif

                });

            }

        }

        /// <summary>
        /// 解放
        /// </summary>
        public void Release()
        {

            UNUserNotificationCenter.Current.RemovePendingNotificationRequests(new string[] { _RequestID });
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;

        }

        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="subTitle">サブタイトル</param>
        /// <param name="message">本文</param>
        /// <param name="sec">表示秒数</param>
        /// <param name="isRepeat">繰り返し表示するか</param>
        /// <param name="isUseBadge">バッジ表示するか</param>
        public void Show(string title, string subTitle, string message, int sec = 1, bool isRepeat = false, bool isUseBadge = true)
        {

            // メインスレッドにて処理
            UIApplication.SharedApplication.InvokeOnMainThread(delegate
            {

                var content = new UNMutableNotificationContent();                               // 表示内容
                var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(sec, isRepeat);   // 表示条件

                // 表示テキスト
                content.Title = title;
                content.Subtitle = subTitle;
                content.Body = message;

                // 表示サウンド
                content.Sound = UNNotificationSound.Default;

                // 解除する際のキー設定
                content.UserInfo = NSDictionary.FromObjectAndKey(new NSString(_RequestValue), new NSString(_RequestID));

                // ローカル通知の予約
                var request = UNNotificationRequest.FromIdentifier(_RequestID, content, trigger);
                UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
                {

#if DEBUG
                    // エラー発生チェック
                    if (err != null)
                    {
                        System.Diagnostics.Debug.WriteLine(err.LocalizedFailureReason + "\n" + err.LocalizedDescription);
                    }

#endif

                });

                // アイコン上に表示されるバッジ数値更新
                if (isUseBadge)
                {
                    UIApplication.SharedApplication.ApplicationIconBadgeNumber = 1;
                }

            });

        }
    }
}