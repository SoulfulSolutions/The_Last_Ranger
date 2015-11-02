using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Facebook
{
    class EditorFacebook : AbstractFacebook
    {
        private FacebookDelegate loginCallback;

        public override bool LimitEventUsage
        {
            get
            {
                return limitEventUsage;
            }
            set
            {
                limitEventUsage = value;
            }
        }

        public override string FacebookSdkVersion
        {
            get
            {
                return "None";
            }
        }

        #region Init
        protected override void OnAwake()
        {
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
            this.isInitialized = true;
            if (onInitComplete != null)
            {
                onInitComplete();
            }
        }

        #endregion

        public override void LogInWithReadPermissions(string scope = "", FacebookDelegate callback = null)
        {
            AddAuthDelegate(callback);
            FBComponentFactory.GetComponent<EditorFacebookAccessToken>();
        }

        public override void LogInWithPublishPermissions(string scope = "", FacebookDelegate callback = null)
        {
            AddAuthDelegate(callback);
            FBComponentFactory.GetComponent<EditorFacebookAccessToken>();
        }

        public override void LogOut()
        {
            isLoggedIn = false;
            UserId = "";
            AccessToken = "";
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
            FBDebug.Info("App Request dialog is not implemented in the Unity editor.");
        }

        public override void ShareLink(
            string contentURL,
            string contentTitle,
            string contentDescription,
            string photoURL, 
            FacebookDelegate callback) 
        {
            FBDebug.Info("Share Link is not implemented in the Unity editor.");
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
            FBDebug.Info("Pay method only works with Facebook Canvas.  Does nothing in the Unity Editor, iOS or Android");
        }

        public override void GameGroupCreate(
            string name,
            string description,
            string privacy = "CLOSED",
            FacebookDelegate callback = null)
        {
            throw new PlatformNotSupportedException("There is no Facebook GameGroupCreate Dialog on Editor");
        }

        public override void GameGroupJoin(
            string id,
            FacebookDelegate callback = null)
        {
            throw new PlatformNotSupportedException("There is no Facebook GameGroupJoin Dialog on Editor");
        }

        public override void ActivateApp(string appId = null)
        {
            FBDebug.Info("This only needs to be called for iOS or Android.");
        }

        public override void GetDeepLink(FacebookDelegate callback)
        {
            FBDebug.Info("No Deep Linking in the Editor");
            if (callback != null)
            {
                callback(new FBResult("<platform dependent>"));
            }
        }

        public override void AppEventsLogEvent(
            string logEvent,
            float? valueToSum = null,
            Dictionary<string, object> parameters = null)
        {
            FBDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
        }

        public override void AppEventsLogPurchase(
            float logPurchase,
            string currency = "USD",
            Dictionary<string, object> parameters = null)
        {
            FBDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
        }

        #region Editor Mock Login

        public void MockLoginCallback(FBResult result)
        {
            Destroy(FBComponentFactory.GetComponent<EditorFacebookAccessToken>());
            if (result.Error != null)
            {
                var errorStruct = (Dictionary<string, object>) MiniJSON.Json.Deserialize(result.Text);
                if(errorStruct.ContainsKey("error"))
                {
                    var errorMessage = errorStruct["error"] as Dictionary<string, object>;

                    var msg = errorMessage.ContainsKey("message") ? errorMessage["message"] : null;
                    var type = errorMessage.ContainsKey("type") ? errorMessage["type"] : null;
                    var code = errorMessage.ContainsKey("code") ? errorMessage["code"] : null;

                    BadAccessToken(type + ": " + code + " " + msg);
                    return;
                }

                BadAccessToken(result.Error);
                return;
            }

            try
            {
                var json = (List<object>) MiniJSON.Json.Deserialize(result.Text);
                var responses = new List<string>();
                foreach (object obj in json)
                {
                    if (!(obj is Dictionary<string, object>))
                    {
                        continue;
                    }

                    var response = (Dictionary<string, object>) obj;

                    if (!response.ContainsKey("body"))
                    {
                        continue;
                    }

                    responses.Add((string) response["body"]);
                }

                var userData = (Dictionary<string, object>) MiniJSON.Json.Deserialize(responses[0]);
                var appData = (Dictionary<string, object>) MiniJSON.Json.Deserialize(responses[1]);

                if (FB.AppId != (string) appData["id"])
                {
                    BadAccessToken("Access token is not for current app id: " + FB.AppId);
                    return;
                }

                userId = (string)userData["id"];
                isLoggedIn = true;

                OnAuthResponse(new FBResult(""));

            }
            catch (Exception e)
            {
                BadAccessToken("Could not get data from access token: " + e.Message);
            }
        }

        public void MockCancelledLoginCallback()
        {
            OnAuthResponse(new FBResult(""));
        }

        private void BadAccessToken(string error)
        {
            FBDebug.Error(error);
            UserId = "";
            AccessToken = "";
            FBComponentFactory.GetComponent<EditorFacebookAccessToken>();
        }

        #endregion
    }
}
