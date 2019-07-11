using System;
using System.Globalization;
using Xamarin.Forms;

namespace DrugManagement.Converter
{

    /// <summary>
    /// Convereter
    /// 薬残量により文字色を変更
    /// </summary>
    public class VolumeToForeground : IValueConverter
    {

        /// <summary>
        /// 薬残量により文字色の変更
        /// </summary>
        /// <param name="value">薬切れFLG</param>
        /// <param name="targetType">バインディング ターゲット プロパティの型</param>
        /// <param name="parameter">使用するコンバーター パラメーター</param>
        /// <param name="culture">コンバーターで使用するカルチャ</param>
        /// <returns>文字色
        /// 黒：残量十分
        /// 赤：残量不足
        /// </returns>
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value == null)
            {
                return Color.Black;
            }
            else
            {
                return (bool)value ? Color.Red : Color.Black;
            }

        }

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="value">バインディング ターゲットによって生成された値</param>
        /// <param name="targetType">変換後の型</param>
        /// <param name="parameter">使用するコンバーター パラメーター</param>
        /// <param name="culture">コンバーターで使用するカルチャ</param>
        /// <returns></returns>
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
