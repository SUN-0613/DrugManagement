using AYam.Common.Interface;
using DrugManagement.Data.Info;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace DrugManagement.Timer
{

    /// <summary>
    /// アラーム用タイマー
    /// </summary>
    public class MainThread
    {

        /// <summary>
        /// パラメータ
        /// </summary>
        private Parameter _Parameter;

        /// <summary>
        /// タイマ続行
        /// </summary>
        private bool _IsRunTimer = true;

        /// <summary>
        /// タイマ処理スキップ
        /// </summary>
        private bool _IsSkipTimer = false;

        /// <summary>
        /// ローカル通知済
        /// </summary>
        private bool _IsLocalAlarm = false;

        /// <summary>
        /// アラーム用タイマー
        /// </summary>
        /// <param name="parameter">パラメータ</param>
        public MainThread(Parameter parameter)
        {

            _Parameter = parameter;

            // ローカル通知許可
            DependencyService.Get<INotificationService>().Allow();

        }

        /// <summary>
        /// タイマ開始
        /// </summary>
        public void Start()
        {

            var app = Application.Current as App;

            _IsRunTimer = true;

            Device.StartTimer(new TimeSpan(0, 0, 1),
                () => 
                {

                    // タイマ処理続行中か
                    if (!_IsSkipTimer)
                    {

                        _IsSkipTimer = true;

                        try
                        {

                            // デバッグ情報表示
#if DEBUG
                            if (_Parameter.Alarm.Drugs.Count > 0)
                            {
                                Debug.WriteLine("Run " + DateTime.Now.ToString("HH:mm:ss") + " :Next " + _Parameter.Alarm.Timer.ToString("HH:mm"));
                            }
                            else
                            {
                                Debug.WriteLine("Run " + DateTime.Now.ToString("HH:mm:ss"));
                            }
#endif

                            // アラーム時刻に到達したか
                            if (_Parameter.Alarm.Timer <= DateTime.Now)
                            {

                                // バックグラウンドで起動していないか
                                
                                

                            }

                        }
                        catch //(Exception ex)
                        {

                        }
                        finally
                        {

                        }

                    }

                    return _IsRunTimer;

                });

        }

    }
}
