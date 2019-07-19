using DrugManagement.Data.Format;
using System;
using System.Text;

namespace DrugManagement.Data.Info
{

    /// <summary>
    /// 薬情報
    /// </summary>
    public class Drug
    {

        #region Property

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreateDateTime = DateTime.Now;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 朝食
        /// </summary>
        public Timing Breakfast { get; set; } = new Timing() { IsDrug = false, Kind = Kind.None, Volume = 0 };

        /// <summary>
        /// 昼食
        /// </summary>
        public Timing Lunch { get; set; } = new Timing() { IsDrug = false, Kind = Kind.None, Volume = 0 };

        /// <summary>
        /// 夕食
        /// </summary>
        public Timing Dinner { get; set; } = new Timing() { IsDrug = false, Kind = Kind.None, Volume = 0 };

        /// <summary>
        /// 就寝
        /// </summary>
        public Timing Sleep { get; set; } = new Timing() { IsDrug = false, Kind = Kind.None, Volume = 0 };

        /// <summary>
        /// 頓服
        /// </summary>
        public Timing ToBeTaken { get; set; } = new Timing() { IsDrug = false, Kind = Kind.None, Volume = 0 };

        /// <summary>
        /// 指定日時
        /// </summary>
        public Timing Appoint { get; set; } = new Timing() { IsDrug = false, Kind = Kind.None, Volume = 0 };

        /// <summary>
        /// 指定日時の設定日時
        /// </summary>
        public DateTime AppointTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 時間ごと
        /// </summary>
        public Timing HourEach { get; set; } = new Timing() { IsDrug = false, Kind = Kind.None, Volume = 0 };

        /// <summary>
        /// 時間ごとの設定時間
        /// </summary>
        public int HourEachTime { get; set; } = 2;

        /// <summary>
        /// 時間ごとの開始日時
        /// </summary>
        public DateTime HourEachNextTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 薬切れお知らせアラーム表示有無プロパティ
        /// </summary>
        public bool IsPrescriptionAlarm { get; private set; } = false;

        /// <summary>
        /// 処方量
        /// </summary>
        public int TotalVolume
        {
            get { return _TotalVolume; }
            set
            {
                _TotalVolume = value < 0 ? 0 : value;
                UpdatePrescription();
            }
        }

        /// <summary>
        /// お知らせを表示する薬残量
        /// </summary>
        public int PrescriptionAlarmVolume
        {
            get { return _PrescriptionAlarmVolume; }
            set
            {
                _PrescriptionAlarmVolume = value;
                UpdatePrescription();
            }
        }

        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; } = "";

        /// <summary>
        /// 服用タイミング説明文
        /// </summary>
        public string DrugTiming { get; set; } = "";

        #endregion

        /// <summary>
        /// 処方量
        /// </summary>
        private int _TotalVolume = 0;

        /// <summary>
        /// お知らせを表示する薬残量
        /// </summary>
        private int _PrescriptionAlarmVolume = 0;

        /// <summary>
        /// 薬情報
        /// </summary>
        public Drug()
        { }

        /// <summary>
        /// 薬切れお知らせアラームFLG更新
        /// </summary>
        private void UpdatePrescription()
        {
            IsPrescriptionAlarm = (_TotalVolume <= _PrescriptionAlarmVolume);
        }

        /// <summary>
        /// 一覧に表示する服用タイミングメッセージの作成
        /// </summary>
        /// <returns></returns>
        public string MakeDrugTiming()
        {

            const string separate = ",";    // 区切文字

            string returnValue;
            var message = new StringBuilder();

            try
            {

                #region 食事

                // 毎食・同タイミング
                if (Breakfast.IsDrug && Lunch.IsDrug && Dinner.IsDrug
                    && Breakfast.Kind.Equals(Lunch.Kind)
                    && Breakfast.Kind.Equals(Dinner.Kind))
                {
                    message.Append(Resx.Parameter.AlwaysMeals);
                    message.Append(GetTimingMessage(Breakfast.Kind));
                }
                else
                {

                    // 朝食
                    if (Breakfast.IsDrug)
                    {
                        message.Append(Resx.Parameter.Breakfast);
                        message.Append(GetTimingMessage(Breakfast.Kind));
                    }

                    // 昼食
                    if (Lunch.IsDrug)
                    {

                        if (message.Length > 0)
                        {
                            message.Append(separate);
                        }

                        message.Append(Resx.Parameter.Lunch);
                        message.Append(GetTimingMessage(Lunch.Kind));

                    }

                    // 夕食
                    if (Dinner.IsDrug)
                    {

                        if (message.Length > 0)
                        {
                            message.Append(separate);
                        }

                        message.Append(Resx.Parameter.Dinner);
                        message.Append(GetTimingMessage(Dinner.Kind));

                    }

                }

                #endregion

                #region 就寝

                if (Sleep.IsDrug)
                {

                    if (message.Length > 0)
                    {
                        message.Append(separate);
                    }

                    message.Append(Resx.Parameter.Sleep);

                }

                #endregion

                #region 頓服

                if (ToBeTaken.IsDrug)
                {

                    if (message.Length > 0)
                    {
                        message.Append(separate);
                    }

                    message.Append(Resx.Parameter.ToBeTaken);

                }

                #endregion

                #region 日時指定

                if (Appoint.IsDrug)
                {

                    if (message.Length > 0)
                    {
                        message.Append(separate);
                    }

                    message.Append(AppointTime.ToString(Resx.Parameter.Appoint));

                }

                #endregion

                #region 時間ごと

                if (HourEach.IsDrug)
                {

                    if (message.Length > 0)
                    {
                        message.Append(separate);
                    }

                    message.Append(Resx.Parameter.HourEach.Replace("_n_", HourEachTime.ToString()));

                }

                #endregion

                returnValue = message.ToString();

            }
            finally
            {

                message.Clear();
                message = null;

            }

            return returnValue;

        }

        /// <summary>
        /// 食前・食間・食後のメッセージを取得
        /// </summary>
        /// <param name="kind">服用タイミング</param>
        private string GetTimingMessage(Kind kind)
        {
            switch (kind)
            {

                case Kind.Between:
                    return Resx.Parameter.BetweenMeals;

                case Kind.After:
                    return Resx.Parameter.AfterMeals;

                case Kind.Before:
                default:
                    return Resx.Parameter.BetweenMeals;

            }
        }

        /// <summary>
        /// 同データであるかを作成日時で判断する
        /// </summary>
        /// <param name="obj">対象データ</param>
        /// <returns>
        /// True:同データ
        /// False:異データ
        /// </returns>
        public override bool Equals(object obj)
        {
            return CreateDateTime.Equals(((Drug)obj).CreateDateTime);
        }

    }

}
