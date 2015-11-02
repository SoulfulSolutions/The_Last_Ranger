//This file is a shared enum between objective-c and c#
//That's a little bit weird, but its better than keeping two enums in sync
namespace NativeDialogModes
{
#if !__cplusplus
    /// <summary>
    /// The dialog mode used by the native applications for displaying the share, login, or other dialogs.
    /// </summary>
    public
#endif
    enum ShareDialogMode
    {
        /// <summary>
        /// The sdk will choose which type of dialog to show
        /// See the Facebook SDKs for ios and android for specific details.
        /// </summary>
        AUTOMATIC = 0,
        /// <summary>
        /// Uses the dialog inside the native facebook applications. Note this will fail if the 
        /// native applications are not installed.
        /// </summary>
        NATIVE = 1,
        /// <summary>
        /// Opens the facebook dialog in a webview.
        /// </summary>
        WEB = 2,
        /// <summary>
        /// Uses the feed dialog.
        /// </summary>
        FEED = 3,
    };

}
