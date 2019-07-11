using System;
using System.Collections.Generic;
using System.Text;

namespace DrugManagement.Data.Info
{

    /// <summary>
    /// 次回アラームにおける薬情報
    /// </summary>
    public class AlarmDrug
    {

        /// <summary>
        /// DrugManagement.Data.File.Drugs[Index]
        /// </summary>
        public int Index = -1;

        /// <summary>
        /// 服用錠数
        /// </summary>
        public int Volume = 0;

        /// <summary>
        /// 指定日時による服用か
        /// </summary>
        public bool IsAppoint = false;

        /// <summary>
        /// 時間ごとによる服用か
        /// </summary>
        public bool IsHourEach = false;

    }

}
