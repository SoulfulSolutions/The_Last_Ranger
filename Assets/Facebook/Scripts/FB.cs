using Facebook;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FB : ScriptableObject
{   
    public static InitDelegate OnInitComplete;
    public static HideUnityDelegate OnHideUnity;

    private static AbstractFacebook facebook;
    private static string authResponse;
    private static bool isInitCalled = false;
    private static string appId;
    private static bool cookie;
    private static bool logging;
    private static bool status;
    private static bool xfbml;
    private static bool frictionlessRequests;

    static AbstractFacebook FacebookImpl
    {
        get
        {
            if (facebook == null)
            {
                throw new NullReferenceException("Facebook object is not yet loaded.  Did you call FB.Init()?");
            }
            return facebook;
        }
    }

    public static string AppId
    {
        get
        {
            // appId might be different from FBSettings.AppId
            // if using the programmatic version of FB.Init()
            return appId;
        }
    }
    public static string UserId
    {
        get
        {
            return (facebook != null) ? facebook.UserId : "";
        }
    }
    public static string AccessToken
    {
        get
        {
            return (facebook != null) ? facebook.AccessToken : "";
        }
    }

    public static DateTime AccessTokenExpiresAt
    {
        get
        {
            return (facebook != null) ? facebook.AccessTokenExpiresAt : DateTime.MinValue;
        }
    }

    public static bool IsLoggedIn
    {
        get
        {
            return (facebook != null) && facebook.IsLoggedIn;
        }
    }

    public static bool IsInitialized
    {
        get
        {
            return (facebook != null) && facebook.IsInitialized;
        }
    }

    #region Init
    /// <summary>
    /// This is the preferred way to call FB.Init().    It will take the facebook app id specified in your "Facebook" 
    /// => "Edit Settings" menu when it is called.
    /// </summary>
    /// <param name="onInitComplete">
    /// Delegate is called when FB.Init() finished initializing everything. By passing in a delegate you can find 
    /// out when you can safely call the other methods.
    /// </param>
    /// <param name="onHideUnity">A delegate to invoke when unity is hidden.</param>
    /// <param name="authResponse">Auth response.</param>
    public static void Init(InitDelegate onInitComplete, HideUnityDelegate onHideUnity = null, string authResponse = null)
    {
        Init(
            onInitComplete,
            FBSettings.AppId,
            FBSettings.Cookie,
            FBSettings.Logging,
            FBSettings.Status,
            FBSettings.Xfbml,
            FBSettings.FrictionlessRequests,
            onHideUnity,
            authResponse);
    }

    /**
      * If you need a more programmatic way to set the facebook app id and other setting call this function.
      * Useful for a build pipeline that requires no human input.
      */
      public static void Init(
        InitDelegate onInitComplete,
        string appId,
        bool cookie = true,
        bool logging = true,
        bool status = true,
        bool xfbml = false,
        bool frictionlessRequests = true,
        HideUnityDelegate onHideUnity = null,
        string authResponse = null)
    {
        FB.appId = appId;
        FB.cookie = cookie;
        FB.logging = logging;
        FB.status = status;
        FB.xfbml = xfbml;
        FB.frictionlessRequests = frictionlessRequests;
        FB.authResponse = authResponse;
        FB.OnInitComplete = onInitComplete;
        FB.OnHideUnity = onHideUnity;

        if (!isInitCalled)
        {
            FB.LogVersion();

#if UNITY_EDITOR
            FBComponentFactory.GetComponent<EditorFacebookLoader>();
#elif UNITY_WEBPLAYER || UNITY_WEBGL
            FBComponentFactory.GetComponent<CanvasFacebookLoader>();
#elif UNITY_IOS
            FBComponentFactory.GetComponent<IOSFacebookLoader>();
#elif UNITY_ANDROID
            FBComponentFactory.GetComponent<AndroidFacebookLoader>();
#else
            throw new NotImplementedException("Facebook API does not yet support this platform");
#endif
            isInitCalled = true;
            return;
        }

        FBDebug.Warn("FB.Init() has already been called.  You only need to call this once and only once.");

        // Init again if possible just in case something bad actually happened.
        if (FacebookImpl != null)
        {
            OnDllLoaded();
        }
    }

    private static void OnDllLoaded()
    {
        FB.LogVersion();
        FacebookImpl.Init(
            OnInitComplete,
            appId,
            cookie,
            logging,
            status,
            xfbml,
            FBSettings.ChannelUrl,
            authResponse,
            frictionlessRequests,
            OnHideUnity
        );
    }
#endregion

    public static void LogInWithPublishPermissions(
        string scope = "",
        FacebookDelegate callback = null)
    {
        FacebookImpl.LogInWithPublishPermissions(scope, callback);
    }

    public static void LogInWithReadPermissions(string scope = "", FacebookDelegate callback = null)
    {
        FacebookImpl.LogInWithReadPermissions(scope, callback);
    }

    public static void LogOut()
    {
        FacebookImpl.LogOut();
    }

    public static void AppRequest(
        string message,
        OGActionType actionType,
        string objectId,
        string[] to,
        string data = "",
        string title = "",
        FacebookDelegate callback = null)
    {
        FacebookImpl.AppRequest(message, actionType, objectId, to, null, null, null, data, title, callback);
    }

    public static void AppRequest(
        string message,
        OGActionType actionType,
        string objectId,
        List<object> filters = null,
        string[] excludeIds = null,
        int? maxRecipients = null,
        string data = "",
        string title = "",
        FacebookDelegate callback = null)
    {
        FacebookImpl.AppRequest(message, actionType, objectId, null, filters, excludeIds, maxRecipients, data, title, callback);
    }

    public static void AppRequest(
        string message,
        string[] to = null,
        List<object> filters = null,
        string[] excludeIds = null,
        int? maxRecipients = null,
        string data = "",
        string title = "",
        FacebookDelegate callback = null)
    {
        FacebookImpl.AppRequest(message, null, null, to, filters, excludeIds, maxRecipients, data, title, callback);
    }

    public static void ShareLink(
        string contentURL = "",
        string contentTitle = "",
        string contentDescription = "",
        string photoURL = "", 
        FacebookDelegate callback = null) 
    {
        FacebookImpl.ShareLink(
            contentURL,
            contentTitle,
            contentDescription,
            photoURL,
            callback);
    }

    public static void API(string query, HttpMethod method, FacebookDelegate callback = null, Dictionary<string, string> formData = null)
    {
        FacebookImpl.API(query, method, formData, callback);
    }

    public static void API(string query, HttpMethod method, FacebookDelegate callback, WWWForm formData)
    {
        FacebookImpl.API(query, method, formData, callback);
    }

    public static void ActivateApp()
    {
        FacebookImpl.ActivateApp(AppId);
    }

    public static void GetDeepLink(FacebookDelegate callback)
    {
        FacebookImpl.GetDeepLink(callback);
    }

    public static void GameGroupCreate(
        string name,
        string description,
        string privacy = "CLOSED",
        FacebookDelegate callback = null)
    {
        FacebookImpl.GameGroupCreate(name, description, privacy, callback);
    }

    public static void GameGroupJoin(
        string id,
        FacebookDelegate callback = null)
    {
        FacebookImpl.GameGroupJoin(id, callback);
    }

    #region App Events
    public sealed class AppEvents
    {

        // If the player has set the limitEventUsage flag to YES, your app will continue
        // to send this data to Facebook, but Facebook will not use the data to serve
        // targeted ads. Facebook may continue to use the information for other purposes,
        // including frequency capping, conversion events, estimating the number of unique
        // users, security and fraud detection, and debugging.

        public static bool LimitEventUsage
        {
            get
            {
                return (facebook != null) && facebook.LimitEventUsage;
            }
            set
            {
                facebook.LimitEventUsage = value;
            }
        }

        public static void LogEvent(
            string logEvent,
            float? valueToSum = null,
            Dictionary<string, object> parameters = null)
        {
            FacebookImpl.AppEventsLogEvent(logEvent, valueToSum, parameters);
        }

        public static void LogPurchase(
            float logPurchase,
            string currency = "USD",
            Dictionary<string, object> parameters = null)
        {
            FacebookImpl.AppEventsLogPurchase(logPurchase, currency, parameters);
        }
    }
    #endregion

    #region Canvas-Only Implemented Methods
    public sealed class Canvas
    {
        public static void Pay(
            string product,
            string action = "purchaseitem",
            int quantity = 1,
            int? quantityMin = null,
            int? quantityMax = null,
            string requestId = null,
            string pricepointId = null,
            string testCurrency = null,
            FacebookDelegate callback = null)
        {
            FacebookImpl.Pay(product, action, quantity, quantityMin, quantityMax, requestId, pricepointId, testCurrency, callback);
        }
    }
    #endregion

    /// <summary>
    /// A class containing the settings specific to the supported mobile platforms.
    /// </summary>
    public sealed class Mobile
    {

        private static NativeDialogModes.ShareDialogMode shareDialogMode =
            NativeDialogModes.ShareDialogMode.AUTOMATIC;

        /// <summary>
        /// Gets or sets the share dialog mode.
        /// </summary>
        /// <value>The share dialog mode.</value>
        public static NativeDialogModes.ShareDialogMode ShareDialogMode
        {
            get
            {
                return shareDialogMode;
            }

            set
            {
                if (FacebookImpl is NativeFacebook) {
                    ((NativeFacebook) FacebookImpl).ShareDialogMode = value;
                }
            }
        }
    }

    #region Android-Only Implemented Methods
    public sealed class Android
    {
        public static string KeyHash
        {
            get
            {
                var androidFacebook = facebook as AndroidFacebook;
                return (androidFacebook != null) ? androidFacebook.KeyHash : "";
            }
        }
    }
    #endregion

    #region CompiledFacebookLoader
    public abstract class CompiledFacebookLoader : MonoBehaviour
    {
        protected abstract AbstractFacebook fb { get; }

        void Start()
        {
            FB.facebook = fb;
            FB.OnDllLoaded();
            Destroy(this);
        }
    }
    #endregion

    private static void LogVersion()
    {
        // If we have initlized we can also get the underlying sdk version
        if (facebook != null)
        {
            FBDebug.Info(String.Format(
                "Using Unity SDK v{0} with {1}",
                FacebookSdkVersion.Build,
                FB.FacebookImpl.FacebookSdkVersion));
        }
        else
        {
            FBDebug.Info(String.Format("Using Unity SDK v{0}", FacebookSdkVersion.Build));
        }
    }
}
