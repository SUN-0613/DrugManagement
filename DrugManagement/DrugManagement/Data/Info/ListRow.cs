using AYam.Common.MVVM;

namespace DrugManagement.Data.Info
{

    /// <summary>
    /// 一覧表示ページの行情報
    /// </summary>
    public class ListRow : ViewModelBase
    {

        /// <summary>
        /// 名称
        /// </summary>
        private string _Name { get; set; }

        /// <summary>
        /// 服用タイミング
        /// </summary>
        private string _Timing { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        private string _Remarks { get; set; }

        /// <summary>
        /// 薬切れ警告を表示するか
        /// </summary>
        private bool _IsPrescription { get; set; }

        /// <summary>
        /// 頓服服用するか
        /// </summary>
        private bool _IsToBeTaken { get; set; }

        /// <summary>
        /// Parameter.Drugs.Index
        /// </summary>
        public int Index;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// :
        /// </summary>
        public string Colon { get; set; } = ":";

        /// <summary>
        /// 服用タイミング
        /// </summary>
        public string Timing
        {
            get { return _Timing; }
            set
            {
                _Timing = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks
        {
            get { return _Remarks; }
            set
            {
                _Remarks = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 薬切れ警告を表示するか
        /// </summary>
        public bool IsPrescription
        {
            get { return _IsPrescription; }
            set
            {
                _IsPrescription = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 頓服服用するか
        /// </summary>
        public bool IsToBeTaken
        {
            get { return _IsToBeTaken; }
            set
            {
                _IsToBeTaken = value;
                CallPropertyChanged();
            }
        }

    }

}
