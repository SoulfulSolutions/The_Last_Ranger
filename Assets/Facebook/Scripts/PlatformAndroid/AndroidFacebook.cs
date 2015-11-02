using System;
using System.Collections.Generic;
using UnityEngine;

namespace Facebook
{
    sealed class AndroidFacebook : NativeFacebook
    {
        // This class holds all the of the wrapper methods that we call into
        private const string AndroidJavaFacebookClass = "com.facebook.unity.FB";

        // key Hash used for Android SDK
        private string keyHash;
        public string KeyHash { get { return keyHash; } }

        #region IFacebook
        public override bool LimitEventUsage
        {
            get
            {
                return limitEventUsage;
            }
            set
            {
                limitEventUsage = value;
                CallFB("SetLimitEventUsage", value.ToString());
            }
        }
        #endregion

        #region FBJava

#if UNITY_ANDROID
        private AndroidJavaClass fbJava;
        private AndroidJavaClass FB
        {
            get
            {
                if (fbJava == null)
                {
                    fbJava = new AndroidJavaClass(AndroidJavaFacebookClass);

                    if (fbJava == null)
                    {
                        throw new MissingReferenceException(string.Format("AndroidFacebook failed to load {0} class", AndroidJavaFacebookClass));
                    }
                }
                return fbJava;
            }
        }

        public override string FacebookSdkVersion
        {
            get
            {
                string buildVersion = FB.CallStatic<string>("GetSdkVersion");
                return String.Format("Facebook.Android.SDK.{0}", buildVersion);
            }
        }
#else
        public override string FacebookSdkVersion
        {
            get
            {
                return "";
            }
        }
#endif
        private void CallFB(string method, string args)
        {
#if UNITY_ANDROID
            FB.CallStatic(method, args);
#else
            FBDebug.Error("Using Android when not on an Android build!  Doesn't Work!");
#endif
        }

        #endregion

        #region FBAndroid

        protected override void OnAwake()
        {
            keyHash = "";
#if UNITY_ANDROID && DEBUG
            AndroidJNIHelper.debug = true;
#endif
        }

        private bool IsErrorResponse(string response)
        {
            //var res = MiniJSON.Json.Deserialize(response);
            return false;
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
            HideUnityDelegate hideUnityDelegate = null)
        {
            if (string.IsNullOrEmpty(appId))
            {
                throw new ArgumentException("appId cannot be null or empty!");
            }

            var parameters = new Dictionary<string, object>();

            parameters.Add("appId", appId);

            if (cookie != false)
            {
                parameters.Add("cookie", true);
            }
            if (logging != true)
            {
                parameters.Add("logging", false);
            }
            if (status != true)
            {
                parameters.Add("status", false);
            }
            if (xfbml != false)
            {
                parameters.Add("xfbml", true);
            }
            if (!string.IsNullOrEmpty(channelUrl))
            {
                parameters.Add("channelUrl", channelUrl);
            }
            if (!string.IsNullOrEmpty(authResponse))
            {
                parameters.Add("authResponse", authResponse);
            }
            if (frictionlessRequests != false)
            {
                parameters.Add("frictionlessRequests", true);
            }

            var paramJson = MiniJSON.Json.Serialize(parameters);
            this.onInitComplete = onInitComplete;

            this.CallFB("Init", paramJson.ToString());
            this.CallFB("SetUserAgentSuffix",
                        String.Format("Unity.{0}", Facebook.FacebookSdkVersion.Build));
        }

        public override void LogInWithReadPermissions (
            string scope = "",
            FacebookDelegate callback = null)
        {
            var parameters = new Dictionary<string, object> ();
            parameters.Add("scope", scope);
            var paramJson = MiniJSON.Json.Serialize(parameters);
            AddAuthDelegate (callback);
            this.CallFB("LoginWithReadPermissions", paramJson);
        }

        public override void LogInWithPublishPermissions (
            string scope = "",
            FacebookDelegate callback = null)
        {
            var parameters = new Dictionary<string, object> ();
            parameters.Add("scope", scope);
            var paramJson = MiniJSON.Json.Serialize(parameters);
            AddAuthDelegate(callback);
            this.CallFB("LoginWithPublishPermissions", paramJson);
        }

        public override void LogOut()
        {
            this.CallFB("Logout", "");
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

            var paramsDict = new Dictionary<string, object>();
            // Marshal all the above into the thing

            paramsDict["message"] = message;

            if (callback != null)
            {
                paramsDict["callback_id"] = AddFacebookDelegate(callback);
            }

            if (actionType != null && !string.IsNullOrEmpty(objectId))
            {
                paramsDict["action_type"] = actionType.ToString();
                paramsDict["object_id"] = objectId;
            }

            if (to != null)
            {
                paramsDict["to"] = string.Join(",", to);
            }

            if (filters != null && filters.Count > 0)
            {
                string mobileFilter = filters[0] as string;
                if(mobileFilter != null)
                {
                    paramsDict["filters"] = mobileFilter;
                }
            }

            if (maxRecipients != null)
            {
                paramsDict["max_recipients"] = maxRecipients.Value;
            }

            if (!string.IsNullOrEmpty(data))
            {
                paramsDict["data"] = data;
            }

            if (!string.IsNullOrEmpty(title))
            {
                paramsDict["title"] = title;
            }

            CallFB("AppRequest", MiniJSON.Json.Serialize(paramsDict));
        }

        public override void ShareLink(
            string contentURL,
            string contentTitle,
            string contentDescription,
            string photoURL, 
            FacebookDelegate callback) 
        {
            Dictionary<string, object> paramsDict = new Dictionary<string, object>();
            // Marshal all the above into the thing
            
            if (callback != null)
            {
                paramsDict["callback_id"] = AddFacebookDelegate(callback);
            }
            
            if (!string.IsNullOrEmpty(contentURL))
            {
                paramsDict.Add("content_url", contentURL);
            }
            
            if (!string.IsNullOrEmpty(contentTitle))
            {
                paramsDict.Add("content_title", contentTitle);
            }
            
            if (!string.IsNullOrEmpty(contentDescription))
            {
                paramsDict.Add("content_description", contentDescription);
            }
            
            if (!string.IsNullOrEmpty(photoURL))
            {
                paramsDict.Add("photo_url", photoURL);
            }

            CallFB("ShareLink", MiniJSON.Json.Serialize(paramsDict));
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
            throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on Android");
        }

        public override void GameGroupCreate(
            string name,
            string description,
            string privacy = "CLOSED",
            FacebookDelegate callback = null)
        {
            var paramsDict = new Dictionary<string, object>();
            paramsDict["name"] = name;
            paramsDict["description"] = description;
            paramsDict["privacy"] = privacy;

            if (callback != null)
            {
                paramsDict["callback_id"] = AddFacebookDelegate(callback);
            }

            CallFB("GameGroupCreate", MiniJSON.Json.Serialize (paramsDict));
        }

        public override void GameGroupJoin(
            string id,
            FacebookDelegate callback = null)
        {
            var paramsDict = new Dictionary<string, object>();
            paramsDict["id"] = id;

            if (callback != null)
            {
                paramsDict["callback_id"] = AddFacebookDelegate(callback);
            }

            CallFB("GameGroupJoin", MiniJSON.Json.Serialize (paramsDict));
        }

        public override void GetDeepLink(FacebookDelegate callback)
        {
            if (callback != null)
            {
                deepLinkDelegate = callback;
                CallFB("GetDeepLink", "");
            }
        }

        public override void AppEventsLogEvent(
            string logEvent,
            float? valueToSum = null,
            Dictionary<string, object> parameters = null)
        {
            var paramsDict = new Dictionary<string, object>();
            paramsDict["logEvent"] = logEvent;
            if (valueToSum.HasValue)
            {
                paramsDict["valueToSum"] = valueToSum.Value;
            }
            if (parameters != null)
            {
                paramsDict["parameters"] = ToStringDict(parameters);
            }
            CallFB("AppEvents", MiniJSON.Json.Serialize(paramsDict));
        }

        public override void AppEventsLogPurchase(
            float logPurchase,
            string currency = "USD",
            Dictionary<string, object> parameters = null)
        {
            var paramsDict = new Dictionary<string, object>();
            paramsDict["logPurchase"] = logPurchase;
            paramsDict["currency"] = (!string.IsNullOrEmpty(currency)) ? currency : "USD";
            if (parameters != null)
            {
                paramsDict["parameters"] = ToStringDict(parameters);
            }
            CallFB("AppEvents", MiniJSON.Json.Serialize(paramsDict));
        }

        public override void ActivateApp(string appId = null)
        {
            var parameters = new Dictionary<string, string>(1);
            if (!string.IsNullOrEmpty(appId))
            {
                parameters["app_id"] = appId;
            }
            CallFB("ActivateApp", MiniJSON.Json.Serialize(parameters));
        }

        #endregion

        protected override void setShareDialogMode(NativeDialogModes.ShareDialogMode mode)
        {
            CallFB("SetShareDialogMode", mode.ToString());
        }
    }
}

