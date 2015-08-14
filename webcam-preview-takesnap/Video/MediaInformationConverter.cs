namespace TakeSnapsWithWebcamUsingWpfMvvm.Video
{
    #region Namespace

    using System;
    using System.Globalization;
    using System.Windows.Data;
    
    #endregion

    /// <summary>
    /// Media information converter.
    /// </summary>
    public class MediaInformationConverter : IValueConverter
    {
        /// <summary>
        /// Convert media information instance to USB ID or USB moniker string.
        /// </summary>
        /// <param name="value">Input value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter for conversion.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Return USB ID or moniker string for the input media information instance.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = string.Empty;
            if (null == value)
            {
                return result;
            }

            var filterInfo = value as MediaInformation;
            if (null != filterInfo)
            {
                result = filterInfo.UsbId;
            }

            return result;
        }

        /// <summary>
        /// Convert back the input value for media information.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
