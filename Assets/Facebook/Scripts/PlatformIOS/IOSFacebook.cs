using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Facebook
{
    class IOSFacebook : NativeFacebook
    {
        private const string CancelledResponse = "{\"cancelled\":true}";
#if UNITY_IOS
        [DllImport ("__Internal")]
        private static extern void iosInit(
            string appId,
            bool cookie,
            bool logging,
            bool status,
            bool frictionlessRequests,
            string urlSuffix,
            string unityUserAgentSuffix);
        [DllImport ("__Internal")]
        private static extern void iosLogInWithReadPermissions(string scope);
        [DllImport ("__Internal")]
        private static extern void iosLogInWithPublishPermissions(string scope);
        [DllImport ("__Internal")]
        private static extern void iosLogOut();
        [DllImport ("__Internal")]
        private static extern void iosSetShareDialogMode(int mode);

        [DllImport ("__Internal")]
        private static extern void iosShareLink(
            int requestId,
            string contentURL,
            string contentTitle,
            string contentDescription,
            string photoURL);

        [DllImport ("__Internal")]
        private static extern void iosAppRequest(
            int requestId,
            string message,
            string actionType,
            string objectId,
            string[] to = null,
            int toLength = 0,
            string filters = "",
            string[] excludeIds = null,
            int excludeIdsLength = 0,
            bool hasMaxRecipients = false,
            int maxRecipients = 0,
            string data = "",
            string title = "");

        [DllImport ("__Internal")]
        private static extern void iosCreateGameGroup(
            int requestId,
            string name,
            string description,
            string privacy);

        [DllImport ("__Internal")]
        private static extern void iosJoinGameGroup(int requestId, string groupId);

        [DllImport ("__Internal")]
        private static extern void iosFBSettingsActivateApp(string appId);

        [DllImport ("__Internal")]
        private static extern void iosFBAppEventsLogEvent(
            string logEvent,
            double valueToSum,
            int numParams,
            string[] paramKeys,
            string[] paramVals);

        [DllImport ("__Internal")]
        private static extern void iosFBAppEventsLogPurchase(
            double logPurchase,
            string currency,
            int numParams,
            string[] paramKeys,
            string[] paramVals);

        [DllImport ("__Internal")]
        private static extern void iosFBAppEventsSetLimitEventUsage(bool limitEventUsage);

        [DllImport ("__Internal")]
        private static extern void iosGetDeepLink();

        [DllImport ("__Internal")]
        private static extern string iosFBSdkVersion();
#else
        void iosInit(string appId,
                     bool cookie,
                     bool logging,
                     bool status,
                     bool frictionlessRequests,
                     string urlSuffix,
                     string unityUserAgentSuffix) { }
        void iosLogInWithReadPermissions(string scope) { }
        void iosLogInWithPublishPermissions(string scope) { }
        void iosLogOut() { }

        void iosSetShareDialogMode(int mode) { }

        void iosShareLink(
            int requestId,
            string contentURL,
            string contentTitle,
            string contentDescription,
            string photoURL) { }

        void iosAppRequest(
            int requestId,
            string message,
            string actionType,
            string objectId,
            string[] to = null,
            int toLength = 0,
            string filters = "",
            string[] excludeIds = null,
            int excludeIdsLength = 0,
            bool hasMaxRecipients = false,
            int maxRecipients = 0,
            string data = "",
            string title = "") { }

        void iosCreateGameGroup(
            int requestId,
            string name,
            string description,
            string privacy) { }

        void iosJoinGameGroup(int requestId, string groupId) {}

        void iosFBSettingsPublishInstall(int requestId, string appId) { }

        void iosFBSettingsActivateApp(string appId) { }

        void iosFBAppEventsLogEvent(
            string logEvent,
            double valueToSum,
            int numParams,
            string[] paramKeys,
            string[] paramVals) { }

        void iosFBAppEventsLogPurchase(
            double logPurchase,
            string currency,
            int numParams,
            string[] paramKeys,
            string[] paramVals) { }

        void iosFBAppEventsSetLimitEventUsage(bool limitEventUsage) { }

        void iosGetDeepLink() { }

        string iosFBSdkVersion() {
            return "NONE";
        }
#endif

        private class NativeDict
        {
            public NativeDict()
            {
                numEntries = 0;
                keys = null;
                vals = null;
            }

            public int numEntries;
            public string[] keys;
            public string[] vals;
        };

        public enum FBInsightsFlushBehavior
        {
            FBInsightsFlushBehaviorAuto,
            FBInsightsFlushBehaviorExplicitOnly,
        };

        public override bool LimitEventUsage
        {
            get
            {
                return limitEventUsage;
            }
            set
            {
                limitEventUsage = value;
                iosFBAppEventsSetLimitEventUsage(value);
            }
        }

        public override string FacebookSdkVersion
        {
            get
            {
                return String.Format("Facebook.iOS.SDK.{0}", iosFBSdkVersion());
            }
        }

        #region Init
        protected override void OnAwake()
        {
            accessToken = "NOT_USED_ON_IOS_FACEBOOK";
        }

        public override void Init(
            InitDelegate onInitComplete,
            string appId,
            bool cookie = false,
            bool logging = true,
            bool status = true,
            bool xfbml = false,
            string channelUrl = "",
            string authResponse = null,
            bool frictionlessRequests = false,
            Facebook.HideUnityDelegate hideUnityDelegate = null)
        {
            string unityUserAgentSuffix = String.Format("Unity.{0}",
                                                        Facebook.FacebookSdkVersion.Build);
            iosInit(appId,
                    cookie,
                    logging,
                    status,
                    frictionlessRequests,
                    FBSettings.IosURLSuffix,
                    unityUserAgentSuffix);
            this.onInitComplete = onInitComplete;
        }
        #endregion

        #region FB public interface
        public override void LogInWithReadPermissions(string scope = "", FacebookDelegate callback = null)
        {
            AddAuthDelegate(callback);
            iosLogInWithReadPermissions(scope);
        }

        public override void LogInWithPublishPermissions(string scope = "", FacebookDelegate callback = null)
        {
            AddAuthDelegate(callback);
            iosLogInWithPublishPermissions(scope);
        }

        public override void LogOut()
        {
            iosLogOut();
            isLoggedIn = false;
        }

        public override void AppRequest(
            string message,
            OGActionType actionType,
            string objectId,
            string[] to = null,
            List<object> filters = null,
            string[] excludeIds = null,
            int? maxRecipients = null,
            string data = "",
            string title = "",
            FacebookDelegate callback = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message", "message cannot be null or empty!");
            }

            if (actionType != null && string.IsNullOrEmpty(objectId))
            {
                throw new ArgumentNullException("objectId", "You cannot provide an actionType without an objectId");
            }

            if (actionType == null && !string.IsNullOrEmpty(objectId))
            {
                throw new ArgumentNullException("actionType", "You cannot provide an objectId without an actionType");
            }

            string mobileFilter = null;
            if(filters != null && filters.Count > 0) {
                mobileFilter = filters[0] as string;
            }

            iosAppRequest(
                Convert.ToInt32(AddFacebookDelegate(callback)),
                message,
                (actionType != null) ? actionType.ToString() : "",
                objectId != null ? objectId : "",
                to,
                to != null ? to.Length : 0,
                mobileFilter != null ? mobileFilter : "",
                excludeIds,
                excludeIds != null ? excludeIds.Length : 0,
                maxRecipients.HasValue,
                maxRecipients.HasValue ? maxRecipients.Value : 0,
                data,
                title);
        }

        public override void ShareLink(
            string contentURL = "",
            string contentTitle = "",
            string contentDescription = "",
            string photoURL = "", 
            FacebookDelegate callback = null) 
        {
            iosShareLink(System.Convert.ToInt32(AddFacebookDelegate(callback)), contentURL, contentTitle, contentDescription, photoURL);
        }

        public override void Pay(
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
            throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on iOS");
        }

        public override void GameGroupCreate(
            string name,
            string description,
            string privacy = "CLOSED",
            FacebookDelegate callback = null)
        {
            iosCreateGameGroup(System.Convert.ToInt32(AddFacebookDelegate(callback)), name, description, privacy);
        }

        public override void GameGroupJoin(
            string id,
            FacebookDelegate callback = null)
        {
            iosJoinGameGroup(System.Convert.ToInt32(AddFacebookDelegate(callback)), id);
        }

        public override void GetDeepLink(FacebookDelegate callback)
        {
            if (callback == null)
            {
                return;
            }
            deepLinkDelegate = callback;
            iosGetDeepLink();
        }

        public override void AppEventsLogEvent(
            string logEvent,
            float? valueToSum = null,
            Dictionary<string, object> parameters = null)
        {
            NativeDict dict = MarshallDict(parameters);
            if (valueToSum.HasValue)
            {
                iosFBAppEventsLogEvent(logEvent, valueToSum.Value, dict.numEntries, dict.keys, dict.vals);
            }
            else
            {
                iosFBAppEventsLogEvent(logEvent, 0.0, dict.numEntries, dict.keys, dict.vals);
            }
        }

        public override void AppEventsLogPurchase(
            float logPurchase,
            string currency = "USD",
            Dictionary<string, object> parameters = null)
        {
            NativeDict dict = MarshallDict(parameters);
            if (string.IsNullOrEmpty(currency))
            {
                currency = "USD";
            }
            iosFBAppEventsLogPurchase(logPurchase, currency, dict.numEntries, dict.keys, dict.vals);
        }

        public override void ActivateApp(string appId = null)
        {
            iosFBSettingsActivateApp(appId);
        }
        #endregion

        protected override void setShareDialogMode(NativeDialogModes.ShareDialogMode mode)
        {
            iosSetShareDialogMode((int) mode);
        }

        #region Interal stuff
        private static NativeDict MarshallDict(Dictionary<string, object> dict)
        {
            NativeDict res = new NativeDict();

            if (dict != null && dict.Count > 0)
            {
                res.keys = new string[dict.Count];
                res.vals = new string[dict.Count];
                res.numEntries = 0;
                foreach (KeyValuePair<string, object> kvp in dict)
                {
                    res.keys[res.numEntries] = kvp.Key;
                    res.vals[res.numEntries] = kvp.Value.ToString();
                    res.numEntries++;
                }
            }
            return res;
        }
        private static NativeDict MarshallDict(Dictionary<string, string> dict)
        {
            NativeDict res = new NativeDict();

            if (dict != null && dict.Count > 0)
            {
                res.keys = new string[dict.Count];
                res.vals = new string[dict.Count];
                res.numEntries = 0;
                foreach (KeyValuePair<string, string> kvp in dict)
                {
                    res.keys[res.numEntries] = kvp.Key;
                    res.vals[res.numEntries] = kvp.Value;
                    res.numEntries++;
                }
            }
            return res;
        }
        #endregion
    }
}
