using System;
using System.Collections.Generic;

namespace DrugManagement.Data.Info
{

    /// <summary>
    /// アラーム
    /// </summary>
    public class Alarm : IDisposable
    {

        /// <summary>
        /// アラーム時間
        /// </summary>
        public DateTime Timer = DateTime.MaxValue;

        /// <summary>
        /// 薬一覧
        /// </summary>
        public List<AlarmDrug> Drugs = new List<AlarmDrug>();

        /// <summary>
        /// アラーム
        /// </summary>
        public Alarm()
        { }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Drugs != null)
            {
                Drugs.Clear();
                Drugs = null;
            }

        }

    }

}
