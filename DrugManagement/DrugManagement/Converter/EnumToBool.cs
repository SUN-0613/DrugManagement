using System;
using System.Globalization;
using Xamarin.Forms;

namespace DrugManagement.Converter
{

    /// <summary>
    /// Converter
    /// Enum ⇔ bool
    /// </summary>
    public class EnumToBool : IValueConverter
    {

        /// <summary>
        /// Enum => bool
        /// </summary>
        /// <param name="value">バインディング ソースによって生成された値</param>
        /// <param name="targetType">バインディング ターゲット プロパティの型</param>
        /// <param name="parameter">使用するコンバーター パラメーター</param>
        /// <param name="culture">コンバーターで使用するカルチャ</param>
        /// <returns></returns>
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return false;
            }
            else
            {
                return value.ToString().Equals(parameter.ToString());
            }
        }

        /// <summary>
        /// bool => Enum
        /// </summary>
        /// <param name="value">バインディング ターゲットによって生成された値</param>
        /// <param name="targetType">変換後の型</param>
        /// <param name="parameter">使用するコンバーター パラメーター</param>
        /// <param name="culture">コンバーターで使用するカルチャ</param>
        /// <returns></returns>
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null || (bool)value)
            {
                return null;
            }
            else
            {
                return Enum.Parse(targetType, parameter.ToString());
            }
        }
    }
}
