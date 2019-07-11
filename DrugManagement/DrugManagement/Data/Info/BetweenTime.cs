using System;

namespace DrugManagement.Data.Info
{

    /// <summary>
    /// 範囲時間
    /// </summary>
    public class BetweenTime
    {

        /// <summary>
        /// 開始時間
        /// </summary>
        public TimeSpan Start;

        /// <summary>
        /// 終了時間
        /// </summary>
        public TimeSpan Finish;

        /// <summary>
        /// 範囲時間
        /// </summary>
        public BetweenTime()
        {
            Start = TimeSpan.MaxValue;
            Finish = TimeSpan.MaxValue;
        }

        /// <summary>
        /// 開始・終了時間を比較
        /// 前後逆なら並び替え
        /// </summary>
        public void CompareTimes()
        {

            if (Start.CompareTo(Finish).Equals(1))
            {
                TimeSpan tmp = Start;
                Start = Finish;
                Finish = tmp;
            }

        }

    }
}
