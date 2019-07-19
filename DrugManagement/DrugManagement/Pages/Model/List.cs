using DrugManagement.Data.Info;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DrugManagement.Pages.Model
{

    /// <summary>
    /// 一覧表示.Model
    /// </summary>
    public class List : IDisposable
    {

        #region Parameter

        /// <summary>
        /// パラメータ
        /// </summary>
        private Parameter _Parameter;

        /// <summary>
        /// 薬一覧
        /// </summary>
        public ObservableCollection<ListRow> Rows;

        #endregion

        #region ViewModel.Property

        #endregion

        /// <summary>
        /// 一覧表示.Model
        /// </summary>
        public List()
        {

            _Parameter = (Xamarin.Forms.Application.Current as App).Parameter;
            MakeRows();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            ClearRows();
            _Parameter = null;

        }

        /// <summary>
        /// 薬一覧の初期化
        /// </summary>
        private void ClearRows()
        {

            if (Rows != null)
            {
                Rows.Clear();
                Rows = null;
            }

        }

        /// <summary>
        /// 薬一覧の作成
        /// </summary>
        private async void MakeRows()
        {

            await Task.Run(() => 
            {

                ClearRows();
                Rows = new ObservableCollection<ListRow>();

                for (int iLoop = 0; iLoop < _Parameter.Drug.Drugs.Count; iLoop++)
                {

                    var drug = _Parameter.Drug.Drugs[iLoop];

                    Rows.Add(new ListRow()
                    {
                        Name = drug.Name,
                        Timing = drug.DrugTiming,
                        Remarks = drug.Remarks,
                        IsPrescription = drug.IsPrescriptionAlarm,
                        IsToBeTaken = drug.ToBeTaken.IsDrug
                    });

                }

            });

        }

        /// <summary>
        /// 選択した薬をパラメータより削除
        /// </summary>
        /// <param name="selectedItem">選択した薬</param>
        public void DeleteDrug(ListRow selectedItem)
        {

            var index = selectedItem.Index;

            if (-1 < index && index < _Parameter.Drug.Drugs.Count)
            {

                var drug = _Parameter.Drug.Drugs[index];

                // 薬削除後、一覧を再作成
                _Parameter.DeleteDrug(drug);
                MakeRows();

            }

        }

        /// <summary>
        /// 頓服服用
        /// </summary>
        /// <param name="selectedItem">選択した薬</param>
        public void DrugToBeTaken(ListRow selectedItem)
        {

            var index = selectedItem.Index;

            if (-1 < index && index < _Parameter.Drug.Drugs.Count)
            {

                var drug = _Parameter.Drug.Drugs[index];

                // 服用処理後、薬切れFLGの更新
                _Parameter.TakeMedicineToBeTaken(drug);
                selectedItem.IsPrescription = drug.IsPrescriptionAlarm;

            }

        }

    }
}
