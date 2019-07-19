using AYam.Common.MVVM;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrugManagement.Pages.View
{

    /// <summary>
    /// 一覧.View
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List : ContentPage, IDisposable
    {

        /// <summary>
        /// 一覧.View
        /// </summary>
        public List()
        {

            InitializeComponent();

            if (BindingContext is ViewModelBase viewModel)
            {
                viewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (BindingContext is ViewModelBase viewModel)
            {
                viewModel.PropertyChanged -= OnPropertyChanged;
            }

            if (BindingContext is IDisposable disposable)
            {
                disposable.Dispose();
            }

            BindingContext = null;

        }

        /// <summary>
        /// ViewModel変更通知イベント
        /// </summary>
        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                default:
                    break;

            }

        }

    }

}