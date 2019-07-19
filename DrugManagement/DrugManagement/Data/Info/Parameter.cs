using DrugManagement.Data.File;
using DrugManagement.Data.Format;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugManagement.Data.Info
{

    /// <summary>
    /// 設定、薬一覧、次回アラームのパラメータ
    /// </summary>
    public class Parameter : IDisposable
    {

        /// <summary>
        /// アラーム一覧
        /// </summary>
        public Alarm Alarm { get; private set; } = new Alarm();

        /// <summary>
        /// 再通知
        /// </summary>
        private List<Alarm> _Realarms = new List<Alarm>();

        /// <summary>
        /// 次回アラームに再通知が設定された時の_Realarms[Index]
        /// </summary>
        private int _RealarmIndex = -1;

        /// <summary>
        /// 設定ファイル
        /// </summary>
        public SettingFile Setting = new SettingFile();

        /// <summary>
        /// 薬ファイル
        /// </summary>
        public DrugFile Drug = new DrugFile();

        /// <summary>
        /// 設定、薬一覧、次回アラームのパラメータ
        /// </summary>
        public Parameter()
        {
            SetNextAlarm();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Setting != null)
            {
                Setting.Dispose();
                Setting = null;
            }

            if (Drug != null)
            {
                Drug.Dispose();
                Drug = null;
            }

            if (Alarm != null)
            {
                Alarm.Dispose();
                Alarm = null;
            }

            if (_Realarms != null)
            {
                _Realarms.ForEach(realarm => { realarm.Dispose(); });
                _Realarms.Clear();
                _Realarms = null;
            }

        }

        /// <summary>
        /// 薬の服用
        /// </summary>
        /// <returns>
        /// True:薬切れ有
        /// False:在庫十分
        /// </returns>
        public async Task<bool> TakeMedicine()
        {

            bool returnValue = false;

            
            // 総量から服用錠を減算
            Alarm.Drugs.ForEach(alarmDrug => 
            {

                var drug = Drug.Drugs[alarmDrug.Index];

                drug.TotalVolume -= alarmDrug.Volume;

                // 指定日時による服用なら、指定日時の設定を解除
                if (alarmDrug.IsAppoint)
                {
                    drug.Appoint.IsDrug = false;
                }

                // 時間ごとによる服用なら、次回時刻を設定
                if (alarmDrug.IsHourEach)
                {
                    drug.HourEachNextTime = DateTime.Now.AddHours(drug.HourEachTime);
                }

            });

            var task1 = Task.Run(() =>  // データ保存後、次回アラーム計算
            {
                Drug.Save();
                SetNextAlarm();
            });
            var task2 = Task.Run(() =>  // 残量チェック
            {

                for (int iLoop = 0; iLoop < Drug.Drugs.Count; iLoop++)
                {

                    returnValue = Drug.Drugs[iLoop].IsPrescriptionAlarm;

                    if (returnValue)
                    {
                        break;
                    }

                }

            });

            await Task.WhenAll(task1, task2);
            return returnValue;

        }

        /// <summary>
        /// 頓服服用
        /// </summary>
        /// <param name="selectedDrug">対象薬</param>
        public void TakeMedicineToBeTaken(Drug drug)
        {

            // 頓服服用しない場合は処理中止
            if (!drug.ToBeTaken.IsDrug)
            {
                return;
            }

            // 総量から服用錠を減算
            drug.TotalVolume -= drug.ToBeTaken.Volume;

            // データ保存
            Drug.Save();

        }

        /// <summary>
        /// 次回アラーム情報の設定
        /// </summary>
        private void SetNextAlarm()
        {

            // アラームに設定した再通知設定を初期化
            if (!_RealarmIndex.Equals(-1))
            {

                _Realarms[_RealarmIndex].Dispose();
                _Realarms.RemoveAt(_RealarmIndex);

            }

            // アラーム時間を記憶
            DateTime beforeAlarm = Alarm.Timer;

            // 初期化
            Alarm.Dispose();
            Alarm = new Alarm();

            // 再通知より次回アラームを取得
            if (_Realarms.Count > 0)
            {

                var alarm = new Alarm()
                {
                    Timer = _Realarms[0].Timer
                };

                _Realarms[0].Drugs.ForEach(drug => { alarm.Drugs.Add((AlarmDrug)drug.Clone()); });

                _RealarmIndex = 0;
                Alarm = alarm;

            }

            // 食事・就寝・指定日時・時間ごとより次回アラームを取得
            for (int iLoop = 0; iLoop < Drug.Drugs.Count; iLoop++)
            {

                var drug = Drug.Drugs[iLoop];

                // 毎時
                if (drug.HourEach.IsDrug)
                {
                    CompareToTime(drug.HourEachNextTime, iLoop, drug.HourEach.Volume, false, true);
                }

                // 指定日時
                if (drug.Appoint.IsDrug)
                {
                    CompareToTime(drug.AppointTime, iLoop, drug.Appoint.Volume, true, false);
                }

                // 朝食
                if (drug.Breakfast.IsDrug)
                {
                    DateTime time = CalcMealsTime(beforeAlarm, Setting.Breakfast.Start, Setting.Breakfast.Finish, drug.Breakfast.Kind);
                    CompareToTime(time, iLoop, drug.Breakfast.Volume, false, false);
                }

                // 昼食
                if (drug.Lunch.IsDrug)
                {
                    DateTime time = CalcMealsTime(beforeAlarm, Setting.Lunch.Start, Setting.Lunch.Finish, drug.Lunch.Kind);
                    CompareToTime(time, iLoop, drug.Lunch.Volume, false, false);
                }

                // 夕食
                if (drug.Dinner.IsDrug)
                {
                    DateTime time = CalcMealsTime(beforeAlarm, Setting.Dinner.Start, Setting.Dinner.Finish, drug.Dinner.Kind);
                    CompareToTime(time, iLoop, drug.Dinner.Volume, false, false);
                }

                // 就寝前
                if (drug.Sleep.IsDrug)
                {

                    DateTime time = beforeAlarm.Date + Setting.Sleep - Setting.BeforeSleep;

                    // 服用時間が前回時間以前の場合は翌日にする
                    if (time <= beforeAlarm)
                    {
                        time = time.AddDays(1);
                    }

                    CompareToTime(time, iLoop, drug.Sleep.Volume, false, false);

                }

            }

        }

        /// <summary>
        /// 次回アラームとなるか比較し、次回アラームにIndexを登録する
        /// </summary>
        /// <param name="time">アラーム候補時刻</param>
        /// <param name="index">Drug.Drugs[Index]</param>
        /// <param name="volume">服用錠数</param>
        /// <param name="isAppoint">指定日時か</param>
        /// <param name="isHourEach">時間毎か</param>
        private void CompareToTime(DateTime time, int index, int volume, bool isAppoint, bool isHourEach)
        {

            var alarmDrug = new AlarmDrug()
            {
                Index = index,
                Volume = volume,
                IsAppoint = isAppoint,
                IsHourEach = isHourEach
            };

            if (Alarm.Timer.Equals(time))
            {

                // 既存データに追加
                Alarm.Drugs.Add(alarmDrug);

            }
            else if (time < Alarm.Timer)
            {

                // 現在設定の次回アラームが再通知であるなら初期化
                if (!_RealarmIndex.Equals(-1))
                {
                    _RealarmIndex = -1;
                }

                // 次回アラーム時刻の更新
                Alarm.Dispose();
                Alarm = new Alarm() { Timer = time };

                Alarm.Drugs.Add(alarmDrug);

            }

        }

        /// <summary>
        /// 食事時の服用時間の計算
        /// </summary>
        /// <param name="beforeAlarm">前回アラーム時間</param>
        /// <param name="start">食事開始時間</param>
        /// <param name="finish">食事終了時間</param>
        /// <param name="kind">服用タイミング</param>
        /// <returns>服用時間</returns>
        private DateTime CalcMealsTime(DateTime beforeAlarm, TimeSpan start, TimeSpan finish, Kind kind)
        {

            DateTime returnValue;

            // 服用タイミングより服用時間を計算
            switch (kind)
            {

                case Kind.Before:
                    returnValue = beforeAlarm.Date + start - Setting.BeforeMeals;
                    break;

                case Kind.Between:
                    returnValue = beforeAlarm.Date + new TimeSpan((long)(finish.Subtract(start).Ticks / 2.0));
                    break;

                case Kind.After:
                    returnValue = beforeAlarm.Date + finish + Setting.AfterMeals;
                    break;

                default:
                    returnValue = beforeAlarm.Date + start;
                    break;

            }

            // 服用時間が前回時間以前の場合は翌日にする
            if (returnValue <= beforeAlarm)
            {
                returnValue = returnValue.AddDays(1);
            }

            return returnValue;

        }

        /// <summary>
        /// 再通知設定
        /// </summary>
        /// <param name="afterSpan">再通知設定時間</param>
        public void SetRealarm(TimeSpan afterSpan)
        {

            DateTime nextTime = DateTime.Now + afterSpan;
            var alarm = new Alarm() { Timer = nextTime };

            Alarm.Drugs.ForEach(drug => 
            {
                alarm.Drugs.Add((AlarmDrug)drug.Clone());
            });

            _Realarms.Add(alarm);

            // 指定日時、時間ごとのアラームならば、元の設定値を更新
            _Realarms.ForEach(realarm =>
            {

                realarm.Drugs.ForEach(drugAlarm => 
                {

                    var drug = Drug.Drugs[drugAlarm.Index];

                    // 指定日時
                    if (drugAlarm.IsAppoint)
                    {
                        drug.AppointTime = nextTime;
                    }

                    // 時間ごと
                    if (drugAlarm.IsHourEach)
                    {
                        drug.HourEachNextTime = nextTime;
                    }

                });

            });

            // 次回アラームを設定
            SetNextAlarm();

        }

        /// <summary>
        /// 指定薬を削除
        /// </summary>
        /// <param name="drug">指定薬</param>
        public void DeleteDrug(Drug drug)
        {
            Drug.Delete(drug);
        }

    }

}
