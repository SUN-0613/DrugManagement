using DrugManagement.Data.Format;
using System;

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
        public int HourTimeEach { get; set; } = 2;

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

    }

}
