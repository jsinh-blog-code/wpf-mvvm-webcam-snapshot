namespace TakeSnapsWithWebcamUsingWpfMvvm.Video
{
    /// <summary>
    /// Represents class that contains media information for video device source.
    /// </summary>
    public sealed class MediaInformation
    {
        /// <summary>
        /// Gets or sets the display name of the video device source.
        /// </summary>
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the USB Id / Moniker string of the video device source.
        /// </summary>
        public string UsbId
        {
            get;
            set;
        }
    }
}