using DrugManagement.Data.File;
using System;
using System.Collections.Generic;

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
        public List<Alarm> Alarms { get; private set; } = new List<Alarm>();

        /// <summary>
        /// アラーム一覧にて直近のアラームとなるIndex
        /// </summary>
        public int NextAlarmIndex = -1;

        /// <summary>
        /// 再通知
        /// </summary>
        private List<Alarm> _Realarms = new List<Alarm>();

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
        { }

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

            if (Alarms != null)
            {
                Alarms.ForEach(alarm => { alarm.Dispose(); });
                Alarms.Clear();
                Alarms = null;
            }

            if (_Realarms != null)
            {
                _Realarms.ForEach(realarm => { realarm.Dispose(); });
                _Realarms.Clear();
                _Realarms = null;
            }

        }




    }

}
