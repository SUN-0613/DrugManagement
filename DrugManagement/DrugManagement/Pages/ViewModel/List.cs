using AYam.Common.MVVM;
using DrugManagement.Data.Info;
using System;
using System.Collections.ObjectModel;

namespace DrugManagement.Pages.ViewModel
{

    /// <summary>
    /// 一覧表示.ViewModel
    /// </summary>
    public class List : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// 一覧表示.Model
        /// </summary>
        private Model.List _Model;

        #endregion

        #region Property

        /// <summary>
        /// 薬一覧
        /// </summary>
        public ObservableCollection<ListRow> Rows
        {
            get { return _Model?.Rows; }
            set
            {
                if (_Model != null)
                {
                    _Model.Rows = value;
                }
            }
        }

        /// <summary>
        /// 一覧にて選択した薬
        /// </summary>
        public ListRow SelectedItem { get; set; }

        #endregion

        /// <summary>
        /// 一覧表示.ViewModel
        /// </summary>
        public List()
        {

            _Model = new Model.List();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_Model != null)
            {
                _Model.Dispose();
                _Model = null;
            }

        }

    }

}
